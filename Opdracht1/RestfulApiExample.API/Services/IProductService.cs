using RestfulApiExample.API.Models;

namespace RestfulApiExample.API.Services
{
	public interface IProductService
	{
		IEnumerable<Product> GetAll();
		Product GetById(int id);
		IEnumerable<Product> ListProducts(string name);
		void Add(Product product);
		void Update(Product product);
		void Delete(int id);
	}
}
