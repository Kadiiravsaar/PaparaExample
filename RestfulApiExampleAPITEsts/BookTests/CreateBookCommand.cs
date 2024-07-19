using RestfulApiExample.Core.DTOs.Author;
using RestfulApiExample.Core.DTOs.Book;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.BookTests
{
	public class CreateBookCommand
	{

		private readonly IBookRepository _repository;
		public CreateBookDto Model { get; set; }

		public CreateBookCommand(IBookRepository repository)
		{
			_repository = repository;
		}

		public async void Handle()
		{
			var book = new Book
			{
				Title = Model.Title,
				PageCount = Model.PageCount,
				PublishDate = Model.PublishDate
			};

			await _repository.AddAsync(book);
		}
	}
}
