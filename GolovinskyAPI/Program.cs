using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GolovinskyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
            //.UseKestrel(o =>
            //{
            //    //o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30);
            //})
            //.UseIISIntegration()
            //.UseStartup<Startup>();
                //.Build();
    }
}
