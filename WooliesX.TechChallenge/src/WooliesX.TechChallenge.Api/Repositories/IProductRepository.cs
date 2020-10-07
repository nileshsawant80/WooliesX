using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Constants;
using WooliesX.TechChallenge.Api.Models;

namespace WooliesX.TechChallenge.Api.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(SortOption sortOption);
    }
}