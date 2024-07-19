using RestfulApiExample.Core.DTOs.Genre;
using RestfulApiExample.Core.Repositories;

namespace RestfulApiExampleAPITests.GenreTests
{
	public class GetGenreDetailQuery
	{
		private readonly IGenreRepository _repository;
		public int GenreId { get; set; }

		public GetGenreDetailQuery(IGenreRepository repository)
		{
			_repository = repository;
		}

		public async Task<GenreDto> Handle()
		{
			var genre = await _repository.GetByIdAsync(GenreId);
			if (genre == null)
				throw new InvalidOperationException("Genre not found.");

			return new GenreDto
			{
				Id = genre.Id,
				Name = genre.Name
			};
		}
	}


}
