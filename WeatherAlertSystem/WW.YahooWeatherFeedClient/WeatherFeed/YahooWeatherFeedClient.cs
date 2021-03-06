﻿using AutoMapper;
using RestSharp;
using System;
using System.Collections.Generic;

namespace WW.WeatherFeedClient.WeatherFeed
{
    public sealed class YahooWeatherFeedClient : IWeatherFeedClient
    {
        private const string BaseUrl = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20%28select%20woeid%20from%20geo.places%281%29%20where%20text%3D%22Chicago%22%29&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
        private readonly IRestClient _restClient;

        public YahooWeatherFeedClient(IRestClient restClient)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(BaseUrl);
        }

        public IEnumerable<WeatherFeedEvent> GetForecastEvents()
        {
            var request = new RestRequest
            {
                Method = Method.GET
            };

            var response = _restClient.Execute<YahooWeatherFeedResponse>(request);
            // could inspect for success / exception / status code -- but there is nothing we can do here better than throw exception
            // so just let existing unhandled exception handler deal with anything

            var forecastEvents = response.Data.Query.Results.Channel.Item.Forecast;

            //TODO: inject IMapper
            var weatherFeedEvents = Mapper.Map<IEnumerable<ForecastEvent>, IEnumerable<WeatherFeedEvent>>(forecastEvents);

            return weatherFeedEvents;
        }
    }
}
