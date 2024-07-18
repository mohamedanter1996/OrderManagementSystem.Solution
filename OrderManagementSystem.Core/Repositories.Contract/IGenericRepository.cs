using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Repositories.Contract
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
		Task<IReadOnlyList<T>> GetAllWithFiltersAsync(Expression<Func<T, bool>> predicate);
		Task<T?> GetByIdAsync(int id);

		Task<IReadOnlyList<T>> GetAllAsync();

		Task AddAsync(T entity);
		void Add(T entity);

		void Update(T entity);

		void Delete(T entity);
	}
}
