using POC.CQRS.Infra.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace POC.CQRS.Tests.Infra.Builders
{
	public partial class UserQueryBuilderTests
	{
		[TestMethod]
		[Description("Given that I want delete a user, " +
		             "when building a delete user query, " +
		             "then should contains user identifier into statement and parameters")]
		public void Should_contains_user_identifier_into_statement_and_parameters_when_building_a_delete_user_query()
		{
			var (query, parameters) = new UserQueryBuilder()
				.DeleteUser(this.user.Id)
				.Build();

			Assert.IsTrue(query.Contains("UserID") && query.Contains(@"@UserID"));
			Assert.IsTrue(parameters.ParameterNames.Contains("UserID"));
			Assert.AreEqual(this.user.Id, parameters.Get<Guid>("UserID"));
		}
	}
}
