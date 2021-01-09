using Apsy.Common.Api.Core;
using Apsy.Common.Api.Core.Graph;
using Apsy.Elemental.Example.Admin.Data;
using Apsy.Example.Models;
using Apsy.Example.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace Apsy.Example.Services
{
    public class PostService : ObservableService<Post>
    {
        private readonly SingltonDataContextService<DataContext> singltonDataContextService;

        public PostService(SingltonDataContextService<DataContext> singltonDataContextService)
        {
            this.singltonDataContextService = singltonDataContextService;
        }

        public PostResponse AddPost(Post post)
        {
            return singltonDataContextService.Execute(c =>
            {
                var addedPost = c.Posts.Add(post);
                c.SaveChanges();
                RaiseEvent(new Observable<Post>(EventType.Added, addedPost.Entity));
                return PostResponse.Success(addedPost.Entity);
            });
        }

        public async Task<PostResponse> UpdatePost(int postId, Post updatedPost)
        {
            return await singltonDataContextService.Execute(async c =>
            {
                var existingPost = c.Posts.FirstOrDefault(p => p.PostId == postId);
                if (existingPost == null)
                {
                    return await Task.FromResult(PostResponse.Failure(PostStatus.PostDoesNotExist));
                }

                var update = new Update<Post>
                {
                    PropName = nameof(Post.Text),
                    OldValue = existingPost.Clone() as Post
                };

                existingPost.Text = updatedPost.Text;
                await c.SaveChangesAsync();

                RaiseEvent(new Observable<Post>(EventType.Added, existingPost, update));
                return PostResponse.Success();
            });
        }

        public PostResponse DeletePost(int postId)
        {
            return singltonDataContextService.Execute(c =>
            {
                var existingPost = c.Posts.FirstOrDefault(p => p.PostId == postId);
                if (existingPost == null)
                {
                    return PostResponse.Failure(PostStatus.PostDoesNotExist);
                }

                c.Posts.Remove(existingPost);
                c.SaveChanges();
                RaiseEvent(new Observable<Post>(EventType.Deleted, existingPost));
                return PostResponse.Success();
            });
        }
    }
}
