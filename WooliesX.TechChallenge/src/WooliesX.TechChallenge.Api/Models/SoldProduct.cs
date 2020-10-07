using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesX.TechChallenge.Api.Models
{
    /// <summary>
    /// SoldProduct data from shopping history
    /// </summary>
    public sealed class SoldProduct
    {
        public SoldProduct(long soldCount, Product product)
        {
            SoldCount = soldCount;
            Name = product.Name;
            Price = product.Price;
            Quantity = product.Quantity;
        }

        public long SoldCount { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }


        public Product ToProduct(SoldProduct soldProduct)
        {
            return new Product { Name = soldProduct.Name, Quantity = soldProduct.Quantity, Price = soldProduct.Price };
        }
    }
}
