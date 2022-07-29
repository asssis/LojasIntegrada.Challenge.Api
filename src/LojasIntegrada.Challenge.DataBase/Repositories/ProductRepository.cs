using LojasIntegrada.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.DataBase.Repositories
{
    public class ProductRepository: IBaseRepository<Product>
    {
        private readonly AppContexto appContexto;

        public ProductRepository(AppContexto _appContexto)
        {
            appContexto = _appContexto;
        }

        public async Task Delete(int id)
        {
            var entity = await appContexto.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return;
            }
            appContexto.Products.Remove(entity);
        }

        public async Task<Product> GetIndexAsync(int id)
        {
            return await appContexto.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetListAsync()
        { 
            string sql = "SELECT Id, " +
                "  Descricao," +
                "  Imagem, " +
                "  pr.Quantidade - Sales.QuteVendida Quantidade, " +
                "  Valor " +
                "FROM Products pr, " +
                "  (select " +
                "    i.ProductId, " +
                "    count(i.ProductId) QuteVendida " +
                "  from ItensPurchaseOrders i, " +
                "    PurchaseOrders p " +
                "  where i.PurchaseOrderId = p.Id " +
                "  group by i.ProductId) Sales " +
                "where Sales.ProductId = pr.Id";

                var products = await appContexto.Products
                                    .FromSqlRaw(sql)
                                    .ToListAsync<Product>();
            
            return products;
        }

        public async Task<int> SaveAsync(Product obj)
        {
            appContexto.Add(obj);
            return await appContexto.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product obj)
        {
            var entity = await appContexto.Products.FirstOrDefaultAsync(c => c.Id == obj.Id);
            if (entity == null)
            {
                return;
            }
            obj.Quantidade = entity.Quantidade;

            appContexto.Entry(entity).CurrentValues.SetValues(obj);
            appContexto.SaveChanges();
        }
        public async Task AddItensAsync(int id, int quantidade)
        {
            var entity = await appContexto.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return;
            }
            entity.Quantidade += quantidade;

            appContexto.Entry(entity).CurrentValues.SetValues(entity);
            appContexto.SaveChanges();
        }
    }
}
