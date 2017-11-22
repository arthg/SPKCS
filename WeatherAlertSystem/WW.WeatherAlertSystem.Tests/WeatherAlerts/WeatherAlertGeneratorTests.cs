using NUnit.Framework;
using WW.WeatherFeedClient.WeatherAlerts;

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
            public void Should_()
            {
                //arrange

                //act
                _sut.EmitAlerts();

                //assert
            }
        }
    }
}
