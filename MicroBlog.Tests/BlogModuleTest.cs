using Nancy;
using Nancy.Testing;
using Xunit;

namespace MicroBlog.Tests
{
    public class BlogModuleTest
    {
        [Fact]
        public void Should_Return_OK_When_Queried_With_Id()
        {
            var browser = new Browser(cfg =>
            {
                cfg.Module<BlogModule>();
            });

            var result = browser.Get("/1", with =>
            {
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Should_Return_A_Blog_When_Queried_With_Id()
        {
            var browser = new Browser(cfg =>
            {
                cfg.Module<BlogModule>();
            });

            var result = browser.Get("/1", with =>
            {
                with.HttpRequest();
            });

            Assert.Equal(typeof(Blog), result.Body.DeserializeJson<Blog>().GetType());
        }
    }
}
