using RestfulApiExample.Core.Repositories;

namespace RestfulApiExampleAPITests.BookTests
{
	using FluentValidation;

	public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
	{
		public DeleteBookCommandValidator()
		{
			RuleFor(command => command.BookId).GreaterThan(0).WithMessage("BookId must be greater than 0");
		}
	}

	public class DeleteBookCommand
	{
		private readonly IBookRepository _repository;
		public int BookId { get; set; }

		public DeleteBookCommand(IBookRepository repository)
		{
			_repository = repository;
		}

		public async Task Handle()
		{
			var book = await _repository.GetByIdAsync(BookId);
			if (book == null)
				throw new InvalidOperationException("Book not found.");

			_repository.Remove(book);
		}
	}

}
