using System;
using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Commands.Response;
using POC.CQRS.Shared.Commands;
using Flunt.Notifications;

namespace POC.CQRS.Domain.Handlers
{
	public partial class BrandHandler : Notifiable, ICommandHandler<BrandCommand>
	{
		public ICommandResult Delete(string id)
		{
			try
			{
				if (Guid.TryParse(id, out var brandId))
				{
					var removed = this.repository.Delete(brandId);
					if (removed)
					{
						return new SuccessfulCommandResult($"The brand {brandId} was removed with successful", null);
					}

					return new UnsuccessfulCommandResult(StatusCodeResult.NotFound,
						$"The brand {brandId} not found", null);
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BadRequest,
					"The brand identifier should be a valid GUID", null);
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
