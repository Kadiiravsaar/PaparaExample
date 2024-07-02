using Microsoft.AspNetCore.Mvc;
using RestfulApiExample.API.DTOs;
using RestfulApiExample.API.Models;
using RestfulApiExample.API.Services;

namespace RestfulApiExample.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		// GET: api/products
		[HttpGet]
		public IActionResult GetProducts()
		{
			var response = _productService.GetAll();
			return Ok(response);
		}

		// GET: api/products/5
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var response = _productService.GetById(id);
			if (!response.IsSuccess)
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		// POST: api/products
		[HttpPost]
		public IActionResult AddProduct([FromBody] Product product)
		{
			var response = _productService.Add(product);
			return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
		}
		// GET: api/products/sort?field=name&order=asc
		[HttpGet("sort")]
		public ActionResult<ApiResponseDto<List<Product>>> SortProducts([FromQuery] string field, [FromQuery] string order)
		{
			var response = _productService.SortProducts(field, order);
			if (!response.IsSuccess)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		// PUT: api/products/5
		[HttpPut("{id}")]
		public IActionResult PutProduct(int id, [FromBody] Product product)
		{
			var response = _productService.Update(id, product);
			if (!response.IsSuccess)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		// DELETE: api/products/5
		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
		{
			var response = _productService.Delete(id);
			if (!response.IsSuccess)
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		// GET: api/products/list?name=abc 
		[HttpGet("list")]
		public IActionResult GetProductsByName([FromQuery] string name)
		{
			var response = _productService.GetProductsByName(name);
			return Ok(response);
		}
	}
}
