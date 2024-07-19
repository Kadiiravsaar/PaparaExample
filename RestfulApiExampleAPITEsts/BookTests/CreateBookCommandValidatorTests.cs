using FluentAssertions;
using FluentValidation;
using RestfulApiExample.Core.DTOs.Book;
using RestfulApiExampleAPITests.AuthorTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.BookTests
{
	public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
	{
		public CreateBookCommandValidator()
		{
			RuleFor(command => command.Model.Title).NotEmpty().WithMessage("Başlık boş olamaz.");
			RuleFor(command => command.Model.PageCount).GreaterThan(0).WithMessage("Sayfa sayısı 0'dan büyük olmalıdır.");
			RuleFor(command => command.Model.PublishDate).LessThan(DateTime.Now).WithMessage("Yayın tarihi bugünden küçük olmalıdır.");
		}
	}
	public class CreateBookCommandValidatorTests
	{
		private readonly CreateBookCommandValidator _validator;

		public CreateBookCommandValidatorTests()
		{
			_validator = new CreateBookCommandValidator();
		}

		[Fact]
		public void WhenTitleIsEmpty_ShouldHaveValidationError()
		{
			// Arrange
			var command = new CreateBookCommand(null)
			{
				Model = new CreateBookDto
				{
					Title = "",
					PageCount = 100,
					PublishDate = new DateTime(2023, 1, 1)
				}
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Başlık boş olamaz.");
		}

		[Fact]
		public void WhenPageCountIsZero_ShouldHaveValidationError()
		{
			// Arrange
			var command = new CreateBookCommand(null)
			{
				Model = new CreateBookDto
				{
					Title = "Test Book",
					PageCount = 0,
					PublishDate = new DateTime(2023, 1, 1)
				}
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Sayfa sayısı 0'dan büyük olmalıdır.");
		}

		[Fact]
		public void WhenPublishDateIsInTheFuture_ShouldHaveValidationError()
		{
			// Arrange
			var command = new CreateBookCommand(null)
			{
				Model = new CreateBookDto
				{
					Title = "Test Book",
					PageCount = 100,
					PublishDate = DateTime.Now.AddDays(1)
				}
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Yayın tarihi bugünden küçük olmalıdır.");
		}

		[Fact]
		public void WhenValidInputsAreGiven_ShouldNotHaveValidationError()
		{
			// Arrange
			var command = new CreateBookCommand(null)
			{
				Model = new CreateBookDto
				{
					Title = "Valid Book",
					PageCount = 200,
					PublishDate = new DateTime(2020, 1, 1)
				}
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Should().BeEmpty();
		}
	}


}
