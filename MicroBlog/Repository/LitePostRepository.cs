using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

using Dapper;
using Dapper.Contrib.Extensions;

using MicroBlog.Interface;
using MicroBlog.Models;

namespace MicroBlog.Repository
{
    public class LitePostRepository : IPostRepository
    {
        private const string DbSource = "|DataDirectory|microblog.sqlite";
        private const string Connectionstring = "Data Source=" + DbSource +";Version=3;New=True;";

        public LitePostRepository() 
        {
            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();
                conn.Execute(@" create table IF NOT EXISTS Posts (Id INTEGER PRIMARY KEY, Content nvarchar(1000) not null) ");
            }
        }

        public List<Post> GetAll()
        {
            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();

                var p = conn.GetAll<Post>().ToList();

                conn.Close();

                return p;
            }
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
            if (post == null)
            {
                return null;
            }

            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();

                var postid = conn.Insert(post);
                post.Id = (int)postid;

                conn.Close();
            }

            return post.Id > 0 ? post : null;
        }

        public async Task<Post> Update(Post post)
        {
            if (post == null)
            {
                return null;
            }

            bool result;
            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();

                result = await conn.UpdateAsync(post);

                conn.Close();
            }

            return result ? post : null;
        }

        public bool Delete(int id)
        {
            bool result = false;

            if (id > 0)
            {

                using (var conn = new SQLiteConnection(Connectionstring))
                {
                    conn.Open();

                    var item = conn.Get<Post>(id);

                    if (item != null)
                    {
                        result = conn.Delete(item);
                    }

                    conn.Close();
                }
            }

            return result;
        }
    }
}