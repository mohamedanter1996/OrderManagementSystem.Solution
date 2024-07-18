using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Repositories.Contract
{
	public interface IUnitOfWork: IAsyncDisposable
	{
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

		Task<int> CompleteAsync();
	}
}
