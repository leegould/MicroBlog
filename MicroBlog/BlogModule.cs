using MicroBlog.Interface;
using MicroBlog.Models;
using Nancy;

namespace MicroBlog
{
    public class BlogModule : NancyModule
    {
        public BlogModule(IPostRepository postRepository)
        {
            Get["/{id}"] = _ =>
            {
                var blog = new Post();
                return Response.AsJson(blog);
            };
        }
    }
}