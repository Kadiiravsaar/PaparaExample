using Microsoft.EntityFrameworkCore;
using RestfulApiExample.Core.Repositories;
using RestfulApiExample.Core.Services;
using RestfulApiExample.Core.UnitOfWorks;
using RestfulApiExample.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Service.Services
{
	// Genel servis uygulaması, temel CRUD işlemleri sağlar
	public class Service<T> : IService<T> where T : class
	{
		protected readonly IGenericRepository<T> _repository;
		private readonly IUnitOfWork _unitOfWork;

		public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<T> AddAsync(T entity)
		{
			var result = await _repository.AddAsync(entity);
			await _unitOfWork.CommitAsync();
			return result;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _repository.GetAll().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null)
			{
				 throw new NotFoundException($"{typeof(T).Name} with id ({id}) not found");

			}
			return entity;
		}

		public async Task RemoveAsync(T entity)
		{
			_repository.Remove(entity);
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_repository.Update(entity);
			await _unitOfWork.CommitAsync();
		}
	}
}