using Dapper;
using POC.CQRS.Domain.Entities;
using POC.CQRS.Domain.Repositories;
using POC.CQRS.Infra.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;
using POC.CQRS.Domain.Commands.Request;

namespace POC.CQRS.Tests.Infra.Repositories
{
	[TestClass]
	public class BrandResposityTests : RepositoryTests<Brand>
	{
		private readonly IBrandRepository repository;
		private readonly Brand brand;

		public BrandResposityTests()
		{
			this.repository = new BrandRepository(this.Context);
			this.brand = new Brand(MockString());
		}

		[TestMethod]
		[Description("Given that I has a new brand, " +
					 "when trying to save a new brand in the database, " +
					 "then should save new brand in the users table")]
		public void Should_save_the_new_brand_in_the_brands_table()
		{
			this.repository.Save(this.brand);

			this.Context
				.Received()
				.Connection
				.Execute(
					Arg.Is<string>(query => query.Contains("INSERT INTO Brands (BrandID, Name)")),
					Arg.Any<object>());
		}

		[TestMethod]
		[Description("Given that I have a valid brand name, " +
					 "when trying to update a brand in the database, " +
		             "then should udate brand in the brands table")]
		public void Should_update_the_brand_in_the_brands_table()
		{
			this.repository.Save(this.brand);
			this.repository.Update(this.brand.Id, new BrandCommand { Name = MockString() });

			this.Context
				.Received()
				.Connection
				.Execute(
					Arg.Is<string>(query => query.Contains("UPDATE Brands SET Name")),
					Arg.Any<object>());
		}

		[TestMethod]
		[Description("Given that I check if a brand exists in the database, " +
					 "when the brand exists, " +
					 "then should return true")]
		public void Should_return_true_when_brand_exists_in_database()
		{
			var brand = new Brand("Brand Test");

			var result = this.repository.CheckBrand(brand.Name);

			Assert.IsTrue(result);
		}

		[TestMethod]
		[Description("Given that I check if a brand exists in the database, " +
					 "when the brand not exists, " +
					 "then should return false")]
		public void Should_return_false_when_brand_exists_in_database()
		{
			var brand = new Brand(MockString());

			var result = this.repository.CheckBrand(brand.Name);

			Assert.IsFalse(result);
		}

		[TestMethod]
		[Description("Given that I trying delete the brand, " +
					 "when brand identifier not exist on database, " +
					 "then should return false")]
		public void Should_return_false_when_not_found_brand_by_id_to_delete()
		{
			var result = this.repository.Delete(Guid.NewGuid());

			Assert.IsFalse(result);
		}

		[TestMethod]
		[Description("Given that I trying delete the brand, " +
					 "when brand identifier exist on database, " +
					 "then should return true")]
		public void Should_return_true_when_delete_brand_from_database()
		{
			var brand = new Brand(MockString());

			this.repository.Save(brand);

			var result = this.repository.Delete(brand.Id);

			Assert.IsTrue(result);
		}

		[TestMethod]
		[Description("Given that exists brands in database, " +
					 "when I trying get all brands, " +
					 "then should return a list of brands")]
		public void Should_return_all_brands_from_database()
		{
			var result = this.repository.GetAll();

			Assert.IsTrue(result.ToList().Any());
		}

		[TestMethod]
		[Description("Given I trying get brand by identifier, " +
		             "when exist brand in database, " +
		             "then should return brand")]
		public void Should_return_brand_when_get_by_id_and_exist_brand_in_database()
		{
			var brand = new Brand(MockString());

			this.repository.Save(brand);
			
			var result = this.repository.GetById(brand.Id);

			Assert.AreEqual(result.Name, brand.Name);
		}

		[TestMethod]
		[Description("Given I trying get brand by identifier, " +
		             "when not exist brand in database, " +
		             "then should return null")]
		public void Should_return_null_when_get_by_id_and_not_exist_brand_in_database()
		{
			var result = this.repository.GetById(Guid.NewGuid());

			Assert.IsNull(result);
		}
	}
}