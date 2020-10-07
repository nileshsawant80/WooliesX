using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Models;

namespace WooliesX.TechChallenge.Api.Services
{
    /// <summary>
    /// Retrieves the product data.
    /// </summary>
    public interface IResourceService
    {
        public Task<List<Product>> GetProductsAsync();
        public Task<List<ShopperHistory>> GetShopperHistoryAsync();

    }
}
