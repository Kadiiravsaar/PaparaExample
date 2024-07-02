using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		public ActionResult<IEnumerable<Product>> GetProducts()
		{
			return Ok(_productService.GetAll());
		}

		[HttpGet("list")]
		public ActionResult<IEnumerable<Product>> ListProducts([FromQuery] string name)
		{
			return Ok(_productService.ListProducts(name));
		}

		// GET: api/products/5
		[HttpGet("{id}")]
		public ActionResult<Product> GetProduct(int id)
		{
			var product = _productService.GetById(id);

			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}

		// POST: api/products
		[HttpPost]
		public ActionResult<Product> PostProduct(Product product)
		{
			_productService.Add(product);
			return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
		}

		// PUT: api/products/5
		[HttpPut("{id}")]
		public IActionResult PutProduct(int id, Product product)
		{
			if (id != product.Id)
			{
				return BadRequest();
			}

			var updateSuccess = _productService.Update(product);
			if (!updateSuccess)
			{
				return NotFound();
			}

			return NoContent();
		}

		// DELETE: api/products/5
		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
		{
			var deleteSuccess = _productService.Delete(id);
			if (!deleteSuccess)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
