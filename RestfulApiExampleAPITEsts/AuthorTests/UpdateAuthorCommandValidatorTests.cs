using FluentAssertions;
using FluentValidation;
using RestfulApiExample.Core.DTOs.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.AuthorTests
{
	public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
	{
		public UpdateAuthorCommandValidator()
		{
			RuleFor(command => command.AuthorId).GreaterThan(0).WithMessage("AuthorId must be greater than 0");

			RuleFor(command => command.Model)
				.NotNull().WithMessage("Model must not be null");

			RuleFor(command => command.Model.Name)
				.NotEmpty().WithMessage("Name must not be empty");

			RuleFor(command => command.Model.LastName)
				.NotEmpty().WithMessage("LastName must not be empty");

			RuleFor(command => command.Model.DateOfBirth)
				.LessThan(DateTime.Now).WithMessage("DateOfBirth must be in the past");
		}
	}


	public class UpdateAuthorCommandValidatorTests
	{
		private readonly UpdateAuthorCommandValidator _validator;

		public UpdateAuthorCommandValidatorTests()
		{
			_validator = new UpdateAuthorCommandValidator();
		}

		[Fact]
		public void WhenAuthorIdIsZero_ShouldHaveValidationError()
		{
			// Arrange
			var command = new UpdateAuthorCommand(null)
			{
				AuthorId = 0,
				Model = new UpdateAuthorDto() // Model başlatılıyor
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "AuthorId must be greater than 0");
		}

		[Fact]
		public void WhenValidInputsAreGiven_ShouldNotHaveValidationError()
		{
			// Arrange
			var command = new UpdateAuthorCommand(null)
			{
				AuthorId = 1,
				Model = new UpdateAuthorDto
				{
					Name = "Updated Name",
					LastName = "Updated LastName",
					DateOfBirth = new DateTime(1980, 1, 1)
				}
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().BeEmpty();
		}
	}


}
