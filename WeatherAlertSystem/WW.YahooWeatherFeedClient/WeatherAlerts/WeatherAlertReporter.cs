using System.Collections.Generic;
using System.Linq;
using WW.WeatherFeedClient.WeatherAlerts.WeatherFeed;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherFeedClient.WeatherAlerts
{
    public interface IWeatherAlertReporter
    {
        IEnumerable<AlertableWeatherEvent> GetWeatherAlerts();
    }

    public sealed class WeatherAlertReporter : IWeatherAlertReporter
    {
        private readonly IWeatherFeedClient _weatherFeedClient;

        public WeatherAlertReporter(IWeatherFeedClient weatherFeedClient)
        {
            _weatherFeedClient = weatherFeedClient;
        }

        public IEnumerable<AlertableWeatherEvent> GetWeatherAlerts()
        {
            //TODO - filter on events of interest
            var forecastEvents = _weatherFeedClient.GetForecastEvents();

            return Enumerable.Empty<AlertableWeatherEvent>(); ;
        }
    }
}
