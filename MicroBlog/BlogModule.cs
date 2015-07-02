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

            Get["/{id:int}"] = x =>
            {
                Post item = postRepository.Get(x.id);
                return Response.AsJson(item);
            };
        }
    }
}