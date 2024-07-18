using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Repository._Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private Hashtable _repositories;
		private readonly OrderManagementDbContext _dbContext;

		public UnitOfWork(OrderManagementDbContext dbContext)
		{
			_dbContext = dbContext;
			_repositories = new Hashtable();

		}
		public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

		public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
		{
			var key = typeof(TEntity).Name;
			if (!_repositories.ContainsKey(key))
			{
				var repository = new GenericRepository<TEntity>(_dbContext);
				_repositories.Add(key, repository);
			}

			return _repositories[key] as IGenericRepository<TEntity>;
		}
	}
}
