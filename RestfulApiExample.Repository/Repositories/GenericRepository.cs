using Microsoft.EntityFrameworkCore;
using RestfulApiExample.Core.Repositories;
using RestfulApiExample.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Repository.Repositories
{

	// Temel CRUD işlemleri için genel repo uygulaması
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly AppDbContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task<T> AddAsync(T entity)
		{
			// Yeni bir nesneyi veritabanına ekledik
			await _dbSet.AddAsync(entity);
			
			return entity;
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
		{
			// Belirli bir koşula göre nesne var mı diye kontrol ettik
			return await _dbSet.AnyAsync(predicate);
		}	


		public IQueryable<T> GetAll()
		{
			return _dbSet.AsQueryable();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			// Belirli bir ID'ye sahip nesneyi getirrdik
			return await _dbSet.FindAsync(id);
		}

		public void Remove(T entity)
		{
			_dbSet.Remove(entity);
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
		}
	}

}
