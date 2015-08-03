using System.Collections.Generic;
using System.Threading.Tasks;
using MicroBlog.Models;

namespace MicroBlog.Interface
{
    public interface IPostRepository
    {
        List<Post> GetAll();
        
        Post Get(int id);

        Post Create(Post post);

        Task<Post> Update(Post post);

        bool Delete(int id);
    }
}
