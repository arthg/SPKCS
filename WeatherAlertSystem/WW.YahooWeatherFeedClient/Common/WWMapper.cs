using AutoMapper;

namespace WW.WeatherFeedClient.Common
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }

    public sealed class WeatherWatchMapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}