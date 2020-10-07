using System;
using WooliesX.TechChallenge.Api.ViewModels;

namespace WooliesX.TechChallenge.Api.Mappers
{
    public class UserToUserMapper : IMapper<Models.User, User>
    {
        public void Map(Models.User source, User destination)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Name = source.Name;
            destination.Token = source.Token;
        }
    }
}
