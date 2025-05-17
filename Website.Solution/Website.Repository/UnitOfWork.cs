using System.Collections;
using Website.Core;
using Website.Core.Entities;
using Website.Core.RepositoryInterfaces;
using Website.Repository.Repositories;

namespace Website.Repository;

public class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationDbContext _context;
	private Hashtable _repositories;


	public UnitOfWork(ApplicationDbContext context)
	{
		_context=context;
		_repositories = new Hashtable();
	}

	public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
	{
		var key = typeof(TEntity).Name;

		if (!_repositories.ContainsKey(key))
		{
			if (key == "Event")
				_repositories.Add(key, new EventRepository(_context));
			else
				_repositories.Add(key, new GenericRepository<TEntity>(_context));
		}

		return _repositories[key] as IGenericRepository<TEntity>;
	}

	public async Task<int> CompleteAsync()
	{
		return await _context.SaveChangesAsync();
	}

	public async ValueTask DisposeAsync()
	{
		await _context.DisposeAsync();
	}

}
