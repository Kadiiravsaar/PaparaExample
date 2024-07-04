namespace RestfulApiExample.API.Middleware
{
	public class RequestLogMiddleware
	{
		private readonly RequestDelegate _next;

		public RequestLogMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
            Console.WriteLine($"Request {context.Request.Method} - {context.Request.Path}");

			await _next(context); // RequestDelegate sınıfı parametre olarak HttpContext geçmelidir

			Console.WriteLine($"End request.");
		}
	}
}
