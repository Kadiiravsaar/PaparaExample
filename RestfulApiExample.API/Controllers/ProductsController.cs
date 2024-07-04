using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Services;
using RestfulApiExample.Core.Utilities;
using RestfulApiExample.Service.Attributes;
using static Azure.Core.HttpHeader;

namespace RestfulApiExample.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductsController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		// GET: api/Products
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var products = await _productService.GetAllAsync();
			var productDtos = _mapper.Map<List<ProductDto>>(products);
			return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var product = await _productService.GetByIdAsync(id);
			var productDto = _mapper.Map<ProductDto>(product);
			return Ok(CustomResponseDto<ProductDto>.Success(200, productDto));
		}

		// POST: api/Products
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] ProductDto productDto)
		{
			var product = _mapper.Map<Product>(productDto);
			var newProduct = await _productService.AddAsync(product);
			var newProductDto = _mapper.Map<ProductDto>(newProduct);
			return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, CustomResponseDto<ProductDto>.Success(201, newProductDto));
		}

		// PUT: api/Products/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
		{
			var existingProduct = await _productService.GetByIdAsync(id);
			_mapper.Map(productDto, existingProduct);
			await _productService.UpdateAsync(existingProduct);
			return NoContent();
		}

		// DELETE: api/Products/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _productService.GetByIdAsync(id);
			await _productService.RemoveAsync(product);
			return NoContent();
		}

		// GET: api/Products/name?name=example
		[HttpGet("name")]
		public async Task<IActionResult> GetProductsByName(string name)
		{
			var result = await _productService.GetProductsByName(name);
			return Ok(result);
		}


		// GET: api/Products/paged?page=1&pageSize=10
		[HttpGet("paged")]
		public async Task<IActionResult> GetPagedProducts([FromQuery] int page, [FromQuery] int pageSize)
		{
			var result = await _productService.GetPagedProductsAsync(page, pageSize);
			return Ok(result);
		}

		// GET: api/Products/sort?field=name&order=asc
		[HttpGet("sort")]
		public async Task<IActionResult> SortProducts(string field, int order)
		{
			var sortOrder = (SortOrder)order;
			var result = await _productService.SortProducts(field, sortOrder);
			return Ok(result);
		}
	}
}
