using AutoMapper;
using Exceptionless;
using FluentAssertions;
using NUnit.Framework;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherFeedClient.Tests.WeatherFeed
{
    [TestFixture]
    public abstract class WeatherFeedMapperProfileTests
    { 
        public sealed class ForecastEvent_to_WeatherFeedEvent_MapperTests : WeatherFeedMapperProfileTests
        {
            [Test]
            public void Should_have_valid_configuration()
            {
                Mapper.Configuration.AssertConfigurationIsValid();
            }

            [Test]
            public void Should_map_properties()
            {
                //arrange
                var forecastEvent = new ForecastEvent
                {
                    Date = RandomData.GetString(10,10),
                    Day = RandomData.GetString(10, 10),
                    Text = RandomData.GetString(10, 10),
                    High = RandomData.GetLong().ToString(),
                    Low = RandomData.GetLong().ToString()
                };

                //act
                var mapped = Mapper.Map<WeatherFeedEvent>(forecastEvent);

                //assert
                mapped.Should().Match<WeatherFeedEvent>(m =>
                    m.Date == forecastEvent.Date &&
                    m.Day == forecastEvent.Day &&
                    m.High == int.Parse(forecastEvent.High) &&
                    m.Low == int.Parse(forecastEvent.Low) &&
                    m.Event == forecastEvent.Text);
            }
        }
    }
}
