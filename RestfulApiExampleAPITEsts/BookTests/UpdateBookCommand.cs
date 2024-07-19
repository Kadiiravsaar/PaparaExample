using RestfulApiExample.Core.DTOs.Book;
using RestfulApiExample.Core.Repositories;

namespace RestfulApiExampleAPITests.BookTests
{
	public class UpdateBookCommand
	{
		private readonly IBookRepository _repository;
		public int BookId { get; set; }
		public UpdateBookDto Model { get; set; }

		public UpdateBookCommand(IBookRepository repository)
		{
			_repository = repository;
		}

		public async Task Handle()
		{
			var book = await _repository.GetByIdAsync(BookId);
			if (book == null)
				throw new InvalidOperationException("Book not found");

			book.Title = Model.Title ?? book.Title;
			book.AuthorId = Model.AuthorId;
			book.GenreId = Model.GenreId;
			book.PublishDate = Model.PublishDate;

			_repository.Update(book);
		}
	}

}
