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
using code.Model;
using DB;
using Interfaces;
using Serilog;

namespace code
{
    public class Startup
    {
        //dotnet ef dbcontext scaffold -c DepartmentDBContext -o Model "Data Source=D:\\My\\dev\\SPA\\db\\db2.db;" Microsoft.EntityFrameworkCore.Sqlite --force
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Fatal()
                .WriteTo.File(
                    Path.Combine(Directory.GetCurrentDirectory()) + $"\\logs\\log.txt"
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
     
        services.AddMvc();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
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
