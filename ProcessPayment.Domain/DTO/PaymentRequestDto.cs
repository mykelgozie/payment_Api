using System;

namespace ProcessPayment.Domain.DTO
{
    public class PaymentRequestDto
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public int Amount { get; set; }
    }
}
