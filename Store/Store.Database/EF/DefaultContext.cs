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

            //Unique Indexes

            //QueryFilters
            builder.Entity<Customer>().HasQueryFilter(x => !x.IsDeleted);
            //PropertySettings

        }

        //Entities
        public DbSet<Customer> Customers { get; set; }
    }
}
