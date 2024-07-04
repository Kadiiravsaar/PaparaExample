using Microsoft.AspNetCore.Diagnostics;
using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Service.Exceptions;
using System.Text.Json;

namespace RestfulApiExample.API.Middleware
{
	public static class UseCustomExceptionHandler
	{
		public static void UseCustomException(this IApplicationBuilder applicationBuilder)
		{

			applicationBuilder.UseExceptionHandler(config =>
			{

				config.Run(async context =>
				{
					context.Response.ContentType = "application/json";

					var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

					var statusCode = exceptionFeature.Error switch
					{
						ClientSideException => 400,
						NotFoundException => 404,
						_ => 500
					};
					context.Response.StatusCode = statusCode;

					var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);


					await context.Response.WriteAsync(JsonSerializer.Serialize(response));

				});
			});

		}
	}
}

