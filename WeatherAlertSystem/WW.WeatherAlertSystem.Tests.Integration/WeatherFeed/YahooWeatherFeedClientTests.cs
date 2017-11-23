using NUnit.Framework;
using RestSharp;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherFeedClient.Tests.Integration.WeatherFeed
{
    [TestFixture]
    public abstract class YahooWeatherFeedClientTests
    {
        private YahooWeatherFeedClient _sut;

        [SetUp]
        public void PrepareWeatherFeedClient()
        {
            _sut = new YahooWeatherFeedClient(new RestClient());
        }

        public sealed class GetForecastEventsMethod : YahooWeatherFeedClientTests
        {
            [Test]
            public void Should_query_the_weather_feed_for_forecast_events()
            {
                //arrange
                
                //act
                var forecastEvents = _sut.GetForecastEvents();

                //assert
            }
        }
    }
}
