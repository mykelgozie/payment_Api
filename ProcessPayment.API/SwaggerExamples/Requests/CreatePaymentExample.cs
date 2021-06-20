using ProcessPayment.Domain.DTO;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace ProcessPayment.API.SwaggerExamples.Requests
{
    public class CreatePaymentExample : IExamplesProvider<PaymentRequestDto>
    {
        public PaymentRequestDto GetExamples()
        {
            return new PaymentRequestDto()
            {
                Amount = 4000,
                CardHolder = "James Smith",
                CreditCardNumber = "5141721053193595",
                ExpirationDate = DateTime.Now.AddYears(2),
                SecurityCode = "123"
            };
        }
    }
}
