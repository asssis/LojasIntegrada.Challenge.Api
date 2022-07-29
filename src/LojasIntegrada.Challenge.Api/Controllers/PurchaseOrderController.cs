using LojasIntegrada.Challenge.Application;
using LojasIntegrada.Challenge.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.Api.Controllers
{
    [ApiController]
    [Route("v1/PurchaseOrder")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly PurchaseOrderService purchaseOrderService;
        private readonly UserService userService;
        public PurchaseOrderController(PurchaseOrderService _purchaseOrderService, UserService _userService)
        {
            purchaseOrderService = _purchaseOrderService;
            userService = _userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<PurchaseOrderDto> Index()
        {
            var  user = await userService.GetUser(User.Identity.Name);
            return await purchaseOrderService.GetPurchaseOrder(user); 
        }

        [HttpPost("AddItemPurchaseOrder")]
        [Authorize]
        public async Task AddItemPurchaseOrder(ProductDto productDto)
        {
            var user = await userService.GetUser(User.Identity.Name);
            await purchaseOrderService.AddItemPurchaseOrder(productDto, user);
        }
        [HttpPost("RemoveItemPurchaseOrder")]
        [Authorize]
        public async Task RemoveItemPurchaseOrder(ItensPurchaseOrderDto itemPurchaseOrderDto)
        {
            var user = await userService.GetUser(User.Identity.Name);
            await purchaseOrderService.RemoveItemPurchaseOrder(itemPurchaseOrderDto, user);
        }
        [HttpPost("AbandonarPurchaseOrder")]
        [Authorize]
        public async Task AbandonarPurchaseOrder()
        { 
            var user = await userService.GetUser(User.Identity.Name);
            await purchaseOrderService.AbandonarPurchaseOrder(user);
        }

        [HttpPost("FinishPurchaseOrder")]
        [Authorize]
        public async Task FinishPurchaseOrder()
        {
            var user = await userService.GetUser(User.Identity.Name);
            await purchaseOrderService.FinishPurchaseOrder(user);
        }
    }
}
