using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eXtensionSharp.AspNet.Test
{
	
	public class HttpContextTest
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void httpcontext_test() 
		{
			var context = MockHelper.FakeHttpContext();

			var host = context.xGetBaseHost();
			var acceptEncoding = context.xGetAcceptEncoding();
			var acceptLanuage = context.xGetAcceptLanguage();

			var conotrollerName = context.xGetControllerName();
			var actionName = context.xGetActionName();

			var expectedAcceptEncoding = "gzip, compress, br";
			var expectedAcceptLanuage = "ko,en;q=0.9,en-US;q=0.8";
			Assert.That(host, Is.EqualTo("http://www.test.com"));
			Assert.That(acceptEncoding, Is.EqualTo(expectedAcceptEncoding));
			Assert.That(acceptLanuage, Is.EqualTo(expectedAcceptLanuage));
		}
	}
}
