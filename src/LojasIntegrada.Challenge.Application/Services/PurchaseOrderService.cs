using AutoMapper;
using LojasIntegrada.Challenge.DataBase.Repositories;
using LojasIntegrada.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.Application.Services
{
    public class PurchaseOrderService
    {
        private readonly IMapper mapper;
        private readonly PurchaseOrderRepository purchaseOrderRepository;
        public PurchaseOrderService(IMapper _mapper, PurchaseOrderRepository _purchaseOrderRepository)
        {
            mapper = _mapper;
            purchaseOrderRepository = _purchaseOrderRepository;
        }

        public async Task<PurchaseOrderDto> GetPurchaseOrder(UserDto userDto)
        {
            var userMap = mapper.Map<User>(userDto);
            PurchaseOrder obj = await purchaseOrderRepository.GetPurchaseOrder(userMap);
            PurchaseOrderDto objMap = mapper.Map<PurchaseOrderDto>(obj);
            return objMap;
        }

        public async Task AddItemPurchaseOrder(ProductDto productDto, UserDto userDto)
        {
            var userMap = mapper.Map<User>(userDto);
            var purchaseOrder = await purchaseOrderRepository.GetOrCreatePurchaseOrder(userMap);

            ItensPurchaseOrder itensPurchaseOrder = new ItensPurchaseOrder();
            itensPurchaseOrder.ProductId = productDto.Id;
            itensPurchaseOrder.PurchaseOrderId = purchaseOrder.Id;
            await purchaseOrderRepository.AddItemPurchaseOrder(itensPurchaseOrder, userMap);
        }


        public async Task RemoveItemPurchaseOrder(ItensPurchaseOrderDto itemPurchaseOrderDto, UserDto userDto)
        {
            var userMap = mapper.Map<User>(userDto);
            var itemPurchaseOrderMap = mapper.Map<ItensPurchaseOrder>(itemPurchaseOrderDto);
            await purchaseOrderRepository.RemoveItemPurchaseOrder(userMap, itemPurchaseOrderMap);
        }
        public async Task AbandonarPurchaseOrder(UserDto userDto)
        {
            var userMap = mapper.Map<User>(userDto);
            PurchaseOrder obj = await purchaseOrderRepository.GetPurchaseOrder(userMap);
            await purchaseOrderRepository.AbandonarPurchaseOrder(obj);
        }

        public async Task FinishPurchaseOrder(UserDto userDto)
        {
            var userMap = mapper.Map<User>(userDto);
            PurchaseOrder obj = await purchaseOrderRepository.GetPurchaseOrder(userMap);
            await purchaseOrderRepository.FinishPurchaseOrder(obj);
        }
    }
}
