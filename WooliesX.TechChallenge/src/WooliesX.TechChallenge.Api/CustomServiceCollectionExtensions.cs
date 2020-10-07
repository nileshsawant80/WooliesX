using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.OperationFilters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using Microsoft.Extensions.Configuration;
using WooliesX.TechChallenge.Api.Options;

namespace WooliesX.TechChallenge.Api
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods which extend ASP.NET Core services.
    /// </summary>
    internal static class CustomServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
        /// Objects (POCO) and adding objects to the services collection.
        /// </summary>
        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration) =>
            services
                // ConfigureSingleton registers IOptions<T> and also T as a singleton to the services collection.
                .AddOptions()
                .Configure<ApplicationOptions>(configuration)
                .Configure<ResourceServiceOptions>(configuration.GetSection("ResourceService"))
                .Configure<ResourceServiceOptions>(configuration.GetSection(nameof(ApplicationOptions.ResourceServiceOptions)));

        /// <summary>
        /// Add custom routing settings which determines how URL's are generated.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The services with routing services added.</returns>
        public static IServiceCollection AddCustomRouting(this IServiceCollection services) =>
            services.AddRouting(options => options.LowercaseUrls = true);

        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services) =>
            services
                .AddHealthChecks()
                // Add health checks for external dependencies here. See https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
                .Services;

        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services) =>
            services
                .AddApiVersioning(
                    options =>
                    {
                        options.AssumeDefaultVersionWhenUnspecified = true;
                        options.ReportApiVersions = true;
                    })
                .AddVersionedApiExplorer(x => x.GroupNameFormat = "'v'VVV"); // Version format: 'v'major[.minor][-status]

        /// <summary>
        /// Adds Swagger services and configures the Swagger services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The services with Swagger services added.</returns>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(
                options =>
                {
                    var assembly = typeof(Startup).Assembly;
                    var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                    //var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

                    options.DescribeAllParametersInCamelCase();
                    options.EnableAnnotations();

                    // Add the XML comment file for this assembly, so its contents can be displayed.
                    //options.IncludeXmlCommentsIfExists(assembly);

                    //options.OperationFilter<ApiVersionOperationFilter>();
                    //options.OperationFilter<ClaimsOperationFilter>();
                    //options.OperationFilter<ForbiddenResponseOperationFilter>();
                    //options.OperationFilter<UnauthorizedResponseOperationFilter>();

                    // Show a default and example model for JsonPatchDocument<T>.
                    //options.SchemaFilter<JsonPatchDocumentSchemaFilter>();

                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
                    {
                        var info = new OpenApiInfo()
                        {
                            Version = apiVersionDescription.ApiVersion.ToString(),
                            Title = assemblyProduct,
                            Description = "WooliesX Tech Challenge Web Api"
                        };
                        options.SwaggerDoc(apiVersionDescription.GroupName, info);
                    }
                });
    }
}
