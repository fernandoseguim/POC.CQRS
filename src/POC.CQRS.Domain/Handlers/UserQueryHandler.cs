using POC.CQRS.Shared.Queries;
using System;
using POC.CQRS.Domain.Queries.Response;
using POC.CQRS.Domain.Repositories;
using POC.CQRS.Shared.Commands;

namespace POC.CQRS.Domain.Handlers
{
	public class UserQueryHandler : IUserQueryHandler
	{
		private readonly IUserRepository repository;

		public UserQueryHandler(IUserRepository repository) => this.repository = repository;


		public IQueryResult GetAll()
		{
			try
			{
				var result = this.repository.GetAll();

				return new SuccessfulQueryResult(result);
			}
			catch (Exception ex)
			{
				return new UnsuccessfulQueryResult(StatusCodeResult.InternalServerError,ex.Message);
			}
		}
	}
}
