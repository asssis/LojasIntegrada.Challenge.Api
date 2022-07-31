using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojasIntegrada.Challenge.Domain.Entities
{
    public enum StatusPurchaseOrder : int
    {
        FazendoCompra = 1, CompraFinalizada = 2, Abandonado = 3
    }

    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public StatusPurchaseOrder Status { get; set; }
        public List<ItensPurchaseOrder> ItensPurchaseOrder { get; set; }
    }
}
