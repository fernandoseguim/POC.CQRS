namespace POC.CQRS.Shared.Queries
{
	public interface IQueryHandler
	{
		IQueryResult GetAll();
	}
}
