using Exceptionless;
using FluentAssertions;
using NUnit.Framework;
using WW.WeatherFeedClient.WeatherAlerts;

namespace WW.WeatherFeedClient.Tests.WeatherAlerts
{
    public sealed class AlertableWeatherEventTests
    {
        [Test]
        public void Should_format_ToString()
        {
           //act & assert
            var sut = new AlertableWeatherEvent
            {
                Event = RandomData.GetString(10, 10),
                Date = RandomData.GetString(10, 10),
                Day = RandomData.GetString(10, 10)
            };
            sut.ToString().Should().Be($"{sut.Day} {sut.Date} {sut.Event}");
        }
    }
}