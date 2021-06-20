namespace ProcessPayment.Domain.Entities
{
    public class Payment : DTO.PaymentRequestDto
    {
        public string PaymentId { get; set; }
        public string PaymentStateId { get; set; }
        public PaymentState PaymentState { get; set; }
    }
}
