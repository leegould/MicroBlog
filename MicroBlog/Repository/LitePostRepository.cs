using System;
using System.Data.SQLite;

using Dapper.Contrib.Extensions;

using MicroBlog.Interface;
using MicroBlog.Models;

namespace MicroBlog.Repository
{
    public class LitePostRepository : IPostRepository
    {
        private const string Connectionstring = "Data Source=:memory:;Version=3;New=True;";

        public LitePostRepository() 
        {
        }

        public Post Get(int id)
        {
            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();

                var p = conn.Get<Post>(id);

                conn.Close();

                return p;
            }
        }

        public Post Create(Post post)
        {
            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();

                var postid = conn.Insert(post);
                post.Id = (int)postid;

                conn.Close();
            }

            if (post.Id > 0)
            {
                return post;
            }

            return null;
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