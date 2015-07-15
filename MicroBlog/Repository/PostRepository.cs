using System;
using System.Data;
using System.Linq;
using Dapper;
using MicroBlog.Interface;
using MicroBlog.Models;

namespace MicroBlog.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IDbConnection connection;

        public PostRepository(IDbConnection conn)
        {
            this.connection = conn;
        }

        public Post Get(int id)
        {
            return connection.Query<Post>("select * from posts where id = @id", id).FirstOrDefault();
        }

        public Post Create(Post post)
        {
            throw new NotImplementedException();
        }

        public Post Update(Post post)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}