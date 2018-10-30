using OnlineStoreProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.EF
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext db)
        {
            Category cat1 = new Category { Name = "Whiskey", Description = "Wiskey is great!" };
            Category cat2 = new Category { Name = "Gin", Description = "Gin for fun!" };
            Category cat3 = new Category { Name = "Liquer", Description = "Nothing better than that!" };
            db.Categories.Add(cat1);
            db.Categories.Add(cat2);
            db.Categories.Add(cat3);
            db.SaveChanges();

            Product prod1 = new Product { Name = "Hankey", Alcohol = 40, Country = "Ireland", Manufacturer = "H&B", Category = cat1, Volume = 0.7, InStock = true, Price = 20 };
            Product prod2 = new Product { Name = "Glenfiddich", Alcohol = 42, Country = "Scotland", Manufacturer = "Glenfiddich&Sons", Category = cat1, Volume = 0.5, InStock = false, Price = 50 };
            Product prod3 = new Product { Name = "Jagermeister", Alcohol = 38, Country = "Geramny", Manufacturer = "Jager", Category = cat3, Volume = 0.75, InStock = true, Price = 30 };
            db.Products.Add(prod1);
            db.Products.Add(prod2);
            db.Products.Add(prod3);
            db.SaveChanges();
        }
    }
}
