using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;
using System.Text.Json.Serialization;

namespace SpreeTail.MultiValueDictionary.SelfHost.Configurations
{
    public static class MvcConfiguration
    {
        public static void ConfigureContainerMVC<T>(this IServiceCollection services, Action<MvcOptions> setupAction) =>
            services.AddMvc(setupAction)
            .AddApplicationPart(typeof(T).Assembly)
            .AddControllersAsServices()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    }
}
