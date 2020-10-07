using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Constants;
using WooliesX.TechChallenge.Api.Mappers;
using WooliesX.TechChallenge.Api.Repositories;
using WooliesX.TechChallenge.Api.ViewModels;

namespace WooliesX.TechChallenge.Api.Commands
{
    public class GetProductsCommand : IGetProductsCommand
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper<List<Models.Product>, List<Product>> productMapper;
        private readonly ILogger logger;

        public GetProductsCommand(
            IProductRepository productRepository,
            IMapper<List<Models.Product>, List<Product>> productMapper,
            ILogger<GetProductsCommand> logger)
        {
            this.productRepository = productRepository;
            this.productMapper = productMapper;
            this.logger = logger;
        }

        public async Task<IActionResult> ExecuteAsync(string sortOptionInput, CancellationToken cancellationToken)
        {
            var sort = ParseEnum<SortOption>(sortOptionInput);
            var sortOption = Enum.IsDefined(typeof(SortOption), sort) ? sort : SortOption.Ascending;
            this.logger.LogInformation($"SortOptionInput={sortOption.ToString()}");

            var products = await this.productRepository.GetProductsAsync(sortOption).ConfigureAwait(false);
            if (products is null)
            {
                this.logger.LogError($"NotFoundResult");
                return new NotFoundResult();
            }

            var productViewModel = new List<Product>();
            this.productMapper.Map(products, productViewModel);
            this.logger.LogDebug($"Sorting succeeded.");
            return new OkObjectResult(products);
        }

        private static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
    }
}
