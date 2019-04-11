using ForecastLibrary;
using System.Timers;
using System.Windows;

namespace ForecastClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayForecastDetails();
            Timer t = new Timer(1000);
            t.Elapsed += TimerElapsed;
            t.Start();                        
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            WeatherMap.Text = string.Empty;
            DisplayForecastDetails();
        }

        private async void DisplayForecastDetails()
        {
            Forecast forecast = await ForecastProcessor.GetForecast();
            int above20DegreeDays = ForecastProcessor.GetNumberOfDaysWithTempAbove(20, forecast);
            int sunnyDays = ForecastProcessor.GetNumberOfWithWeatherCondition("Clear", forecast);

            WeatherMap.Text += "In next 5 days" + "\n";
            WeatherMap.Text += "--------------" + "\n";
            WeatherMap.Text += "The number of days have temperature above 20 degrees = " + above20DegreeDays + "\n";
            WeatherMap.Text += "The number of sunny days = " + sunnyDays + "\n";

            WeatherMap.Text += "\n N.B 'Clear' days are taken as 'Sunny' days.";
        }
    }
}
