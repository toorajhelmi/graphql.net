using Apsy.Common.Api.Graph;

namespace Apsy.Example.Models
{
    public class Comment
    {
        [Api(false)]
        public int CommentId { get; set; }
        [Api]
        public string Text { get; set; }
        [Api]
        public int CommenterId { get; set; }
        public User Commenter { get; set; }
        [Api]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
