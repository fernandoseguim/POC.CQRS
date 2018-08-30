using POC.CQRS.Infra.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace POC.CQRS.Tests.Infra.Builders
{
	public partial class UserQueryBuilderTests
	{
		[TestMethod]
		[Description("Given that I want save a user, " +
		             "when building a create user query, " +
		             "then should contains user identifier into statement and parameters")]
		public void Should_contains_user_identifier_into_statement_and_parameters_when_building_a_create_user_query()
		{
			var (query, parameters) = new UserQueryBuilder()
				.CreateUser(this.user)
				.Build();

			Assert.IsTrue(query.Contains("UserID") && query.Contains(@"@UserID"));
			Assert.IsTrue(parameters.ParameterNames.Contains("UserID"));
			Assert.AreEqual(this.user.Id, parameters.Get<Guid>("UserID"));
		}

		[TestMethod]
		[Description("Given that I want save a user, " +
		             "when building a create user query, " +
		             "then should contains name into statement and parameters")]
		public void Should_contains_name_into_statement_and_parameters_when_building_a_create_user_query()
		{
			var (query, parameters) = new UserQueryBuilder()
				.CreateUser(this.user)
				.Build();

			Assert.IsTrue(query.Contains("Name") && query.Contains(@"@Name"));
			Assert.IsTrue(parameters.ParameterNames.Contains("Name"));
			Assert.AreEqual(this.user.Name.ToString(), parameters.Get<string>("Name"));
		}

		[TestMethod]
		[Description("Given that I want save a user, " +
		             "when building a create user query, " +
		             "then should contains email into statement and parameters")]
		public void Should_contains_email_into_statement_and_parameters_when_building_a_create_user_query()
		{
			var (query, parameters) = new UserQueryBuilder()
				.CreateUser(this.user)
				.Build();

			Assert.IsTrue(query.Contains("Email") && query.Contains(@"@Email"));
			Assert.IsTrue(parameters.ParameterNames.Contains("Email"));
			Assert.AreEqual(this.user.Email.ToString(), parameters.Get<string>("Email"));
		}

		[TestMethod]
		[Description("Given that I want save a user, " +
		             "when building a create user query, " +
		             "then should contains password hash into statement and parameters")]
		public void Should_contains_password_hash_into_statement_and_parameters_when_building_a_create_user_query()
		{
			var (query, parameters) = new UserQueryBuilder()
				.CreateUser(this.user)
				.Build();

			Assert.IsTrue(query.Contains("PasswordHash") && query.Contains(@"@PasswordHash"));
			Assert.IsTrue(parameters.ParameterNames.Contains("PasswordHash"));
			Assert.AreEqual(this.user.Password.HashValue, parameters.Get<string>("PasswordHash"));
		}

		[TestMethod]
		[Description("Given that I want save a user, " +
		             "when building a create user query, " +
		             "then should contains salt into statement and parameters")]
		public void Should_contains_salt_into_statement_and_parameters_when_building_a_create_user_query()
		{
			var (query, parameters) = new UserQueryBuilder()
				.CreateUser(this.user)
				.Build();

			Assert.IsTrue(query.Contains("Salt") && query.Contains(@"@Salt"));
			Assert.IsTrue(parameters.ParameterNames.Contains("Salt"));
			Assert.AreEqual(this.user.Password.Salt, parameters.Get<string>("Salt"));
		}
	}
}
