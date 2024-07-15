using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using RestfulApiExample.Core.Services;
using RestfulApiExample.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Service.Services
{
	public class AuthorService : Service<Author>, IAuthorService
	{
		public AuthorService(IGenericRepository<Author> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
		{
		}
	}
}
