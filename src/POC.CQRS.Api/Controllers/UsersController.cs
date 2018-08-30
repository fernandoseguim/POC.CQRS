using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Handlers;
using POC.CQRS.Shared.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace POC.CQRS.Api.Controllers
{
	[Authorize]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class UsersController : Controller
	{
		private readonly ICommandHandler<UserCommand> userCommandHandler;
		private readonly IUserQueryHandler userQueryHandler;

		public UsersController(ICommandHandler<UserCommand> userCommandHandler, IUserQueryHandler userQueryHandler)
		{
			this.userCommandHandler = userCommandHandler;
			this.userQueryHandler = userQueryHandler;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var result = this.userQueryHandler.GetAll();

			return this.StatusCode((int)result.StatusCode, result);
		}

		// POST api/users
		[HttpPost]  
		public IActionResult Post([FromBody] UserCommand userCommand)
		{
			var result = this.userCommandHandler.Create(userCommand);
			
			return this.StatusCode((int)result.StatusCode, result);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] string id)
		{
			var result = this.userCommandHandler.Delete(id);

			return this.StatusCode((int)result.StatusCode, result);
		}
	}
}
