using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.AuthorTests
{
	public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
	{
		public DeleteAuthorCommandValidator()
		{
			RuleFor(command => command.AuthorId).GreaterThan(0).WithMessage("AuthorId must be greater than zero");
		}
	}

	public class DeleteAuthorCommandValidatorTests
	{
		private readonly DeleteAuthorCommandValidator _validator;

		public DeleteAuthorCommandValidatorTests()
		{
			_validator = new DeleteAuthorCommandValidator();
		}

		[Fact]
		public void WhenAuthorIdIsZero_ShouldHaveValidationError()
		{
			// Arrange
			var command = new DeleteAuthorCommand(null) { AuthorId = 0 };

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "AuthorId must be greater than zero");

		}

		[Fact]
		public void WhenAuthorIdIsGreaterThanZero_ShouldNotHaveValidationError()
		{
			// Arrange
			var command = new DeleteAuthorCommand(null) { AuthorId = 1 };

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().BeEmpty();
		}
	}

}
