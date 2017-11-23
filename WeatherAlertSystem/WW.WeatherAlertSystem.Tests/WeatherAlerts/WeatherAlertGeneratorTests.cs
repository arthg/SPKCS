using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using Exceptionless;
using Moq;
using WW.WeatherFeedClient.Common;
using WW.WeatherFeedClient.WeatherAlerts;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherFeedClient.Tests.WeatherAlerts
{
    [TestFixture]
    public abstract class WeatherAlertGeneratorTests
    {
        private Mock<IMapper> _mapper;
        private WeatherAlertGenerator _sut;
        private WeatherFeedEvent _testWeatherFeedEvent;
        private List<WeatherFeedEvent> _weatherFeedEvents;

        private const int HighHeatLimitDegreesF = 85;
        private const int FreezingLimitDegreesF = 32;

        [SetUp]
        public void PrepareWeatherAlertGenerator()
        {
            _testWeatherFeedEvent = new WeatherFeedEvent
            {
                Event = RandomData.GetString(10, 10),
                High = RandomData.GetInt(FreezingLimitDegreesF, HighHeatLimitDegreesF),
                Low = RandomData.GetInt(FreezingLimitDegreesF, HighHeatLimitDegreesF)
            };
            _weatherFeedEvents = new List<WeatherFeedEvent>
            {
                _testWeatherFeedEvent
            };

            _mapper = new Mock<IMapper>(MockBehavior.Strict);
            _sut = new WeatherAlertGenerator(_mapper.Object);
        }

        public sealed class EmitAlertsMethod : WeatherAlertGeneratorTests
        {
            [Test]
            public void Should_not_emit_alerts_when_there_are_no_alertable_weather_feed_events()
            {
                //act & assert
                _sut.EmitAlerts(_weatherFeedEvents).Should().BeEmpty();
            }

            [Test]
            public void Should_not_emit_alerts_when_there_are_no_weather_feed_events()
            {
                //arrange
                _weatherFeedEvents = Enumerable.Empty<WeatherFeedEvent>().ToList();

                //act & assert
                _sut.EmitAlerts(Enumerable.Empty<WeatherFeedEvent>()).Should().BeEmpty();
            }

            [TestCaseSource(nameof(AlertableEvents))]
            public void Should_emit_an_alert_when_there_is_a_alertable_event_in_the_weather_feed_events(string alertableEvent)
            {
                //arrange
                _testWeatherFeedEvent.Event = alertableEvent;

                var alertableWeatherEvent = new AlertableWeatherEvent();
                _mapper
                    .Setup(m => m.Map<WeatherFeedEvent, AlertableWeatherEvent>(_testWeatherFeedEvent))
                    .Returns(alertableWeatherEvent);

                //act
                var alerts = _sut.EmitAlerts(_weatherFeedEvents);

                //assert
                alerts.Single().Should().BeSameAs(alertableWeatherEvent);
            }

            [Test]
            public void Should_emit_a_high_heat_alert_when_the_high_temperature_exceeds_the_high_heat_temperature_limit()
            {
                //arrange
                _testWeatherFeedEvent.High = HighHeatLimitDegreesF + 1;

                var alertableWeatherEvent = new AlertableWeatherEvent();
                _mapper
                    .Setup(m => m.Map<WeatherFeedEvent, AlertableWeatherEvent>(_testWeatherFeedEvent))
                    .Returns(alertableWeatherEvent);

                //act
                var alerts = _sut.EmitAlerts(_weatherFeedEvents).ToArray();

                //assert
                alerts.Single().Should().BeSameAs(alertableWeatherEvent);
                alerts.Single().Event.Should().Be("High heat");
            }

            [Test]
            public void Should_emit_a_freezing_temperature_alert_when_the_low_temperature_is_below_the_freezing_temperature_limit()
            {
                //arrange
                _testWeatherFeedEvent.Low = FreezingLimitDegreesF - 1;

                var alertableWeatherEvent = new AlertableWeatherEvent();
                _mapper
                    .Setup(m => m.Map<WeatherFeedEvent, AlertableWeatherEvent>(_testWeatherFeedEvent))
                    .Returns(alertableWeatherEvent);

                //act
                var alerts = _sut.EmitAlerts(_weatherFeedEvents).ToArray();

                //assert
                alerts.Single().Should().BeSameAs(alertableWeatherEvent);
                alerts.Single().Event.Should().Be("Freezing temperature");
            }

            [Test]
            public void Should_emit_multiple_alerts_when_more_than_one_alertable_event_is_detected_for_a_single_feed_event()
            {
                //arrange
                _testWeatherFeedEvent.Event = "Rain";
                _testWeatherFeedEvent.Low = FreezingLimitDegreesF - 1;

                _mapper
                    .SetupSequence(m => m.Map<WeatherFeedEvent, AlertableWeatherEvent>(_testWeatherFeedEvent))
                    .Returns(new AlertableWeatherEvent{Event = "Some event"})
                    .Returns(new AlertableWeatherEvent());

                //act
                var alerts = _sut.EmitAlerts(_weatherFeedEvents);

                //assert
                alerts.Select(a => a.Event).Should().BeEquivalentTo("Some event", "Freezing temperature");
            }

            [Test]
            public void Should_emit_multiple_alerts_when_more_than_one_alertable_event_is_detected_in_the_weather_feed_events()
            {
                //arrange
                var rainEvent = new WeatherFeedEvent
                {
                    Event = "Rain",
                    High = RandomData.GetInt(FreezingLimitDegreesF, HighHeatLimitDegreesF),
                    Low = RandomData.GetInt(FreezingLimitDegreesF, HighHeatLimitDegreesF)
                };
                var thunderstormEvent = new WeatherFeedEvent
                {
                    Event = "Thunderstorms",
                    High = RandomData.GetInt(FreezingLimitDegreesF, HighHeatLimitDegreesF),
                    Low = RandomData.GetInt(FreezingLimitDegreesF, HighHeatLimitDegreesF)
                };
                _weatherFeedEvents.AddRange(new[]
                {
                    rainEvent,
                    thunderstormEvent
                });

                _mapper
                    .Setup(m => m.Map<WeatherFeedEvent, AlertableWeatherEvent>(rainEvent))
                    .Returns(new AlertableWeatherEvent { Event = "Some event" });
                _mapper
                    .Setup(m => m.Map<WeatherFeedEvent, AlertableWeatherEvent>(thunderstormEvent))
                    .Returns(new AlertableWeatherEvent { Event = "Another event" });

                //act
                var alerts = _sut.EmitAlerts(_weatherFeedEvents);

                //assert
                alerts.Select(a => a.Event).Should().BeEquivalentTo("Some event", "Another event");
            }

            private static readonly IEnumerable<string> AlertableEvents = new[] {"Rain", "Thunderstorms", "Snow", "Ice" };
        }
    }
}
