using MicroBlog.Interface;
using MicroBlog.Models;
using Nancy;

namespace MicroBlog
{
    public class BlogModule : NancyModule
    {
        private IPostRepository postRepository;

        public BlogModule(IPostRepository postrepository)
        {
            postRepository = postrepository;

            Get["/{id}"] = _ =>
            {
                var blog = new Post();
                return Response.AsJson(blog);
            };
        }
    }
}