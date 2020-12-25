using Apsy.Common.Api.Graph;
using Apsy.Example.Models;
using Apsy.Example.Responses;
using Apsy.Example.Services;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Apsy.Example.Mutations
{
    public partial class Mutation : ObjectGraphType
    {
        private PostService postService;

        private void InitializePost()
        {
            postService = serviceProvider.GetRequiredService<PostService>();
            CreatePost();
            UpdatePost();
            DeletePost();
        }

        private void CreatePost()
        {
            Field<GraphApi<PostResponse>>(
                "CreatePost",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<GraphInputApi<Post>>> { Name = "post" }),
                resolve: context =>
                {
                    var post = context.GetArgument<Post>("post");
                    return postService.AddPost(post);
                });
        }

        private void UpdatePost()
        {
            FieldAsync<GraphApi<PostResponse>>(
                "UpdatePost",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "postId" },
                    new QueryArgument<GraphInputApi<Post>> { Name = "updatedPost" }),
                resolve: async context =>
                {
                    var postId = context.GetArgument<int>("postId");
                    var updatedPost = context.GetArgument<Post>("updatedPost");
                    return await postService.UpdatePost(postId, updatedPost);
                });
        }

        private void DeletePost()
        {
            Field<GraphApi<PostResponse>>(
                "DeletePost",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "postId" }),
                resolve: context =>
                {
                    var postId = context.GetArgument<int>("postId");
                    return postService.DeletePost(postId);
                });
        }
    }
}
