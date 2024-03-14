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

			var expectedAcceptEncoding = "gzip, compress, br";
			var expectedAcceptLanuage = "ko,en;q=0.9,en-US;q=0.8";

			Assert.That(host, Is.EqualTo("http://www.test.com"));
			Assert.That(acceptEncoding, Is.EqualTo(expectedAcceptEncoding));
			Assert.That(acceptLanuage, Is.EqualTo(expectedAcceptLanuage));
		}

		[Test]
		public void mock_httpcontext_builder_test()
		{
			var expectedControllerName = "login";
			var expectedActionName = "login";

			var context = MockHttpContextBuilder.Create()
				.WithRequestUrl("https://example.com")
				.WithRequestBody((new { ID = "test", PW = "test" }).xToJson())
				.WithControllerName(expectedControllerName)
				.WithActionName(expectedActionName)
				.Build();

			var controllerName = context.xGetControllerName();
			var actionName = context.xGetActionName();
			
			Assert.That(context, Is.Not.Null);
			Assert.That(controllerName, Is.EqualTo(expectedControllerName));
			Assert.That(actionName, Is.EqualTo(expectedActionName));
		}
	}
}
