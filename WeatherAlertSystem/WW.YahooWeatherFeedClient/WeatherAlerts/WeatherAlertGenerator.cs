using System;
using System.Collections.Generic;
using WW.WeatherFeedClient.WeatherAlerts.WeatherFeed;

namespace WW.WeatherFeedClient.WeatherAlerts
{
    public interface IWeatherAlertGenerator
    {
        IEnumerable<AlertableWeatherEvent> EmitAlerts();
    }

    public sealed class WeatherAlertGenerator : IWeatherAlertGenerator
    {
        public IEnumerable<AlertableWeatherEvent> EmitAlerts()
        {
            throw new NotImplementedException();
        }
    }
}
