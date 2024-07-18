using Microsoft.EntityFrameworkCore;
using RestfulApiExample.Core.Repositories;
using RestfulApiExample.Core.Services;
using RestfulApiExample.Core.UnitOfWorks;
using RestfulApiExample.Repository.Context;
using RestfulApiExample.Repository.Repositories;
using RestfulApiExample.Repository.UnitOfWorks;
using RestfulApiExample.Service.Mapping;
using RestfulApiExample.Service.Services;

namespace RestfulApiExample.API.Extensions
{

	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Add DbContext
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

			// Add UnitOfWork
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			// Add Repositories
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IProductRepository, ProductRepository>();


			services.AddScoped<IBookRepository, BookRepository>();
			services.AddScoped<IAuthorRepository, AuthorRepository>();
			services.AddScoped<IGenreRepository, GenreRepository>();




			// Add Services
			services.AddScoped(typeof(IService<>), typeof(Service<>));
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IGenreService, GenreService>();
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<IAuthorService, AuthorService>();

			services.AddScoped<FakeUserService>();


			// Add AutoMapper
			services.AddAutoMapper(typeof(MapProfile));

			return services;
		}
	}


}
