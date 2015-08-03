using System.Collections.Generic;
using System.Threading.Tasks;
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
        #region Getting Lists

        [Fact]
        public void Should_Return_OK_When_Queried()
        {
            var fakePostRepository = new Mock<IPostRepository>();
            fakePostRepository.Setup(x => x.GetAll()).Returns(new List<Post>());

            var browser = new Browser(cfg =>
            {
                cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                cfg.Module<BlogModule>();
            });

            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Should_Return_ListOfPosts_When_Queried()
        {
            var fakePostRepository = new Mock<IPostRepository>();
            fakePostRepository.Setup(x => x.GetAll()).Returns(new List<Post>());

            var browser = new Browser(cfg =>
            {
                cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                cfg.Module<BlogModule>();
            });

            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
            });

            Assert.Equal(typeof(List<Post>), result.Body.DeserializeJson<List<Post>>().GetType());
        }

        #endregion

        #region Getting

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

        #endregion

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
                    cfg.RequestStartup((container, pipelines, context) =>
                    {
                        context.CurrentUser = new UserIdentity {UserName = "Test"};
                    });
                });

            var result = browser.Post("/", with =>
            {
                with.HttpRequest();
                with.FormValue("Content", "Test Content");
            });

            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }
        [Fact]
        public void Should_Return_ServerError_If_Cannot_Bind()
        {
            var fakePostRepository = new Mock<IPostRepository>();
            fakePostRepository.Setup(x => x.Create((Post)null)).Returns((Post)null);

            var browser = new Browser(
                cfg =>
                {
                    cfg.Module<BlogModule>();
                    cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                    cfg.RequestStartup((container, pipelines, context) =>
                    {
                        context.CurrentUser = new UserIdentity { UserName = "Test" };
                    });
                });

            var result = browser.Post("/", with =>
            {
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
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
                    cfg.RequestStartup((container, pipelines, context) =>
                    {
                        context.CurrentUser = new UserIdentity { UserName = "Test" };
                    });
                });

            var result = browser.Post("/", with =>
            {
                with.HttpRequest();
                with.FormValue("Content", "Test Content");
            });

            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public void Should_Return_Unauthorized_If_InvalidUser_Creating()
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

            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        #endregion

        #region Updating

        [Fact]
        public void Should_Return_OK_If_Updated()
        {
            var fakePostRepository = new Mock<IPostRepository>();
            var fakePost = new Post();
            fakePostRepository.Setup(x => x.Update(It.IsAny<Post>())).Returns(Task.FromResult(fakePost));

            var browser = new Browser(
                cfg =>
                {
                    cfg.Module<BlogModule>(); 
                    cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                    cfg.RequestStartup((container, pipelines, context) =>
                    {
                        context.CurrentUser = new UserIdentity { UserName = "Test" };
                    });
                });

            var result = browser.Put("/1", with =>
            {
                with.HttpRequest();
                with.FormValue("Content", "Test Content");
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Should_Return_ServerError_If_Cannot_Updated()
        {
            var fakePostRepository = new Mock<IPostRepository>();
            fakePostRepository.Setup(x => x.Update(It.IsAny<Post>())).Returns(Task.FromResult((Post)null));

            var browser = new Browser(
                cfg =>
                {
                    cfg.Module<BlogModule>();
                    cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                    cfg.RequestStartup((container, pipelines, context) =>
                    {
                        context.CurrentUser = new UserIdentity { UserName = "Test" };
                    });
                });

            var result = browser.Put("/999", with =>
            {
                with.HttpRequest();
                with.FormValue("Content", "Test Content");
            });

            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public void Should_Return_Unauthorized_If_InvalidUser_Updating()
        {
            var fakePostRepository = new Mock<IPostRepository>();
            fakePostRepository.Setup(x => x.Update(It.IsAny<Post>())).Returns(Task.FromResult((Post)null));

            var browser = new Browser(
                cfg =>
                {
                    cfg.Module<BlogModule>();
                    cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                });

            var result = browser.Put("/1", with =>
            {
                with.HttpRequest();
                with.FormValue("Content", "Test Content");
            });

            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        #endregion

        #region Delete

        [Fact]
        public void Should_Return_OK_If_Deleted()
        {
            var fakePostRepository = new Mock<IPostRepository>();
            fakePostRepository.Setup(x => x.Delete(It.IsAny<int>())).Returns(true);

            var browser = new Browser(
                cfg =>
                {
                    cfg.Module<BlogModule>();
                    cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                    cfg.RequestStartup((container, pipelines, context) =>
                    {
                        context.CurrentUser = new UserIdentity { UserName = "Test" };
                    });
                });

            var result = browser.Delete("/1", with =>
            {
                with.HttpRequest();
                with.FormValue("Content", "Test Content");
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Should_Return_ServerError_If_Cannot_Delete()
        {
            var fakePostRepository = new Mock<IPostRepository>();
            fakePostRepository.Setup(x => x.Delete(It.IsAny<int>())).Returns(false);

            var browser = new Browser(
                cfg =>
                {
                    cfg.Module<BlogModule>();
                    cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                    cfg.RequestStartup((container, pipelines, context) =>
                    {
                        context.CurrentUser = new UserIdentity { UserName = "Test" };
                    });
                });

            var result = browser.Delete("/999", with =>
            {
                with.HttpRequest();
                with.FormValue("Content", "Test Content");
            });

            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public void Should_Return_Unauthorized_If_InvalidUser_Deleting()
        {
            var fakePostRepository = new Mock<IPostRepository>();
            fakePostRepository.Setup(x => x.Delete(It.IsAny<int>())).Returns(false);

            var browser = new Browser(
                cfg =>
                {
                    cfg.Module<BlogModule>();
                    cfg.Dependencies<IPostRepository>(fakePostRepository.Object);
                });

            var result = browser.Delete("/1", with =>
            {
                with.HttpRequest();
                with.FormValue("Content", "Test Content");
            });

            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        #endregion
    }
}
