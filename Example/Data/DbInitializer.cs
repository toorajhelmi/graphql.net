using Apsy.Elemental.Example.Web.Models;
using Apsy.Elemental.Example.Admin.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Apsy.Elemental.Example.Web.Data
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
            dataContext.Configuration.Add(new Configuration { Key = Constants.TaxeRate, Value = "" });
            dataContext.Configuration.Add(new Configuration { Key = Constants.ServiceCharge, Value = "" });
            dataContext.Configuration.Add(new Configuration { Key = Constants.DeliveryCharge, Value = "" });

            var peronalPortion = dataContext.Portion.Add(new Portion
            {
                Name = "Personal",
                IsDefault = true
            });

            var familyPortion = dataContext.Portion.Add(new Portion
            {
                Name = "Family"
            });

            var ingredientCategories = new List<IngredientCategory>
            {
                new IngredientCategory
                {
                    Name = "Veggies",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient
                        {
                            Name = "Carrots"
                        },
                        new Ingredient
                        {
                            Name = "Olive"
                        },
                    }
                },
                new IngredientCategory
                {
                    Name = "Meats",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient
                        {
                            Name = "Sausage"
                        },
                        new Ingredient
                        {
                            Name = "Pepperoni"
                        }
                    }
                }
            };

            var restaurant = dataContext.Restaurant.Add(new Restaurant
            {
                Name = "AziPizzi",
                IngredientCategories = ingredientCategories,
                Portions = new List<Portion>()
            });

            restaurant.Entity.Portions.Add(peronalPortion.Entity);
            restaurant.Entity.Portions.Add(familyPortion.Entity);
            restaurant.Entity.Branches = new List<Branch>
            {
                new Branch
                {
                    Name = "Los Angeles",
                    StreetAddress = "601 S Sweetzer Ave",
                    City = "Los Angeles",
                    State = "CA",
                    Zip = "90048",
                    Country = "USA",
                    Phone = "3105551212",
                    Email = "support@azipizzi.com",
                    OperationHours = new List<OperationHours>
                    {
                        new OperationHours
                        {
                            Name = "Lunch",
                            From = DateTime.Now.Date.Add(new TimeSpan(11, 00, 0)),
                            To = DateTime.Now.Date.Add(new TimeSpan(15, 00, 0)),
                        },
                        new OperationHours
                        {
                            Name = "Dinner",
                            From = DateTime.Now.Date.Add(new TimeSpan(18, 00, 0)),
                            To = DateTime.Now.Date.Add(new TimeSpan(21, 00, 0)),
                        }
                    }
                }
            };

            restaurant.Entity.Menus = new List<Menu>
            {
                new Menu
                {
                    Name = "Lunch",
                    AvailableFrom = DateTime.Now.Date.Add(new TimeSpan(11, 00, 0)),
                    AvailabelTo = DateTime.Now.Date.Add(new TimeSpan(15, 00, 0)),
                },

                new Menu
                {
                    Name = "Dinner",
                    AvailableFrom = DateTime.Now.Date.Add(new TimeSpan(18, 00, 0)),
                    AvailabelTo = DateTime.Now.Date.Add(new TimeSpan(21, 00, 0)),
                    Sections = new List<MenuSection>
                    {
                        new MenuSection
                        {
                            Name = "Appetizers",
                            Items = new List<MenuItem>
                            {
                                new MenuItem
                                {
                                    Name = "Green Salad",
                                    Description = "Fresh greens tossed with our special dressing",
                                    PhotoUIrl = "https://bit.ly/335RQxo",
                                    Portions = new List<ItemPortion>
                                    {
                                        new ItemPortion
                                        {
                                                Portion = peronalPortion.Entity,
                                                Calories = 300,
                                                Price = 7,
                                                Ingredients = new List<ItemIngredient>
                                                {
                                                    new ItemIngredient
                                                    {
                                                        Included = true,
                                                        CaloriesDelta = 20,
                                                        PriceDelta = 1,
                                                        Ingredient = ingredientCategories[0].Ingredients[0]
                                                    }
                                                }
                                        }
                                    }
                                }
                            }
                        },
                        new MenuSection
                        {
                            Name = "Main Dishes",
                            Items = new List<MenuItem>
                            {
                                new MenuItem
                                {
                                    Name = "Special Pizza",
                                    Description = "Pizza with the right combination of veggies and meats",
                                    PhotoUIrl = "https://bit.ly/391jnE6",
                                    Portions = new List<ItemPortion>
                                    {
                                        new ItemPortion
                                        {
                                                Portion = peronalPortion.Entity,
                                                Calories = 800,
                                                Price = 15,
                                                Ingredients = new List<ItemIngredient>
                                                {
                                                    new ItemIngredient
                                                    {
                                                        Included = true,
                                                        CaloriesDelta = 100,
                                                        PriceDelta = 2,
                                                        Ingredient = ingredientCategories[1].Ingredients[0]
                                                    },
                                                    new ItemIngredient
                                                    {
                                                        Included = true,
                                                        CaloriesDelta = 120,
                                                        PriceDelta = 2,
                                                        Ingredient = ingredientCategories[1].Ingredients[1]
                                                    },
                                                    new ItemIngredient
                                                    {
                                                        Included = true,
                                                        CaloriesDelta = 30,
                                                        PriceDelta = 1,
                                                        Ingredient = ingredientCategories[0].Ingredients[1]
                                                    }
                                                }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            dataContext.SaveChanges();
        }
    }
}