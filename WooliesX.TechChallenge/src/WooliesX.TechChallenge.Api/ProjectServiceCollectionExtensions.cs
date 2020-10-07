using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Commands;
using WooliesX.TechChallenge.Api.Mappers;
using WooliesX.TechChallenge.Api.Options;
using WooliesX.TechChallenge.Api.Repositories;
using WooliesX.TechChallenge.Api.Services;
using WooliesX.TechChallenge.Api.ViewModels;

namespace WooliesX.TechChallenge.Api
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods add project services.
    /// </summary>
    /// <remarks>
    /// AddSingleton - Only one instance is ever created and returned.
    /// AddScoped - A new instance is created and returned for each request/response cycle.
    /// AddTransient - A new instance is created and returned each time.
    /// </remarks>
    public static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectCommands(this IServiceCollection services) =>
            services
                .AddSingleton<IGetUserCommand, GetUserCommand>()
                .AddScoped<IGetProductsCommand, GetProductsCommand>();

        public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
            services
                .AddSingleton<IMapper<Models.User, User>, UserToUserMapper>()
                .AddSingleton<IMapper<List<Models.Product>,List<Product>>, ProductToProductMapper>();

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<IProductRepository, ProductRepository>();

        public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
            services
                .AddSingleton<IResourceService, ResourceService>();

        //public static IServiceCollection AddCustomHttpClients(this IServiceCollection services) =>
        //    services;
                //.AddHttpClient<IResourceService, ResourceService>();

        public static IServiceCollection AddCustomHttpClients(this IServiceCollection services)
        {
        //    var provider = services.BuildServiceProvider();

        //    //default http client
            services.AddHttpClient();

        //    // HttpClientFactory typed clients
        //    //services.AddHttpClient<IResourceService, ResourceService>((serviceProvider, httpClient) =>
        //    //{
        //    //    var options = serviceProvider.GetRequiredService<ResourceServiceOptions>();
        //    //    //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        //    //    httpClient.BaseAddress = new Uri(options.BaseAddress);
        //    //    httpClient.Timeout = TimeSpan.Parse(options.Timeout);

        //    //    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        //    //    //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        //    //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    //});
        //    //.ConfigurePrimaryHttpMessageHandler(x => new DefaultHttpClientHandler());

            return services;
        }
    }
}
