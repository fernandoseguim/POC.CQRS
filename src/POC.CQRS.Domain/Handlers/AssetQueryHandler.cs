using POC.CQRS.Shared.Queries;
using System;
using POC.CQRS.Domain.Queries.Response;
using POC.CQRS.Domain.Repositories;
using POC.CQRS.Shared.Commands;

namespace POC.CQRS.Domain.Handlers
{
	public class AssetQueryHandler : IAssetQueryHandler
	{
		private readonly IAssetRepository repository;

		public AssetQueryHandler(IAssetRepository repository) => this.repository = repository;


		public IQueryResult GetAll()
		{
			try
			{
				var result = this.repository.GetAll();

				return new SuccessfulQueryResult(result);
			}
			catch (Exception ex)
			{
				return new UnsuccessfulQueryResult(StatusCodeResult.InternalServerError, ex.Message);
			}
		}

		public IQueryResult GetById(string id)
		{
			try
			{
				if (Guid.TryParse(id, out var assetId))
				{
					var asset = this.repository.GetById(assetId);
					if (asset is null)
					{
						return new UnsuccessfulQueryResult(StatusCodeResult.NotFound,
							$"The asset {assetId} not found");
					}

					return new SuccessfulQueryResult(asset);
				}

				return new UnsuccessfulQueryResult(StatusCodeResult.BadRequest,
					"The asset identifier should be a valid GUID");
			}
			catch (Exception ex)
			{
				return new UnsuccessfulQueryResult(StatusCodeResult.InternalServerError,
					"There was an error saving the user, please contact your system administrator.");
			}
		}

		public IQueryResult GetAllByBrandId(string id)
		{
			try
			{
				if (Guid.TryParse(id, out var brandId))
				{
					var brand = this.repository.GetAllByBrandId(brandId);
					if (brand is null)
					{
						return new UnsuccessfulQueryResult(StatusCodeResult.NotFound,
							$"The brand {brandId} not found");
					}

					return new SuccessfulQueryResult(brand);
				}

				return new UnsuccessfulQueryResult(StatusCodeResult.BadRequest,
					"The brand identifier should be a valid GUID");
			}
			catch (Exception ex)
			{
				return new UnsuccessfulQueryResult(StatusCodeResult.InternalServerError,
					"There was an error saving the user, please contact your system administrator.");
			}
		}
	}
}
