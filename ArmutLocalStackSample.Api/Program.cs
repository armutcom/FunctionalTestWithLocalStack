using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ArmutLocalStackSample.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    bool isTest = context.HostingEnvironment.IsEnvironment("Testing");
                    if (isTest)
                    {
                        return;
                    }

                    var awsOptions = new AWSOptions
                    {
                        Region = RegionEndpoint.EUCentral1
                    };
                    config.AddSystemsManager("/amazon", awsOptions);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
