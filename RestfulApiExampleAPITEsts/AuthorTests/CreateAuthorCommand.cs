using RestfulApiExample.Core.DTOs.Author;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.AuthorTests
{
	public class CreateAuthorCommand
	{
		private readonly IAuthorRepository _repository;
		public CreateAuthorDto Model { get; set; }

		public CreateAuthorCommand(IAuthorRepository repository)
		{
			_repository = repository;
		}

		public async void Handle()
		{
			var author = new Author
			{
				Name = Model.Name,
				LastName = Model.LastName,
				DateOfBirth = Model.DateOfBirth
			};

			await _repository.AddAsync(author);
		}
	}
}

