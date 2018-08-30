using System;
using POC.CQRS.Shared.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace POC.CQRS.Tests.Domain.Handlers
{
	public partial class BrandHandlerTests : UnitTestBase
    {
	    [TestMethod]
	    [Description("Given that I trying delete brand, " +
					 "when brand identifier is invalid" +
	                 "then should contains bad request status code in command result")]
	    public void Should_contains_bad_request_status_code_in_command_result_when_brand_identifier_is_invalid()
	    {
		    var commandResult = this.brandHandler.Delete(this.MockString());

		    Assert.AreEqual(StatusCodeResult.BadRequest, commandResult.StatusCode);
	    }

	    [TestMethod]
	    [Description("Given that I trying delete brand, " +
	                 "when brand identifier not found on database" +
	                 "then should contains not found status code in command result")]
	    public void Should_contains_not_found_status_code_in_command_result_when_brand_identifier_not_found_on_database()
	    {
		    var commandResult = this.brandHandler.Delete(NOT_FOUND_BRAND_ID);

		    Assert.AreEqual(StatusCodeResult.NotFound, commandResult.StatusCode);
	    }

	    [TestMethod]
	    [Description("Given that brand identifier is and exist on database, " +
	                 "when trying delete brand" +
	                 "then should contains success status code in command result")]
	    public void Should_contains_success_status_code_in_command_result_when_brand_identifier_not_found_on_database()
	    {
		    var commandResult = this.brandHandler.Delete(Guid.NewGuid().ToString());

		    Assert.AreEqual(StatusCodeResult.Success, commandResult.StatusCode);
	    }
	}
}
