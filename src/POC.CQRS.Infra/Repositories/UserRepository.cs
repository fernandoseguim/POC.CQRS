using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using POC.CQRS.Domain.Commands.Response;
using POC.CQRS.Domain.Entities;
using POC.CQRS.Domain.Queries;
using POC.CQRS.Domain.Queries.Response;
using POC.CQRS.Domain.Repositories;
using POC.CQRS.Domain.ValueObjects;
using POC.CQRS.Infra.Builders;

namespace POC.CQRS.Infra.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly IDatabaseContext context;

		public UserRepository(IDatabaseContext context) => this.context = context;

		public IEnumerable<UserQueryResult> GetAll()
		{
			var (query, parameters) = new UserQueryBuilder().GetAll().Build();

			var users = this.context.Connection.Query<UserQueryResult>(query);

			return users;
		}

		public void Save(User user)
		{
			var (query, parameters) = new UserQueryBuilder().CreateUser(user).Build();

			this.context.Connection.Execute(query, parameters);
		}

		public bool CheckEmail(Email email)
		{
			var (query, parameters) = new UserQueryBuilder().CheckEmail(email).Build();

			var result = this.context.Connection.Query<bool>(query, parameters).FirstOrDefault();
			return result;
		}

		public bool Delete(Guid userId)
		{
			var (query, parameters) = new UserQueryBuilder().DeleteUser(userId).Build();

			var result = this.context.Connection.Execute(query, parameters);
			return Convert.ToBoolean(result);
		}
	}
}
