using Apsy.Common.Api.Graph;
using System;
using System.Collections.Generic;

namespace Apsy.Example.Models
{
    public class Post : ICloneable
    {
        [Api(false)]
        public int PostId { get; set; }
        [Api]
        public string Text { get; set; }
        [Api]
        public List<Comment> Commnets { get; set; }
        [Api]
        public int PosterId { get; set; }
        public User Poster { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
