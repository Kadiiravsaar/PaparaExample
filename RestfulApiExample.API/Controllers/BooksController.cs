using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Core.DTOs.Book;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Services;

namespace RestfulApiExample.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IBookService _bookService;
		private readonly IMapper _mapper;

		public BooksController(IBookService bookService, IMapper mapper)
		{
			_bookService = bookService;
			_mapper = mapper;
		}

		// GET: api/Books
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var books = await _bookService.GetAllAsync();
			var bookDtos = _mapper.Map<List<BookDto>>(books);
			return Ok(CustomResponseDto<List<BookDto>>.Success(200, bookDtos));
		}

		// GET: api/Books/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var book = await _bookService.GetByIdAsync(id);
			var bookDto = _mapper.Map<BookDto>(book);
			return Ok(CustomResponseDto<BookDto>.Success(200, bookDto));
		}

		// POST: api/Books
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateBookDto createBookDto)
		{
			var book = _mapper.Map<Book>(createBookDto);
			var newBook = await _bookService.AddAsync(book);
			var newBookDto = _mapper.Map<BookDto>(newBook);
			return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, CustomResponseDto<BookDto>.Success(201, newBookDto));
		}

		// PUT: api/Books/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto updateBookDto)
		{
			var existingBook = await _bookService.GetByIdAsync(id);
			_mapper.Map(updateBookDto, existingBook);
			await _bookService.UpdateAsync(existingBook);
			return NoContent();
		}

		// DELETE: api/Books/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var book = await _bookService.GetByIdAsync(id);
			await _bookService.RemoveAsync(book);
			return NoContent();
		}

		//// GET: api/Books/name?name=example
		//[HttpGet("name")]
		//public async Task<IActionResult> GetBooksByName(string name)
		//{
		//	var result = await _BookService.GetBooksByName(name);
		//	return Ok(result);
		//}



		//// GET: api/Books/paged?page=1&pageSize=10
		//[HttpGet("paged")]
		//public async Task<IActionResult> GetPagedBooks([FromQuery] int page, [FromQuery] int pageSize)
		//{
		//	var result = await _BookService.GetPagedBooksAsync(page, pageSize);
		//	return Ok(result);
		//}

		//// GET: api/Books/sort?field=name&order=asc
		//[HttpGet("sort")]
		//public async Task<IActionResult> SortBooks(string field, int order)
		//{
		//	var sortOrder = (SortOrder)order;
		//	var result = await _bookService.SortBooks(field, sortOrder);
		//	return Ok(result);
		//}
	}
}
