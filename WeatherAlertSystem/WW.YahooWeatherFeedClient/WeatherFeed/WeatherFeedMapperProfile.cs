using AutoMapper;

namespace WW.WeatherFeedClient.WeatherFeed
{
    public sealed class WeatherFeedMapperProfile : Profile
    {
        public WeatherFeedMapperProfile()
        {
            CreateMap<ForecastEvent, WeatherFeedEvent>()
                .ForMember(d => d.Event, opt => opt.MapFrom(s => s.Text));
        }
    }
}
