using POC.CQRS.Infra.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace POC.CQRS.Tests.Infra.Builders
{
	public partial class BrandQueryBuilderTests : UnitTestBase
	{
		[TestMethod]
		[Description("Given that I want save a brand, " +
					 "when building a create brand query, " +
					 "then should contains brand identifier into statement and parameters")]
		public void Should_contains_brand_identifier_into_statement_and_parameters_when_building_a_create_brand_query()
		{
			var (query, parameters) = new BrandQueryBuilder()
				.CreateBrand(this.brand)
				.Build();

			Assert.IsTrue(query.Contains("BrandID") && query.Contains(@"@BrandID"));
			Assert.IsTrue(parameters.ParameterNames.Contains("BrandID"));
			Assert.AreEqual(this.brand.Id, parameters.Get<Guid>("BrandID"));
		}

		[TestMethod]
		[Description("Given that I want save a brand, " +
					 "when building a create brand query, " +
					 "then should contains name into statement and parameters")]
		public void Should_contains_name_into_statement_and_parameters_when_building_a_create_brand_query()
		{
			var (query, parameters) = new BrandQueryBuilder()
				.CreateBrand(this.brand)
				.Build();

			Assert.IsTrue(query.Contains("Name") && query.Contains(@"@Name"));
			Assert.IsTrue(parameters.ParameterNames.Contains("Name"));
			Assert.AreEqual(this.brand.Name, parameters.Get<string>("Name"));
		}
	}
}
