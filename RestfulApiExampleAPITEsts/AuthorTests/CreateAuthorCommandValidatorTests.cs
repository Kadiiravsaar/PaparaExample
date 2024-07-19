using FluentAssertions;
using FluentValidation;
using RestfulApiExample.Core.DTOs.Author;
using RestfulApiExample.Service.Validations.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.AuthorTests
{
	public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
	{
		public CreateAuthorCommandValidator()
		{
			RuleFor(command => command.Model.Name).NotEmpty().WithMessage("İsim boş olamaz.");
			RuleFor(command => command.Model.LastName).NotEmpty().WithMessage("Soyisim boş olamaz.");
			RuleFor(command => command.Model.DateOfBirth).LessThan(DateTime.Now).WithMessage("Doğum tarihi bugünden küçük olmalıdır.");
		}
	}
	public class CreateAuthorCommandValidatorTests
	{
		private readonly CreateAuthorCommandValidator _validator;

		public CreateAuthorCommandValidatorTests()
		{
			_validator = new CreateAuthorCommandValidator();
		}

		[Fact]
		public void WhenNameIsEmpty_ShouldHaveValidationError()
		{
			// Arrange
			var command = new CreateAuthorCommand(null)
			{
				Model = new CreateAuthorDto
				{
					Name = "",
					LastName = "Author",
					DateOfBirth = new DateTime(1980, 1, 1)
				}
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "İsim boş olamaz.");
		}

		[Fact]
		public void WhenDateOfBirthIsInTheFuture_ShouldHaveValidationError()
		{
			// Arrange
			var command = new CreateAuthorCommand(null)
			{
				Model = new CreateAuthorDto
				{
					Name = "Test",
					LastName = "Author",
					DateOfBirth = DateTime.Now.AddDays(1)
				}
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle("Doğum tarihi bugünden küçük olmalıdır.");
		}
	}
}
