using System;
using POC.CQRS.Domain.Entities;
using POC.CQRS.Domain.Queries.Response;
using System.Collections.Generic;
using POC.CQRS.Domain.Commands.Request;

namespace POC.CQRS.Domain.Repositories
{
	public interface IBrandRepository : IEntityRepository<Brand>
	{
		IEnumerable<BrandQueryResult> GetAll();
		BrandQueryResult GetById(Guid brandId);
		bool CheckBrand(string name);
		bool Update(Guid id, BrandCommand brandCommand);
	}
}
