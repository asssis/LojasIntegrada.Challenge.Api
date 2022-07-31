using System;
using System.Collections.Generic;
using System.Text;

namespace LojasIntegrada.Challenge.Application
{
    public enum StatusPurchaseOrder : int
    {
        FazendoCompra = 1, CompraFinalizada = 2, Abandonado = 3
    }

    public class PurchaseOrderDto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public UserDto User { get; set; }
        public int UserId { get; set; }
        public StatusPurchaseOrder Status { get; set; }
        public List<ItensPurchaseOrderDto> ItensPurchaseOrder { get; set; }
    }
    public class PurchaseOrderListDto
    {
        public int Id { get; set; } 
        public List<ItensPurchaseListDto> ItensPurchaseOrder { get; set; }
        public double ValorTotal { get; set; }
    }
}
