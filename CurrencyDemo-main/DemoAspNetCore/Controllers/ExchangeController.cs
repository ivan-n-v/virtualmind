using DemoAspNetCore.Exceptions;
using DemoAspNetCore.Models;
using DemoAspNetCore.Services;
using DemoAspNetCore.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private ICurrenciesExchangeService _exchangeService;

        public ExchangeController(ICurrenciesExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpGet]
        public IActionResult Get(string code)
        {
            try
            {
                return Ok(_exchangeService.GetExchange(code));
            }
            catch (NotSupportedCurrencyException)
            {
                return BadRequest();
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
