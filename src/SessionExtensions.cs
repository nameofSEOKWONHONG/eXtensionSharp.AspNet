using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace eXtensionSharp.AspNet;

public static class SessionExtensions
{
    /// <summary>
    /// set session value
    /// </summary>
    /// <param name="session"></param>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    public static void vSet<T>(this ISession session, string key, T data)
    {
        if (session.Keys.Any(m => m.Equals(key, StringComparison.OrdinalIgnoreCase)))
        {
            session.Remove(key);
        }
        session.SetString(key, data.xToJson());
    }

    /// <summary>
    /// get session value
    /// </summary>
    /// <param name="session"></param>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T vGet<T>(this ISession session, string key)
    {
        session.vTryGet<T>(key, out T value);
        return value!;
    }

    /// <summary>
    /// get session value
    /// </summary>
    /// <param name="session"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool vTryGet<T>(this ISession session, string key, out T value)
    {
        value = default;
        
        if (session.Keys.Any(m => m.Equals(key, StringComparison.OrdinalIgnoreCase)))
        {
            var v = session.GetString(key);
            value = JsonSerializer.Deserialize<T>(v);
            
            session.Remove(key);

            return true;
        }

        return false;
    }
}