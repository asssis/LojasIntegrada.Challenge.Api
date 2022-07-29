using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.Application.Services
{
    public interface IBaseService<T>
    {
        public Task<List<T>> GetListAsync();
        public Task<T> GetIndexAsync(int id);
        public Task SaveAsync(T obj);
        public Task UpdateAsync(T obj);
        public Task Delete(int id);
    }
} 