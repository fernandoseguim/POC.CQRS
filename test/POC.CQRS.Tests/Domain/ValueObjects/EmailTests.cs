using POC.CQRS.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace POC.CQRS.Tests.Domain.ValueObjects
{
	[TestClass]
	public class EmailTests : UnitTestBase
	{
		[TestMethod]
		[Description("Given that email address is invalid, " +
		             "when trying to create a new email, " +
		             "then should throw argument exception")]
		public void Should_throw_argument_exception_when_trying_to_create_a_new_email_with_invalid_email_address()
		{
			Assert.ThrowsException<ArgumentException>(() => new Email(this.MockString()));
		}

		[TestMethod]
		[Description("Given that email address is valid, " +
		             "when converting email to string, " +
		             "then should return the Email address")]
		public void Should_return_the_email_address_when_converting_email_to_string()
		{
			const string EMAIL_ADDRESS = "fernando.seguim@gmail.com";
			var email = new Email(EMAIL_ADDRESS);
			Assert.AreEqual(EMAIL_ADDRESS, email.ToString());
		}
	}
}
