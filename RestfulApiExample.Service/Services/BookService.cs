using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using RestfulApiExample.Core.Services;
using RestfulApiExample.Core.UnitOfWorks;

namespace RestfulApiExample.Service.Services
{
	public class BookService : Service<Book>, IBookService
	{
		public BookService(IGenericRepository<Book> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
		{
		}
	}
}
