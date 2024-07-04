using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Utilities;

namespace RestfulApiExample.Core.Repositories
{
	// Ürünlere özel repo arayüzü, genel repo arayüzünü genişletir
	public interface IProductRepository : IGenericRepository<Product>
	{
		// İsme göre ürünleri getirir
		Task<List<Product>> GetProductsByName(string name);

		// Belirli bir alan ve sıraya göre ürünleri sıralar
		Task<List<Product>> SortProducts(string field, SortOrder order);

		// Sayfalı ürünleri getirir
		Task<PagedResultDto<Product>> GetPagedProductsAsync(int page, int pageSize);
	}
}
