using FluentAssertions;
using Moq;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;

namespace RestfulApiExampleAPITests.BookTests
{

	public class GetBookDetailQueryTests
	{
		private readonly Mock<IBookRepository> _mockRepo;

		public GetBookDetailQueryTests()
		{
			_mockRepo = new Mock<IBookRepository>();
		}

		[Fact]
		public async Task WhenBookIdDoesNotExist_ShouldThrowInvalidOperationException()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Book)null);

			// Act
			var query = new GetBookDetailQuery(_mockRepo.Object) { BookId = 1 };

			// Assert
			await Assert.ThrowsAsync<InvalidOperationException>(async () => await query.Handle());
		}

		[Fact]
		public async Task WhenBookIdExists_ShouldReturnBookDetail()
		{
			// Arrange
			var book = new Book { Id = 1, Title = "Test Book" };
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(book);

			// Act
			var query = new GetBookDetailQuery(_mockRepo.Object) { BookId = 1 };
			var result = await query.Handle();

			// Assert
			result.Should().NotBeNull();
			result.Title.Should().Be("Test Book");
			// Diğer doğrulamalar burada yapılabilir, örneğin AuthorId, GenreId, PublishDate vb.
		}
	}


}
