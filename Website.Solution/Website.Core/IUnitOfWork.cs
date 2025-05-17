using Website.Core.Entities;
using Website.Core.RepositoryInterfaces;

namespace Website.Core;

public interface IUnitOfWork : IAsyncDisposable
{
	IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
	Task<int> CompleteAsync();
}
