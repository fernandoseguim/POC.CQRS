using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Shared.Commands;
using Flunt.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POC.CQRS.Tests.Domain.Handlers
{
	public partial class BrandHandlerTests : UnitTestBase
	{
		[TestMethod]
		[Description("Given that I trying update brand, " +
					 "when brand name already exists" +
					 "then should contains conflict status code in command result")]
		public void Should_contains_conflict_status_code_in_command_result_when_brand_name_already_exists_in_database()
		{
			var commandResult = this.brandHandler.Update(Guid.NewGuid().ToString(), this.brandCommand);
			Assert.AreEqual(StatusCodeResult.Conflict, commandResult.StatusCode);
		}

		[TestMethod]
		[Description("Given that I trying update brand, " +
					 "when brand name already exists" +
					 "then should contains notification in command result")]
		public void Should_contains_notification_in_command_result_when_brand_name_already_exists_in_database()
		{
			var commandResult = this.brandHandler.Update(Guid.NewGuid().ToString(), this.brandCommand);

			var notifications = (List<Notification>)commandResult.Data;

			Assert.IsTrue(notifications.First().Property.Equals("Name"));
		}

		[TestMethod]
		[Description("Given that I trying update brand, " +
		             "when brand not found " +
		             "then should contains conflict status code in command result")]
		public void Should_contains_not_found_status_code_in_command_result_when_brand_name_already_exists_in_database()
		{
			var commandResult = this.brandHandler.Update(NOT_FOUND_BRAND_ID, new BrandCommand{ Name = MockString() });
			Assert.AreEqual(StatusCodeResult.NotFound, commandResult.StatusCode);
		}

		[TestMethod]
		[Description("Given that I trying update brand, " +
		             "when brand name already exists" +
		             "then should contains notification in command result")]
		public void Should_contains_notification_in_command_result_when_brand_not_found_in_database()
		{
			var commandResult = this.brandHandler.Update(NOT_FOUND_BRAND_ID, new BrandCommand { Name = MockString() });

			var notifications = (List<Notification>)commandResult.Data;

			Assert.IsTrue(notifications.First().Property.Equals("Id"));
		}

		[TestMethod]
		[Description("Given that brand command is invalid, " +
					 "when trying update a brand" +
					 "then should contains bad request status code in command result")]
		public void Should_contains_bad_request_status_code_in_command_result_when_trying_update_brand_with_comand_is_invalid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand());

			Assert.AreEqual(StatusCodeResult.BadRequest, commandResult.StatusCode);
		}

		[TestMethod]
		[Description("Given that brand command is invalid, " +
					 "when trying update a brand" +
					 "then should contains noticiations in command result")]
		public void Should_contains_notifications_command_result_trying_update_brand_with_comand_is_invalid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand());

			var notifications = (List<Notification>)commandResult.Data;

			Assert.IsTrue(notifications.Any());
		}

		[TestMethod]
		[Description("Given that brand command is valid and name not exists, " +
					 "when updating a brand on database" +
					 "then should return successful command result")]
		public void Should_contains_success_status_code_in_command_result_when_trying_update_user_with_comand_is_valid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand() { Name = MockString() });

			Assert.AreEqual(StatusCodeResult.Success, commandResult.StatusCode);
		}
	}
}
