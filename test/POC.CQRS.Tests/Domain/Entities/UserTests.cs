using System;
using POC.CQRS.Domain.Entities;
using POC.CQRS.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace POC.CQRS.Tests.Domain.Entities
{
	[TestClass]
	public class UserTests : UnitTestBase
	{
		private readonly Name name;
		private readonly Email email;
		private readonly SecurityPassword password;

		public UserTests()
		{
			this.name = new Name("Fernando", "Seguim");
			this.email = new Email("fernando.seguim@gmail.com");
			this.password = new SecurityPassword(this.MockString());
		}

		[TestMethod]
		[Description("Given that name is null, " +
		             "when trying to create a new user, " +
		             "then should throw argument null exception")]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_user_with_null_name()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new User(null, this.email, this.password));
		}

		[TestMethod]
		[Description("Given that email is null, " +
		             "when trying to create a new user, " +
		             "then should throw argument null exception")]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_user_with_null_email()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new User(this.name, null, this.password));
		}

		[TestMethod]
		[Description("Given that email is null, " +
		             "when trying to create a new user, " +
		             "then should throw argument null exception")]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_user_with_null_password()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new User(this.name, null, this.password));
		}
	}
}
