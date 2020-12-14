using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCore.Models
{
    public class CurrencyExchange
    {
        public decimal BuyingRate { get; set; }
        public decimal SellingRate { get; set; }
        public string Message { get; set; }

        public CurrencyExchange(decimal BuyingRate, decimal SellingRate, string Message = "")
        {
            this.BuyingRate = BuyingRate;
            this.SellingRate = SellingRate;
            this.Message = Message;
        }
    }
}
