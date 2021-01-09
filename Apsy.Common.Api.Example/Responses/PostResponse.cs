using Apsy.Common.Api.Core;
using Apsy.Example.Models;

namespace Apsy.Example.Responses
{
    public enum PostStatus
    {
        Success = 200,
        PostDoesNotExist = 300
    }

    public class PostResponse : ResultResponse<Post, PostStatus>
    {
        public static PostResponse Success() => new PostResponse { Status = PostStatus.Success };
        public static PostResponse Success(Post post) => new PostResponse { Status = PostStatus.Success, Result = post };
        public static PostResponse Failure(PostStatus status) => new PostResponse { Status = status };
    }
}
