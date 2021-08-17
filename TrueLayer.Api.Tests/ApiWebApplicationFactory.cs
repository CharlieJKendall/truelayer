using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;

namespace TrueLayer.Api.Tests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.test.json"));
            });

            base.ConfigureWebHost(builder);
        }

        public static HttpClient CreateHttpClient()
        {
            return new ApiWebApplicationFactory().CreateClient();
        }
    }
}
