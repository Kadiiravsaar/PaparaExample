using FluentAssertions;
using FluentValidation;
using RestfulApiExample.Core.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.GenreTests
{
	public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
	{
		public CreateGenreCommandValidator()
		{
			RuleFor(command => command.Model).NotNull().WithMessage("Model is required");
			RuleFor(command => command.Model.Name).NotEmpty().WithMessage("Name is required");
		}
	}

	public class CreateGenreCommandValidatorTests
	{
		private readonly CreateGenreCommandValidator _validator;

		public CreateGenreCommandValidatorTests()
		{
			_validator = new CreateGenreCommandValidator();
		}

		[Fact]
		public void WhenNameIsEmpty_ShouldHaveValidationError()
		{
			// Arrange
			var command = new CreateGenreCommand(null)
			{
				Model = new CreateGenreDto { Name = "" }
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Name is required");
		}

		[Fact]
		public void WhenValidInputsAreGiven_ShouldNotHaveValidationError()
		{
			// Arrange
			var command = new CreateGenreCommand(null)
			{
				Model = new CreateGenreDto { Name = "Valid Genre" }
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().BeEmpty();
		}
	}

}
