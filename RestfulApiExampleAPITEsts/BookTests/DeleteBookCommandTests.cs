using FluentAssertions;
using Moq;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.BookTests
{

	public class DeleteBookCommandTests
	{
		private readonly Mock<IBookRepository> _mockRepo;

		public DeleteBookCommandTests()
		{
			_mockRepo = new Mock<IBookRepository>();
		}

		[Fact]
		public async Task WhenBookIdDoesNotExist_ShouldThrowInvalidOperationException()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Book>(null));

			var command = new DeleteBookCommand(_mockRepo.Object) { BookId = 1 };

			// Act & Assert
			await Assert.ThrowsAsync<InvalidOperationException>(async () => await command.Handle());
		}

		[Fact]
		public async Task WhenBookIdExists_ShouldDeleteBook()
		{
			// Arrange
			var book = new Book { Id = 1, Title = "Test Book" };
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(book));
			_mockRepo.Setup(repo => repo.Remove(It.IsAny<Book>()));

			var command = new DeleteBookCommand(_mockRepo.Object) { BookId = 1 };

			// Act
			await command.Handle();

			// Assert
			_mockRepo.Verify(repo => repo.Remove(book), Times.Once);
		}
	}
}
