using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
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
using WooliesX.TechChallenge.Api.Commands;
using WooliesX.TechChallenge.Api.Controllers;
using WooliesX.TechChallenge.Api.Mappers;
using WooliesX.TechChallenge.Api.Repositories;
using WooliesX.TechChallenge.Api.Tests.Fixtures;
using WooliesX.TechChallenge.Api.ViewModels;
using Xunit;
using Xunit.Abstractions;

namespace WooliesX.TechChallenge.Api.Tests.Controllers
{
    public class UserControllerTest
    {
        private readonly HttpClient client;
        private readonly UserController _controllerUnderTest;
        private readonly IGetUserCommand getUserCommand;
        public UserControllerTest(ITestOutputHelper testOutputHelper)
        {
            var logger = Mock.Of<ILogger<GetUserCommand>>();
            var mapper = new UserToUserMapper();
            var userRepo = new UserRepository();
            getUserCommand = new GetUserCommand(userRepo, mapper, logger);
            _controllerUnderTest = new UserController();
        }

        [Fact]
        public async Task Get_UserFound_Returns200OkAsync()
        {
            var user = new Models.User() { Name="Nilesh", Token= "8e2c03a8-8267-4490-a9d4-d5d5a882cc24" };

            var result = await _controllerUnderTest.GetAsync(getUserCommand, new CancellationToken());
            var okObjectResult = result as OkObjectResult;

            var respUser = okObjectResult.Value as User;

            Assert.Equal(200, okObjectResult.StatusCode);
            //Assert.Equal(user.Name,respUser.Name);
            //Assert.Equal(user.Token, respUser.Token);
        }
    }
}
