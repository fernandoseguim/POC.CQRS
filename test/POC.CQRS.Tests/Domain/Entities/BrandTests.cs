using POC.CQRS.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace POC.CQRS.Tests.Domain.Entities
{
	[TestClass]
	public class BrandTests : UnitTestBase
	{
		[TestMethod]
		[Description("Given that name is null, " +
		             "when trying to create a new brand, " +
		             "then should throw argument null exception")]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_brand_with_null_name()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Brand(null));
		}

		[TestMethod]
		[Description("Given that name is withspace, " +
		             "when trying to create a new brand, " +
		             "then should throw argument null exception")]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_brand_with_withspace_name()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Brand(""));
		}
	}
}
