using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;
using WW.WeatherFeedClient.WeatherAlerts;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherFeedClient.Tests.WeatherAlerts
{
    [TestFixture]
    public abstract class WeatherAlertReporterTests
    {
        private Mock<IWeatherFeedClient> _weatherFeedClient;
        private Mock<IWeatherAlertGenerator> _weatherAlertGenerator;
        private WeatherAlertReporter _sut;

        [SetUp]
        public void PrepareWeatherAlertReporter()
        {
            _weatherFeedClient = new Mock<IWeatherFeedClient>(MockBehavior.Strict);
            _weatherAlertGenerator = new Mock<IWeatherAlertGenerator>(MockBehavior.Strict);
            _sut = new WeatherAlertReporter(_weatherFeedClient.Object, _weatherAlertGenerator.Object);
        }

        public sealed class GetWeatherAlertsMethod : WeatherAlertReporterTests
        {
            [Test]
            public void Should_fetch_the_forecast_events_from_the_service_and_report_the_alert_events()
            {
                //arrange
                var forecastEvents = Enumerable.Empty<WeatherFeedEvent>().ToArray();
                _weatherFeedClient
                    .Setup(c => c.GetForecastEvents())
                    .Returns(forecastEvents);

                var alertableEvents = Enumerable.Empty<AlertableWeatherEvent>().ToArray();
                _weatherAlertGenerator
                    .Setup(g => g.EmitAlerts(forecastEvents))
                    .Returns(alertableEvents);

                //act
                var alerts = _sut.GetWeatherAlerts();

                //assert
                alerts.Should().BeSameAs(alertableEvents);
            }
        }
    }
}
