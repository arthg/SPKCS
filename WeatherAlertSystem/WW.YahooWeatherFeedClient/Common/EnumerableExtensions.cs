using System;
using System.Collections.Generic;

namespace WW.WeatherFeedClient.Common
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            if (collection == null)
            {
                return;
            }
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}