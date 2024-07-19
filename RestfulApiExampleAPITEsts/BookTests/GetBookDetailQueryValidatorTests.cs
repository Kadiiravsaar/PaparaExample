using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;

namespace RestfulApiExampleAPITests.BookTests
{
	public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
	{
		public GetBookDetailQueryValidator()
		{
			RuleFor(query => query.BookId).GreaterThan(0).WithMessage("BookId must be greater than 0");
		}
	}


	public class GetBookDetailQueryValidatorTests
	{
		private readonly GetBookDetailQueryValidator _validator;

		public GetBookDetailQueryValidatorTests()
		{
			_validator = new GetBookDetailQueryValidator();
		}

		[Fact]
		public void WhenBookIdIsZero_ShouldHaveValidationError()
		{
			// Arrange
			var query = new GetBookDetailQuery(null) { BookId = 0 };

			// Act
			ValidationResult result = _validator.Validate(query);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "BookId must be greater than 0");
		}

		[Fact]
		public void WhenBookIdIsValid_ShouldNotHaveValidationError()
		{
			// Arrange
			var query = new GetBookDetailQuery(null) { BookId = 1 };

			// Act
			ValidationResult result = _validator.Validate(query);

			// Assert
			result.Errors.Should().BeEmpty();
		}
	}

}
