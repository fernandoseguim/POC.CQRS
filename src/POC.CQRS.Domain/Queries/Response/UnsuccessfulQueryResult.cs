using POC.CQRS.Shared.Commands;
using POC.CQRS.Shared.Queries;

namespace POC.CQRS.Domain.Queries.Response
{
	public class UnsuccessfulQueryResult : IQueryResult
	{
		public UnsuccessfulQueryResult(StatusCodeResult statusCode, object data)
		{
			this.StatusCode = statusCode;
			this.Data = data;
		}

		public StatusCodeResult StatusCode { get; }
		public bool Success => false;
		public object Data { get; }
	}
}
