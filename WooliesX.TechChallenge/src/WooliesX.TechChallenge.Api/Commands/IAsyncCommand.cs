using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WooliesX.TechChallenge.Api.Commands
{
    /// <summary>
    /// Executes a single command and returns a result.
    /// </summary>
    public interface IAsyncCommand
    {    
        /// <summary>
        /// Executes the command asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the command.</returns>
        Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Executes a single command and returns a result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncCommand<T>
    {  
        /// <summary>
        /// Executes the command asynchronously.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The result of the command.</returns>
        Task<IActionResult> ExecuteAsync(T parameter, CancellationToken cancellationToken = default);
    }
}