using Nancy;

namespace MicroBlog
{
    public class BlogModule : NancyModule
    {
        public BlogModule()
        {
            Get["/{id}"] = _ => "Test";
        }
    }
}