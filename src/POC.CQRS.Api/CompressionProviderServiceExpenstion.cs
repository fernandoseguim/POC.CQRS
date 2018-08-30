using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace POC.CQRS.Api
{
	public static class CompressionProviderServiceExpenstion
	{
		public static IServiceCollection AddGzipCompression(this IServiceCollection services)
		{
			services.Configure<GzipCompressionProviderOptions>(options
				=> options.Level = CompressionLevel.Optimal);

			services.AddResponseCompression(options
				=> options.Providers.Add<GzipCompressionProvider>());

			return services;
		}
	}
}
