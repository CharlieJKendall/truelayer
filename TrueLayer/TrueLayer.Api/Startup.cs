using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrueLayer.Services;

namespace TrueLayer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddApiExplorer();
            services.AddControllers();
            services.AddOpenApiDocument(options =>
            {
                options.Title = "TrueLayer.Api";
                options.Version = "v1";
                options.DocumentName = "v1";
            });
            services.AddHttpClient();
            services.AddMemoryCache();
            services.AddLogging();
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = ApiVersion.Default;
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ServicesDependencyModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
