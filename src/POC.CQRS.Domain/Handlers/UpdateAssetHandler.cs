using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Commands.Response;
using POC.CQRS.Shared.Commands;
using Flunt.Notifications;
using System;

namespace POC.CQRS.Domain.Handlers
{
	public partial class AssetHandler : Notifiable, ICommandHandler<AssetCommand>
	{
		public ICommandResult Update(string id, AssetCommand command)
		{
			try
			{
				if (command.IsValid())
				{
					var brandResult = this.brandRepository.GetById(new Guid(command.BrandId));

					if (brandResult is null)
					{
						AddNotification("BrandID", "Brand not found");
						return new UnsuccessfulCommandResult(StatusCodeResult.NotFound,
							"Please, check the properties before creating the user", this.Notifications);
					}

					var updated = this.assetRepository.Update(new Guid(id), command);

					if (updated)
					{
						return new SuccessfulCommandResult("Asset was updated with successful",
							new { UserId = id, Name = command.Name, Description = command.Description });
					}

					AddNotification("Id", $"The brand {id} not found");
					return new UnsuccessfulCommandResult(StatusCodeResult.NotFound,
						"Please, check the properties before updating the asset", this.Notifications);
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BadRequest,
					"Please, check the properties before updating the asset", command.Notifications);
			}
			catch (Exception ex)
			{
				return new UnsuccessfulCommandResult(StatusCodeResult.InternalServerError,
					"There was an error saving the user, please contact your system administrator.", ex.Message);
			}
		}
	}
}
