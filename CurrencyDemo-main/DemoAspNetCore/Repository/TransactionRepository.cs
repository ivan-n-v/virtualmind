using DemoAspNetCore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCore.Repository
{
    public class TransactionRepository : Generic.GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApiDBContext context) : base(context)
        {}

        public List<Transaction> GetAllTransactions()
        {
            return table.ToList();
        }

        public decimal GetAccumulated(int userId, string currency, DateTime date)
        {
            var x = (from t in base.table
                     where t.UserId == userId
                     && t.TransactionDateTime.Month == date.Month
                     select t.Amount).Sum();

            return x;
        }
    }
}
