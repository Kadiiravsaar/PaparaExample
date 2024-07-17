using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulApiExample.Core.DTOs.Genre;
using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Core.Services;
using RestfulApiExample.Core.Models;

namespace RestfulApiExample.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenresController : ControllerBase
	{
		private readonly IGenreService _genreService;
		private readonly IMapper _mapper;

		public GenresController(IGenreService genreService, IMapper mapper)
		{
			_genreService = genreService;
			_mapper = mapper;
		}

		// GET: api/Genres
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var Genres = await _genreService.GetAllAsync();
			var GenreDtos = _mapper.Map<List<GenreDto>>(Genres);
			return Ok(CustomResponseDto<List<GenreDto>>.Success(200, GenreDtos));
		}

		// GET: api/Genres/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var genre = await _genreService.GetByIdAsync(id);
			var genreDto = _mapper.Map<GenreDto>(genre);
			return Ok(CustomResponseDto<GenreDto>.Success(200, genreDto));
		}

		// POST: api/Genres
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateGenreDto createGenreDto)
		{
			var genre = _mapper.Map<Genre>(createGenreDto);
			var newGenre = await _genreService.AddAsync(genre);
			var newGenreDto = _mapper.Map<GenreDto>(newGenre);
			return CreatedAtAction(nameof(GetById), new { id = newGenreDto.Id }, CustomResponseDto<GenreDto>.Success(201, newGenreDto));
		}

		// PUT: api/Genres/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] GenreDto genreDto)
		{
			var existingGenre = await _genreService.GetByIdAsync(id);
			_mapper.Map(genreDto, existingGenre);
			await _genreService.UpdateAsync(existingGenre);
			return NoContent();
		}

		// DELETE: api/Genres/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var Genre = await _genreService.GetByIdAsync(id);
			await _genreService.RemoveAsync(Genre);
			return NoContent();
		}
	}
}
