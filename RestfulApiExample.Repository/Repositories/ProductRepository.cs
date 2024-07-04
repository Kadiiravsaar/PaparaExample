using Microsoft.EntityFrameworkCore;
using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using RestfulApiExample.Core.Utilities;
using RestfulApiExample.Repository.Context;
using System.Linq.Expressions;

namespace RestfulApiExample.Repository.Repositories
{
	// Ürünlere özel repo uygulaması
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		public ProductRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<PagedResultDto<Product>> GetPagedProductsAsync(int page, int pageSize)
		{
			var query = _context.Products.AsQueryable();


			// Sayfalama işlemi
			// Veritabanındaki toplam ürün sayısını asenkron olarak hesaplar
			var totalCount = await query.CountAsync();
			var items = await query.Skip((page - 1) * pageSize)               // Örnek (page(1) - 1) * pageSize(1) şu demek => 1. sayfada 1 değer olsun => (page(2) - 1) * pageSize(1) => 1. safyada ki 1 değeri atla sonraki 1 getir
				.Take(pageSize)												  // Sayfa boyutu kadar öğe alır
				.ToListAsync();                                               // Sonuçları liste olarak asenkron şekilde döner


			// Sayfa sonuçlarını ve toplam ürün sayısını içeren bir PagedResultDto nesnesi döner	
			return new PagedResultDto<Product>
			{
				TotalCount = totalCount, // Toplam ürün sayısını eşler => map işlemi yapılabilir
				Items = items            // Sayfadaki ürünleri eşler	
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

			// Dinamik olarak sıralama ifadesi oluşturma
			// Sıralama yapılacak ürün tipinde bir parametre oluşturuluyor, burada "p" adını veriyoruz
			var parameter = Expression.Parameter(typeof(Product), "p");

			// Parametreye göre sıralama yapılacak alanın ismi (field) ile property (özellik) ifadesi oluşturuyoruz.
			var property = Expression.Property(parameter, field);

			// Property ifadesine göre lambda ifadesi oluşturuyoruz
			var lambda = Expression.Lambda(property, parameter);

			// Sıralama yönüne göre kullanılacak metodun ismi belirleniyor "SortOrder.2"= desc değilse "OrderBy" 
			var methodName = (order == SortOrder.Desc) ? "OrderByDescending" : "OrderBy";

			var resultExpression = Expression.Call(typeof(Queryable),
				methodName,												  // Kullanılacak sıralama metodunun ismi
				new Type[] { typeof(Product),property.Type },             // Metodun parametre tipleri			
				products.Expression,                                      // Mevcut sorgu 
				lambda                                                    // Sıralama kriteri olarak kullanılacak lambda 
				);

			
			// Sıralama ifadesi kullanılarak yeni bir IQueryable oluşturuyoruz
			products = products.Provider.CreateQuery<Product>(resultExpression);

			// Sıralanmış ürünler listesi asenkron olarak döndürüyoruz.
			return await products.ToListAsync();
		}
	}
}
