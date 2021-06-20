using Moq;
using ProcessPayment.Domain.DTO;
using ProcessPayment.Domain.Entities;
using ProcessPayment.Domain.IServices;
using ProcessPayment.Domain.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PaymentProcess.Test
{
    public class UnitTest1
    {

        [Fact]
        public async Task Should_Process_Cheap_Payment()
        {

            var cheapPaymentMock = new Mock<ICheapPaymentGateway>();
            var expensivePaymentMock = new Mock<IExpensivePaymentGateway>();
            var premiumPaymentMock = new Mock<IPremiumPaymentGateway>();

            var paymentDto = new PaymentRequestDto()
            {
                Amount = 10,
                CardHolder = "James Smith",
                CreditCardNumber = "5141721053193595",
                ExpirationDate = DateTime.Now.AddYears(2),
                SecurityCode = "123",

            };

            var payment = new Payment()
            {
                Amount = paymentDto.Amount,
                CardHolder = paymentDto.CardHolder,
                CreditCardNumber = paymentDto.CreditCardNumber,
                ExpirationDate = paymentDto.ExpirationDate,
                PaymentId = "1",
                PaymentState = new PaymentState()
                {
                    CreatedAt = DateTime.Now,
                    Id = "1",
                    State = "pending",
                    UpdatedAt = DateTime.Now
                }
            };


            cheapPaymentMock.Setup(x => x.ProcessPayment(payment));
            payment.PaymentState.State = "processed";
            cheapPaymentMock.Setup(x => x.UpdatePayment(payment.PaymentId)).ReturnsAsync(payment);

            var service = new PaymentService(cheapPaymentMock.Object, expensivePaymentMock.Object,
                premiumPaymentMock.Object);

            var response = await service.ProcessPayment(payment);
            Assert.Equal("processed", response.State);
            premiumPaymentMock.Verify(x => x.ProcessPayment(payment), Times.Never);
        }
    }
}
