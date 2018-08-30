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
	public class AssetsController : Controller
	{
		private readonly ICommandHandler<AssetCommand> assetCommandHandler;
		private readonly IAssetQueryHandler assetQueryHandler;

		public AssetsController(ICommandHandler<AssetCommand> assetCommandHandler, IAssetQueryHandler assetQueryHandler)
		{
			this.assetCommandHandler = assetCommandHandler;
			this.assetQueryHandler = assetQueryHandler;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var result = this.assetQueryHandler.GetAll();

			return this.StatusCode((int)result.StatusCode, result);
		}

		[HttpGet("{id}")]
		public IActionResult Get(string id)
		{
			var result = this.assetQueryHandler.GetById(id);

			return this.StatusCode((int)result.StatusCode, result);
		}

		[HttpPost]
		public IActionResult Post([FromBody] AssetCommand assetCommand)
		{
			var result = this.assetCommandHandler.Create(assetCommand);

			return this.StatusCode((int)result.StatusCode, result);
		}

		[HttpPut("{id}")]
		public IActionResult Put(string id, [FromBody] AssetCommand assetCommand)
		{
			var result = this.assetCommandHandler.Update(id, assetCommand);

			return this.StatusCode((int)result.StatusCode, result);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] string id)
		{
			var result = this.assetCommandHandler.Delete(id);

			return this.StatusCode((int)result.StatusCode, result);
		}
	}
}
