using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherAlertSystem
{
    class Program
    {
        /*
         * open source tools:
         * NUnit
         * RestSharp
         * 
         */
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IWeatherFeedClient, YahooWeatherFeedClient>()
                .AddSingleton<IRestClient, RestClient>()
                .BuildServiceProvider();

            var weatherFeedClient = serviceProvider.GetService<IWeatherFeedClient>();
            weatherFeedClient.GetForecastEvents();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
