using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Commands.Response;
using POC.CQRS.Domain.Factories;
using POC.CQRS.Domain.Repositories;
using POC.CQRS.Shared.Commands;
using Flunt.Notifications;
using System;
using POC.CQRS.Domain.ValueObjects;

namespace POC.CQRS.Domain.Handlers
{
	public partial class UserHandler : Notifiable, ICommandHandler<UserCommand>
	{
		private readonly IUserRepository repository;

		public UserHandler(IUserRepository repository) => this.repository = repository;

		public ICommandResult Create(UserCommand command)
		{
			try
			{
				if (command.IsValid())
				{	
					var user = UserFactory.Create(command);

					if(this.CheckEmail(user.Email))
					{
						return new UnsuccessfulCommandResult(StatusCodeResult.Conflict,
							"Please, check the properties before creating the user", this.Notifications);
					}

					this.repository.Save(user);

					return new SuccessfulCommandResult("User was saved with successful", new
					{
						Id = user.Id,
						Name = user.Name.ToString(),
						Email = user.Email.ToString()
					});
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BadRequest, 
					"Please, check the properties before creating the user", command.Notifications);
			}
			catch(Exception ex)
			{
				return new UnsuccessfulCommandResult(StatusCodeResult.InternalServerError, 
					"There was an error saving the user, please contact your system administrator.",
					ex.Message);
			}
		}

		public ICommandResult Update(string id, UserCommand command)
		{
			throw new NotImplementedException();
		}

		private bool CheckEmail(Email email)
		{
			var emailAlreadyExists = this.repository.CheckEmail(email);

			if (emailAlreadyExists)
			{
				this.AddNotification(nameof(email), "Email already exists on database");
			}			
			return emailAlreadyExists;
		}
	}
}
