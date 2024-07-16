using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using RestfulApiExample.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Repository.Repositories
{
	public class GenreRepository : GenericRepository<Genre>, IGenreRepository
	{
		public GenreRepository(AppDbContext context) : base(context)
		{
		}
	}
}
