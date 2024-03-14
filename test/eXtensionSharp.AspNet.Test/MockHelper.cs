using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eXtensionSharp.AspNet.Test
{
	internal class MockHelper
	{
		public static HttpContext FakeHttpContext(string requestBody = "1", string controllerName = "test", string actionName = "index")
		{
			var httpContext = new DefaultHttpContext();

			var httpRequestFeature = new HttpRequestFeature();
			httpRequestFeature.Scheme = "http";
			httpRequestFeature.Method = "GET";
			httpRequestFeature.Path = "/";
			//httpRequestFeature.Headers["Host"] = "http://www.test.com";
			//httpRequestFeature.Headers[HttpContextHeaderName.AcceptEncoding] = "gzip, compress, br";
			//httpRequestFeature.Headers[HttpContextHeaderName.AcceptLanguage] = "ko,en;q=0.9,en-US;q=0.8";
			
			var requestStream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
			httpContext.Request.Headers["Host"] = "http://www.test.com";
			httpContext.Request.Headers[HttpContextHeaderName.AcceptEncoding] = "gzip, compress, br";
			httpContext.Request.Headers[HttpContextHeaderName.AcceptLanguage] = "ko,en;q=0.9,en-US;q=0.8";
			httpContext.Request.Body = requestStream;

			var httpResponseFeature = new HttpResponseFeature();
			var memoryStream = new MemoryStream();			
			httpResponseFeature.Body = memoryStream;
			httpContext.Features.Set<IHttpResponseFeature>(httpResponseFeature);

			// ControllerActionDescriptor 생성
			var actionDescriptor = new ControllerActionDescriptor
			{
				ControllerName = controllerName,
				ActionName = actionName
			};

			// Endpoint Metadata 설정
			var metadata = new List<object>
			{
				actionDescriptor
			};

			// Endpoint 설정
			var endpoint = new RouteEndpoint(context => Task.CompletedTask,
											 RoutePatternFactory.Parse("/"),
											 0,
											 new EndpointMetadataCollection(metadata),
											 "");
			httpContext.SetEndpoint(endpoint);

			httpContext.Session = new TestSession();

			return httpContext;
		}		
	}

	internal class TestSession : Microsoft.AspNetCore.Http.ISession
	{
		private readonly Dictionary<string, byte[]> _sessionData = new Dictionary<string, byte[]>();

		public string Id => Guid.NewGuid().ToString();

		public bool IsAvailable => true;

		public IEnumerable<string> Keys => _sessionData.Keys;

		public Task LoadAsync(CancellationToken cancellationToken = default)
		{
			return Task.CompletedTask;
		}

		public Task CommitAsync(CancellationToken cancellationToken = default)
		{
			return Task.CompletedTask;
		}

		public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
		{
			_sessionData.Remove(key);
			return Task.CompletedTask;
		}

		public Task SetAsync(string key, byte[] value, CancellationToken cancellationToken = default)
		{
			_sessionData[key] = value;
			return Task.CompletedTask;
		}

		public Task<byte[]> GetAsync(string key, CancellationToken cancellationToken = default)
		{
			_sessionData.TryGetValue(key, out byte[] value);
			return Task.FromResult(value);
		}

		public void Clear()
		{
			_sessionData.Clear();
		}

		public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value)
		{
			throw new NotImplementedException();
		}

		public void Set(string key, byte[] value)
		{
			throw new NotImplementedException();
		}

		public void Remove(string key)
		{
			throw new NotImplementedException();
		}
	}
}
