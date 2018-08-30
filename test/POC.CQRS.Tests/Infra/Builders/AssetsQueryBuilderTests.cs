using System;
using System.Linq;
using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Entities;
using POC.CQRS.Infra.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace POC.CQRS.Tests.Infra.Builders
{
	[TestClass]
	public partial class AssetsQueryBuilderTests : UnitTestBase
	{
		private readonly Asset asset;
		private readonly Brand brand;
		private readonly AssetCommand assetCommand;
		private readonly Guid id;

		public AssetsQueryBuilderTests()
		{
			this.id = Guid.NewGuid();
			this.assetCommand = new AssetCommand { Name = MockString(), Description = MockString(), BrandId = Guid.NewGuid().ToString() };
			this.brand = new Brand(MockString());
			this.asset = new Asset(MockString(), this.brand);
		}

		[TestMethod]
		[Description("Given that I want update a asset, " +
					 "when building a update asset query, " +
					 "then should contains asset identifier into statement and parameters")]
		public void Should_contains_asset_identifier_into_statement_and_parameters_when_building_a_update_asset_query()
		{
			var (query, parameters) = new AssetQueryBuilder()
				.UpdateAsset(this.id, this.assetCommand)
				.Build();

			Assert.IsTrue(query.Contains("AssetID") && query.Contains(@"@AssetID"));
			Assert.IsTrue(parameters.ParameterNames.Contains("AssetID"));
			Assert.AreEqual(this.id, parameters.Get<Guid>("AssetID"));
		}

		[TestMethod]
		[Description("Given that I want update a asset, " +
					 "when building a update asset query, " +
					 "then should contains name into statement and parameters")]
		public void Should_contains_name_into_statement_and_parameters_when_building_a_update_asset_query()
		{
			var (query, parameters) = new AssetQueryBuilder()
				.UpdateAsset(this.id, this.assetCommand)
				.Build();

			Assert.IsTrue(query.Contains("Name") && query.Contains(@"@Name"));
			Assert.IsTrue(parameters.ParameterNames.Contains("Name"));
			Assert.AreEqual(this.assetCommand.Name, parameters.Get<string>("Name"));
		}

		[TestMethod]
		[Description("Given that I want update a asset, " +
		             "when building a update asset query, " +
		             "then should contains description into statement and parameters")]
		public void Should_contains_description_into_statement_and_parameters_when_building_a_update_asset_query()
		{
			var (query, parameters) = new AssetQueryBuilder()
				.UpdateAsset(this.id, this.assetCommand)
				.Build();

			Assert.IsTrue(query.Contains("Description") && query.Contains(@"@Description"));
			Assert.IsTrue(parameters.ParameterNames.Contains("Description"));
			Assert.AreEqual(this.assetCommand.Description, parameters.Get<string>("Description"));
		}

		[TestMethod]
		[Description("Given that I want update a asset, " +
		             "when building a update asset query, " +
		             "then should contains brand idenfier into statement and parameters")]
		public void Should_contains_brand_idenfier_into_statement_and_parameters_when_building_a_update_asset_query()
		{
			var (query, parameters) = new AssetQueryBuilder()
				.UpdateAsset(this.id, this.assetCommand)
				.Build();

			Assert.IsTrue(query.Contains("BrandID") && query.Contains(@"@BrandID"));
			Assert.IsTrue(parameters.ParameterNames.Contains("BrandID"));
			Assert.AreEqual(this.assetCommand.BrandId, parameters.Get<string>("BrandID"));
		}

		[TestMethod]
		[Description("Given that I want delete a asset, " +
		             "when building a delete asset query, " +
		             "then should contains asset identifier into statement and parameters")]
		public void Should_contains_asset_identifier_into_statement_and_parameters_when_building_a_delete_asset_query()
		{
			var assetId = Guid.NewGuid();
			var (query, parameters) = new AssetQueryBuilder()
				.DeleteAsset(assetId)
				.Build();

			Assert.IsTrue(query.Contains("AssetID") && query.Contains(@"@AssetID"));
			Assert.IsTrue(parameters.ParameterNames.Contains("AssetID"));
			Assert.AreEqual(assetId, parameters.Get<Guid>("AssetID"));
		}
	}
}
