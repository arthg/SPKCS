using System.Collections.Generic;

namespace WW.WeatherFeedClient.WeatherFeed
{
    public interface IAlertableWeatherEvent
    {
        IEnumerable<WeatherFeedEvent> GetForecastEvents();
    }
}
