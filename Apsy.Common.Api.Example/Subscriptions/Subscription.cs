using Apsy.Example.Models;
using Apsy.Example.Services;
using Apsy.Common.Api.GraphQL;
using System;
using Apsy.Common.Api;

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
