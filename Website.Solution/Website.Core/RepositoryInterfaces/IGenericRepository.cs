using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Core.Entities;
using Website.Core.Specifications;

namespace Website.Core.RepositoryInterfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
	IReadOnlyList<T> GetAll();
	T? GetEntityById(string id);

	IReadOnlyList<T> GetAllWithSpec(ISpecification<T> spec);
	T? GetEntityWithSpec(ISpecification<T> spec);

	void Add(T entity);
	void Update(T entity);
	void Delete(T entity);
}
