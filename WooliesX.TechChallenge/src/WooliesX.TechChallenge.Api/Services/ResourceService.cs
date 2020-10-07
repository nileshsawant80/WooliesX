using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Constants;
using WooliesX.TechChallenge.Api.Models;
using WooliesX.TechChallenge.Api.Options;

namespace WooliesX.TechChallenge.Api.Services
{
    /// <summary>
    /// Retrieves the current date and/or time. Helps with unit testing by letting you mock the system clock.
    /// </summary>
    public class ResourceService : IResourceService
    {
        //private readonly HttpClient httpClient;
        private readonly ResourceServiceOptions options;

        //Todo : Fix dependency injection error
        //public ResourceService(HttpClient httpClient, IOptions<ResourceServiceOptions> options)
        //{
        //    this.httpClient = httpClient;
        //    this.options = options.Value;
        //}

        public ResourceService(IOptions<ResourceServiceOptions> options)
        {
            this.options = options.Value;
            //this.httpClient = new HttpClient();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var endpoint = $"{this.options.BaseAddress}{this.options.UrlPathBase}{this.options.ProductsPath}";
            var result = await this.GetResourceAsync<List<Product>>(endpoint);
            if (result == default(List<Product>))
            {
                throw new Exception("Failed to get the products.");
            }

            return result;
        }

        public async Task<List<ShopperHistory>> GetShopperHistoryAsync()
        {
            var endpoint = $"{this.options.BaseAddress}{this.options.UrlPathBase}{this.options.ShopperHistoryPath}";
            var result = await this.GetResourceAsync<List<ShopperHistory>>(endpoint);
            if (result == default(List<ShopperHistory>))
            {
                throw new Exception("Failed to get the ShopperHistory.");
            }

            return result;
        }

        private async Task<T> GetResourceAsync<T>(string endpoint)
        {
            HttpResponseMessage httpResponse;
            try
            {
                UriBuilder builder = new UriBuilder(endpoint)
                {
                    Query = $"token={ApiConstants.Token}"
                };

                using var httpClient = new HttpClient();
                //httpClient.BaseAddress = new Uri(options.BaseAddress);
                //httpClient.Timeout = TimeSpan.Parse(options.Timeout);
                //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpResponse = await httpClient.GetAsync(builder.Uri).ConfigureAwait(false);
                //Log.Debug($"service response status - {httpResponse.StatusCode} and response={httpResponse}");
            }
            catch (Exception ex)
            {
                //Log.Error($"Error Calling service Exception:{ex}");
                throw new Exception(ex.Message);
            }

            httpResponse.EnsureSuccessStatusCode();
            var responseString = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
