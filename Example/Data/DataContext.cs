using Microsoft.EntityFrameworkCore;
using Apsy.Elemental.Example.Web.Models;
using Apsy.Elemental.Example.Web.Models;

namespace Apsy.Elemental.Example.Admin.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemPortion>()
                .HasOne<Portion>(ip => ip.Portion)
                .WithMany(p => p.ItemPortions)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ItemIngredient>()
                .HasOne<ItemPortion>(ii => ii.ItemPortion)
                .WithMany(ip => ip.Ingredients)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuSection> MenuSection { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<ItemPortion> ItemPortion { get; set; }
        public DbSet<ItemIngredient> ItemIngredient { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Portion> Portion { get; set; }
        public DbSet<IngredientCategory> IngredientCategory { get; set; }
        public DbSet<OperationHours> OperationHours { get; set; }
        public DbSet<BranchOperationHours> BranchOperationHours { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<SearchHistory> SearchHistory { get; set; }
    }
}
