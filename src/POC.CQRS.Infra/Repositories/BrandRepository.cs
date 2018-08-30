using Dapper;
using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Entities;
using POC.CQRS.Domain.Queries.Response;
using POC.CQRS.Domain.Repositories;
using POC.CQRS.Infra.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POC.CQRS.Infra.Repositories
{
	public class BrandRepository : IBrandRepository
	{
		private readonly IDatabaseContext context;
		public BrandRepository(IDatabaseContext context) => this.context = context;

		public IEnumerable<BrandQueryResult> GetAll()
		{
			var (query, parameters) = new BrandQueryBuilder().GetAll().Build();

			var brands = this.context.Connection.Query<BrandQueryResult>(query);

			return brands;
		}

		public BrandQueryResult GetById(Guid brandId)
		{
			var (query, parameters) = new BrandQueryBuilder().GetById(brandId).Build();

			var brand = this.context.Connection.QueryFirstOrDefault<BrandQueryResult>(query, parameters);

			return brand;
		}

		public bool CheckBrand(string name)
		{
			var (query, parameters) = new BrandQueryBuilder().CheckBrand(name).Build();

			var result = this.context.Connection.Query<bool>(query, parameters).FirstOrDefault();
			return result;
		}

		public bool Update(Guid id, BrandCommand brandCommand)
		{
			var (query, parameters) = new BrandQueryBuilder().UpdateBrand(id, brandCommand).Build();

			var result = this.context.Connection.Execute(query, parameters);
			return Convert.ToBoolean(result);
		}

		public void Save(Brand brand)
		{
			var (query, parameters) = new BrandQueryBuilder().CreateBrand(brand).Build();

			this.context.Connection.Execute(query, parameters);
		}

		public bool Delete(Guid brandId)
		{
			var (query, parameters) = new BrandQueryBuilder().DeleteBrand(brandId).Build();

			var result = this.context.Connection.Execute(query, parameters);
			return Convert.ToBoolean(result);
		}
	}
}
