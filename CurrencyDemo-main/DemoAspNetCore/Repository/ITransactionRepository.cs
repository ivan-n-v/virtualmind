using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCore.Repository
{
    public interface ITransactionRepository
    {
        decimal GetAccumulated(int userId, string currency, DateTime date);
    }
}
