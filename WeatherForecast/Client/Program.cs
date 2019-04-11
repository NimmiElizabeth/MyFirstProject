using ForecastLibrary;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Timers;

namespace Client
{
    class Program
    {
        /// <summary>
        /// Key of url.
        /// </summary>
        private const string CONFIG_URL = "forecasturl";

        /// <summary>
        /// Url of the API.
        /// </summary>
        private static string url = string.Empty;

        /// <summary>
        /// The entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ApiHelper.InitializeClient();
            try
            {
                url = GetForecastUrlFromConfig();
                DisplayForecastDetails().Wait();
                //To update the forecast every 10 minutes.
                Timer timer = new Timer(600000);
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }            
            Console.ReadKey();
        }

        /// <summary>
        /// This method is to read the API url from config file.
        /// </summary>
        /// <returns>The url.</returns>
        private static string GetForecastUrlFromConfig()
        {
            var forecastUrl = ConfigurationManager.AppSettings[CONFIG_URL];
            return forecastUrl ?? string.Empty;
        }

        /// <summary>
        /// This methos is to update the forecast every 10 minutes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                DisplayForecastDetails().Wait();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }
       
        /// <summary>
        /// This method is to display the forecast details.
        /// </summary>
        /// <returns></returns>
        private static async Task DisplayForecastDetails()
        {
            Forecast forecast = await ForecastProcessor.GetForecast(url);
            int above20DegreeDays = ForecastProcessor.GetNumberOfDaysWithTempAbove(20, forecast);
            int sunnyDays = ForecastProcessor.GetNumberOfWithWeatherCondition("Clear", forecast);

            Console.WriteLine("In next 5 days");
            Console.WriteLine("--------------");
            Console.WriteLine("The number of days have temperature above 20 degrees = " + above20DegreeDays);
            Console.WriteLine("The number of sunny days = " + sunnyDays);

            Console.WriteLine("N.B 'Clear' days are taken as 'Sunny' days.");
            Console.WriteLine("Updated at " + DateTime.Now);
        }
    }
}
