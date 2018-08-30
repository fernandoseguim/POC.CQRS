using POC.CQRS.Infra.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace POC.CQRS.Tests.Infra.Builders
{
	public partial class AssetsQueryBuilderTests : UnitTestBase
	{
		[TestMethod]
		[Description("Given that I want save a asset, " +
		             "when building a create asset query, " +
		             "then should contains asset identifier into statement and parameters")]
		public void Should_contains_asset_identifier_into_statement_and_parameters_when_building_a_create_asset_query()
		{
			var (query, parameters) = new AssetQueryBuilder()
				.CreateAsset(this.asset)
				.Build();

			Assert.IsTrue(query.Contains("AssetID") && query.Contains(@"@AssetID"));
			Assert.IsTrue(parameters.ParameterNames.Contains("AssetID"));
			Assert.AreEqual(this.asset.Id, parameters.Get<Guid>("AssetID"));
		}

		[TestMethod]
		[Description("Given that I want save a asset, " +
		             "when building a create asset query, " +
		             "then should contains name into statement and parameters")]
		public void Should_contains_name_into_statement_and_parameters_when_building_a_create_asset_query()
		{
			var (query, parameters) = new AssetQueryBuilder()
				.CreateAsset(this.asset)
				.Build();

			Assert.IsTrue(query.Contains("Name") && query.Contains(@"@Name"));
			Assert.IsTrue(parameters.ParameterNames.Contains("Name"));
			Assert.AreEqual(this.asset.Name, parameters.Get<string>("Name"));
		}

		[TestMethod]
		[Description("Given that I want save a asset, " +
		             "when building a create asset query, " +
					 "then should contains description into statement and parameters")]
		public void Should_contains_description_into_statement_and_parameters_when_building_a_create_asset_query()
		{
			var (query, parameters) = new AssetQueryBuilder()
				.CreateAsset(this.asset)
				.Build();

			Assert.IsTrue(query.Contains("Description") && query.Contains(@"@Description"));
			Assert.IsTrue(parameters.ParameterNames.Contains("Description"));
			Assert.AreEqual(this.asset.Description, parameters.Get<string>("Description"));
		}

		[TestMethod]
		[Description("Given that I want save a asset, " +
		             "when building a create asset query, " +
		             "then should contains registry into statement and parameters")]
		public void Should_contains_registry_into_statement_and_parameters_when_building_a_create_asset_query()
		{
			var (query, parameters) = new AssetQueryBuilder()
				.CreateAsset(this.asset)
				.Build();

			Assert.IsTrue(query.Contains("Registry") && query.Contains(@"@Registry"));
			Assert.IsTrue(parameters.ParameterNames.Contains("Registry"));
			Assert.AreEqual(this.asset.Registry, parameters.Get<string>("Registry"));
		}

		[TestMethod]
		[Description("Given that I want save a asset, " +
		             "when building a create asset query, " +
		             "then should contains brand identifier into statement and parameters")]
		public void Should_contains_brand_identifier_into_statement_and_parameters_when_building_a_create_asset_query()
		{
			var (query, parameters) = new AssetQueryBuilder()
				.CreateAsset(this.asset)
				.Build();

			Assert.IsTrue(query.Contains("BrandID") && query.Contains(@"@BrandID"));
			Assert.IsTrue(parameters.ParameterNames.Contains("BrandID"));
			Assert.AreEqual(this.asset.Brand.Id, parameters.Get<Guid>("BrandID"));
		}
	}
}
