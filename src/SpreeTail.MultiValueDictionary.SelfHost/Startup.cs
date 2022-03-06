using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SpreeTail.MultiValueDictionary.Host.Configurations;
using SpreeTail.MultiValueDictionary.SelfHost.Configurations;
using SpreeTail.MultiValueDictionary.User.Api.Controllers;

namespace SpreeTail.MultiValueDictionary.SelfHost
{
    public class Startup
    {
        public readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ServiceRegistry services)
        {
            LamarConfigurations.ConfigureLamar(services, _configuration);
            services.ConfigureSwagger(_configuration);
            services.ConfigureContainerMVC<HealthController>(options =>
            {
                //options.Filters.Ad
            });
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment env)
        {

            applicationBuilder.UseRouting();
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            applicationBuilder.ConfigureSwagger();
        }
    }
}
