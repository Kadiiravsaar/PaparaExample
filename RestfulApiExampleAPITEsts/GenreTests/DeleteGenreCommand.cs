using RestfulApiExample.Core.Repositories;

namespace RestfulApiExampleAPITests.GenreTests
{
	public class DeleteGenreCommand
	{
		private readonly IGenreRepository _repository;
		public int GenreId { get; set; }

		public DeleteGenreCommand(IGenreRepository repository)
		{
			_repository = repository;
		}

		public async Task Handle()
		{
			var genre = await _repository.GetByIdAsync(GenreId);
			if (genre == null)
				throw new InvalidOperationException("Genre not found");

			_repository.Remove(genre);
		}
	}

}
