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
    public class CurrenciesExchangeServiceUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetExchange()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var exchangeService = new Mock<ICurrencyExchangeService>();
            var exchange = new CurrencyExchange(1, 1, "");

            serviceProvider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(exchangeService.Object);
            exchangeService.Setup(x => x.GetCurrencyExchange()).Returns(exchange);

            var sut = new CurrenciesExchangeService(serviceProvider.Object);

            var result = sut.GetExchange("USD");

            Assert.AreSame(exchange, result);
        }

        [Test]
        public void TestGetExchangeWithException()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var exchangeService = new Mock<ICurrencyExchangeService>();
            
            var sut = new CurrenciesExchangeService(serviceProvider.Object);

            Assert.Throws<NotSupportedCurrencyException>(
                () => sut.GetExchange("ABC")
                );
        }
    }
}