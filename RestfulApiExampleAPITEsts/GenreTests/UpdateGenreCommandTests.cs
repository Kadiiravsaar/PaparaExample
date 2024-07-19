using FluentAssertions;
using FluentValidation;
using Moq;
using RestfulApiExample.Core.DTOs.Genre;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;

namespace RestfulApiExampleAPITests.GenreTests
{
	public class UpdateGenreCommandTests
	{
		private readonly Mock<IGenreRepository> _mockRepo;
		private readonly UpdateGenreCommandValidator _validator;

		public UpdateGenreCommandTests()
		{
			_mockRepo = new Mock<IGenreRepository>();
			_validator = new UpdateGenreCommandValidator();
		}

		[Fact]
		public async Task WhenGenreIdDoesNotExist_ShouldThrowInvalidOperationException()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Genre>(null));

			// Act
			var command = new UpdateGenreCommand(_mockRepo.Object) { GenreId = 1, Model = new UpdateGenreDto { Name = "Updated Genre" } };
			Func<Task> act = async () => await command.Handle();

			// Assert
			await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Genre not found");
		}

		[Fact]
		public async Task WhenValidInputsAreGiven_ShouldUpdateGenre()
		{
			// Arrange
			var genre = new Genre { Id = 1, Name = "Old Genre" };
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(genre));

			var command = new UpdateGenreCommand(_mockRepo.Object) { GenreId = 1, Model = new UpdateGenreDto { Name = "Updated Genre" } };

			// Act
			await command.Handle();

			// Assert
			_mockRepo.Verify(repo => repo.Update(It.Is<Genre>(g => g.Name == "Updated Genre")), Times.Once);
		}
	}

}
