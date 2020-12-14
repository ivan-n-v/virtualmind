using DemoAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCore.Services
{
    public class DollarCurrencyService : ICurrencyExchangeService
    {
        private IBancoProvinciaProxyService _currencyService;

        public DollarCurrencyService(IBancoProvinciaProxyService currencyService)
        {
            _currencyService = currencyService;
        }

        public  CurrencyExchange GetCurrencyExchange()
        {
            var result = _currencyService.GetDollarExchange();
            
            return result.Result;
        }
    }
}
