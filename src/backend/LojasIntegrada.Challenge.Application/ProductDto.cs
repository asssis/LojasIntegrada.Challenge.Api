using LojasIntegrada.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LojasIntegrada.Challenge.Application
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public int? Quantidade { get; set; }
        public double Valor { get; set; } 
    }
    public class ProductAddQuteDto
    {
        public int Id { get; set; } 
        public int Quantidade { get; set; } 
    }
}
