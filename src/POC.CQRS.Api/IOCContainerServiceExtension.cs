using POC.CQRS.Domain.Commands.Request;
using POC.CQRS.Domain.Handlers;
using POC.CQRS.Domain.Repositories;
using POC.CQRS.Infra;
using POC.CQRS.Infra.Repositories;
using POC.CQRS.Shared.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using POC.CQRS.Domain.Queries.Response;

namespace POC.CQRS.Api
{
	public static class IOCContainerServiceExtension
	{
		public static IServiceCollection AddIOCConteiner(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IDbConnection>(connection
				=> new SqlConnection(configuration.GetConnectionString("POCCQRSDatabaseContext")));

			services.AddTransient<IDatabaseContext, POCCQRSDatabaseContext>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IBrandRepository, BrandRepository>();
			services.AddTransient<IAssetRepository, AssetRepository>();
			services.AddTransient<ICommandHandler<UserCommand>, UserHandler>();
			services.AddTransient<ICommandHandler<BrandCommand>, BrandHandler>();
			services.AddTransient<ICommandHandler<AssetCommand>, AssetHandler>();
			services.AddTransient<IUserQueryHandler, UserQueryHandler>();
			services.AddTransient<IBrandQueryHandler, BrandQueryHandler>();
			services.AddTransient<IAssetQueryHandler, AssetQueryHandler>();

			return services;
		}
	}
}
