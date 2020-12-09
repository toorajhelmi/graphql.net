using Apsy.Elemental.Example.Admin.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Apsy.Elemental.Example.Web.Services
{
    public class SingltonDataContextService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public SingltonDataContextService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public void Execute(Action<DataContext> action)
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            action(context);
        }

        public async Task Execute(Func<DataContext, Task> action)
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            await action(context);
        }

        public async Task<T> Execute<T>(Func<DataContext, Task<T>> action)
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            return await action(context);
        }
    }
}
