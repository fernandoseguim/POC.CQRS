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
	public class BrandsController : Controller
	{
		private readonly ICommandHandler<BrandCommand> brandCommandHandler;
		private readonly IBrandQueryHandler brandQueryHandler;
		private readonly IAssetQueryHandler assetQueryHandler;

		public BrandsController(ICommandHandler<BrandCommand> brandCommandHandler, 
			IBrandQueryHandler brandQueryHandler, 
			IAssetQueryHandler assetQueryHandler)
		{
			this.brandCommandHandler = brandCommandHandler;
			this.brandQueryHandler = brandQueryHandler;
			this.assetQueryHandler = assetQueryHandler;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var result = this.brandQueryHandler.GetAll();

			return this.StatusCode((int)result.StatusCode, result);
		}

		[HttpGet("{id}")]
		public IActionResult Get(string id)
		{
			var result = this.brandQueryHandler.GetById(id);

			return this.StatusCode((int)result.StatusCode, result);
		}

		[HttpGet("{id}/assets")]
		public IActionResult GetAssetsByBrandId(string id)
		{
			var result = this.assetQueryHandler.GetAllByBrandId(id);

			return this.StatusCode((int)result.StatusCode, result);
		}

		[HttpPost]
		public IActionResult Post([FromBody] BrandCommand brandCommand)
		{
			var result = this.brandCommandHandler.Create(brandCommand);

			return this.StatusCode((int)result.StatusCode, result);
		}

		[HttpPut("{id}")]
		public IActionResult Put(string id, [FromBody] BrandCommand brandCommand)
		{
			var result = this.brandCommandHandler.Update(id, brandCommand);

			return this.StatusCode((int)result.StatusCode, result);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] string id)
		{
			var result = this.brandCommandHandler.Delete(id);

			return this.StatusCode((int)result.StatusCode, result);
		}
	}
}
