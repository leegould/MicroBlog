using System.Collections.Generic;
using Nancy.Security;

namespace MicroBlog.Models
{
    public class UserIdentity : IUserIdentity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; private set; }
    }
}