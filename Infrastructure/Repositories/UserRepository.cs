using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public List<IdentityUser> GetAllUser(string role)
        {
            var users = _userManager.GetUsersInRoleAsync(role).Result.ToList();
            return users;
        }

        public async Task LockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.UtcNow.AddDays(30); 
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    throw new Exception("Not found user");
                }
            }
            else
            {
                throw new Exception("Not found user");
            }
        }


        public async Task UnlockUser(string id)
        {
            var user = _userManager.Users.First(x => x.Id == id);
            if (user != null)
            {
                user.LockoutEnabled = false;
                user.LockoutEnd = null;
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    throw new Exception("Not found user");
                }
            }
            else
            {
                throw new Exception("Not found user");
            }
            
        }

      
    }
}
