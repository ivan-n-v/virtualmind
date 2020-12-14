using System;
using System.Collections.Generic;

#nullable disable

namespace DemoAspNetCore.DAL
{
    public partial class Transaction
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public decimal PurchasedAmount { get; set; }
    }
}
