using ProcessPayment.Domain.Entities;
using System.Threading.Tasks;

namespace ProcessPayment.Domain.IRepository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> UpdatePayment(string id);
    }
}
