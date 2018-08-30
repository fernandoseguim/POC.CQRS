using System;
using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Commands.Response;
using POC.CQRS.Shared.Commands;
using Flunt.Notifications;

namespace POC.CQRS.Domain.Handlers
{
	public partial class UserHandler : Notifiable, ICommandHandler<UserCommand>
	{
		public ICommandResult Delete(string id)
		{
			try
			{
				if(Guid.TryParse(id, out var userId))
				{
					var removed = this.repository.Delete(userId);
					if(removed)
					{
						return new SuccessfulCommandResult($"The user {userId} was removed with successful", null);
					}

					return new UnsuccessfulCommandResult(StatusCodeResult.NotFound,
						$"The user {userId} not found", null);
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BadRequest, 
					"The user identifier should be a valid GUID", null);
			}
			catch(Exception ex)
			{
				return new UnsuccessfulCommandResult(StatusCodeResult.InternalServerError,
					"There was an error saving the user, please contact your system administrator.",
					ex.Message);
			}
		}
	}
}
