using POC.CQRS.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace POC.CQRS.Tests.Domain.Entities
{
	[TestClass]
	public class AssetTests : UnitTestBase
	{
		private readonly Brand brand;
		public AssetTests() => 
			this.brand = new Brand(base.MockString());

		[TestMethod]
		[Description("Given that name is null, " +
		             "when trying to create a new asset, " +
		             "then should throw argument null exception")]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_asset_with_null_name()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Asset(null, this.brand));
		}

		[TestMethod]
		[Description("Given that name is withspace, " +
		             "when trying to create a new asset, " +
		             "then should throw argument null exception")]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_asset_with_withspace_name()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Asset("", this.brand));
		}

		[TestMethod]
		[Description("Given that brand is null, " +
		             "when trying to create a new asset, " +
		             "then should throw argument null exception")]
		public void Should_generate_a_register_number_when_trying_to_create_a_new_asset_with_null_brand()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Asset(base.MockString(), null));
		}

		[TestMethod]
		[Description("Given that name is valid, " +
		             "when trying to create a new asset, " +
		             "then should generate a registry number")]
		public void Should_generate_a_register_number_when_trying_to_create_a_new_asset_with_valid_name()
		{
			var asset = new Asset(base.MockString(), this.brand);

			Assert.IsNotNull(asset.Registry);
		}

		[TestMethod]
		[Description("Given that description length is lower or equal to max allowed size, " +
		             "when trying to add description to asset," +
		             "then should add description")]
		public void Should_add_description_when_description_length_is_lower_or_equal_to_the_maximum_allowed_size()
		{
			var asset = new Asset(base.MockString(), this.brand);
			var description = this.MockString(10);

			asset.AddDescription(description);

			Assert.AreEqual(description, asset.Description);
		}

		[TestMethod]
		[Description("Given that description length is greater than max allowed size, " +
		             "when trying to add description to asset, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_add_description_with_length_greater_than_the_maximum_allowed_size()
		{
			var asset = new Asset(base.MockString(), this.brand);
			var description = this.MockString(Asset.MAXIMUM_DESCRIPTION_SIZE + 1);

			asset.AddDescription(description);

			Assert.IsFalse(asset.Valid);
			Assert.IsTrue(asset.Notifications.ToList().Any(n => n.Property.Contains(nameof(asset.Description))));
		}
	}
}
