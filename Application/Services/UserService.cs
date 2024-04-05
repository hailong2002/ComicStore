using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public List<IdentityUser> GetAllUser(string role)
        {
            return _repository.GetAllUser(role);
        }
        public async Task LockUser(string id)
        {
            await _repository.LockUser(id);
        }

        public async Task UnlockUser(string id)
        {
            await _repository.UnlockUser(id);
        }

    }
}
