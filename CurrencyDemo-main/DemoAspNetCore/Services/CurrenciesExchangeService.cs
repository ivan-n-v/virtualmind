using DemoAspNetCore.Exceptions;
using DemoAspNetCore.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCore.Services
{
    public class CurrenciesExchangeService : ICurrenciesExchangeService
    {
        private Dictionary<string, Type> _services;
        private IServiceProvider _serviceProvider;
        public CurrenciesExchangeService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _services = new Dictionary<string, Type>
            {
                { "USD", typeof(DollarCurrencyService) },
                { "BRL", typeof(RealBrCurrencyService) }
            };
        }
        public CurrencyExchange GetExchange(string isoCode)
        {
            isoCode = isoCode.ToUpper();

            if (_services.ContainsKey(isoCode))
            {
                var service = (ICurrencyExchangeService)_serviceProvider.GetRequiredService(_services[isoCode]);

                return service.GetCurrencyExchange();
            }

            throw new NotSupportedCurrencyException();
        }
    }
}
