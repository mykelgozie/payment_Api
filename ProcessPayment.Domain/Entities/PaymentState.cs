using System;

namespace ProcessPayment.Domain.Entities
{
    public class PaymentState
    {
        public string Id { get; set; }
        public string State { get; set; } = "pending";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
