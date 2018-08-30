using System.Linq;
using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace POC.CQRS.Tests.Domain.Commands
{
	[TestClass]
	public class UserCommandTests : UnitTestBase
	{
		[TestMethod]
		[Description("Given that first name is null, " +
		             "when trying to create a new brand, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_creating_a_new_user_and_first_name_is_null()
		{
			var userCommand = new UserCommand();

			Assert.IsFalse(userCommand.IsValid());
			Assert.IsTrue(userCommand.Notifications.ToList().Any(n => n.Property.Contains(nameof(userCommand.FirstName))));
		}

		[TestMethod]
		[Description("Given that last name is null, " +
		             "when trying to create a new brand, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_creating_a_new_user_and_last_name_is_null()
		{
			var userCommand = new UserCommand();

			Assert.IsFalse(userCommand.IsValid());
			Assert.IsTrue(userCommand.Notifications.ToList().Any(n => n.Property.Contains(nameof(userCommand.LastName))));
		}

		[TestMethod]
		[Description("Given that first name is lower than min allowed size, " +
		             "when trying to create a new brand, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_creating_a_new_user_and_first_name_is_lower_than_min_allowed_size()
		{
			var userCommand = new UserCommand {FirstName = this.MockString(Name.MINIMUM_FIRST_NAME_SIZE - 1)};

			Assert.IsFalse(userCommand.IsValid());
			Assert.IsTrue(userCommand.Notifications.ToList().Any(n => n.Property.Contains(nameof(userCommand.FirstName))));
		}

		[TestMethod]
		[Description("Given that last name is lower than min allowed size, " +
		             "when trying to create a new brand, " +
		             "then should return a notification")]
		public void Should_return_a_notification_when_creating_a_new_user_and_last_name_is_lower_than_min_allowed_size()
		{
			var userCommand = new UserCommand { LastName = this.MockString(Name.MINIMUM_LAST_NAME_SIZE - 1) };

			Assert.IsFalse(userCommand.IsValid());
			Assert.IsTrue(userCommand.Notifications.ToList().Any(n => n.Property.Contains(nameof(userCommand.LastName))));
		}
	}
}
