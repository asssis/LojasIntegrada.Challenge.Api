using LojasIntegrada.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.DataBase.Repositories
{
    public class UserRepository
    {
        private readonly AppContexto appContexto;

        public UserRepository(AppContexto _appContexto)
        {
            appContexto = _appContexto;
        }

        public async Task<List<User>> GetListAsync()
        {
            return await appContexto.Users.ToListAsync();
        }

        public async Task<int> SaveAsync(User obj)
        {
            appContexto.Add(obj);
            return await appContexto.SaveChangesAsync();
        }
    }
}
