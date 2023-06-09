using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreStartApp
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
		}

		private static void About(IApplicationBuilder app)
		{
			app.Run(async context =>
			{
				await context.Response.WriteAsync($"{env.ApplicationName} - ASP.Net Core tutorial project");
			});
		}

		private static void Config(IApplicationBuilder app)
		{
			app.Run(async context =>
			{
				await context.Response.WriteAsync($"App name: {env.ApplicationName}. App running configuration: {env.EnvironmentName}");
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment() || env.IsStaging())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseStaticFiles();

			app.UseMiddleware<LoggingMiddleware>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello World!");
				});
			});

			app.Map("/about", About);
			app.Map("/config", Config);

			app.UseStatusCodePages();
		}
	}
}
