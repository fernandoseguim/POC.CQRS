using POC.CQRS.Shared.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace POC.CQRS.Tests.Domain.Handlers
{
	public partial class UserHandlerTests : UnitTestBase
	{
		[TestMethod]
		[Description("Given that I trying delete user, " +
		             "when user identifier is invalid" +
		             "then should contains bad request status code in command result")]
		public void Should_contains_bad_request_status_code_in_command_result_when_user_identifier_is_invalid()
		{
			var commandResult = this.userHandler.Delete(this.MockString());

			Assert.AreEqual(StatusCodeResult.BadRequest, commandResult.StatusCode);
		}
		
		[TestMethod]
		[Description("Given that I trying delete user, " +
		             "when user identifier not found on database" +
		             "then should contains bad request status code in command result")]
		public void Should_contains_bad_request_status_code_in_command_result_when_user_identifier_not_found_on_database()
		{
			var commandResult = this.userHandler.Delete("07F28933-A0BE-4C18-847A-346A92497362");

			Assert.AreEqual(StatusCodeResult.NotFound, commandResult.StatusCode);
		}

		[TestMethod]
		[Description("Given that user identifier is and exist on database, " +
		             "when trying delete user" +
		             "then should contains success status code in command result")]
		public void Should_contains_success_status_code_in_command_result_when_user_identifier_not_found_on_database()
		{
			var commandResult = this.userHandler.Delete(Guid.NewGuid().ToString());

			Assert.AreEqual(StatusCodeResult.Success, commandResult.StatusCode);
		}
	}
}
