namespace WW.WeatherFeedClient.WeatherFeed
{
    public sealed class WeatherFeedEvent
    {
        public string Event { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public int High { get; set; }
        public int Low { get; set; }
    }
}
