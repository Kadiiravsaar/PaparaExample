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
	public class GenreService : Service<Genre>, IGenreService
	{
		public GenreService(IGenericRepository<Genre> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
		{
		}
	}
}
