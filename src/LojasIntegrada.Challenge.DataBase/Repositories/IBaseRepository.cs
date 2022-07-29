using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.DataBase.Repositories
{
    public interface IBaseRepository<T>
    {
        public Task<List<T>> GetListAsync(); 
        public Task<T> GetIndexAsync(int id);
        public Task<int> SaveAsync(T obj);
        public Task UpdateAsync(T obj);
        public Task Delete(int id);
    }
}
