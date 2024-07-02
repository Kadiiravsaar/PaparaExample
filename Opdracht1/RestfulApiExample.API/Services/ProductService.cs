using RestfulApiExample.API.DTOs;
using RestfulApiExample.API.Models;

namespace RestfulApiExample.API.Services
{
	public class ProductService : IProductService
	{
		private List<Product> _products;

		public ProductService()
		{
			_products = new List<Product>();
			_products.Add(new Product() { Id = 1, Name = "Product1", Description = "Lorem ipsum1", Price= 10});
			_products.Add(new Product() { Id = 2, Name = "Product2", Description = "Lorem ipsum2", Price = 20 });
			_products.Add(new Product() { Id = 3, Name = "Laptop", Description = "Lorem ipsum LAPTOP", Price = 2000 });
		}

		

		public ApiResponseDto<Product> Add(Product product)
		{
			_products.Add(product);
			return new ApiResponseDto<Product>(product);
		}

		public ApiResponseDto<Product> Delete(int id)
		{
			var deleteProduct = _products.FirstOrDefault(p => p.Id == id);
			if (deleteProduct == null)
			{
				return new ApiResponseDto<Product>(null, $"Product ID ({id}) not found.", false);

			}
			_products.Remove(deleteProduct);

			return new ApiResponseDto<Product>(deleteProduct);

		}

		public ApiResponseDto<List<Product>> GetAll()
		{
			return new ApiResponseDto<List<Product>>(_products);
		}

		public ApiResponseDto<Product> GetById(int id)
		{
			var product = _products.FirstOrDefault(p => p.Id == id);
			if (product == null)
			{
				return new ApiResponseDto<Product>(null, $"Product ID ({id}) not found.", false);
			}
			return new ApiResponseDto<Product>(product);

		}

		public ApiResponseDto<Product> Update(int id, Product product)
		{

			if (id != product.Id)
			{

				return new ApiResponseDto<Product>(null, "The product ID doesn't match.", false);
			}
			var existingProduct = _products.FirstOrDefault(b => b.Id == product.Id);
			if (existingProduct is null)
			{
				return new ApiResponseDto<Product>(null, $"{product.Id} item not found", false);
			}
			existingProduct.Name = product.Name;
			existingProduct.Description = product.Description;
			existingProduct.Price = product.Price;

			return new ApiResponseDto<Product>(existingProduct);
			
		}

		public ApiResponseDto<List<Product>> GetProductsByName(string name)
		{
			var products = _products;

			if (!string.IsNullOrEmpty(name))
			{
				products = products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
			}

			return new ApiResponseDto<List<Product>>(products);
		}

		/// <summary>
		/// Sorts the list of products based on the specified field and order.
		/// </summary>
		/// <param name="field">The field by which to sort (e.g., "name", "price").</param>
		/// <param name="order">The sorting order ("asc" for ascending, "desc" for descending).</param>
		/// <returns>An ApiResponseDto containing the sorted list of products.</returns>
		public ApiResponseDto<List<Product>> SortProducts(string field, string order)
		{
			IEnumerable<Product> sortedProducts = _products;

			// Field validation																																																																																																							
			if (field.ToLower() == "name")
			{
				sortedProducts = (order.ToLower() == "desc") ? _products.OrderByDescending(p => p.Name) : _products.OrderBy(p => p.Name);
				// eğer desc yazmazsak asc olarak algılar
				// desc yerine ne yazarsak asc algılar
			}
			else if (field.ToLower() == "price")
			{
				sortedProducts = (order.ToLower() == "desc") ? _products.OrderByDescending(p => p.Price) : _products.OrderBy(p => p.Price);
			}
			else
			{
				return new ApiResponseDto<List<Product>>(null, $"Invalid fiedl '{field}'.", false);
			}

			return new ApiResponseDto<List<Product>>(sortedProducts.ToList());
		}

	}
}
