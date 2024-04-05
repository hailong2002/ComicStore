using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        List<IdentityUser> GetAllUser(string role);

        Task LockUser(string id);

        Task UnlockUser(string id);
    }
}
