using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureKestrel(a =>
                {
                    a.AddServerHeader = false;
                });
            var port = Environment.GetEnvironmentVariable("PORT");
            if (!String.IsNullOrWhiteSpace(port))
            {
                builder.UseUrls("http://*:" + port);
            }
            return builder;
        }
    }
}