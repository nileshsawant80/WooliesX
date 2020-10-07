using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.TechChallenge.Api.Commands;
using WooliesX.TechChallenge.Api.Constants;
using WooliesX.TechChallenge.Api.ViewModels;

namespace WooliesX.TechChallenge.Api.Controllers
{
    [Route("sort")]
    [ApiController]
    [ApiVersion(ApiVersionName.V1)]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Gets the sorted product list.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="sortOption"></param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the User or a 404 Not Found if a User with the specified unique
        /// identifier was not found.</returns>//"{sortOption}", 
        [HttpGet(Name = ControllerRoute.GetProducts)]
        [SwaggerOperation(Summary = "Get Sorted Products", OperationId = "GetProducts")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetAsync(
            [FromServices] IGetProductsCommand command,
            [FromQuery] string sortOption,
            CancellationToken cancellationToken) => command.ExecuteAsync(sortOption, cancellationToken);
    }
}
