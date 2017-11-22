using System.Collections.Generic;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherFeedClient.WeatherAlerts
{
    public interface IWeatherAlertReporter
    {
        IEnumerable<WeatherFeedEvent> GetWeatherAlerts();
    }

    public sealed class WeatherAlertReporter : IWeatherAlertReporter
    {
        private readonly IWeatherFeedClient _weatherFeedClient;

        public WeatherAlertReporter(IWeatherFeedClient weatherFeedClient)
        {
            _weatherFeedClient = weatherFeedClient;
        }

        public IEnumerable<WeatherFeedEvent> GetWeatherAlerts()
        {
            //TODO - filter on events of interest

            return _weatherFeedClient.GetForecastEvents();
        }
    }
}
