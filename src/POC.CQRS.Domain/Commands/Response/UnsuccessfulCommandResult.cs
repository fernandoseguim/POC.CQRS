using POC.CQRS.Shared.Commands;
using Newtonsoft.Json;

namespace POC.CQRS.Domain.Commands.Response
{
	public class UnsuccessfulCommandResult : ICommandResult
	{
		public UnsuccessfulCommandResult(StatusCodeResult statusCode, string message, object data)
		{
			this.StatusCode = statusCode;
			this.Success = false;
			this.Message = message;
			this.Data = data;
			this.StatusCode = statusCode;
		}

		[JsonIgnore]
		public StatusCodeResult StatusCode { get; }
		public bool Success { get; }
		public string Message { get; }
		public object Data { get; }
	}
}
