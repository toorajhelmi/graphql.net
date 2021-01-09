using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Apsy.Common.Api.Core.Graph
{
    public class SingltonDataContextService<TDataContext>
    {
        private readonly IServiceScopeFactory scopeFactory;

        public SingltonDataContextService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public void Execute(Action<TDataContext> action)
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TDataContext>();
            action(context);
        }

        public T Execute<T>(Func<TDataContext, T> action)
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TDataContext>();
            return action(context);
        }

        public async Task Execute(Func<TDataContext, Task> action)
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TDataContext>();
            await action(context);
        }

        public async Task<T> Execute<T>(Func<TDataContext, Task<T>> action)
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TDataContext>();
            return await action(context);
        }
    }
}
