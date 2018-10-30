using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineStoreProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        //public ApplicationRoleManager(RoleStore<ApplicationRole> store) : base(store) { }
        private RoleStore<ApplicationRole> store;
        public ApplicationRoleManager(RoleStore<ApplicationRole> _store) : base(_store)
        {
            this.store = _store;
        }
    }
}
