using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Commands;
using WooliesX.TechChallenge.Api.Controllers;
using WooliesX.TechChallenge.Api.Mappers;
using WooliesX.TechChallenge.Api.Repositories;
using WooliesX.TechChallenge.Api.Services;
using WooliesX.TechChallenge.Api.Tests.Fixtures;
using WooliesX.TechChallenge.Api.ViewModels;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.Options;
using WooliesX.TechChallenge.Api.Options;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace WooliesX.TechChallenge.Api.Tests.Controllers
{
    public class ProductsControllerTest
    {
        private readonly HttpClient client;
        private readonly ProductsController _controllerUnderTest;
        private readonly IGetProductsCommand getProductsCommand;
        public ProductsControllerTest()
        {
            var optionsMock = new Mock<IOptions<ResourceServiceOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new ResourceServiceOptions());

            var svc = new ResourceService(optionsMock.Object);

            var logger = Mock.Of<ILogger<GetProductsCommand>>();
            var mapper = new ProductToProductMapper();
            var userRepo = new ProductRepository(svc);
            getProductsCommand = new GetProductsCommand(userRepo, mapper, logger);
            _controllerUnderTest = new ProductsController();
        }

        [Fact]
        public async Task Get_ProductsFound_Returns200OkAsync()
        {
            var products = new List<Models.Product>
            {
                new Models.Product { Name = "A", Price = 99.99M, Quantity = 10 },
                new Models.Product { Name = "B", Price = 101.99M, Quantity = 20 },
                new Models.Product { Name = "C", Price = 10.99M, Quantity = 30 },
                new Models.Product { Name = "D", Price = 5, Quantity = 40 }
            };

            var result = await _controllerUnderTest.GetAsync(getProductsCommand,"High", new CancellationToken());
            var okObjectResult = result as OkObjectResult;

            Assert.Equal(200, okObjectResult.StatusCode);
        }

        //[Fact]
        //public async Task Get_ProductsNotFound_Returns404NotFoundAsync()
        //{
        //    var products = new List<Models.Product>();
        //    products.Add(new Models.Product { Name = "A", Price = 99.99M, Quantity = 10 });
        //    products.Add(new Models.Product { Name = "B", Price = 101.99M, Quantity = 20 });
        //    products.Add(new Models.Product { Name = "C", Price = 10.99M, Quantity = 30 });
        //    products.Add(new Models.Product { Name = "D", Price = 5, Quantity = 40 });

        //    this.ProductRepositoryMock.Setup(x => x.GetProductsAsync(Constants.SortOption.Ascending)).ReturnsAsync(products);

        //    var response = await this.client.GetAsync(new Uri("/sort?8e2c03a8-8267-4490-a9d4-d5d5a882cc24", UriKind.Relative)).ConfigureAwait(false);

        //    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        //}
    }
}
