using Apsy.Common.Api;
using Apsy.Common.Api.GraphQL;
using Apsy.Example.Models;
using Apsy.Example.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Apsy.Example.Subscriptions
{
    public partial class Subscription : SubscriptionBase
    {
        private PostService postService;

        private void InitializePost()
        {
            postService = serviceProvider.GetRequiredService<PostService>();
            AddPostByUserSubscription();
            AddPostUpdatesSubscription();
        }

        private void AddPostByUserSubscription()
        {
            AddField(CreateObjectSubscription<Post>(postService, "postByUserEvent", "posterId",
                new Func<Observable<Post>, int>(o => o.Object.PosterId)));
        }

        private void AddPostUpdatesSubscription()
        {
            AddField(CreateObjectSubscription<Post>(postService, "postUpdatedEvent", "postId",
                new Func<Observable<Post>, int>(o => o.Object.PostId)));
        }
    }
}
