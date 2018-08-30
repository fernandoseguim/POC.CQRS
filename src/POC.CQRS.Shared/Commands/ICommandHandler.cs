namespace POC.CQRS.Shared.Commands
{
	public interface ICommandHandler<in TCommand> where TCommand : BaseCommand
	{
		ICommandResult Create(TCommand command);
		ICommandResult Update(string id, TCommand command);
		ICommandResult Delete(string id);
	}
}
