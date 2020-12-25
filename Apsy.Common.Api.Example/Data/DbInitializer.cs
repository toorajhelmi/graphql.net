using Apsy.Example.Models;
using Apsy.Elemental.Example.Admin.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Apsy.Example.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataContext dataContext)
        {
            if (dataContext.Database.EnsureCreated())
            {
                Seed(dataContext);
            }
            else
            {
                dataContext.Database.Migrate();
            }
        }

        public static void Seed(DataContext dataContext)
        {
            dataContext.Users.Add(new User
            {
                Name = "John Doe",
                Email = "jdoe@apsy.io",
                Posts = new List<Post>
                {
                    new Post
                    {
                        Text = "What a cool framework",
                        Commnets = new List<Comment>
                        {
                            new Comment
                            {
                                Text = "Ditto",
                                Commenter = new User
                                {
                                    Name = "Sarah Smith",
                                    Email = "sarah@apsy.io"
                                }
                            }
                        }
                    },
                    new Post { Text = "How is this different than other GraphQL frameworls" }
                }
            });
            dataContext.SaveChanges();
        }
    }
}