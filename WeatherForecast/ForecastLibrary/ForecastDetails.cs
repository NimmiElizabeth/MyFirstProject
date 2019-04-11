using System.Collections.Generic;

namespace ForecastLibrary
{
    /// <summary>
    /// Details regarding forecast.
    /// </summary>
    public class ForecastDetails
    {
        /// <summary>
        /// Temperature details.
        /// </summary>
        public TemperatureDetails Main { get; set; }

        /// <summary>
        /// Data/time of calculation, UTC.
        /// </summary>
        public string Dt_txt { get; set; }

        /// <summary>
        /// List of weather details.
        /// </summary>
        public List<WeatherDetails> Weather { get; set; }
    }
}
