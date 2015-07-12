using MicroBlog.Models;

namespace MicroBlog.Interface
{
    public interface IPostRepository
    {
        Post Get(int id);

        Post Create(Post post);

        Post Update(Post post);

        bool Delete(int id);
    }
}
