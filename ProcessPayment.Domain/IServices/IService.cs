using ProcessPayment.Domain.Entities;
using System.Threading.Tasks;

namespace ProcessPayment.Domain.IServices
{
    public interface IService
    {
        Task<Response<Payment>> ProcessPayment(Payment payment);
        Task<Payment> UpdatePayment(string id);
    }
}
