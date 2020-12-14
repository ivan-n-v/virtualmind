using DemoAspNetCore.DAL;
using DemoAspNetCore.DBControllers;
using DemoAspNetCore.Exceptions;
using DemoAspNetCore.Models;
using DemoAspNetCore.Services;
using DemoAspNetCore.Validator;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace NUnitTestCurrency
{
    public class DollarCurrencyServiceUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetCurrencyExchange()
        {
            var bancoProvinciaProxyService = new Mock<IBancoProvinciaProxyService>();
            var exchange = new CurrencyExchange(1, 1, "");
            bancoProvinciaProxyService.Setup(x => x.GetDollarExchange()).Returns(Task.FromResult(exchange));

            var sut = new DollarCurrencyService(bancoProvinciaProxyService.Object);

            var result = sut.GetCurrencyExchange();

            Assert.AreSame(exchange, result);
        }
    }
}