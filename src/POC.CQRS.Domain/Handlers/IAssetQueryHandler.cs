using POC.CQRS.Shared.Queries;

namespace POC.CQRS.Domain.Handlers
{
	public interface IAssetQueryHandler : IQueryHandler
	{
		IQueryResult GetById(string id);
		IQueryResult GetAllByBrandId(string id);
	}
}
