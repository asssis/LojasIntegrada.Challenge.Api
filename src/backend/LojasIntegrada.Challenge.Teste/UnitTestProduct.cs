using LojasIntegrada.Challenge.DataBase;
using LojasIntegrada.Challenge.DataBase.Repositories;
using LojasIntegrada.Challenge.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LojasIntegrada.Challenge.Teste
{
    public class UnitTestProduct
    {
        private readonly Mock<AppContexto> appContexto;
        private readonly Mock<IBaseRepository<Product>> mockProductRepository;
        private readonly ProductRepository productRepositor;
        public UnitTestProduct()
        {
            appContexto = new Mock<AppContexto>();
            mockProductRepository = new Mock<IBaseRepository<Product>>();
            productRepositor = new ProductRepository();
        }

        [Theory(DisplayName = "Verificando Products Salvos")]
        [InlineData(1)]
        public async Task ProductsAsync(int id)
        {
            var produt = new Product()
            {
                Id = id,
                Descricao = "teste",
                Imagem = "teste",
                Quantidade = 10,
                Valor = 10
            };

            mockProductRepository.Setup(x => x.SaveAsync(produt)).ReturnsAsync(id);

            var result = await mockProductRepository.Object.SaveAsync(produt);
            Assert.Equal(result, produt.Id);
        }

        [Theory(DisplayName = "Verificando Products Salvos")]
        [InlineData(1)]
        public async Task GetProductsAsync(int id)
        {
            var produt = new Product()
            {
                Id = id,
                Descricao = "teste",
                Imagem = "teste",
                Quantidade = 10,
                Valor = 10
            };
            mockProductRepository.Setup(x => x.GetIndexAsync(It.IsAny<int>())).ReturnsAsync(produt);
            var result = await mockProductRepository.Object.GetIndexAsync(id);

            Assert.Equal(result.Id, id);
        }

        [Fact(DisplayName = "Verificando Products Salvos")]
        public void ListProducts()
        {
            var listTopics = new List<Product>()
            {
                new Product(){ Id = 1 },
                new Product(){ Id = 2},
                new Product(){ Id = 3}
            };
            int total = 3;

            mockProductRepository.Setup(x => x.GetListAsync()).ReturnsAsync(listTopics);
            var result = mockProductRepository.Object.GetListAsync().Result;

            Assert.Equal(result.Count(), total);
        }

    }
}
