using AutoMapper;
using WW.WeatherFeedClient.WeatherFeed;

namespace WW.WeatherFeedClient.WeatherAlerts
{
    public sealed class WeatherAlertMapperProfile : Profile
    {
        public WeatherAlertMapperProfile()
        {
            CreateMap<WeatherFeedEvent, AlertableWeatherEvent>();
        }
    }  
}