using DemoAspNetCore.DAL;
using System;
using System.Collections.Generic;

namespace DemoAspNetCore.DBControllers
{
    public interface ITransactionDbController
    {
        void AddTransaction(Transaction model);
        void DeleteTransaction(int areaId);
        void EditTransaction(int transactionId, Transaction newTransaction);
        List<Transaction> GetAllTransactions();
        Transaction getById(int id);
        decimal GetAccumulated(int userId, string currencyCode, DateTime utcNow);
    }
}