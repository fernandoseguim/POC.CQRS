using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace POC.CQRS.Auth
{
	public class Startup
    {
	    private readonly IHostingEnvironment environment;

		public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.Configuration = configuration;
	        this.environment = environment;
        }

        public IConfiguration Configuration { get; }
		        
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc();
	        services.AddIdentityServer()
				.AddDeveloperSigningCredential()
				.AddInMemoryApiResources(Configurations.GetApiResources())			
		        .AddInMemoryClients(Configurations.GetClients());
        }

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
			
			app.UseIdentityServer();

			app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
