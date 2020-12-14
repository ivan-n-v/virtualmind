using DemoAspNetCore.DAL;
using DemoAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCore.Services
{
    public interface ICurrencyPurchaseService
    {
        Transaction Purchase(Transaction transaction);
    }
}
