using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Constants;
using WooliesX.TechChallenge.Api.Models;

namespace WooliesX.TechChallenge.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly User user = new User()
        {
            Name = ApiConstants.Name,
            Token = ApiConstants.Token
        };

        public Task<User> GetAsync()
        {
            return Task.FromResult(user);
        }
    }
}
