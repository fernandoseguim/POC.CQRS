using Dapper;
using POC.CQRS.Domain.Entities;
using System;
using POC.CQRS.Domain.Commands.Request;

namespace POC.CQRS.Infra.Builders
{
	public class BrandQueryBuilder
	{
		private readonly DynamicParameters parameters;
		private string query = "";

		public BrandQueryBuilder() => this.parameters = new DynamicParameters();

		public BrandQueryBuilder CheckBrand(string name)
		{
			this.query = @"SELECT 1 FROM Brands WHERE Name = @Name";
			this.parameters.Add("Name", name);
			return this;
		}

		public BrandQueryBuilder CreateBrand(Brand brand)
		{
			this.query = @"INSERT INTO Brands (BrandID, Name) VALUES(@BrandID, @Name)";
			this.parameters.Add("BrandID", brand.Id);
			this.parameters.Add("Name", brand.Name);
			return this;
		}

		public BrandQueryBuilder UpdateBrand(Guid id, BrandCommand brandCommand)
		{
			this.query = @"UPDATE Brands SET Name = @Name WHERE BrandID = @BrandID";
			this.parameters.Add("BrandID", id);
			this.parameters.Add("Name", brandCommand.Name);
			return this;
		}

		public BrandQueryBuilder DeleteBrand(Guid brandId)
		{
			this.query = @"DELETE FROM Brands WHERE BrandID = @BrandID";
			this.parameters.Add("BrandID", brandId);
			return this;
		}

		public BrandQueryBuilder GetAll()
		{
			this.query = @"SELECT CAST(BrandID as VARCHAR(36)) as Id, Name FROM Brands";
			return this;
		}

		public BrandQueryBuilder GetById(Guid brandId)
		{
			this.query = @"SELECT CAST(BrandID as VARCHAR(36)) as Id, Name FROM Brands WHERE BrandID = @BrandID";
			this.parameters.Add("BrandID", brandId);
			return this;
		}
		
		public (string, DynamicParameters) Build() =>
			(this.query, this.parameters);
	}
}
