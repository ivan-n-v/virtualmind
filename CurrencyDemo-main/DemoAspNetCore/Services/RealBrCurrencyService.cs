using DemoAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCore.Services
{
    public class RealBrCurrencyService : ICurrencyExchangeService
    {
        private ICurrencyExchangeService _currencyExchangeService;

        public RealBrCurrencyService(DollarCurrencyService dollarExchangeService)
        {
            _currencyExchangeService = dollarExchangeService;
        }

        public CurrencyExchange GetCurrencyExchange()
        {
            var result = _currencyExchangeService.GetCurrencyExchange();

            return new CurrencyExchange(
            result.BuyingRate / 4.0m,
            result.SellingRate / 4.0m
            );
        }
    }
}
