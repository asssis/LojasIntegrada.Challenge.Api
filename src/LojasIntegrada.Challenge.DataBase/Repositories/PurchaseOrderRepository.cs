using LojasIntegrada.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.DataBase.Repositories
{
    public class PurchaseOrderRepository
    {
        private readonly AppContexto appContexto;
        private readonly IMemoryCache cache;
        private readonly TimeSpan expirationCache = TimeSpan.FromSeconds(10);
        public PurchaseOrderRepository(AppContexto _appContexto, IMemoryCache _cache)
        {
            appContexto = _appContexto;
            cache = _cache;
        }
        private string getKey(int id)
        {
            return string.Concat("purchaseOrder_",id);
        }
        private async Task<PurchaseOrder> CreatePurchaseOrder(User user)
        {
            PurchaseOrder obj = new PurchaseOrder();
            obj.UserId = user.Id;
            obj.Data = DateTime.Now;
            obj.Status = StatusPurchaseOrder.FazendoCompra;

            appContexto.Add(obj);
            int id = await appContexto.SaveChangesAsync();
            if(obj != null)
                obj.Id = id;

            cache.Set(getKey(obj.Id), obj, expirationCache);
            return obj;
        }
        public async Task<PurchaseOrder> GetPurchaseOrder(User user, bool reload = false)
        {
            PurchaseOrder obj = null;
            if(!reload)
                obj = cache.Get<PurchaseOrder>(getKey(user.Id));
            if (obj != null)
                return obj;

            obj = await appContexto.PurchaseOrders
                    .Include(d => d.ItensPurchaseOrder)
                    .Where(x =>
                        x.UserId == user.Id &&
                        x.Status == StatusPurchaseOrder.FazendoCompra &&
                        x.Data < DateTime.Now.AddDays(1)).FirstOrDefaultAsync();
            
            foreach (var item in obj.ItensPurchaseOrder)
            {
                item.Product = await appContexto.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
            }
            obj = cache.Set(getKey(user.Id), obj);
            return obj;
        }

        public async Task RemoveItemPurchaseOrder(User user, ItensPurchaseOrder itemPurchaseOrderMap)
        {
            var entity = await appContexto.PurchaseOrders.FirstOrDefaultAsync(c => c.Id == itemPurchaseOrderMap.PurchaseOrderId);
            if (entity.UserId == user.Id)
            {
                appContexto.PurchaseOrders.Remove(entity);
                appContexto.SaveChanges();
                PurchaseOrder obj = await GetPurchaseOrder(user, true);
            }


        }

        public async Task<PurchaseOrder> GetOrCreatePurchaseOrder(User user)
        {
            PurchaseOrder obj = await GetPurchaseOrder(user);

            if(obj == null)
                obj = await CreatePurchaseOrder(user);

            return obj;
        }
        public async Task AddItemPurchaseOrder(ItensPurchaseOrder obj, User user)
        {
            appContexto.Add(obj);
            int id = await appContexto.SaveChangesAsync();
            obj.Id = id;
            PurchaseOrder objPurchaseOrder = cache.Get<PurchaseOrder>(getKey(obj.Id));
            objPurchaseOrder.ItensPurchaseOrder.Add(obj);
            cache.Set(getKey(user.Id), objPurchaseOrder, expirationCache);
        }
        public async Task AbandonarPurchaseOrder(PurchaseOrder obj)
        {
            var entity = await appContexto.PurchaseOrders.FirstOrDefaultAsync(c => c.Id == obj.Id);
            if (entity == null)
            {
                return;
            }
            entity.Status = StatusPurchaseOrder.Abandonado;
            appContexto.Entry(entity).CurrentValues.SetValues(obj);
            appContexto.SaveChanges();
            cache.Remove(getKey(obj.UserId));
        }

        public async Task FinishPurchaseOrder(PurchaseOrder obj)
        {
            var entity = await appContexto.PurchaseOrders.FirstOrDefaultAsync(c => c.Id == obj.Id);
            if (entity == null)
            {
                return;
            }
            entity.Status = StatusPurchaseOrder.CompraFinalizada;
            appContexto.Entry(entity).CurrentValues.SetValues(obj);
            appContexto.SaveChanges();
            cache.Remove(getKey(obj.UserId));
        }
    }
}
