using Nancy;

namespace MicroBlog
{
    public class BlogModule : NancyModule
    {
        public BlogModule()
        {
            Get["/{id}"] = _ =>
            {
                var blog = new Blog();
                return Response.AsJson(blog);
            };
        }
    }
}