using System;
using POC.CQRS.Domain.Handlers;
using POC.CQRS.Domain.Queries;
using POC.CQRS.Domain.Queries.Response;
using POC.CQRS.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using POC.CQRS.Shared.Commands;

namespace POC.CQRS.Tests.Domain.Handlers
{
	[TestClass]
	public class BrandQueryHandlerTests : UnitTestBase
	{
		private const string NOT_FOUND_USER_ID = "07F28933-A0BE-4C18-847A-346A92497362";
		private readonly IBrandRepository repository;
		private readonly IBrandQueryHandler brandQueryHandler;

		public BrandQueryHandlerTests()
		{
			this.repository = Substitute.For<IBrandRepository>();
			this.brandQueryHandler = new BrandQueryHandler(this.repository);
		}

		[TestInitialize]
		public void Setup()
		{
			var n = Guid.NewGuid();
			this.repository.GetAll().Returns(new List<BrandQueryResult>());
			this.repository.GetById(Arg.Any<Guid>()).Returns(new BrandQueryResult());
			this.repository.GetById(Arg.Is<Guid>(a => a.Equals(new Guid(NOT_FOUND_USER_ID)))).Returns(default(BrandQueryResult));
		}

		[TestMethod]
		[Description("When request all brands" +
		             "then should return a successful query result")]
		public void Should_return_a_successful_query_result()
		{
			var result = this.brandQueryHandler.GetAll();
			Assert.IsInstanceOfType(result, typeof(SuccessfulQueryResult));
		}

		[TestMethod]
		[Description("When request brand by id" +
		             "then should return a success")]
		public void Should_return_a_success_when_request_brand_by_id_and_brand_exists()
		{
			var result = this.brandQueryHandler.GetById(Guid.NewGuid().ToString());
			Assert.AreEqual(StatusCodeResult.Success, result.StatusCode);
		}

		[TestMethod]
		[Description("When request brand by id" +
		             "then should return a not found")]
		public void Should_return_a_not_found_when_request_brand_by_id_and_brand_exists()
		{
			var result = this.brandQueryHandler.GetById(NOT_FOUND_USER_ID);
			Assert.AreEqual(StatusCodeResult.NotFound, result.StatusCode);
		}
	}
}
