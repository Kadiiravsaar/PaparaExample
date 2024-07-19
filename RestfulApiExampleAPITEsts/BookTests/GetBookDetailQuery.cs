using RestfulApiExample.Core.DTOs.Book;
using RestfulApiExample.Core.Repositories;

namespace RestfulApiExampleAPITests.BookTests
{
	public class GetBookDetailQuery
	{
		private readonly IBookRepository _repository;
		public int BookId { get; set; }

		public GetBookDetailQuery(IBookRepository repository)
		{
			_repository = repository;
		}

		public async Task<BookDto> Handle()
		{
			var book = await _repository.GetByIdAsync(BookId);
			if (book == null)
				throw new InvalidOperationException("Book not found.");

			return new BookDto
			{
				Id = book.Id,
				Title = book.Title,
				AuthorId = book.AuthorId,
				GenreId = book.GenreId,
				PublishDate = book.PublishDate
			};
		}
	}

}
