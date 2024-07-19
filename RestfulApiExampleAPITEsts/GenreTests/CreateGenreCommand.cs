using RestfulApiExample.Core.DTOs.Genre;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExampleAPITests.GenreTests
{
	public class CreateGenreCommand
	{
		private readonly IGenreRepository _repository;
		public CreateGenreDto Model { get; set; }

		public CreateGenreCommand(IGenreRepository repository)
		{
			_repository = repository;
		}

		public async Task Handle()
		{
			// Model validation could be done in the validator
			var genre = new Genre
			{
				Name = Model.Name
			};

			await _repository.AddAsync(genre);
		}
	}

}
