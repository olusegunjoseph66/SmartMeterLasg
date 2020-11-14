using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SmartMeterWebApp.Areas.Identity.IdentityHostingStartup))]
namespace SmartMeterWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}