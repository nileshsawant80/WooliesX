using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WooliesX.TechChallenge.Api.Options;
using WooliesX.TechChallenge.Api.Repositories;
using WooliesX.TechChallenge.Api.Services;
using Xunit.Abstractions;

namespace WooliesX.TechChallenge.Api.Tests.Fixtures
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        public CustomWebApplicationFactory(ITestOutputHelper testOutputHelper)
        {
            this.ClientOptions.AllowAutoRedirect = false;
            this.ClientOptions.BaseAddress = new Uri("https://localhost");
        }

        public ApplicationOptions ApplicationOptions { get; private set; }
        public ResourceServiceOptions ResourceServiceOptions { get; set; }

        public Mock<IProductRepository> ProductRepositoryMock { get; private set; }
        public Mock<IUserRepository> UserRepositoryMock { get; private set; }
        public Mock<IResourceService> ResourceServiceMock { get; private set; }

        public void VerifyAllMocks() => Mock.VerifyAll(this.ProductRepositoryMock, this.UserRepositoryMock, this.ResourceServiceMock);

        protected override void ConfigureClient(HttpClient client)
        {
            using (var serviceScope = this.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                this.ApplicationOptions = serviceProvider.GetRequiredService<IOptions<ApplicationOptions>>().Value;
                this.ResourceServiceOptions = serviceProvider.GetRequiredService<IOptions<ResourceServiceOptions>>().Value;
                this.UserRepositoryMock = serviceProvider.GetRequiredService<Mock<IUserRepository>>();
                this.ProductRepositoryMock = serviceProvider.GetRequiredService<Mock<IProductRepository>>();
                this.ResourceServiceMock = serviceProvider.GetRequiredService<Mock<IResourceService>>();
            }

            base.ConfigureClient(client);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder
                .UseEnvironment("Test")
                .UseStartup<TestStartup>();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.VerifyAllMocks();
            }

            base.Dispose(disposing);
        }
    }
}
