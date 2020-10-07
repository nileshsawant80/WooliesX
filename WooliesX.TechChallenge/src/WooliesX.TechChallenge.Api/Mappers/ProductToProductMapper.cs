using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.ViewModels;

namespace WooliesX.TechChallenge.Api.Mappers
{
    public class ProductToProductMapper : IMapper<List<Models.Product>, List<Product>>
    {
        public void Map(List<Models.Product> source, List<Product> destination)
        {
            if (source is null || source.Count==0)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination is null )
            {
                throw new ArgumentNullException(nameof(destination));
            }

            source.ForEach(s => destination.Add(new Product(s.Name, s.Price, s.Quantity)));
            
        }
    }
}
