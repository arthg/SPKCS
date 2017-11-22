using AutoMapper;

namespace WW.WeatherFeedClient.WeatherFeed
{
    public sealed class WeatherFeedMapperProfile : Profile
    {
        public WeatherFeedMapperProfile()
        {
            CreateMap<ForecastEvent, WeatherFeedEvent>();
        }
    }
}
