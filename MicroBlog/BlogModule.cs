using MicroBlog.Interface;
using MicroBlog.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace MicroBlog
{
    public class BlogModule : NancyModule
    {
        private readonly IPostRepository postRepository;

        public BlogModule(IPostRepository postrepository)
        {
            postRepository = postrepository;

            Get["/{id:int}"] = x =>
            {
                Post item = postRepository.Get(x.id);
                return item != null ? Response.AsJson(item) : HttpStatusCode.NotFound;
            };

            Post["/"] = x =>
            {
                //this.RequiresAuthentication();

                var newItem = this.Bind<Post>();
                var item = postrepository.Create(newItem);
                return item != null ? Response.AsJson(item, HttpStatusCode.Created) : HttpStatusCode.InternalServerError;
            };

            Put["/{id:int}"] = x =>
            {
                this.RequiresAuthentication();

                var item = this.Bind<Post>();
                var updatedItem = postrepository.Update(item);
                return updatedItem != null ? Response.AsJson(updatedItem) : HttpStatusCode.InternalServerError;
            };

            Delete["/{id:int}"] = x =>
            {
                this.RequiresAuthentication();

                var result = postRepository.Delete(x.id);
                return (result) ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            };
        }
    }
}