using System.Net.Http;
using System.Net.Http.Headers;

namespace ForecastLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static HttpClient ApiClient { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
