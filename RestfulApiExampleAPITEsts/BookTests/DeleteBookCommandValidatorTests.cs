using FluentAssertions;
using FluentValidation.Results;

namespace RestfulApiExampleAPITests.BookTests
{

	public class DeleteBookCommandValidatorTests
	{
		private readonly DeleteBookCommandValidator _validator;

		public DeleteBookCommandValidatorTests()
		{
			_validator = new DeleteBookCommandValidator();
		}

		[Fact]
		public void WhenBookIdIsZero_ShouldHaveValidationError()
		{
			// Arrange
			var command = new DeleteBookCommand(null) { BookId = 0 };

			// Act
			ValidationResult result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "BookId must be greater than 0");
		}

		[Fact]
		public void WhenBookIdIsValid_ShouldNotHaveValidationError()
		{
			// Arrange
			var command = new DeleteBookCommand(null) { BookId = 1 };

			// Act
			ValidationResult result = _validator.Validate(command);

			// Assert
			result.Errors.Should().BeEmpty();
		}
	}

}
