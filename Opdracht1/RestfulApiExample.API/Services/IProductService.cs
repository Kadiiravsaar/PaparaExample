using RestfulApiExample.API.DTOs;
using RestfulApiExample.API.Models;

namespace RestfulApiExample.API.Services
{
	public interface IProductService
	{
		ApiResponseDto<List<Product>> GetAll();
		ApiResponseDto<Product> Add(Product product);
		ApiResponseDto<Product> Update(int id, Product product); 
		ApiResponseDto<Product> Delete(int id);
		ApiResponseDto<Product> GetById(int id);
		ApiResponseDto<List<Product>> GetProductsByName(string name);

		ApiResponseDto<List<Product>> SortProducts(string field, string order);
	}
}
