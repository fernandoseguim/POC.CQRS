using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Entities;
using POC.CQRS.Domain.ValueObjects;

namespace POC.CQRS.Domain.Factories
{
	public class UserFactory
	{
		public static User Create(UserCommand command)
		{
			var name = new Name(command.FirstName, command.LastName);
			var email = new Email(command.Email);
			var password = new SecurityPassword(command.Password);
			var user = new User(name, email, password);

			return user;
		}
	}
}
