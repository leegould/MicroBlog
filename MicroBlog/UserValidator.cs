﻿using MicroBlog.Models;

using Nancy.Authentication.Basic;
using Nancy.Security;

namespace MicroBlog
{
    public class UserValidator : IUserValidator
    {
        public IUserIdentity Validate(string username, string password)
        {
            if (username == "demo" && password == "demo")
            {
                return new UserIdentity { UserName = username };
            }

            return null;
        }
    }
}