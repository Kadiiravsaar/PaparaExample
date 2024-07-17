using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using RestfulApiExample.Core.Services;
using RestfulApiExample.Core.UnitOfWorks;
using RestfulApiExample.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Service.Services
{
	public class AuthorService : Service<Author>, IAuthorService
	{
		private readonly IGenericRepository<Book> _bookRepository;
		public AuthorService(IGenericRepository<Author> repository, IUnitOfWork unitOfWork, IGenericRepository<Book> bookRepository) : base(repository, unitOfWork)
		{
			_bookRepository = bookRepository;
		}

		public async Task ConditionRemoveAuthor(int authorId)
		{
			var author = await _repository.GetByIdAsync(authorId); // Author için repository'yi kullanın
			if (author == null)
			{
				throw new NotFoundException($"Author with id ({authorId}) not found");
			}

			var hasPublishedBooks = await _bookRepository.AnyAsync(b => b.Author.Id == authorId && b.IsActive);
			if (hasPublishedBooks)
			{
				throw new InvalidOperationException("Yayında olan kitabı olan bir yazar silinemez. Öncelikle kitap silinmelidir.");
			}

			await RemoveAsync(author);
		}
	}
}
