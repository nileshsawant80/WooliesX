using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesX.TechChallenge.Api.Options
{
    public class ResourceServiceOptions
    {
        public string BaseAddress { get; set; }
        public string Timeout { get; set; }
        public string UrlPathBase { get; set; }
        public string ProductsPath { get; set; }
        public string ShopperHistoryPath { get; set; }
        public string UrlParamToken { get; set; }
    }
}
