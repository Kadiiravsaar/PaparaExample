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
			// Tüm türleri listeledik
			var Genres = await _genreService.GetAllAsync();

			// Genre nesnelerini GenreDto'ya dönüştürdük
			var GenreDtos = _mapper.Map<List<GenreDto>>(Genres);

			// Başarılı yanıt döndük(200)
			return Ok(CustomResponseDto<List<GenreDto>>.Success(200, GenreDtos));
		}

		// GET: api/Genres/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			// Belirli bir ID'ye sahip türü getirdik
			var genre = await _genreService.GetByIdAsync(id);

			// Genre nesnesini GenreDto'ya dönüştürdük
			var genreDto = _mapper.Map<GenreDto>(genre);

			// Başarılı yanıt döndük(200)
			return Ok(CustomResponseDto<GenreDto>.Success(200, genreDto));
		}

		// POST: api/Genres
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateGenreDto createGenreDto)
		{
			// gelen Dto'yu Genre'ye dönüştürdük ve ekledik(AddAsync)
			var genre = _mapper.Map<Genre>(createGenreDto);
			var newGenre = await _genreService.AddAsync(genre);

			// newGenre'yi(Genre) GenreDto'ya Oluşturulan türün bilgilerini döndük
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
			// Belirli bir ID'ye sahip türü sildik
			var genre = await _genreService.GetByIdAsync(id);
			await _genreService.RemoveAsync(genre);
			// Başarılı yanıt döndük (Boş bir içerik)
			return NoContent();
		}
	}
}
