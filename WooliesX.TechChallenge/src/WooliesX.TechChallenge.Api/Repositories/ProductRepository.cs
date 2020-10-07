using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Constants;
using WooliesX.TechChallenge.Api.Services;

namespace WooliesX.TechChallenge.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IResourceService resourceService;
        public ProductRepository(IResourceService resourceService)
        {
            this.resourceService = resourceService;
        }

        /// <summary>
        /// Returns a product list based on the given sort option.
        /// "Low" - Low to High Price
        /// "High" - High to Low Price
        /// "Ascending" - A - Z sort on the Name
        /// "Descending" - Z - A sort on the Name
        /// "Recommended" - this will call the "shopperHistory" resource to get a list of customers orders and needs to return based on popularity
        /// </summary>
        /// <param name="sortOption">Sort option</param>
        /// <returns>A list of products</returns>
        public async Task<List<Models.Product>> GetProductsAsync(SortOption sortOption)
        {
            return await GetSortedProductsAsync(sortOption).ConfigureAwait(false);
        }

        private async Task<List<Models.Product>> GetSortedProductsAsync(SortOption sortOption)
        {
            var products = await this.resourceService.GetProductsAsync();
            if (products == null || products.Count == 0)
            {
                return products;
            }

            List<Models.Product> result = null;
            result = sortOption switch
            {
                SortOption.High => products.OrderByDescending(o => o.Price).ToList(),
                SortOption.Low => products.OrderBy(o => o.Price).ToList(),
                SortOption.Ascending => products.OrderBy(o => o.Name).ToList(),
                SortOption.Descending => products.OrderByDescending(o => o.Name).ToList(),
                SortOption.Recommended => await GetSortedRecomendedProductsAsync(),
                _ => products,
            };
            return result;
        }


        /// <summary>
        /// The method returns a summarised shopper history. In other words this method returns a list of 
        /// products with total quantities from the history.
        /// </summary>
        /// <returns>A list of products</returns>
        private async Task<List<Models.Product>> GetSortedRecomendedProductsAsync()
        {
            var shopperHistories = await this.resourceService.GetShopperHistoryAsync();
            var productsDict = new Dictionary<string, Models.SoldProduct>();

            foreach (Models.ShopperHistory shopperHistory in shopperHistories)
            {
                foreach (Models.Product product in shopperHistory.Products)
                {
                    if (productsDict.ContainsKey(product.Name))
                    {
                        productsDict[product.Name].Quantity += product.Quantity;
                        productsDict[product.Name].SoldCount++;
                    }
                    else
                    {
                        productsDict.Add(product.Name, new Models.SoldProduct(1, product));
                    }
                }
            }

            var products = productsDict.Values.ToList().OrderByDescending(p => p.SoldCount).ThenByDescending(p => p.Quantity).Select(p => p.ToProduct(p)).ToList();
            return await Task.FromResult(products);
        }
    }
}
