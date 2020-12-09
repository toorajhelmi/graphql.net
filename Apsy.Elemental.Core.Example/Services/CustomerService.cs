using Apsy.Elemental.Core.Identity;
using Apsy.Elemental.Example.Web.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Apsy.Elemental.Example.Web.Services
{
    public class CustomerService
    {
        private readonly SingltonDataContextService singltonDataContextService;
        private readonly IAuthService authService;
        private readonly IConfiguration configuration;

        public CustomerService(SingltonDataContextService singltonDataContextService, IAuthService authService,
            IConfiguration configuration)
        {
            this.singltonDataContextService = singltonDataContextService;
            this.authService = authService;
            this.configuration = configuration;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            try
            {
                var authConfig = configuration.GetSection("AuthConfig").Get<AuthConfig>();
                var authToken = await authService.Signup(authConfig, customer.Email, customer.Password);

                return await singltonDataContextService.Execute<Customer>(async dataContext =>
                {
                    customer.UserId = authToken.UserId;
                    var customerEntry = dataContext.Customer.Add(customer);
                    await dataContext.SaveChangesAsync();
                    return customerEntry.Entity;
                });
            }
            catch (AuthException se)
            {
                throw new Exception("Error while creating a customer");
            }
        }

        public async Task<Customer> UpdateCustomer(int customerId, Customer updatedCustomer)
        {
            return await singltonDataContextService.Execute<Customer>(async dataContext =>
            {
                var existingCustomer = dataContext.Customer.FirstOrDefault(o => o.CustomerId == customerId);
                if (existingCustomer != null)
                {
                    existingCustomer.CopyFrom(updatedCustomer);
                    await dataContext.SaveChangesAsync();
                }
                else
                {
                    ///TODO: Exception
                }

                return existingCustomer;
            });
        }
    }
}
