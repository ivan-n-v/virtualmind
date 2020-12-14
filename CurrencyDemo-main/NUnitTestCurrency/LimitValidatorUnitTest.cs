using DemoAspNetCore.DAL;
using DemoAspNetCore.DBControllers;
using DemoAspNetCore.Exceptions;
using DemoAspNetCore.Validator;
using Moq;
using NUnit.Framework;
using System;

namespace NUnitTestCurrency
{
    public class LimitValidatorUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(0, "USD", true)]
        [TestCase(200, "USD", false)]
        [TestCase(0, "BRL", true)]
        [TestCase(200, "BRL", true)]
        [TestCase(300, "BRL", false)]
        public void TestIsValid(decimal previousAmount, string currency, bool expectedValue)
        {
            var dbController = new Mock<ITransactionDbController>();
            dbController.Setup(x => x.GetAccumulated(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(previousAmount);

            var sut = new LimitValidator(dbController.Object);

            var result = sut.IsValid(new Transaction() { UserId = 1, Amount = 100, CurrencyCode = currency });

            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void TestIsValidWithException()
        {
            var dbController = new Mock<ITransactionDbController>();
            dbController.Setup(x => x.GetAccumulated(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(0);

            var sut = new LimitValidator(dbController.Object);

            Assert.Throws<NotSupportedCurrencyException>(
                () => sut.IsValid(new Transaction() { UserId = 1, Amount = 100, CurrencyCode = "ABC" })
                );
        }

    }
}