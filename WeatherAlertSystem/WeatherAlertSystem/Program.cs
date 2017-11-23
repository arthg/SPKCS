using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using System.Linq;
using WW.WeatherFeedClient.Common;
using WW.WeatherFeedClient.WeatherAlerts;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherAlertSystem
{
    class Program
    {        
        public static void Main(string[] parameters)
        {
            BootstrapMappers();
            
            var weatherAlertReporter = BootstrapWeatherAlertReporter();

            try
            {
                var alerts = weatherAlertReporter.GetWeatherAlerts().ToArray();
                if (alerts.Any())
                {
                    alerts.ForEach(a => Console.WriteLine(a.ToString()));
                }
                else
                {
                    Console.WriteLine("No weather alerts at this time.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Oh snap, that didn't work.  So sorry, please try again later.");
                //TODO: swallow the exception but there should be some logging here
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private static void BootstrapMappers()
        {
            Mapper.Initialize(m =>
            {
                m.AddProfile(new WeatherFeedMapperProfile());
                m.AddProfile(new WeatherAlertMapperProfile());
            });
        }

        private static IWeatherAlertReporter BootstrapWeatherAlertReporter()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IWeatherFeedClient, YahooWeatherFeedClient>()
                .AddSingleton<IRestClient, RestClient>()
                .AddSingleton<IWeatherAlertReporter, WeatherAlertReporter>()
                .AddSingleton<IWeatherAlertGenerator, WeatherAlertGenerator>()
                .BuildServiceProvider();

            return serviceProvider.GetService<IWeatherAlertReporter>();
        }
    }
}
