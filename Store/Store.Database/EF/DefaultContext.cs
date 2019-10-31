using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Database.Entities;

namespace Store.Database.EF
{
    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options)
              : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder
                .AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information)
                .AddDebug();
            });

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Relations
            builder.Entity<Customer>()
                .HasOne(q => q.Cart)
                .WithOne(q => q.Customer);

            builder.Entity<Cart>()
                .HasMany(q => q.CartItems)
                .WithOne(q => q.Cart);

            builder.Entity<CartItem>()
            .HasOne(q => q.Product)
            .WithMany(q => q.CartItems)
            .HasForeignKey(q => q.ProductId);


            //Unique Indexes

            //QueryFilters
            builder.Entity<Customer>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Cart>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<CartItem>().HasQueryFilter(x => !x.IsDeleted);
            //PropertySettings

        }

        //Entities
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
