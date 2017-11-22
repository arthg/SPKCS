using AutoMapper;
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
                .BuildServiceProvider();

            var weatherFeedClient = serviceProvider.GetService<IWeatherFeedClient>();
            weatherFeedClient.GetForecastEvents();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
