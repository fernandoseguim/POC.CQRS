using POC.CQRS.Domain.Entities;
using POC.CQRS.Domain.Queries;
using POC.CQRS.Domain.ValueObjects;
using System.Collections.Generic;
using POC.CQRS.Domain.Queries.Response;

namespace POC.CQRS.Domain.Repositories
{
	public interface IUserRepository : IEntityRepository<User>
	{
		IEnumerable<UserQueryResult> GetAll();
		bool CheckEmail(Email email);
	}
}
