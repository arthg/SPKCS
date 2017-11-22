using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using WW.WeatherFeedClient.WeatherAlerts;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherFeedClient.Tests.WeatherAlerts
{
    [TestFixture]
    public abstract class WeatherAlertGeneratorTests
    {
        private WeatherAlertGenerator _sut;

        [SetUp]
        public void PrepareWeatherAlertGenerator()
        {
            _sut = new WeatherAlertGenerator();
        }

        public sealed class EmitAlertsMethod : WeatherAlertGeneratorTests
        {
            [Test]
            public void Should_not_emit_alerts_when_there_are_no_weather_feed_events()
            {
                //act & assert
                _sut.EmitAlerts(Enumerable.Empty<WeatherFeedEvent>()).Should().BeEmpty();
            }
        }
    }
}
