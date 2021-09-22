using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace SPM_Project
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build().Run();
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });



        //https://stackoverflow.com/questions/63323199/run-async-code-during-startup-in-a-asp-net-core-application
        //add async stuff
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //// Resolve the StartupTasks from the ServiceProvider
            //var startupTasks = host.Services.GetServices<IStartupTask>();

            //// Run the StartupTasks
            //foreach (var startupTask in startupTasks)
            //{
            //    await startupTask.Execute();
            //}
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }




}
