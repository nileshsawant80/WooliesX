using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WooliesX.TechChallenge.Api.Commands;
using WooliesX.TechChallenge.Api.Constants;
using WooliesX.TechChallenge.Api.ViewModels;

namespace WooliesX.TechChallenge.Api.Controllers
{
    /// <summary>
    /// User controller to provide user details
    /// </summary>
    [ApiController]
    [Route("api/answers/[controller]")]
    [ApiVersion(ApiVersionName.V1)]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Gets the User with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the User or a 404 Not Found if a User with the specified unique
        /// identifier was not found.</returns>
        [HttpGet(Name = ControllerRoute.GetUser)]
        [SwaggerOperation(Summary = "Get User", OperationId = "GetUser")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetAsync(
            [FromServices] IGetUserCommand command,
            CancellationToken cancellationToken) => command.ExecuteAsync(cancellationToken);
    }
}
