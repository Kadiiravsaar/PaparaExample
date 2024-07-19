using FluentAssertions;
using Moq;
using RestfulApiExample.Core.DTOs.Author;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.AuthorTests
{
	public class UpdateAuthorCommandTests
	{
		private readonly Mock<IAuthorRepository> _mockRepo;
		private readonly UpdateAuthorCommand _command;

		public UpdateAuthorCommandTests()
		{
			_mockRepo = new Mock<IAuthorRepository>();
			_command = new UpdateAuthorCommand(_mockRepo.Object);
		}

		[Fact]
		public async Task WhenAuthorDoesNotExist_ShouldThrowInvalidOperationException()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Author>(null));
			_command.AuthorId = 1;
			_command.Model = new UpdateAuthorDto { Name = "Updated Name" };

			// Act & Assert
			await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
		}

		[Fact]
		public async Task WhenAuthorExists_ShouldUpdateAuthor()
		{
			// Arrange
			var author = new Author { Id = 1, Name = "Test Author" };
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(author));

			_command.AuthorId = 1;
			_command.Model = new UpdateAuthorDto { Name = "Updated Name" };

			// Act
			await _command.Handle();

			// Assert
			_mockRepo.Verify(repo => repo.Update(author), Times.Once);
			author.Name.Should().Be("Updated Name");
		}
	}

}
