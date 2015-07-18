﻿using System;
using System.Data.SQLite;
using System.IO;

using Dapper;
using Dapper.Contrib.Extensions;

using MicroBlog.Interface;
using MicroBlog.Models;

namespace MicroBlog.Repository
{
    public class LitePostRepository : IPostRepository
    {
        //private const string Connectionstring = "Data Source=:memory:;Version=3;New=True;";
        private const string DbSource = "|DataDirectory|microblog.sqlite";
        private const string Connectionstring = "Data Source=" + DbSource +";Version=3;New=True;";

        public LitePostRepository() 
        {
            //if (!File.Exists(DbSource))
            //{
                using (var conn = new SQLiteConnection(Connectionstring))
                {
                    conn.Open();
                    conn.Execute(@" create table IF NOT EXISTS Posts (Id INTEGER PRIMARY KEY, Content nvarchar(1000) not null) ");
                }
            //}
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