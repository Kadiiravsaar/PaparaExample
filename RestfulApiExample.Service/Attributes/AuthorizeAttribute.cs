using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RestfulApiExample.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Service.Attributes
{
	public class AuthorizeAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var userService = (FakeUserService)context.HttpContext.RequestServices.GetService(typeof(FakeUserService));
			var username = context.HttpContext.Request.Headers["Username"].ToString();
			var password = context.HttpContext.Request.Headers["Password"].ToString();

			if (!userService.ValidateUser(username, password))
			{
				context.Result = new UnauthorizedResult();
			}
		}
	}
}
