using Nancy;

namespace MicroBlog
{
    public class BlogModule : NancyModule
    {
        public BlogModule()
        {
            Get["/{id}"] = _ =>
            {
                var blog = new Post();
                return Response.AsJson(blog);
            };
        }
    }
}