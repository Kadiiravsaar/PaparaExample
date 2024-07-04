using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductDto>>> GetProductsByName(string name);
        Task<CustomResponseDto<List<ProductDto>>> SortProducts(string field, SortOrder order);

		Task<CustomResponseDto<PagedResultDto<ProductDto>>> GetPagedProductsAsync(int page, int pageSize);

	}
}
