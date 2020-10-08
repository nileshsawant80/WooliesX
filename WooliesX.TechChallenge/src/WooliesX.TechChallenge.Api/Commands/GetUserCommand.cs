using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Mappers;
using WooliesX.TechChallenge.Api.Repositories;
using WooliesX.TechChallenge.Api.ViewModels;

namespace WooliesX.TechChallenge.Api.Commands
{
    public class GetUserCommand : IGetUserCommand
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper<Models.User, User> userMapper;
        private readonly ILogger<GetUserCommand> logger;

        public GetUserCommand(
            IUserRepository UserRepository,
            IMapper<Models.User, User> UserMapper,
            ILogger<GetUserCommand> logger)
        {
            this.userRepository = UserRepository;
            this.userMapper = UserMapper;
            this.logger = logger;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var user = await this.userRepository.GetAsync().ConfigureAwait(false);
            if (user is null)
            {
                logger.LogError($"usr not found.");
                return new NotFoundResult();
            }

            var userViewModel = new User();
            userMapper.Map(user, userViewModel);
            this.logger.LogDebug($"user found -  {JsonConvert.SerializeObject(userViewModel)}");
            return new OkObjectResult(userViewModel);
        }
    }
}
