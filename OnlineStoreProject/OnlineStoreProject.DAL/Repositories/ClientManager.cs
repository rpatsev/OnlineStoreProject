using OnlineStoreProject.DAL.EF;
using OnlineStoreProject.DAL.Entities;
using OnlineStoreProject.DAL.Identity;
using OnlineStoreProject.DAL.Interfaces;
using System;
using System.Collections.Generic;
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

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
