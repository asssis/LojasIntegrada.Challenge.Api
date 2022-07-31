using AutoMapper;
using LojasIntegrada.Challenge.DataBase.Repositories;
using LojasIntegrada.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.Application.Services
{
    public class ProductService : IBaseService<ProductDto>
    {
        private readonly IMapper mapper;
        private readonly ProductRepository productRepository;
        public ProductService(IMapper _mapper, ProductRepository _productRepository)
        {
            mapper = _mapper;
            productRepository = _productRepository;
        }

        public async Task Delete(int id)
        {
            await productRepository.Delete(id);
        }

        public async Task<ProductDto> GetIndexAsync(int id)
        { 
            Product products = await productRepository.GetIndexAsync(id);
            return mapper.Map<ProductDto>(products);
        }

        public async Task<List<ProductDto>> GetListAsync()
        {
            List<Product> products = await productRepository.GetListAsync();
            return mapper.Map<List<ProductDto>>(products);
        }

        public async Task SaveAsync(ProductDto obj)
        {
            Product objMap =  mapper.Map<Product>(obj);
            await productRepository.SaveAsync(objMap); 
        }

        public async Task UpdateAsync(ProductDto obj)
        { 
            Product objMap = mapper.Map<Product>(obj);
            await productRepository.UpdateAsync(objMap);
        }

        public async Task AddItensAsync(ProductAddQuteDto product)
        {
            await productRepository.AddItensAsync(product.Id, product.Quantidade);
        }
    }
}
