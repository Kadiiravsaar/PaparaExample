using FluentAssertions;
using Moq;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;

namespace RestfulApiExampleAPITests.GenreTests
{
	public class GetGenreDetailQueryTests
	{
		private readonly Mock<IGenreRepository> _mockRepo;

		public GetGenreDetailQueryTests()
		{
			_mockRepo = new Mock<IGenreRepository>();
		}

		[Fact]
		public async Task WhenGenreIdDoesNotExist_ShouldThrowInvalidOperationException()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Genre)null);

			// Act
			var query = new GetGenreDetailQuery(_mockRepo.Object) { GenreId = 1 };

			// Assert
			await Assert.ThrowsAsync<InvalidOperationException>(async () => await query.Handle());
		}

		[Fact]
		public async Task WhenGenreIdExists_ShouldReturnGenreDetail()
		{
			// Arrange
			var genre = new Genre { Id = 1, Name = "Test Genre" };
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(genre);

			// Act
			var query = new GetGenreDetailQuery(_mockRepo.Object) { GenreId = 1 };
			var result = await query.Handle();

			// Assert
			result.Should().NotBeNull();
			result.Name.Should().Be("Test Genre");
			// Diğer doğrulamalar burada yapılabilir
		}
	}

}
