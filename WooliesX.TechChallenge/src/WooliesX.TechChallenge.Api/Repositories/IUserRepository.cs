using System.Threading.Tasks;

namespace WooliesX.TechChallenge.Api.Repositories
{
    public interface IUserRepository
    {
        Task<Models.User> GetAsync();
    }
}