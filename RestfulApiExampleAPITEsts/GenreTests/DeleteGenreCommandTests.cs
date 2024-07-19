using FluentAssertions;
using FluentValidation;
using Moq;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;

namespace RestfulApiExampleAPITests.GenreTests
{
	public class DeleteGenreCommandTests
	{
		private readonly Mock<IGenreRepository> _mockRepo;

		public DeleteGenreCommandTests()
		{
			_mockRepo = new Mock<IGenreRepository>();
		}
		[Fact]
		public async Task WhenGenreIdDoesNotExist_ShouldThrowInvalidOperationException()
		{
			// Arrange
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Genre>(null));

			// Act
			var command = new DeleteGenreCommand(_mockRepo.Object) { GenreId = 1 };
			Func<Task> act = async () => await command.Handle();

			// Assert
			await act.Should().ThrowAsync<InvalidOperationException>()
				.WithMessage("Genre not found");
		}


		[Fact]
		public async Task WhenGenreIdExists_ShouldDeleteGenre()
		{
			// Arrange
			var genre = new Genre { Id = 1, Name = "Test Genre" };
			_mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(genre));

			// Act
			var command = new DeleteGenreCommand(_mockRepo.Object) { GenreId = 1 };
			await command.Handle();

			// Assert
			_mockRepo.Verify(repo => repo.Remove(It.IsAny<Genre>()), Times.Once);
		}
	}

}
