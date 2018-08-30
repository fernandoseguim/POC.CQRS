using System;
using System.Linq;
using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace POC.CQRS.Tests.Domain.Commands
{
	[TestClass]
	public class BrandCommandTests : UnitTestBase
	{
		[TestMethod]
		[Description("Given that name is null, " +
		             "when trying to create a new brand, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_creating_a_new_brand_and_brand_name_is_null()
		{
			var brandCommand = new BrandCommand();

			Assert.IsFalse(brandCommand.IsValid());
			Assert.IsTrue(brandCommand.Notifications.ToList().Any(n => n.Property.Contains(nameof(brandCommand.Name))));
		}

		[TestMethod]
		[Description("Given that name length is lower than min name size, " +
		             "when trying to create a new brand, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_creating_a_new_brand_with_a_brand_name_lower_than_the_minimum_allowed_size()
		{
			var brandCommand = new BrandCommand { Name = this.MockString(Brand.MINIMUM_NAME_SIZE - 1) };

			Assert.IsFalse(brandCommand.IsValid());
			Assert.IsTrue(brandCommand.Notifications.ToList().Any(n => n.Property.Contains(nameof(brandCommand.Name))));
		}

		[TestMethod]
		[Description("Given that name length is greater than max name size, " +
		             "when trying to create a new brand, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_creating_a_new_brand_with_a_brand_name_greater_than_the_maximum_allowed_size()
		{
			var brandCommand = new BrandCommand { Name = this.MockString(Brand.MAXIMUM_NAME_SIZE + 1) };

			Assert.IsFalse(brandCommand.IsValid());
			Assert.IsTrue(brandCommand.Notifications.ToList().Any(n => n.Property.Contains(nameof(brandCommand.Name))));
		}

		[TestMethod]
		[Description("Given that name length is between min and max size, " +
		             "when trying to create a new brand, " +
		             "then does not should return notifications")]
		public void Should_return_true_when_creating_a_new_brand_with_a_brand_name_between_min_and_max_size()
		{
			var brandNameSize = new Random().Next(Brand.MINIMUM_NAME_SIZE, Brand.MAXIMUM_NAME_SIZE);
			var brandCommand = new BrandCommand { Name = this.MockString(brandNameSize)};

			Assert.IsTrue(brandCommand.IsValid());
			Assert.IsFalse(brandCommand.Notifications.ToList().Any());
		}
	}
}
