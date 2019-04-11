using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;

namespace ForecastLibrary
{
    /// <summary>
    /// Class to process the forecast request.
    /// </summary>
    public class ForecastProcessor
    {
        /// <summary>
        /// This method is to get the forecast of next 5 days.
        /// </summary>
        /// <returns>Forecast details of next 5 days.</returns>
        public static async Task<Forecast> GetForecast(string url)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Forecast forecast = await response.Content.ReadAsAsync<Forecast>();
                    return forecast;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// This method is to get the number of days having temperature 
        /// greater than the given temperature in next 5 days.
        /// </summary>
        /// <param name="maxTemp">Given temperature in degrees.</param>
        /// <param name="forecast">Forecast of next 5 days.</param>
        /// <returns>Number of days.</returns>
        public static int GetNumberOfDaysWithTempAbove(int maxTemp, Forecast forecast)
        {
            int aboveMaxTempCount = 0;

            if (forecast != null)
            {
                //Filter days with temperature above given temperature.
                List<ForecastDetails> aboveMaxTempDayDetails = forecast.List.Where(x => (Convert.ToDouble(x.Main.Temp) >= maxTemp)).ToList();

                string date = string.Empty;
                foreach (var day in aboveMaxTempDayDetails)
                {
                    var localDateTime = DateTimeOffset.Parse(day.Dt_txt).ToLocalTime(); //Convert UTC to local time.
                    if (date != localDateTime.Date.ToShortDateString())
                    {
                        aboveMaxTempCount++;
                        date = localDateTime.Date.ToShortDateString();
                    }
                }
            }
            return aboveMaxTempCount;
        }

        /// <summary>
        /// This method is to get the no of days with given weather condition.
        /// </summary>
        /// <param name="condition">Weather parameters (Rain, Clear, Snow, Extreme etc.)</param>
        /// <param name="forecast">Forecast of next 5 days.</param>
        /// <returns>Number of days.</returns>
        public static int GetNumberOfWithWeatherCondition(string condition, Forecast forecast)
        {
            int sunnyDayCount = 0;
            string sunnyDay = string.Empty;
            if (forecast != null)
            {
                foreach (var day in forecast.List)
                {
                    foreach (var weather in day.Weather)
                    {
                        var localDateTime = DateTimeOffset.Parse(day.Dt_txt).ToLocalTime();
                        if (weather.Main.Equals(condition) && sunnyDay != localDateTime.Date.ToShortDateString())
                        {
                            sunnyDayCount++;
                            sunnyDay = localDateTime.Date.ToShortDateString();
                        }
                    }
                }
            }
            return sunnyDayCount;
        }
    }
}
