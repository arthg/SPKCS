using System.Collections.Generic;

namespace WW.WeatherFeedClient.WeatherFeed
{
    //TODO: consider using dynamic.  Did that, did not like the outcome but there may be better approach 
    public sealed class YahooWeatherFeedResponse
    {
        public Query Query { get; set; }
    }

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

    public sealed class ForecastEvent
    {
        public string Date { get; set; }
        public string Day { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Text { get; set; }
    }
}
