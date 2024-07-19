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
	public class CreateAuthorCommandTests
	{
		private readonly Mock<IAuthorRepository> _mockRepo;
		private readonly CreateAuthorCommand _command;

		public CreateAuthorCommandTests()
		{
			_mockRepo = new Mock<IAuthorRepository>();
			_command = new CreateAuthorCommand(_mockRepo.Object);
		}

		[Fact]
		public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
		{
			// Arrange
			var model = new CreateAuthorDto
			{
				Name = "Test",
				LastName = "Author",
				DateOfBirth = new DateTime(1980, 1, 1)
			};
			_command.Model = model;

			// Act
			_command.Handle();

			// Assert
			_mockRepo.Verify(repo => repo.AddAsync(It.Is<Author>(a => a.Name == model.Name && a.LastName == model.LastName && a.DateOfBirth == model.DateOfBirth)), Times.Once);
		}
	}

}
