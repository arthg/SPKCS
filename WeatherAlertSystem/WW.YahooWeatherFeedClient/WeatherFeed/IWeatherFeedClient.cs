using System.Collections.Generic;

namespace WW.WeatherFeedClient.WeatherFeed
{
    public interface IWeatherFeedClient
    {
        IEnumerable<WeatherFeedEvent> GetForecastEvents();
    }
}
