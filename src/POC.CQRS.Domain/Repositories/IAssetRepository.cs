using System;
using System.Collections.Generic;
using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Entities;
using POC.CQRS.Domain.Queries.Response;

namespace POC.CQRS.Domain.Repositories
{
	public interface IAssetRepository : IEntityRepository<Asset>
	{
		IEnumerable<AssetQueryResult> GetAll();
		AssetQueryResult GetById(Guid assetId);
		IEnumerable<AssetQueryResult> GetAllByBrandId(Guid brandId);
		bool Update(Guid id, AssetCommand assetCommand);
	} 
}

