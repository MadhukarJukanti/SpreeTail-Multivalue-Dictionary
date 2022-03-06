using Lamar;
using Microsoft.Extensions.Configuration;
using SpreeTail.MultiValueDictionary.Host.Lamar.Registry;

namespace SpreeTail.MultiValueDictionary.Host.Configurations
{
    public static class LamarConfigurations
    {
        public static void ConfigureLamar(ServiceRegistry services, IConfiguration configuration)
        {
            services.Scan(scan =>
            {
                scan.AssembliesAndExecutablesFromApplicationBaseDirectory(assembly => assembly.FullName.StartsWith("SpreeTail.MultiValueDictionary"));
                scan.WithDefaultConventions();
            });

            services.IncludeRegistry<MediatrRegistry>();

        }
    }
}
