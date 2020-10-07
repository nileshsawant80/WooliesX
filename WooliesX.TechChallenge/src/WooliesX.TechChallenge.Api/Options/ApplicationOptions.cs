using System.ComponentModel.DataAnnotations;

namespace WooliesX.TechChallenge.Api.Options
{
    /// <summary>
    /// All options for the application.
    /// </summary>
    public class ApplicationOptions
    {
        [Required]
        public ResourceServiceOptions ResourceServiceOptions { get; set; }
    }
}
