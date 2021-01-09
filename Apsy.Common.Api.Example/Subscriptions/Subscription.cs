using Apsy.Common.Api.Core.GraphQL;
using System;

namespace Apsy.Example.Subscriptions
{
    public partial class Subscription : SubscriptionBase
    {
        private readonly IServiceProvider serviceProvider;

        public Subscription(IServiceProvider serviceProvider)
        {
            Name = "Subscription";
            this.serviceProvider = serviceProvider;
            InitializePost();
        } 
    }
}
