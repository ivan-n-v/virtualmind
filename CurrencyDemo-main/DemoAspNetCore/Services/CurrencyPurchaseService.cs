using DemoAspNetCore.DAL;
using DemoAspNetCore.DBControllers;
using DemoAspNetCore.Exceptions;
using DemoAspNetCore.Models;
using DemoAspNetCore.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCore.Services
{
    public class CurrencyPurchaseService : ICurrencyPurchaseService
    {
        private ICurrenciesExchangeService _exchangeService;
        private ITransactionDbController _transactionDbController;
        private IValidator<Transaction> _validator;

        public CurrencyPurchaseService(ICurrenciesExchangeService exchangeService, 
            ITransactionDbController transactionDbController,
            IValidator<Transaction> validator
            )
        {
            _exchangeService = exchangeService;
            _transactionDbController = transactionDbController;
            _validator = validator;
        }

        public Transaction Purchase(Transaction transaction)
        {
            if (!_validator.IsValid(transaction))
            {
                throw new NotAllowedAmountException();
            }

            transaction.PurchasedAmount = CalcPurchaseAmount(transaction);
            _transactionDbController.AddTransaction(transaction);

            return transaction;
        }

        private decimal CalcPurchaseAmount(Transaction transaction)
        {
            var currencyExchange = _exchangeService.GetExchange(transaction.CurrencyCode);

            return Math.Round(transaction.Amount / currencyExchange.BuyingRate, 2);
        }
    }
}
