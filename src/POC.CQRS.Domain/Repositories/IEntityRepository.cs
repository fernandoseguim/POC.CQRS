using System;
using POC.CQRS.Shared.Entities;

namespace POC.CQRS.Domain.Repositories
{
	public interface IEntityRepository<in TEntity> where TEntity : Entity
	{
		void Save(TEntity entity);
		bool Delete(Guid id);
	}
}
