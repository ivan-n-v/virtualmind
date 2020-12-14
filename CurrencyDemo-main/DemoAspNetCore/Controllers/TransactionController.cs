using DemoAspNetCore.DAL;
using DemoAspNetCore.DBControllers;
using DemoAspNetCore.Exceptions;
using DemoAspNetCore.Models;
using DemoAspNetCore.Services;
using DemoAspNetCore.Validator;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class TransactionController : Controller
    {
        private ICurrencyPurchaseService _currencyPurchaseService;

        public TransactionController(ICurrencyPurchaseService currencyPurchaseService)
        {
            _currencyPurchaseService = currencyPurchaseService;
        }

        [HttpPost]
        public IActionResult Post(Transaction transaction)
        {
            try
            {
                return Ok(_currencyPurchaseService.Purchase(transaction));
            }
            catch (NotSupportedCurrencyException)
            {
                return BadRequest();
            }
            catch (NotAllowedAmountException)
            {
                return BadRequest("This amount will exeed the monthly amount.");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
