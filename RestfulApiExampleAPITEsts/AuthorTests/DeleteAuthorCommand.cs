using RestfulApiExample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.AuthorTests
{
	public class DeleteAuthorCommand
	{
		private readonly IAuthorRepository _repository;
		public int AuthorId { get; set; }

		public DeleteAuthorCommand(IAuthorRepository repository)
		{
			_repository = repository;
		}

		public async Task Handle()
		{
			var author = await _repository.GetByIdAsync(AuthorId);
			if (author is null)
				throw new InvalidOperationException("Author not found");

			_repository.Remove(author);
		}
	}

}
