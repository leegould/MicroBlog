using Nancy;

namespace MicroBlog
{
    public class BlogModule : NancyModule
    {
        public BlogModule()
        {
            Get["/{id}"] = _ =>
            {
                var blog = new BlogPost();
                return Response.AsJson(blog);
            };
        }
    }
}