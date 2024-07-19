using FluentAssertions;
using FluentValidation;
using RestfulApiExample.Core.DTOs.Genre;
using RestfulApiExample.Core.Repositories;

namespace RestfulApiExampleAPITests.GenreTests
{
	public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
	{
		public UpdateGenreCommandValidator()
		{
			RuleFor(command => command.GenreId).GreaterThan(0).WithMessage("GenreId must be greater than 0");
			RuleFor(command => command.Model).NotNull().WithMessage("Model is required");
			RuleFor(command => command.Model.Name).NotEmpty().WithMessage("Name is required");
		}
	}

	public class UpdateGenreCommand
	{
		private readonly IGenreRepository _repository;
		public int GenreId { get; set; }
		public UpdateGenreDto Model { get; set; }

		public UpdateGenreCommand(IGenreRepository repository)
		{
			_repository = repository;
		}

		public async Task Handle()
		{
			var genre = await _repository.GetByIdAsync(GenreId);
			if (genre == null)
				throw new InvalidOperationException("Genre not found");

			genre.Name = Model.Name ?? genre.Name;

			_repository.Update(genre);
		}
	}

}
