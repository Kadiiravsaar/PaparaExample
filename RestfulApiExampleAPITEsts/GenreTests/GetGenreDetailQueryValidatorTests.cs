using FluentAssertions;
using FluentValidation;

namespace RestfulApiExampleAPITests.GenreTests
{
	public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
	{
		public GetGenreDetailQueryValidator()
		{
			RuleFor(query => query.GenreId).GreaterThan(0).WithMessage("GenreId must be greater than 0");
		}
	}
	public class GetGenreDetailQueryValidatorTests
	{
		private readonly GetGenreDetailQueryValidator _validator;

		public GetGenreDetailQueryValidatorTests()
		{
			_validator = new GetGenreDetailQueryValidator();
		}

		[Fact]
		public void WhenGenreIdIsZero_ShouldHaveValidationError()
		{
			// Arrange
			var query = new GetGenreDetailQuery(null) { GenreId = 0 };

			// Act
			var result = _validator.Validate(query);

			// Assert
			result.Errors.Should().ContainSingle(e => e.ErrorMessage == "GenreId must be greater than 0");
		}

		[Fact]
		public void WhenGenreIdIsValid_ShouldNotHaveValidationError()
		{
			// Arrange
			var query = new GetGenreDetailQuery(null) { GenreId = 1 };

			// Act
			var result = _validator.Validate(query);

			// Assert
			result.Errors.Should().BeEmpty();
		}
	}

}
