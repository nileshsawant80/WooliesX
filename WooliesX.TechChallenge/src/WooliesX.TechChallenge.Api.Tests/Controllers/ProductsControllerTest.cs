using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Tests.Fixtures;
using WooliesX.TechChallenge.Api.ViewModels;
using Xunit;
using Xunit.Abstractions;

namespace WooliesX.TechChallenge.Api.Tests.Controllers
{
    public class ProductsControllerTest : CustomWebApplicationFactory<Startup>
    {
        private readonly HttpClient client;
        private readonly MediaTypeFormatterCollection formatters;

        public ProductsControllerTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            this.client = this.CreateClient();
            this.formatters = new MediaTypeFormatterCollection();
            this.formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(Fixtures.ContentType.RestfulJson));
            this.formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(Fixtures.ContentType.ProblemJson));
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

            this.ProductRepositoryMock.Setup(x => x.GetProductsAsync(Constants.SortOption.Ascending)).ReturnsAsync(products);

            var response = await this.client.GetAsync(new Uri("/sort?sortOption=Ascending", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(new DateTimeOffset(2000, 1, 2, 3, 4, 5, TimeSpan.FromHours(6)), response.Content.Headers.LastModified);
            Assert.Equal(Fixtures.ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            var productsViewModel = await response.Content.ReadAsAsync<Product>(this.formatters).ConfigureAwait(false);
        }

        [Fact]
        public async Task Get_ProductsNotFound_Returns404NotFoundAsync()
        {
            var products = new List<Models.Product>();
            products.Add(new Models.Product { Name = "A", Price = 99.99M, Quantity = 10 });
            products.Add(new Models.Product { Name = "B", Price = 101.99M, Quantity = 20 });
            products.Add(new Models.Product { Name = "C", Price = 10.99M, Quantity = 30 });
            products.Add(new Models.Product { Name = "D", Price = 5, Quantity = 40 });

            this.ProductRepositoryMock.Setup(x => x.GetProductsAsync(Constants.SortOption.Ascending)).ReturnsAsync(products);

            var response = await this.client.GetAsync(new Uri("/sort?8e2c03a8-8267-4490-a9d4-d5d5a882cc24", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var problemDetails = await response.Content.ReadAsAsync<string>(this.formatters).ConfigureAwait(false);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            //Assert.Equal(StatusCodes.Status404NotFound, problemDetails.Status);
        }
    }
}
