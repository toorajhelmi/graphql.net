using Apsy.Common.Api.Graph;
using Apsy.Example.Models;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Apsy.Example.Queries
{
    public partial class Query : ObjectGraphType
    {
        private void InitializePost()
        {
            GetPost();
            GetPosts();
        }

        private void GetPost()
        {
            Field<GraphApi<Post>>(
                name: "GetPost",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "postId" }
                ),
                resolve: context =>
                {
                    var postId = context.GetArgument<int>("postId");
                    return singltonDataContextService.Execute(c =>
                    {
                        return c.Posts
                        .Include(p => p.Commnets)
                        .FirstOrDefault(p => p.PostId == postId);
                    });
                });
        }

        private void GetPosts()
        {
            Field<ListGraphType<GraphApi<Post>>>(
                name: "GetPosts",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "userId" }
                ),
                resolve: context =>
                {
                    var userId = context.GetArgument<int>("userId");
                    return singltonDataContextService.Execute(c =>
                    {
                        return c.Posts.Where(p => p.PosterId == userId).ToList();
                    });
                });
        }
    }
}
