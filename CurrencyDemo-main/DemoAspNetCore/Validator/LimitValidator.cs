using DemoAspNetCore.DAL;
using DemoAspNetCore.DBControllers;
using DemoAspNetCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCore.Validator
{
    public class LimitValidator: IValidator<Transaction>
    {
        private Dictionary<string, decimal> _limits;
        private string ErrorMessage = "";
        ITransactionDbController _transactionDbController;
        public LimitValidator(ITransactionDbController transactionDbController)
        {
            _transactionDbController = transactionDbController;
            _limits = new Dictionary<string, decimal>();
            _limits.Add("USD", 200);
            _limits.Add("BRL", 300);
        }

        public bool IsValid(Transaction transaction)
        {
            if (!_limits.ContainsKey(transaction.CurrencyCode))
            {
                throw new NotSupportedCurrencyException();
            }

            var accumulated = _transactionDbController.GetAccumulated(
                transaction.UserId,
                transaction.CurrencyCode,
                DateTime.UtcNow
                );

            var result = accumulated + transaction.Amount <= _limits[transaction.CurrencyCode];

            if (!result)
            {
                ErrorMessage = "Not valid amount.";
            }

            return result;
        }

        public string GetMessage()
        {
            return ErrorMessage;
        }
    }
}
