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
	public class BookRepository : GenericRepository<Book>, IBookRepository
	{
		public BookRepository(AppDbContext context) : base(context)
		{
		}
	}
}
