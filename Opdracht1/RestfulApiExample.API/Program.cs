
using RestfulApiExample.API.Services;

namespace RestfulApiExample.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			// Her istek attýðýmda yenilensin istersem => AddScoped

			//builder.Services.AddScoped<IProductService, ProductService>();


			// _products listesi uygulama boyunca korunsun istiyorum o yüzden =>  AddSingleton 
			builder.Services.AddSingleton<IProductService, ProductService>();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
