using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulApiExample.Core.DTOs.Author;
using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Services;

namespace RestfulApiExample.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorsController : ControllerBase
	{
		private readonly IAuthorService _authorService;
		private readonly IMapper _mapper;

		public AuthorsController(IAuthorService authorService, IMapper mapper)
		{
			_authorService = authorService;
			_mapper = mapper;
		}

		// GET: api/Authors
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var Authors = await _authorService.GetAllAsync();
			var AuthorDtos = _mapper.Map<List<AuthorDto>>(Authors);
			return Ok(CustomResponseDto<List<AuthorDto>>.Success(200, AuthorDtos));
		}

		// GET: api/Authors/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var Author = await _authorService.GetByIdAsync(id);
			var AuthorDto = _mapper.Map<AuthorDto>(Author);
			return Ok(CustomResponseDto<AuthorDto>.Success(200, AuthorDto));
		}

		// POST: api/Authors
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateAuthorDto createAuthorDto)
		{
			var Author = _mapper.Map<Author>(createAuthorDto);
			var newAuthor = await _authorService.AddAsync(Author);
			var newAuthorDto = _mapper.Map<AuthorDto>(newAuthor);
			return CreatedAtAction(nameof(GetById), new { id = newAuthorDto.Id }, CustomResponseDto<AuthorDto>.Success(201, newAuthorDto));
		}

		// PUT: api/Authors/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorDto updateAuthorDto)
		{
			var existingAuthor = await _authorService.GetByIdAsync(id);
			_mapper.Map(updateAuthorDto, existingAuthor);
			await _authorService.UpdateAsync(existingAuthor);
			return NoContent();
		}

		// DELETE: api/Authors/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var Author = await _authorService.GetByIdAsync(id);
			await _authorService.RemoveAsync(Author);
			return NoContent();
		}

		// DELETE: api/Authors/DeleteCondition/{id}
		[HttpDelete("DeleteCondition/{id}")]
		public async Task<IActionResult> DeleteCondition(int id)
		{
			await _authorService.ConditionRemoveAuthor(id);
			return NoContent();
		}

	}
}
