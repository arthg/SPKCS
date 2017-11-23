using System.Collections.Generic;

namespace WW.WeatherFeedClient.WeatherFeed
{
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public sealed class YahooWeatherFeedResponse
    {
        public Query Query { get; set; }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    public sealed class Query
    {
        public Results Results { get; set; }
    }

    public sealed class Results
    {
        public Channel Channel { get; set; }
    }

    public sealed class Channel
    {
        public Item Item { get; set; }
    }

    public sealed class Item
    {
        public IEnumerable<ForecastEvent> Forecast { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
    // ReSharper restore ClassNeverInstantiated.Global

    public sealed class ForecastEvent
    {
        public string Date { get; set; }
        public string Day { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Text { get; set; }
    }
}
