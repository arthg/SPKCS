using System;
using System.Collections.Generic;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherFeedClient.WeatherAlerts
{
    public interface IWeatherAlertGenerator
    {
        IEnumerable<AlertableWeatherEvent> EmitAlerts(IEnumerable<WeatherFeedEvent> weatherFeedEvents);
    }

    public sealed class WeatherAlertGenerator : IWeatherAlertGenerator
    {
        public IEnumerable<AlertableWeatherEvent> EmitAlerts(IEnumerable<WeatherFeedEvent> weatherFeedEvents)
        {
            throw new NotImplementedException();
        }
    }
}
