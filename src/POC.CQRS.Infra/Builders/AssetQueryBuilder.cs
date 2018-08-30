using Dapper;
using POC.CQRS.Domain.Entities;
using System;
using POC.CQRS.Domain.Commands.Request;

namespace POC.CQRS.Infra.Builders
{
	public class AssetQueryBuilder
	{
		private readonly DynamicParameters parameters;
		private string query = "";

		public AssetQueryBuilder() => this.parameters = new DynamicParameters();

		public AssetQueryBuilder CreateAsset(Asset asset)
		{
			this.query = @"INSERT INTO Assets (AssetID, Name, Description, Registry, BrandID) 
						VALUES (@AssetID, @Name, @Description, @Registry, @BrandID)";
			this.parameters.Add("AssetID", asset.Id);
			this.parameters.Add("Name", asset.Name);
			this.parameters.Add("Description", asset.Description);
			this.parameters.Add("Registry", asset.Registry);
			this.parameters.Add("BrandID", asset.Brand.Id);
			return this;
		}

		public AssetQueryBuilder UpdateAsset(Guid id, AssetCommand assetCommand)
		{
			this.query = @"UPDATE Assets SET Name = @Name, Description = @Description, BrandID = @BrandID 
						WHERE AssetID = @AssetID";
			this.parameters.Add("AssetID", id);
			this.parameters.Add("Name", assetCommand.Name);
			this.parameters.Add("Description", assetCommand.Description);
			this.parameters.Add("BrandID", assetCommand.BrandId);
			return this;
		}

		public AssetQueryBuilder DeleteAsset(Guid assetId)
		{
			this.query = @"DELETE FROM Assets WHERE AssetID = @AssetID";
			this.parameters.Add("AssetID", assetId);
			return this;
		}

		public AssetQueryBuilder GetAll()
		{
			this.query = @"SELECT CAST(AssetID as VARCHAR(36)) as Id, Name, Description, Registry, CAST(BrandID as VARCHAR(36)) as BrandID FROM Assets";
			return this;
		}

		public AssetQueryBuilder GetById(Guid assetId)
		{
			this.query = @"SELECT CAST(AssetID as VARCHAR(36)) as Id, Name, Description, Registry, CAST(BrandID as VARCHAR(36)) as BrandID
						FROM Assets WHERE AssetID = @AssetID";
			this.parameters.Add("AssetID", assetId);
			return this;
		}

		public AssetQueryBuilder GetAllByBrandId(Guid brandId)
		{
			this.query = @"SELECT CAST(AssetID as VARCHAR(36)) as Id, Name, Description, Registry FROM Assets WHERE BrandID = @BrandID";
			this.parameters.Add("BrandID", brandId);
			return this;
		}

		public (string, DynamicParameters) Build() =>
			(this.query, this.parameters);
	}
}
