using System.Collections.Generic;

namespace ForecastLibrary
{
    /// <summary>
    /// Class to map the json response.
    /// </summary>
    public class Forecast
    {
        /// <summary>
        /// List of forecast details.
        /// </summary>
        public List<ForecastDetails> List { get; set; }
    }
}
