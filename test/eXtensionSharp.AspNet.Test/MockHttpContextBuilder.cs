using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eXtensionSharp.AspNet.Test
{
	internal class MockHttpContextBuilder
	{
		private string _requestBody = "1";
		private string _controllerName = "test";
		private string _actionName = "index";
		private string _url = string.Empty;

		public MockHttpContextBuilder WithRequestUrl(string url)
		{
			_url = url;
			return this;
		}

		public MockHttpContextBuilder WithRequestBody(string requestBody)
		{
			_requestBody = requestBody;
			return this;
		}

		public MockHttpContextBuilder WithControllerName(string controllerName)
		{
			_controllerName = controllerName;
			return this;
		}

		public MockHttpContextBuilder WithActionName(string actionName)
		{
			_actionName = actionName;
			return this;
		}

		public HttpContext Build()
		{
			var httpContext = new DefaultHttpContext();

			var httpRequestFeature = new HttpRequestFeature();
			httpRequestFeature.Scheme = "http";
			httpRequestFeature.Method = "GET";
			httpRequestFeature.Path = "/";

			var requestStream = new MemoryStream(Encoding.UTF8.GetBytes(_requestBody));
			httpContext.Request.Headers["Host"] = "http://www.test.com";
			httpContext.Request.Headers[HeaderNames.AcceptEncoding] = "gzip, compress, br";
			httpContext.Request.Headers[HeaderNames.AcceptLanguage] = "ko,en;q=0.9,en-US;q=0.8";
			httpContext.Request.Body = requestStream;

			var httpResponseFeature = new HttpResponseFeature();
			var memoryStream = new MemoryStream();
			httpResponseFeature.Body = memoryStream;
			httpContext.Features.Set<IHttpResponseFeature>(httpResponseFeature);

			// ControllerActionDescriptor 생성
			var actionDescriptor = new ControllerActionDescriptor
			{
				ControllerName = _controllerName,
				ActionName = _actionName
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

		public static MockHttpContextBuilder Create() => new MockHttpContextBuilder();
	}
}
