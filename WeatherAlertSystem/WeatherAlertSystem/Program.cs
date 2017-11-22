using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using System.Linq;
using WW.WeatherFeedClient.WeatherAlerts;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherAlertSystem
{
    class Program
    {
        /*
         * open source tools:
         * NUnit
         * RestSharp
         * AutoMapper
         * FluentAssertions
         * Exceptionless.RandomData
         * 
         */
        static void Main(string[] args)
        {
            //TODO: use discovery
            Mapper.Initialize(m => m.AddProfile(new WeatherFeedMapperProfile()));

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IWeatherFeedClient, YahooWeatherFeedClient>()
                .AddSingleton<IRestClient, RestClient>()
                .AddSingleton<IWeatherAlertReporter, WeatherAlertReporter>()
                .AddSingleton<IWeatherAlertGenerator, WeatherAlertGenerator>()
                .BuildServiceProvider();

            var weatherAlertReporter = serviceProvider.GetService<IWeatherAlertReporter>();
            var alerts = weatherAlertReporter.GetWeatherAlerts().ToArray();
            if (alerts.Any())
            {
                //TODO - extract to some kind of testable formatter
                alerts.ToList().ForEach(a => Console.WriteLine($"{a.Day} {a.Date} {a.Event}"));
            }
            else
            {
                Console.WriteLine("No weather alerts at this time.");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
