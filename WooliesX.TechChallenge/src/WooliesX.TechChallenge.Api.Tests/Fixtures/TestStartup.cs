using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WooliesX.TechChallenge.Api.Repositories;
using WooliesX.TechChallenge.Api.Services;

namespace WooliesX.TechChallenge.Api.Tests.Fixtures
{
    public class TestStartup : Startup
    {
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly Mock<IResourceService> resourceServiceMock;

        public TestStartup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
            : base(configuration, webHostEnvironment)
        {
            this.userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            this.productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            this.resourceServiceMock = new Mock<IResourceService>(MockBehavior.Strict);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(this.userRepositoryMock)
                .AddSingleton(this.productRepositoryMock)
                .AddSingleton(this.resourceServiceMock);

            base.ConfigureServices(services);

            services
                .AddSingleton(this.userRepositoryMock.Object)
                .AddSingleton(this.productRepositoryMock.Object)
                .AddSingleton(this.resourceServiceMock.Object);
        }
    }
}