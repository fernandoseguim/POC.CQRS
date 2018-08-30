using System;
using System.Linq;
using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace POC.CQRS.Tests.Domain.Commands
{
	[TestClass]
	public class AssetCommandTests : UnitTestBase
	{
		[TestMethod]
		[Description("Given that required properties are valid, " +
		             "when trying to create a new asset, " +
		             "then does not should return notifications")]
		public void Should_return_true_when_creating_a_new_asset_with_valid_requides_properties()
		{
			var assetCommand = new AssetCommand
			{
				Name = this.MockString(Asset.MINIMUM_NAME_SIZE),
				BrandId = Guid.NewGuid().ToString()
			};

			Assert.IsTrue(assetCommand.IsValid());
			Assert.IsFalse(assetCommand.Notifications.ToList().Any());
		}

		[TestMethod]
		[Description("Given that name is null, " +
		             "when trying to create a new asset, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_creating_a_new_asset_and_asset_name_is_null()
		{
			var assetCommand = new AssetCommand
			{
				BrandId = Guid.NewGuid().ToString()
			};

			Assert.IsFalse(assetCommand.IsValid());
			Assert.IsTrue(assetCommand.Notifications.ToList().Any(n => n.Property.Contains(nameof(assetCommand.Name))));
		}

		[TestMethod]
		[Description("Given that name length is lower than min name size, " +
		             "when trying to create a new asset, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_creating_a_new_asset_with_a_asset_name_lower_than_the_minimum_allowed_size()
		{
			var assetCommand = new AssetCommand
			{
				Name = this.MockString(Asset.MINIMUM_NAME_SIZE - 1),
				BrandId = Guid.NewGuid().ToString()
			};

			Assert.IsFalse(assetCommand.IsValid());
			Assert.IsTrue(assetCommand.Notifications.ToList().Any(n => n.Property.Contains(nameof(assetCommand.Name))));
		}

		[TestMethod]
		[Description("Given that name length is greater than min name size, " +
		             "when trying to create a new asset, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_creating_a_new_asset_with_a_asset_name_greater_than_the_maximum_allowed_size()
		{
			var assetCommand = new AssetCommand
			{
				Name = this.MockString(Asset.MAXIMUM_NAME_SIZE + 1),
				BrandId = Guid.NewGuid().ToString()
			};

			Assert.IsFalse(assetCommand.IsValid());
			Assert.IsTrue(assetCommand.Notifications.ToList().Any(n => n.Property.Contains(nameof(assetCommand.Name))));
		}

		[TestMethod]
		[Description("Given that brand identifier is not a valid Guid, " +
		             "when trying to create a new asset, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_creating_a_new_asset_with_a_invalid_brand_identifier()
		{
			var assetCommand = new AssetCommand
			{
				Name = this.MockString(Asset.MINIMUM_NAME_SIZE),
				BrandId = Guid.NewGuid().ToString().Substring(0, 9)
			};

			Assert.IsFalse(assetCommand.IsValid());
			Assert.IsTrue(assetCommand.Notifications.ToList().Any(n => n.Property.Contains(nameof(assetCommand.BrandId))));
		}		
	}
}
