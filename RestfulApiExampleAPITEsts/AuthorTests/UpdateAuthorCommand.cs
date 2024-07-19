using RestfulApiExample.Core.DTOs.Author;
using RestfulApiExample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.AuthorTests
{
	public class UpdateAuthorCommand
	{
		private readonly IAuthorRepository _repository;
		public int AuthorId { get; set; }
		public UpdateAuthorDto Model { get; set; }

		public UpdateAuthorCommand(IAuthorRepository repository)
		{
			_repository = repository;
		}

		public async Task Handle()
		{
			var author = await _repository.GetByIdAsync(AuthorId);
			if (author == null)
				throw new InvalidOperationException("Author not found");

			author.Name = Model.Name != default ? Model.Name : author.Name;
			author.LastName = Model.LastName != default ? Model.LastName : author.LastName;
			author.DateOfBirth = Model.DateOfBirth != default ? Model.DateOfBirth : author.DateOfBirth;

			_repository.Update(author);
		}
	}

}
