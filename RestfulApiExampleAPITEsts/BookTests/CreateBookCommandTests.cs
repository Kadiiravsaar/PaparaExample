using Moq;
using RestfulApiExample.Core.DTOs.Author;
using RestfulApiExample.Core.DTOs.Book;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using RestfulApiExampleAPITests.AuthorTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.BookTests
{
	public class CreateBookCommandTests
	{
		private readonly Mock<IBookRepository> _mockRepo;
		private readonly CreateBookCommand _command;

		public CreateBookCommandTests()
		{
			_mockRepo = new Mock<IBookRepository>();
			_command = new CreateBookCommand(_mockRepo.Object);
		}

		[Fact]
		public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
		{
			// Arrange
			var model = new CreateBookDto
			{
				Title = "Test",
				PageCount = 100,
				PublishDate = new DateTime(1980, 1, 1)
				
			};
			_command.Model = model;

			// Act
			_command.Handle();

			// Assert
			_mockRepo.Verify(repo => repo.AddAsync(It.Is<Book>(a => a.Title == model.Title && a.PageCount == model.PageCount && a.PublishDate == model.PublishDate)), Times.Once);
		}
	}
}
