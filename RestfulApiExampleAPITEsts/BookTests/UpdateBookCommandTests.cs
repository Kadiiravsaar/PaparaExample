using FluentAssertions;
using Moq;
using RestfulApiExample.Core.DTOs.Book;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;

namespace RestfulApiExampleAPITests.BookTests
{
	public class UpdateBookCommandTests
	{
		private readonly Mock<IBookRepository> _mockRepo;

		public UpdateBookCommandTests()
		{
			_mockRepo = new Mock<IBookRepository>();
		}

		[Fact]
		public async Task WhenBookIdDoesNotExist_ShouldThrowInvalidOperationException()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Book)null);

			var command = new UpdateBookCommand(_mockRepo.Object)
			{
				BookId = 1,
				Model = new UpdateBookDto()
			};

			// Act & Assert
			await Assert.ThrowsAsync<InvalidOperationException>(async () => await command.Handle());
		}

		[Fact]
		public async Task WhenBookIdExists_ShouldUpdateBook()
		{
			// Arrange
			var book = new Book { Id = 1, Title = "Test Book" };
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(book);
			_mockRepo.Setup(repo => repo.Update(It.IsAny<Book>()));

			var command = new UpdateBookCommand(_mockRepo.Object)
			{
				BookId = 1,
				Model = new UpdateBookDto { Title = "Updated Title" }
			};

			// Act
			await command.Handle();

			// Assert
			_mockRepo.Verify(repo => repo.Update(It.Is<Book>(b => b.Title == "Updated Title")), Times.Once);
		}
	}


}
