using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulApiExample.Service.Attributes;

namespace RestfulApiExample.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		[HttpGet]
		[Authorize]
		public IActionResult Get()
		{
			return Ok("Successful login.");
		}
	}
}
