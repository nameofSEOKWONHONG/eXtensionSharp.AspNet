using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace eXtensionSharp.AspNet;

public static class DistributeCacheExtensions
{
    /// <summary>
    /// set value, distribute cache
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <param name="options"></param>
    /// <typeparam name="T"></typeparam>
    public static void vSet<T>(this IDistributedCache cache, string key, T data, DistributedCacheEntryOptions options = null)
    {
        if (options.xIsNotEmpty())
        {
            var exist = cache.vGet<T>(key);
            if (exist.xIsNotEmpty())
            {
                cache.Remove(key);
            }
            cache.Set(key, data.xToBytes(), options);
            return;
        }
        
        cache.Set(key, data.xToBytes());
    }

    /// <summary>
    /// get value, distribute cache
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T vGet<T>(this IDistributedCache cache, string key)
    {
        var bytes = cache.Get(key);
        return JsonSerializer.Deserialize<T>(bytes.xToString());
    }
}