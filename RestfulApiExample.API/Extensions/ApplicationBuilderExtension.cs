using RestfulApiExample.API.Middleware;

namespace RestfulApiExample.API.Extensions
{
	public static class ApplicationBuilderExtension
	{
		public static IApplicationBuilder UseCustomRequestLog(this IApplicationBuilder applicationBuilder)
		{

			return applicationBuilder.UseMiddleware<RequestLogMiddleware>();
		}
	}


}
