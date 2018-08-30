using POC.CQRS.Shared.Commands;

namespace POC.CQRS.Shared.Queries
{
	public interface IQueryResult
    {
	    StatusCodeResult StatusCode { get; }
	    bool Success { get; }
	    object Data { get; }
	}
}
