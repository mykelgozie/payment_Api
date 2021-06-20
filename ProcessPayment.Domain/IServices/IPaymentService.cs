using ProcessPayment.Domain.Entities;
using System.Threading.Tasks;

namespace ProcessPayment.Domain.IServices
{
    public interface IPaymentService
    {
        Task<Response<Payment>> ProcessPayment(Payment payment);
    }
}
