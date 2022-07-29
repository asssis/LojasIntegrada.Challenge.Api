using LojasIntegrada.Challenge.Application;
using LojasIntegrada.Challenge.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.Api.Controllers
{
    [ApiController]
    [Route("v1/Product")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;
        public ProductController(ProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet("Index")] 
        [AllowAnonymous]
        public async Task<List<ProductDto>> IndexAsync()
        {
            List<ProductDto> listProduct = await productService.GetListAsync();
            return listProduct;
        }

        [HttpGet("Index/{id}")]
        [AllowAnonymous]
        public async Task<ProductDto> IndexAsync(int id)
        {
            ProductDto product = await productService.GetIndexAsync(id);
            return product;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task SaveAsync([FromBody] ProductDto product)
        {
            await productService.SaveAsync(product); 
        }
        [HttpPost("AddItens")]
        [Authorize(Roles = "admin")]
        public async Task AddItensAsync([FromBody] ProductAddQuteDto product)
        {
            await productService.AddItensAsync(product);
        }
        [HttpPut("Update/{id}")]
        [Authorize(Roles = "admin")]
        public async Task Put(int id, [FromBody] ProductDto product)
        {
            await productService.UpdateAsync(product);
        }
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "admin")]
        public async Task Delete(int id)
        {
            await productService.Delete(id);
        }

    }
}
