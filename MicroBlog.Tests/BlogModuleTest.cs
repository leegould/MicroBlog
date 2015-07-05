using MicroBlog.Interface;
using MicroBlog.Models;

using Moq;

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
            var fakePostRepository = new Mock<IPostRepository>();
            fakePostRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Post()); 
            
            var browser = new Browser(cfg =>
            {
                cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
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
            var fakePostRepository = new Mock<IPostRepository>();
            fakePostRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Post()); 

            var browser = new Browser(cfg =>
            {
                cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
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
            fakePostRepository.Setup(x => x.Get(It.IsAny<int>())).Returns((Post)null); //.Throws(new Exception("Not Found"));
            
            var browser = new Browser(
                cfg =>
                {
                    cfg.Module<BlogModule>();
                    cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                });

            var result = browser.Get("/999", with =>
            {
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        #region Creating

        [Fact]
        public void Should_Return_Created_If_Created()
        {
            var fakePostRepository = new Mock<IPostRepository>();
            var fakePost = new Post();
            fakePostRepository.Setup(x => x.Create(It.IsAny<Post>())).Returns(fakePost);

            var browser = new Browser(
                cfg =>
                {
                    cfg.Module<BlogModule>();
                    cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                });

            var result = browser.Post("/", with =>
            {
                with.HttpRequest();
                with.FormValue("Content", "Test Content");
            });

            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public void Should_Return_ServerError_If_Cannot_Created()
        {
            var fakePostRepository = new Mock<IPostRepository>();
            var fakePost = new Post();
            fakePostRepository.Setup(x => x.Create(fakePost)).Returns((Post)null);

            var browser = new Browser(
                cfg =>
                {
                    cfg.Module<BlogModule>();
                    cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                });

            var result = browser.Post("/", with =>
            {
                with.HttpRequest();
                with.FormValue("Content", "Test Content");
            });

            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
