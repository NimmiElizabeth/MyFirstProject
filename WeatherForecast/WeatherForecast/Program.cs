using ForecastLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiHelper.InitializeClient();
            GetWeatherForecast();
        }
        private async static void GetWeatherForecast()
        {
           await ForecastProcessor.ForecastWeather();
        }
    }
}
