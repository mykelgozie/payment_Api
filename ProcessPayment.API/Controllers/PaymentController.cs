using Microsoft.AspNetCore.Mvc;
using ProcessPayment.Domain.DTO;
using ProcessPayment.Domain.Entities;
using ProcessPayment.Domain.IServices;
using ProcessPayment.Domain.Services;
using System;
using System.Threading.Tasks;

namespace ProcessPayment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPremiumPaymentGateway _premiumPaymentGateway;

        public PaymentController(ICheapPaymentGateway cheapPaymentGateway,
                                 IExpensivePaymentGateway expensivePaymentGateway,
                                 IPremiumPaymentGateway premiumPaymentGateway)
        {
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _premiumPaymentGateway = premiumPaymentGateway;
        }
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDto model)
        {
            try
            {
                var service = new PaymentService(_cheapPaymentGateway, _expensivePaymentGateway, _premiumPaymentGateway);

                var payment = new Payment
                {
                    PaymentId = Guid.NewGuid().ToString(),
                    Amount = model.Amount,
                    CardHolder = model.CardHolder,
                    CreditCardNumber = model.CreditCardNumber,
                    ExpirationDate = model.ExpirationDate,
                    SecurityCode = model.SecurityCode,
                    PaymentStateId = Guid.NewGuid().ToString(),
                    PaymentState = new PaymentState
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }

                };
                var response = await service.ProcessPayment(payment);

                if (response.State != "processed")
                {
                    return BadRequest(response);
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);

            }
        }
    }
}
