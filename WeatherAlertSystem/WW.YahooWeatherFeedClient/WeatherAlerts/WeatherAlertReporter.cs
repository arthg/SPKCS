using System.Collections.Generic;
using System.Linq;
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
        private readonly IWeatherAlertGenerator _weatherAlertGenerator;

        public WeatherAlertReporter(IWeatherFeedClient weatherFeedClient, IWeatherAlertGenerator weatherAlertGenerator)
        {
            _weatherFeedClient = weatherFeedClient;
            _weatherAlertGenerator = weatherAlertGenerator;
        }

        public IEnumerable<AlertableWeatherEvent> GetWeatherAlerts()
        {
            //TODO - filter on events of interest
            var forecastEvents = _weatherFeedClient.GetForecastEvents();

            return Enumerable.Empty<AlertableWeatherEvent>(); ;
        }
    }
}
