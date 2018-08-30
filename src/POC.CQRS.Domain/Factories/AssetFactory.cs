using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Entities;

namespace POC.CQRS.Domain.Factories
{
	public class AssetFactory
	{
		public static Asset Create(AssetCommand command, Brand brand)
		{
			var asset = new Asset(command.Name, brand);
			asset.AddDescription(command.Description);
			return asset;
		}
	}
}