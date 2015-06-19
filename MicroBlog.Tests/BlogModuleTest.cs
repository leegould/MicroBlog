using Nancy;
using Nancy.Testing;
using Xunit;

namespace MicroBlog.Tests
{
    public class BlogModuleTest
    {
        [Fact]
        public void Should_Return_Blog_When_Queried_With_Id()
        {
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);

            var result = browser.Get("/1", with =>
            {
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
