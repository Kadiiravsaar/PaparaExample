
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestfulApiExample.API.Extensions;
using RestfulApiExample.API.Middleware;
using RestfulApiExample.Core.Repositories;
using RestfulApiExample.Core.Services;
using RestfulApiExample.Repository.Context;
using RestfulApiExample.Repository.Repositories;
using RestfulApiExample.Service.Mapping;
using RestfulApiExample.Service.Services;
using RestfulApiExample.Services.Validations;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RestfulApiExample.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());
			

			// Add custom services
			builder.Services.AddCustomServices(builder.Configuration);

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestfulApiExample", Version = "v1" });

				// Add custom headers for authentication
				c.AddSecurityDefinition("Username", new OpenApiSecurityScheme
				{
					Name = "Username",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Description = "Enter your username",
				});

				c.AddSecurityDefinition("Password", new OpenApiSecurityScheme
				{
					Name = "Password",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Description = "Enter your password",
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Username"
							}
						},
						new List<string>()
					},
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Password"
							}
						},
						new List<string>()
					}
				});
			});



			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(opt =>
				{
					opt.DocExpansion(DocExpansion.None);
				});
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseCustomRequestLog(); // Log Middlware
			app.UseCustomException(); // Exception Middlware

			app.MapControllers();

			app.Run();
		}
	}
}
