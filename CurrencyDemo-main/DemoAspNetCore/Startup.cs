using DemoAspNetCore.DAL;
using DemoAspNetCore.DBControllers;
using DemoAspNetCore.Services;
using DemoAspNetCore.Validator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c => {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                //c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()); For any source 
            });

            services.AddScoped<ICurrenciesExchangeService, CurrenciesExchangeService>();
            services.AddScoped<DollarCurrencyService, DollarCurrencyService>();
            services.AddScoped<RealBrCurrencyService, RealBrCurrencyService>();
            services.AddScoped<IBancoProvinciaProxyService, BancoProvinciaProxyService>();
            services.AddScoped<ICurrenciesExchangeService, CurrenciesExchangeService>();
            services.AddScoped<ITransactionDbController, TransactionDbController>();
            services.AddScoped<ICurrencyPurchaseService, CurrencyPurchaseService>();
            services.AddScoped<IValidator<Transaction>, LimitValidator>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            //app.UseCors(options => options.AllowAnyOrigin()); For any source
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
