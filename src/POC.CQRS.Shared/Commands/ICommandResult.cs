namespace POC.CQRS.Shared.Commands
{
	public interface ICommandResult
	{
		StatusCodeResult StatusCode { get; }
		bool Success { get; }
		string Message { get; }
		object Data { get; }
	}
}
