using System;
using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Commands.Response;
using POC.CQRS.Shared.Commands;
using Flunt.Notifications;

namespace POC.CQRS.Domain.Handlers
{
	public partial class AssetHandler : Notifiable, ICommandHandler<AssetCommand>
	{
		public ICommandResult Delete(string id)
		{
			try
			{
				if (Guid.TryParse(id, out var assetId))
				{
					var removed = this.assetRepository.Delete(assetId);
					if (removed)
					{
						return new SuccessfulCommandResult($"The asset {assetId} was removed with successful", null);
					}

					return new UnsuccessfulCommandResult(StatusCodeResult.NotFound,
						$"The asset {assetId} not found", null);
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BadRequest,
					"The asset identifier should be a valid GUID", null);
			}
			catch (Exception ex)
			{
				return new UnsuccessfulCommandResult(StatusCodeResult.InternalServerError,
					"There was an error saving the user, please contact your system administrator.",
					ex.Message);
			}
		}
	}
}
