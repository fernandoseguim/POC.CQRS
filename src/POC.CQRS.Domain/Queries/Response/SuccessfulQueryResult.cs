using POC.CQRS.Shared.Commands;
using POC.CQRS.Shared.Queries;
using Newtonsoft.Json;

namespace POC.CQRS.Domain.Queries.Response
{
	public class SuccessfulQueryResult : IQueryResult
	{
		public SuccessfulQueryResult(object data)
		{
			this.Data = data;
		}

		[JsonIgnore]
		public StatusCodeResult StatusCode => StatusCodeResult.Success;
		public bool Success => true;
		public object Data { get; }
	}
}
