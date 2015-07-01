using System;
using System.Linq.Expressions;

using MicroBlog.Interface;
using MicroBlog.Models;

using Moq;

using Nancy;
using Nancy.Responses.Negotiation;
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
        public void Should_Return_A_Post_When_Queried_With_Id()
        {
            var browser = new Browser(cfg =>
            {
                cfg.Module<BlogModule>();
            });

            var result = browser.Get("/1", with =>
            {
                with.HttpRequest();
            });

            Assert.Equal(typeof(Post), result.Body.DeserializeJson<Post>().GetType());
        }

        [Fact]
        public void Should_Return_NotFound_If_Id_Doesnt_Exist()
        {
            var fakePostRepository = new Mock<IPostRepository>();          
            fakePostRepository.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception("Not Found"));
            
            var browser = new Browser(
                cfg =>
                {
                    cfg.Module<BlogModule>();
                    cfg.Dependencies<IPostRepository>(fakePostRepository);
                });

            var result = browser.Get("/999", with =>
            {
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
