using AutoMapper;
using LojasIntegrada.Challenge.DataBase.Repositories;
using LojasIntegrada.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.Application.Services
{
    public class PurchaseOrderService
    {
        private readonly IMapper mapper;
        private readonly PurchaseOrderRepository purchaseOrderRepository;
        private readonly ProductService productService;
        public PurchaseOrderService(IMapper _mapper, PurchaseOrderRepository _purchaseOrderRepository, ProductService _productService)
        {
            mapper = _mapper;
            purchaseOrderRepository = _purchaseOrderRepository;
            productService = _productService;
        }

        public async Task<PurchaseOrderDto> GetPurchaseOrder(UserDto userDto)
        {
            var userMap = mapper.Map<User>(userDto);
            PurchaseOrder obj = await purchaseOrderRepository.GetPurchaseOrder(userMap);
            PurchaseOrderDto objMap = mapper.Map<PurchaseOrderDto>(obj);
            objMap.User.Password = "";
            return objMap;
        }


        public async Task<PurchaseOrderListDto> GetPurchaseOrderList(UserDto userDto)
        {
            var userMap = mapper.Map<User>(userDto);
            PurchaseOrder obj = await purchaseOrderRepository.GetPurchaseOrder(userMap);
            PurchaseOrderDto objMap = mapper.Map<PurchaseOrderDto>(obj);

            PurchaseOrderListDto listPurchaseOrder = new PurchaseOrderListDto();

            listPurchaseOrder.Id = objMap.Id; 
            listPurchaseOrder.ValorTotal = objMap.ItensPurchaseOrder.Sum(x => x.Product.Valor);
            listPurchaseOrder.ItensPurchaseOrder = new List<ItensPurchaseListDto>();
            var listGroupBy = objMap.ItensPurchaseOrder.GroupBy(x => x.ProductId);

            foreach (var item in listGroupBy)
            {
                ProductDto productDto = item.FirstOrDefault().Product;
                ItensPurchaseListDto purchaseList = new ItensPurchaseListDto();
                purchaseList.Descricao = productDto.Descricao;
                purchaseList.Imagem = productDto.Imagem;
                purchaseList.ProductDto = await productService.GetIndexAsync(productDto.Id);
                purchaseList.ProductId = productDto.Id;
                purchaseList.Quantidade = objMap.ItensPurchaseOrder.Count(x => x.ProductId == productDto.Id);
                purchaseList.Valor = productDto.Valor * purchaseList.Quantidade;
                listPurchaseOrder.ItensPurchaseOrder.Add(purchaseList);
            }
             
            return listPurchaseOrder;
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


        public async Task RemoveItemPurchaseOrder(ProductDto productDto, UserDto userDto)
        {
            var userMap = mapper.Map<User>(userDto);
            var listPurchaseOrder = await GetPurchaseOrder(userDto);
            var itenPurchaseOrder = listPurchaseOrder.ItensPurchaseOrder.FirstOrDefault(x => x.ProductId == productDto.Id);
            var mapPurchase = mapper.Map<ItensPurchaseOrder>(itenPurchaseOrder);
            await purchaseOrderRepository.RemoveItemPurchaseOrder(userMap, mapPurchase);
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
