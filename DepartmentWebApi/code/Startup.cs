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
using DepartmentWebApi.code.Model;
using DepartmentWebApi.code.DB;
using Serilog;
using BaseWebApi.Code.Interfaces;
using BaseWebApi.Code.AbstractClasses;

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
					Path.Combine(Directory.GetCurrentDirectory()) + $"\\logs\\departmentwebapi\\log.txt"
					, fileSizeLimitBytes: 10240
					, outputTemplate: "{Message}" + Environment.NewLine)
				.CreateLogger();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var connection = Configuration["ConnectionString:SqliteDB"];

			services.AddDbContext<DepartmentDBContext>(options =>
			options.UseSqlite(connection));
			services.AddCors();
			services.AddScoped<IBaseRepository<Department>, DepartmentRepository>();

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
			loggerFactory.AddSerilog();

			app.UseMvc();
		}
	}
}
