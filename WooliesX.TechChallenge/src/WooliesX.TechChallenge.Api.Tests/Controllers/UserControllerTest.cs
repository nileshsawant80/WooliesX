using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Tests.Fixtures;
using WooliesX.TechChallenge.Api.ViewModels;
using Xunit;
using Xunit.Abstractions;

namespace WooliesX.TechChallenge.Api.Tests.Controllers
{
    public class UserControllerTest : CustomWebApplicationFactory<Startup>
    {
        private readonly HttpClient client;
        private readonly MediaTypeFormatterCollection formatters;

        public UserControllerTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            this.client = this.CreateClient();
            this.formatters = new MediaTypeFormatterCollection();
            this.formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(Fixtures.ContentType.RestfulJson));
            this.formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(Fixtures.ContentType.ProblemJson));
        }

        [Fact]
        public async Task Get_UserFound_Returns200OkAsync()
        {
            var user = new Models.User() { Name="Nilesh", Token="" };
            this.UserRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(user);

            var response = await this.client.GetAsync(new Uri("/api/answers/User", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(new DateTimeOffset(2000, 1, 2, 3, 4, 5, TimeSpan.FromHours(6)), response.Content.Headers.LastModified);
            Assert.Equal(Fixtures.ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            var carViewModel = await response.Content.ReadAsAsync<User>(this.formatters).ConfigureAwait(false);
        }

        [Fact]
        public async Task Get_CarNotFound_Returns404NotFoundAsync()
        {
            this.UserRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync((Models.User)null);

            var response = await this.client.GetAsync(new Uri("/api/answers/User", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var problemDetails = await response.Content.ReadAsAsync<string>(this.formatters).ConfigureAwait(false);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            //Assert.Equal(StatusCodes.Status404NotFound, problemDetails.Status);
        }

    }
}
