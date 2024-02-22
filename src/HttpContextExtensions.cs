using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Primitives;

namespace eXtensionSharp.AspNet;

public static class HttpContextExtensions
{
    /// <summary>
    /// get boolean https
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static bool xIsHttps(this HttpContext context) => context.Request.IsHttps;
    
    /// <summary>
    /// get request scheme
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetBaseScheme(this HttpContext context) => $"{context.Request.Scheme}";

    /// <summary>
    /// get request host
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetBaseHost(this HttpContext context) => $"{context.Request.Host}";

    /// <summary>
    /// get request header value
    /// </summary>
    /// <param name="context"></param>
    /// <param name="headerName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool xTryGetRequestHeader(this HttpContext context, string headerName, out StringValues value) => context.Request.Headers.TryGetValue(headerName, out value);

    /// <summary>
    /// get remote address
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetRemoteIpAddress(this HttpContext context)
    {   
        return context.Connection.RemoteIpAddress.ToString();
    }

    /// <summary>
    /// get remote port
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetRemotePort(this HttpContext context)
    {
        return context.Connection.RemotePort.xValue<string>();
    }

    /// <summary>
    /// get remote full ip addresss
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetRemoteFullIpAddress(this HttpContext context)
    {
        return $"{context.Connection.RemoteIpAddress}:{context.Connection.RemotePort}";
    }

    /// <summary>
    /// get user claim role exist
    /// </summary>
    /// <param name="context"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public static bool xHasRole(this HttpContext context, string role)
    {
        if (context.User.xIsEmpty()) return false;
        if (context.User.Identity.xIsEmpty()) return false;
        if (context.User.Identity.IsAuthenticated.xIsFalse()) return false;
        
        return context.User.HasClaim(m =>
            m.Type == ClaimTypes.Role && m.Value.Equals(role, StringComparison.OrdinalIgnoreCase));
    }
    
    /// <summary>
    /// get user claim roles
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IEnumerable<string> xGetRoles(this HttpContext context)
    {
        if (context.User.xIsEmpty()) return default;
        if (context.User.Claims.xIsEmpty()) return default;
        return context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(s => s.Value);
    }
    
    /// <summary>
    /// get user claim value
    /// </summary>
    /// <param name="context"></param>
    /// <param name="claim"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T xGetClaim<T>(this HttpContext context, string claim)
    {
        if (context.User.xIsEmpty()) return default;
        if (context.User.Claims.xIsEmpty()) return default;
        var result = context.User.Claims.FirstOrDefault(f => f.Type.Equals(claim, StringComparison.OrdinalIgnoreCase))?.Value;
        return result.xValue<T>();
    }

    /// <summary>
    /// get authenticated
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static bool xIsAuthenticated(this HttpContext context)
    {
        if (context.User.xIsEmpty()) return false;
        if (context.User.Identity.xIsEmpty()) return false;
        
        return context.User.Identity!.IsAuthenticated;
    }

    /// <summary>
    /// get method (get, post, update ....)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetMethod(this HttpContext context)
    {
        return context.Request.Method;
    }

    /// <summary>
    /// get controller name;
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetControllerName(this HttpContext context)
    {
        if (context.xIsEmpty()) return default;
        return context.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>().ControllerName;
    }

    /// <summary>
    /// get controller full name (import namespace path)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetControllerFullName(this HttpContext context)
    {
        if (context.xIsEmpty()) return default;
        return context.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>().ControllerTypeInfo.FullName;
    }

    /// <summary>
    /// get action name
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetActionName(this HttpContext context)
    {
        if (context.xIsEmpty()) return default;
        return context.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>().ActionName;
    }

    /// <summary>
    /// get user agent(header:User-Agent)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetUserAgent(this HttpContext context)
    {
        context.xTryGetRequestHeader("User-Agent", out var v);
        return v;
    }

    /// <summary>
    /// get accept encoding(header:Accept-Encoding)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetAcceptEncoding(this HttpContext context)
    {
        context.xTryGetRequestHeader("Accept-Encoding", out var v);
        return v;
    }

    /// <summary>
    /// get accept encoding(header:Accept-Language)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetAcceptLanguage(this HttpContext context)
    {
        context.xTryGetRequestHeader("Accept-Language", out var v);
        return v;
    }

    /// <summary>
    /// get accept encoding(header:Referer)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string xGetReferer(this HttpContext context)
    {
        context.xTryGetRequestHeader("Referer", out var v);
        return v;
    }
}