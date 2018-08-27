using System;
using System.Collections.Generic;

namespace IPos.Models
{
    public class Bill
    {
        public int BillNumber { get; set; }
        public List<BillItem> BillItems = new List<BillItem>();
        public decimal? Discount { get; set; }

    }

    [Serializable]
    public class BillItem
    {
        public Products Product { get; set; }
        public Product_Unit Product_Unit { get; set; }
        public int Quantity { get; set; }
        public Transaction Transaction { get; set; }
        public Transaction_Payment TransactionPayment { get; set; }
        public List<Transaction_Detail> TransactionDetail { get; set; }
    }
}