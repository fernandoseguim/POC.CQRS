using System;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace POC.CQRS.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration) => this.Configuration = configuration;

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication("Bearer").AddIdentityServerAuthentication(options =>
			{
				options.RequireHttpsMetadata = false;
				options.Authority = "http://localhost:5000";
				options.ApiName = "POC.CQRS";
			});

			services.AddIOCConteiner(this.Configuration);

			services.AddMvc().AddJsonOptions(options
				=> options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);

			services.AddGzipCompression();

			services.AddElmahIo(options =>
			{
				options.ApiKey = "c198d9a3404a49acbccff911ec680026";
				options.LogId = new Guid("989d44e2-dfbe-4b19-81ce-84ec689916f9");
			});

			services.AddVersionedApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'V";
				options.SubstituteApiVersionInUrl = true;
			});
			services.AddApiVersioning(options => options.ReportApiVersions = true);

			services.AddSwaggerGen(options =>
			{
				var provider = services.BuildServiceProvider()
					.GetRequiredService<IApiVersionDescriptionProvider>();

				foreach (var description in provider.ApiVersionDescriptions)
				{
					options.SwaggerDoc(
						description.GroupName,
						new Info()
						{
							Title = $"POC CQRS API {description.ApiVersion}",
							Version = description.ApiVersion.ToString()
						});
				}
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseAuthentication();
			app.UseElmahIo();
			app.UseResponseCompression();
			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				foreach (var description in provider.ApiVersionDescriptions)
				{
					options.SwaggerEndpoint(
						$"/swagger/{description.GroupName}/swagger.json",
						description.GroupName.ToUpperInvariant());
				}
			});
		}
	}
}
