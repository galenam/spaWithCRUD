using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace code
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("hosts.json", optional: false)
			.Build();

			var hosts = WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseConfiguration(config)
				.Build();

			hosts.Run();
		}
	}
}
