using Apsy.Common.Api.Graph;
using Apsy.Common.Api.Identity;
using System.Collections.Generic;

namespace Apsy.Example.Models
{
    public class User : UserBase
    {
        [Api(false)]
        public int UserId { get; set; }
        [Api]
        public string Name { get; set; }
        [Api]
        public List<Post> Posts { get; set; }
    }
}
