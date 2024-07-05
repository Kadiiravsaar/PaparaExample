namespace RestfulApiExample.API.Middleware
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Logging;
	using RestfulApiExample.Core.Models;
	using RestfulApiExample.Repository.Context;
	using System;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Threading.Tasks;

	public class RequestLogMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<RequestLogMiddleware> _logger;
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public RequestLogMiddleware(RequestDelegate next, ILogger<RequestLogMiddleware> logger, IServiceScopeFactory serviceScopeFactory)
		{
			_next = next;
			_logger = logger;
			_serviceScopeFactory = serviceScopeFactory;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var log = new Log
			{
				Method = context.Request.Method,
				Path = context.Request.Path,
				RequestTime = DateTime.UtcNow
			};

			// Log request to console
			_logger.LogInformation("Start Request: {Method} {Path} at {RequestTime}", log.Method, log.Path, log.RequestTime);

			await _next(context);

			log.StatusCode = context.Response.StatusCode;
			log.ResponseTime = DateTime.UtcNow;

			// Log response to console
			_logger.LogInformation("End Response : {StatusCode} for {Method} {Path} at {ResponseTime}", log.StatusCode, log.Method, log.Path, log.ResponseTime);

			// Use IServiceScopeFactory to create a scope for the DbContext
			using (var scope = _serviceScopeFactory.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
				dbContext.Logs.Add(log);
				await dbContext.SaveChangesAsync();
			}
		}
	}


}
