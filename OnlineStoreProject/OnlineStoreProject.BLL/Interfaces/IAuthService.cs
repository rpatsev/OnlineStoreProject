using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.Interfaces
{
    public interface IAuthService : IDisposable
    {
        Task<OperationDetails> Create(UserModel userModel);
        Task<ClaimsIdentity> Authentificate(UserModel userModel);
        Task SetInitialData(UserModel admin, List<string> roles);
        UserModel GetUserData(string userid);

    }
}
