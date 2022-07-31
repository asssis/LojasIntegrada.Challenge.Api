using System;
using System.Collections.Generic;
using System.Text;

namespace LojasIntegrada.Challenge.Domain.Entities
{
    public class ItensPurchaseOrder
    {
        public int Id { get; set; }
        //public PurchaseOrder PurchaseOrder { get; set; }
        public int PurchaseOrderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
