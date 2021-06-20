using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProcessPayment.Data;
using ProcessPayment.Data.Repository;
using ProcessPayment.Domain.IRepository;
using ProcessPayment.Domain.IServices;
using ProcessPayment.Domain.Services;
using Swashbuckle.AspNetCore.Filters;

namespace ProcessPayment.API
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
            services.AddControllers()
                    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddScoped<ICheapPaymentGateway, CheapPaymentService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();


            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentService>();
            services.AddScoped<IPremiumPaymentGateway, PremiumPaymentService>();



            services.AddDbContext<PaymentContext>(options => options.UseSqlite("Data source=Payment.DB"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProcessPayment.API", Version = "v1" });
                c.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProcessPayment.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
