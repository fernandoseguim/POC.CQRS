using POC.CQRS.Shared.Queries;

namespace POC.CQRS.Domain.Handlers
{
	public interface IBrandQueryHandler : IQueryHandler
	{
		IQueryResult GetById(string id);
	}
}
