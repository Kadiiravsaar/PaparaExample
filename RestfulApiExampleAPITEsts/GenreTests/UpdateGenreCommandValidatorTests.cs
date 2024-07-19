using FluentAssertions;
using RestfulApiExample.Core.DTOs.Genre;

namespace RestfulApiExampleAPITests.GenreTests
{
	public class UpdateGenreCommandValidatorTests
	{
		private readonly UpdateGenreCommandValidator _validator;

		public UpdateGenreCommandValidatorTests()
		{
			_validator = new UpdateGenreCommandValidator();
		}

		[Fact]
		public void WhenGenreIdIsZero_ShouldHaveValidationError()
		{
			// Arrange
			var command = new UpdateGenreCommand(null) { GenreId = 0, Model = new UpdateGenreDto { Name = "Valid Name" } };

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "GenreId must be greater than 0");
		}

		[Fact]
		public void WhenNameIsEmpty_ShouldHaveValidationError()
		{
			// Arrange
			var command = new UpdateGenreCommand(null) { GenreId = 1, Model = new UpdateGenreDto { Name = "" } };

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Name is required");
		}

		[Fact]
		public void WhenValidInputsAreGiven_ShouldNotHaveValidationError()
		{
			// Arrange
			var command = new UpdateGenreCommand(null) { GenreId = 1, Model = new UpdateGenreDto { Name = "Updated Name" } };

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().BeEmpty();
		}
	}

}
