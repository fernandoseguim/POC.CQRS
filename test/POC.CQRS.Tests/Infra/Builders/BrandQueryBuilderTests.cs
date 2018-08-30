using System;
using System.Linq;
using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Entities;
using POC.CQRS.Infra.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace POC.CQRS.Tests.Infra.Builders
{
	[TestClass]
	public partial class BrandQueryBuilderTests : UnitTestBase
	{
		private readonly Brand brand;
		private readonly BrandCommand brandCommand;
		private readonly Guid id;

		public BrandQueryBuilderTests()
		{
			this.id = Guid.NewGuid();
			this.brandCommand = new BrandCommand {Name = MockString()};
			this.brand = new Brand(MockString());
		}

		[TestMethod]
		[Description("Given that I want check name from brand, " +
		             "when building a check name query, " +
		             "then should contains name into statement and parameters")]
		public void Should_contains_name_into_statement_when_building_a_check_brand_query()
		{
			var name = MockString();
			var (query, parameters) = new BrandQueryBuilder()
				.CheckBrand(name)
				.Build();

			Assert.IsTrue(query.Contains("Name") && query.Contains(@"@Name"));
			Assert.IsTrue(parameters.ParameterNames.Contains("Name"));
			Assert.AreEqual(name, parameters.Get<string>("Name"));
		}

		[TestMethod]
		[Description("Given that I want update a brand, " +
					 "when building a update brand query, " +
		             "then should contains brand identifier into statement and parameters")]
		public void Should_contains_brand_identifier_into_statement_and_parameters_when_building_a_update_brand_query()
		{
			var (query, parameters) = new BrandQueryBuilder()
				.UpdateBrand(this.id, this.brandCommand)
				.Build();

			Assert.IsTrue(query.Contains("BrandID") && query.Contains(@"@BrandID"));
			Assert.IsTrue(parameters.ParameterNames.Contains("BrandID"));
			Assert.AreEqual(this.id, parameters.Get<Guid>("BrandID"));
		}

		[TestMethod]
		[Description("Given that I want update a brand, " +
					 "when building a update brand query, " +
		             "then should contains name into statement and parameters")]
		public void Should_contains_name_into_statement_and_parameters_when_building_a_update_brand_query()
		{
			var (query, parameters) = new BrandQueryBuilder()
				.UpdateBrand(this.id, this.brandCommand)
				.Build();

			Assert.IsTrue(query.Contains("Name") && query.Contains(@"@Name"));
			Assert.IsTrue(parameters.ParameterNames.Contains("Name"));
			Assert.AreEqual(this.brandCommand.Name, parameters.Get<string>("Name"));
		}
	}
}
