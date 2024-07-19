using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using RestfulApiExample.Core.DTOs.Book;

namespace RestfulApiExampleAPITests.BookTests
{
	public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
	{
		public UpdateBookCommandValidator()
		{
			RuleFor(command => command.BookId).GreaterThan(0).WithMessage("BookId must be greater than 0");
			RuleFor(command => command.Model).NotNull().WithMessage("Model is required");
			RuleFor(command => command.Model.Title).NotEmpty().WithMessage("Title is required");
		}
	}
	public class UpdateBookCommandValidatorTests
	{
		private readonly UpdateBookCommandValidator _validator;

		public UpdateBookCommandValidatorTests()
		{
			_validator = new UpdateBookCommandValidator();
		}

		[Fact]
		public void WhenTitleIsEmpty_ShouldHaveValidationError()
		{
			// Arrange
			var command = new UpdateBookCommand(null)
			{
				BookId = 1, // Ensure BookId is set
				Model = new UpdateBookDto { Title = "" }
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Title is required");
		}

		[Fact]
		public void WhenValidInputsAreGiven_ShouldNotHaveValidationError()
		{
			// Arrange
			var command = new UpdateBookCommand(null)
			{
				BookId = 1, // Ensure BookId is set
				Model = new UpdateBookDto { Title = "Updated Title" }
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().BeEmpty();
		}
	}


}
