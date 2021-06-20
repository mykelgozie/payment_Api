using ProcessPayment.Domain.Entities;
using ProcessPayment.Domain.IServices;
using System.Threading.Tasks;

namespace ProcessPayment.Domain.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPremiumPaymentGateway _premiumPaymentGateway;


        public PaymentService(ICheapPaymentGateway cheapPaymentGateway,
                              IExpensivePaymentGateway expensivePaymentGateway,
                              IPremiumPaymentGateway premiumPaymentGateway)
        {
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _premiumPaymentGateway = premiumPaymentGateway;
        }
        public async Task<Response<Payment>> ProcessPayment(Payment payment)
        {
            var response = new Response<Payment>();
            switch (payment.Amount)
            {
                case < 20:
                    {
                        await _cheapPaymentGateway.ProcessPayment(payment);
                        var paymentData = await _cheapPaymentGateway.UpdatePayment(payment.PaymentId);
                        response.Data = paymentData;
                        response.State = paymentData.PaymentState.State;
                        if (response.State == "processed")
                        {
                            return response;
                        }
                        break;
                    }
                case >= 21 and <= 500:
                    {
                        if (_expensivePaymentGateway != null)
                        {
                            await _expensivePaymentGateway.ProcessPayment(payment);
                            var paymentData = await _expensivePaymentGateway.UpdatePayment(payment.PaymentId);
                            response.Data = paymentData;
                            response.State = paymentData.PaymentState.State;
                            if (response.State == "processed")
                            {
                                return response;
                            }
                        }
                        await _cheapPaymentGateway.ProcessPayment(payment);
                        var result = await _cheapPaymentGateway.UpdatePayment(payment.PaymentId);
                        response.Data = result;
                        response.State = result.PaymentState.State;
                        if (response.State == "processed")
                        {
                            return response;
                        }

                        break;
                    }
                case > 500:
                    {
                        const int numberOfTimes = 3;
                        var i = 0;
                        while (i++ < numberOfTimes)
                        {
                            await _premiumPaymentGateway.ProcessPayment(payment);
                            var paymentData = await _premiumPaymentGateway.UpdatePayment(payment.PaymentId);
                            response.Data = paymentData;
                            response.State = paymentData.PaymentState.State;
                            if (response.State == "processed")
                            {
                                return response;
                            }
                        }

                        break;
                    }
            }
            response.State = "failed";
            return response;
        }
    }
}
