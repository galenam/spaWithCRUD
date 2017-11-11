using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using UserWebApi.Code.Model;
using UserWebApi.code.DB;
using Serilog;
using BaseWebApi.Code.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;

namespace code
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			Log.Logger = new LoggerConfiguration()
			   .MinimumLevel.Warning()
				.WriteTo.File(
					Path.Combine(Directory.GetCurrentDirectory()) + $"\\logs\\userwebapi\\log.txt"
					, fileSizeLimitBytes: 10240
					, outputTemplate: "{Message}" + Environment.NewLine)
				.CreateLogger();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var connection = Configuration["ConnectionString:SqliteDB"];

			services.AddDbContext<UserDBContext>(options =>
			options.UseSqlite(connection));
			services.AddScoped<IBaseRepository<User>, UserRepository>();
			services.AddSingleton<IConfiguration>(Configuration);

			var _client = new HttpClient() { BaseAddress = new Uri(Configuration["OuterApiWebApi"]) };
			var media = new MediaTypeWithQualityHeaderValue("application/json");
			if (!_client.DefaultRequestHeaders.Accept.Contains(media))
			{
				_client.DefaultRequestHeaders.Accept.Clear();
				_client.DefaultRequestHeaders.Accept.Add(media);
			}
			services.AddSingleton<HttpClient>(_client);

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			loggerFactory.AddSerilog();

			app.UseMvc();
		}
	}
}
