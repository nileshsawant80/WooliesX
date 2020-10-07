using WooliesX.TechChallenge.Api.Models;

namespace WooliesX.TechChallenge.Api.Mappers
{
    public interface IMapper<in TSource, in TDestination>
    {
        /// <summary>
        /// Maps an object of type TSource to TDestination.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        void Map(TSource source, TDestination destination);
    }
}