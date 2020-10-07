using Newtonsoft.Json;

namespace WooliesX.TechChallenge.Api.ViewModels
{

    /// <summary>
    /// A make and model of car.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <example>Niel</example>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user token.
        /// </summary>
        /// <example>xyz</example>
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
