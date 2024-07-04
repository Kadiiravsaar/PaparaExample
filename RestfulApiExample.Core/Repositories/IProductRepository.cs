using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Utilities;

namespace RestfulApiExample.Core.Repositories
{
	public interface IProductRepository : IGenericRepository<Product>
	{	
		Task<List<Product>> GetProductsByName(string name);
		Task<List<Product>> SortProducts(string field, SortOrder order);


		Task<PagedResultDto<Product>> GetPagedProductsAsync(int page, int pageSize);
	}
}
