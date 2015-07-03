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
                if (item != null)
                {
                    return Response.AsJson(item);
                }

                return HttpStatusCode.NotFound;
            };
        }
    }
}