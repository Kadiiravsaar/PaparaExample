using Moq;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.AuthorTests
{
	public class DeleteAuthorCommandTests
	{
		private readonly Mock<IAuthorRepository> _mockRepo;
		private readonly DeleteAuthorCommand _command;

		public DeleteAuthorCommandTests()
		{
			_mockRepo = new Mock<IAuthorRepository>();
			_command = new DeleteAuthorCommand(_mockRepo.Object);
		}

		[Fact]
		public async Task WhenAuthorDoesNotExist_ShouldThrowInvalidOperationException()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Author>(null));
			_command.AuthorId = 1;

			// Act & Assert
			await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
		}

		[Fact]
		public async Task WhenAuthorExists_ShouldDeleteAuthor()
		{
			// Arrange
			var author = new Author { Id = 1, Name = "Test Author" };
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(author));
			_mockRepo.Setup(repo => repo.Remove(It.IsAny<Author>()));

			_command.AuthorId = 1;

			// Act
			await _command.Handle();

			// Assert
			_mockRepo.Verify(repo => repo.Remove(author), Times.Once);
		}
	}

}
