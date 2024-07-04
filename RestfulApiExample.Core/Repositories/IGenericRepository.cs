using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Core.Repositories
{
	// Temel CRUD işlemleri için genel (repository) arayüzü
	public interface IGenericRepository<T> where T : class
	{
		IQueryable<T> GetAll();
		Task<T> GetByIdAsync(int id);
		Task<T> AddAsync(T entity);
		void Remove(T entity);
		void Update(T entity);
	}
}
