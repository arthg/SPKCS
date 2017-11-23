using AutoMapper;
using Exceptionless;
using FluentAssertions;
using NUnit.Framework;
using WW.WeatherFeedClient.WeatherAlerts;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherFeedClient.Tests.WeatherAlerts
{
    [TestFixture]
    public abstract class WeatherAlertMapperProfileTests : MapperProfileTestsBase<WeatherAlertMapperProfile>
    {
        // ReSharper disable once InconsistentNaming
        public sealed class WeatherFeedEvent_to_AlertableWeatherEvent_MapperTests : WeatherAlertMapperProfileTests
        {
            [Test]
            public void Should_map_properties()
            {
                //arrange
                var weatherFeedEvent = new WeatherFeedEvent
                {
                    Date = RandomData.GetString(10, 10),
                    Day = RandomData.GetString(10, 10),
                    Event = RandomData.GetString(10, 10)
                };

                //act
                var mapped = Mapper.Map<AlertableWeatherEvent>(weatherFeedEvent);

                //assert
                mapped.Should().Match<AlertableWeatherEvent>(m =>
                    m.Date == weatherFeedEvent.Date &&
                    m.Day == weatherFeedEvent.Day &&
                    m.Event == weatherFeedEvent.Event);
            }
        }
    }
}