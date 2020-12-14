using DemoAspNetCore.DAL;
using DemoAspNetCore.DBControllers;
using DemoAspNetCore.Exceptions;
using DemoAspNetCore.Models;
using DemoAspNetCore.Services;
using DemoAspNetCore.Validator;
using Moq;
using NUnit.Framework;
using System;

namespace NUnitTestCurrency
{
    public class CurrencyPurchaseServiceUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPurchase()
        {
            var currenciesExchangeService = new Mock<ICurrenciesExchangeService>();
            var transactionDbController = new Mock<ITransactionDbController>();
            var validator = new Mock<IValidator<Transaction>>();
            var exchange = new CurrencyExchange(1, 1, "");
            var transaction = new Transaction() { UserId = 1, Amount = 100, CurrencyCode = "USD" };

            validator.Setup(x => x.IsValid(It.IsAny<Transaction>())).Returns(true);
            currenciesExchangeService.Setup(x => x.GetExchange(It.IsAny<string>())).Returns(exchange);

            var sut = new CurrencyPurchaseService(
                currenciesExchangeService.Object,
                transactionDbController.Object,
                validator.Object
                );

            var result = sut.Purchase(transaction);

            Assert.AreSame(transaction, result);
        }

        [Test]
        public void TestPurchaseWithException()
        {
            var currenciesExchangeService = new Mock<ICurrenciesExchangeService>();
            var transactionDbController = new Mock<ITransactionDbController>();
            var validator = new Mock<IValidator<Transaction>>();
            var exchange = new CurrencyExchange(1, 1, "");
            var transaction = new Transaction() { UserId = 1, Amount = 100, CurrencyCode = "USD" };

            validator.Setup(x => x.IsValid(It.IsAny<Transaction>())).Returns(false);
            currenciesExchangeService.Setup(x => x.GetExchange(It.IsAny<string>())).Returns(exchange);

            var sut = new CurrencyPurchaseService(
                currenciesExchangeService.Object,
                transactionDbController.Object,
                validator.Object
                );

            Assert.Throws<NotAllowedAmountException>(() => sut.Purchase(transaction));
        }
    }
}