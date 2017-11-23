using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using Exceptionless;
using WW.WeatherFeedClient.WeatherAlerts;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherFeedClient.Tests.WeatherAlerts
{
    [TestFixture]
    public abstract class WeatherAlertGeneratorTests
    {
        private WeatherAlertGenerator _sut;
        private List<WeatherFeedEvent> _weatherFeedEvents;

        [SetUp]
        public void PrepareWeatherAlertGenerator()
        {
            _weatherFeedEvents = Enumerable.Empty<WeatherFeedEvent>().ToList();
            _sut = new WeatherAlertGenerator();
        }

        public sealed class EmitAlertsMethod : WeatherAlertGeneratorTests
        {
            private const int HighHeatLimitDegreesF = 85;
            private const int FreezingLimitDegreesF = 32;

            [Test]
            public void Should_not_emit_alerts_when_there_are_no_weather_feed_events()
            {
                //act & assert
                _sut.EmitAlerts(Enumerable.Empty<WeatherFeedEvent>()).Should().BeEmpty();
            }

            [Test]
            public void Should_not_emit_alerts_when_there_are_no_alertable_weather_feed_events()
            {
                //arrange
                _weatherFeedEvents.Add(new WeatherFeedEvent{Event = RandomData.GetString(10,10)});

                //act & assert
                _sut.EmitAlerts(_weatherFeedEvents).Should().BeEmpty();
            }

            [TestCaseSource(nameof(_alertableEvents))]
            public void Should_emit_an_alert_when_there_is_a_alertable_event_in_the_weather_feed_events(string alertableEvent)
            {
                //arrange
                _weatherFeedEvents.Add(new WeatherFeedEvent
                {
                    Event = alertableEvent,
                    High = RandomData.GetInt(FreezingLimitDegreesF, HighHeatLimitDegreesF),
                    Low = RandomData.GetInt(FreezingLimitDegreesF, HighHeatLimitDegreesF)
                });

                //act
                var alerts = _sut.EmitAlerts(_weatherFeedEvents);

                //assert
                // mapper testing will cover the rest of the properties
                alerts.Single().Event.Should().Be(alertableEvent);
            }

            [Test]
            public void Should_emit_a_high_heat_alert_when_the_high_temperature_exceeds_the_high_heat_temperature_limit()
            {
                //arrange
                _weatherFeedEvents.Add(new WeatherFeedEvent
                {
                    Event = RandomData.GetString(10,10),
                    High = HighHeatLimitDegreesF+1,
                    Low = RandomData.GetInt(FreezingLimitDegreesF, HighHeatLimitDegreesF)
                });

                //act
                var alerts = _sut.EmitAlerts(_weatherFeedEvents);

                //assert
                alerts.Single().Event.Should().Be("High heat");
            }

            [Test]
            public void Should_emit_a_freezing_temperature_alert_when_the_low_temperature_is_below_the_freezing_temperature_limit()
            {
                //arrange
                _weatherFeedEvents.Add(new WeatherFeedEvent
                {
                    Event = RandomData.GetString(10, 10),
                    High = RandomData.GetInt(FreezingLimitDegreesF, HighHeatLimitDegreesF),
                    Low = FreezingLimitDegreesF -1
                });

                //act
                var alerts = _sut.EmitAlerts(_weatherFeedEvents);

                //assert
                alerts.Single().Event.Should().Be("Freezing temperature");
            }

            private readonly IEnumerable<string> _alertableEvents = new[] {"Rain", "Thunderstorms", "Snow", "Ice" };
        }
    }
}
