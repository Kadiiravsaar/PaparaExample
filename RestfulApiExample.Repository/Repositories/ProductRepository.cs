using Microsoft.EntityFrameworkCore;
using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using RestfulApiExample.Core.Utilities;
using RestfulApiExample.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Repository.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		public ProductRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<PagedResultDto<Product>> GetPagedProductsAsync(int page, int pageSize)
		{
			var query = _context.Products.AsQueryable();

			// Sayfalama işlemi
			var totalCount = await query.CountAsync();
			var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

			return new PagedResultDto<Product>
			{
				TotalCount = totalCount,
				Items = items
			};
		}

		public async Task<List<Product>> GetProductsByName(string name)
		{
			var products = await _context.Products
				.Where(p => p.Name.ToLower().Contains(name.ToLower()))
				.ToListAsync();
			return products;
		}

		public async Task<List<Product>> SortProducts(string field, SortOrder order)
		{
			var products = _context.Products.AsQueryable();

			if (string.IsNullOrEmpty(field))
			{
				return await products.ToListAsync();
			}

			// Dinamik olarak sıralama ifadesi oluşturma
			var parameter = Expression.Parameter(typeof(Product), "p");
			var property = Expression.Property(parameter, field);
			var lambda = Expression.Lambda(property, parameter);

			var methodName = (order == SortOrder.Desc) ? "OrderByDescending" : "OrderBy";
			var resultExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(Product), property.Type }, products.Expression, lambda);

			products = products.Provider.CreateQuery<Product>(resultExpression);

			return await products.ToListAsync();
		}
	}
}
