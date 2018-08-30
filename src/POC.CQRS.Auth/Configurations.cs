using System.Collections.Generic;
using IdentityServer4.Models;

namespace POC.CQRS.Auth
{
	public static class Configurations
	{
		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource> { new ApiResource("POC.CQRS", "POC CQRS"), new ApiResource("SampleApi", "SampleApi") };
		}
		public static IEnumerable<Client> GetClients()
		{
			return new List<Client> {
				new Client {
					ClientId = "DD7D1B78-126E-4DD5-A429-BB61C6BCAA20",
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					ClientSecrets = new List<Secret> { new Secret("da39a3ee5e6b4b0d3255bfef95601890afd80709".Sha256()) },
					AllowedScopes = { "POC.CQRS", "SampleApi" }
				}
			};
		}
	}
}
