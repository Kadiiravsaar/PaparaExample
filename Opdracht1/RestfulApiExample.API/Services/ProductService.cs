using RestfulApiExample.API.Models;

namespace RestfulApiExample.API.Services
{
	public class ProductService : IProductService
	{
		private readonly List<Product> _products;

		public ProductService()
		{
			_products = new List<Product>()
			{
				new Product()
				{
					Id = 1,
					Name = "Test",
					Description = "Test",
					Price = 1
				},
				new Product()
				{
					Id = 2,
					Name = "Test 2",
					Description = "Test 2",
					Price = 2
				},
				new Product()
				{
					Id = 4,
					Name = "Deneme2",
					Description = "Deneme 2",
					Price = 2
				}
			};
		}

		public void Add(Product product)
		{
			_products.Add(product);
		}

		public bool Delete(int id)
		{
			var deleteProduct = _products.FirstOrDefault(p => p.Id == id);
			if (deleteProduct == null)
			{
				return false;
			}
			_products.Remove(deleteProduct);
			return true;
		}

		public IEnumerable<Product> GetAll()
		{
			return _products;
		}

		public Product GetById(int id)
		{
			return _products.FirstOrDefault(p => p.Id == id);
		}

		public bool Update(Product product)
		{
			var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
			if (existingProduct == null)
			{
				return false;
			}

			existingProduct.Name = product.Name;
			existingProduct.Description = product.Description;
			existingProduct.Price = product.Price;

			return true;
		}

		public IEnumerable<Product> ListProducts(string name)
		{
			var products = _products;

			if (!string.IsNullOrEmpty(name))
			{
				products = products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
			}

			return products;
		}
	}
}
