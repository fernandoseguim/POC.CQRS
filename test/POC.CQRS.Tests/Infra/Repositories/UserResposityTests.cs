using System;
using System.Linq;
using Dapper;
using POC.CQRS.Domain.Entities;
using POC.CQRS.Domain.Repositories;
using POC.CQRS.Domain.ValueObjects;
using POC.CQRS.Infra.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace POC.CQRS.Tests.Infra.Repositories
{
	[TestClass]
	public class UserResposityTests : RepositoryTests<User>
	{
		private readonly IUserRepository repository;

		public UserResposityTests()
		{
			this.repository = new UserRepository(this.Context);
		}

		[TestMethod]
		[Description("Given that I has a new user, " +
		             "when trying to save a new user in the database, " +
		             "then should save new user in the users table")]
		public void Should_save_the_new_user_in_the_users_table()
		{
			var name = new Name("Fernando", "Seguim");
			var email = new Email("fernando.seguim@gmail.com");
			var password = new SecurityPassword("12634567890");
			var user = new User(name, email, password);

			this.repository.Save(user);

			this.Context
				.Received()
				.Connection
				.Execute(
					Arg.Is<string>(query => query.Contains("INSERT INTO Users (UserID, Name, Email, PasswordHash, Salt)")), 
					Arg.Any<object>());
		}

		[TestMethod]
		[Description("Given that I check if a emails exists in the database, " +
		             "when the email exists, " +
		             "then should return true")]
		public void Should_return_true_when_email_exists_in_database()
		{
			var email = new Email("teste@gmail.com");
			
			var result = this.repository.CheckEmail(email);

			Assert.IsTrue(result);
		}

		[TestMethod]
		[Description("Given that I check if a emails exists in the database, " +
		             "when the email not exists, " +
		             "then should return false")]
		public void Should_return_false_when_email_exists_in_database()
		{
			var email = new Email($"{this.MockString()}@teste.com");

			var result = this.repository.CheckEmail(email);

			Assert.IsFalse(result);
		}

		[TestMethod]
		[Description("Given that I trying delete the user, " +
		             "when user identifier not exist on database, " +
		             "then should return false")]
		public void Should_return_false_when_not_found_userd_by_id_to_delete()
		{
			var result = this.repository.Delete(Guid.NewGuid());

			Assert.IsFalse(result);
		}

		[TestMethod]
		[Description("Given that I trying delete the user, " +
		             "when user identifier exist on database, " +
		             "then should return true")]
		public void Should_return_true_when_delete_user_from_database()
		{
			var name = new Name(this.MockString(), this.MockString());
			var email = new Email($"{this.MockString()}@gmail.com");
			var password = new SecurityPassword(this.MockString());
			var user = new User(name, email, password);

			this.repository.Save(user);

			var result = this.repository.Delete(user.Id);

			Assert.IsTrue(result);
		}

		[TestMethod]
		[Description("Given that exists users in database, " +
					 "when I trying get all users, " +
					 "then should return a list of users")]
		public void Should_return_all_users_from_database()
		{
			var result = this.repository.GetAll();

			Assert.IsTrue(result.ToList().Any());
		}
	}
}