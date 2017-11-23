using System.Collections.Generic;
using System.Linq;
using WW.WeatherFeedClient.Common;
using WW.WeatherFeedClient.WeatherFeed;
using IMapper = WW.WeatherFeedClient.Common.IMapper;

namespace WW.WeatherFeedClient.WeatherAlerts
{
    public interface IWeatherAlertGenerator
    {
        IEnumerable<AlertableWeatherEvent> EmitAlerts(IEnumerable<WeatherFeedEvent> weatherFeedEvents);
    }

    public sealed class WeatherAlertGenerator : IWeatherAlertGenerator
    {
        private readonly IMapper _mapper;
        private const int HighHeatLimitDegreesF = 85;
        private const int FreezingLimitDegreesF = 32;
        private readonly IEnumerable<string> _alertableEvents = new[] {"Rain", "Thunderstorms", "Snow", "Ice" };

        public WeatherAlertGenerator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<AlertableWeatherEvent> EmitAlerts(IEnumerable<WeatherFeedEvent> weatherFeedEvents)
        {
            var alerts = new List<AlertableWeatherEvent>();
            weatherFeedEvents.ForEach(e =>
            {
                alerts.AddRange(GetAlertsForEvent(e));
            });
            return alerts;
        }

        private IEnumerable<AlertableWeatherEvent> GetAlertsForEvent(WeatherFeedEvent weatherFeedEvent)
        {
            var alerts = new List<AlertableWeatherEvent>();
            if (_alertableEvents.Any(e => e == weatherFeedEvent.Event))
            {
                AddAlert(weatherFeedEvent, alerts);
            }
            if (IsHeatHeatEvent(weatherFeedEvent))
            {
                AddAlert(weatherFeedEvent, alerts, "High heat");
            }
            if (IsFreezingTemperatureEvent(weatherFeedEvent))
            {
                AddAlert(weatherFeedEvent, alerts, "Freezing temperature");
            }
            return alerts;
        }

        private static bool IsHeatHeatEvent(WeatherFeedEvent weatherFeedEvent)
        {
            return weatherFeedEvent.High > HighHeatLimitDegreesF;
        }

        private static bool IsFreezingTemperatureEvent(WeatherFeedEvent weatherFeedEvent)
        {
            return weatherFeedEvent.Low < FreezingLimitDegreesF;
        }

        private void AddAlert(WeatherFeedEvent weatherFeedEvent, ICollection<AlertableWeatherEvent> alerts, string @event = null)
        {
            var alert = _mapper.Map<WeatherFeedEvent, AlertableWeatherEvent>(weatherFeedEvent);
            if (@event != null)
            {
                alert.Event = @event;
            }
            alerts.Add(alert);
        }
    }
}
