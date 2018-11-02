using OnlineStoreProject.DAL.EF;
using OnlineStoreProject.DAL.Entities;
using OnlineStoreProject.DAL.Identity;
using OnlineStoreProject.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public DataContext Database { get; set; }
        public ClientManager(DataContext db)
        {
            this.Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Update(ClientProfile item)
        {
            Database.ClientProfiles.AddOrUpdate(item);
            Database.SaveChanges();
        }

        public ClientProfile Get(string id)
        {
            return Database.ClientProfiles.Find(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
