using System;
using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Entities;
using POC.CQRS.Domain.Queries.Response;

namespace POC.CQRS.Domain.Factories
{
	public class BrandFactory
	{
		public static Brand Create(BrandCommand command) => new Brand(command.Name);
		public static Brand Create(BrandQueryResult queryResult) => new Brand(new Guid(queryResult.Id), queryResult.Name);
	}
}