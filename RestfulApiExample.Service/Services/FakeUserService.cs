using RestfulApiExample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Service.Services
{
	public class FakeUserService
	{
		private readonly List<User> _users = new List<User>
	{
		new User { Username = "Papara", Password = "Papara" }
	};

		public bool ValidateUser(string username, string password)
		{
			return _users.Any(u => u.Username == username && u.Password == password);
		}
	}
}
