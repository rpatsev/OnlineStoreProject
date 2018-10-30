using Microsoft.AspNet.Identity.EntityFramework;
using OnlineStoreProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.EF
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext() : base("name=DataContext", throwIfV1Schema: false)
        {
            //Database.SetInitializer<DataContext>(new DataInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DataContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public static DataContext Create()
        {
            return new DataContext();
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
