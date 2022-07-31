using System;
using System.Collections.Generic;
using System.Text;

namespace LojasIntegrada.Challenge.Application
{
    public class ItensPurchaseOrderDto
    {
        public int Id { get; set; }

        //public PurchaseOrderDto PurchaseOrder { get; set; }
        public int PurchaseOrderId { get; set; }
        public ProductDto Product { get; set; }
        public int ProductId { get; set; }
    }
    public class ItensPurchaseListDto
    {
        public int ProductId { get; set; }
        public ProductDto ProductDto { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }
        public string Imagem { get; internal set; }
    }
}
