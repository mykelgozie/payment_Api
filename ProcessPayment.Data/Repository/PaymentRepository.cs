using Microsoft.EntityFrameworkCore;
using ProcessPayment.Domain.Entities;
using ProcessPayment.Domain.IRepository;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Data.Repository
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly PaymentContext _ctx;

        public PaymentRepository(PaymentContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<Payment> UpdatePayment(string id)
        {
            var payment = await _ctx.Payments.Where(x => x.PaymentId == id)
                              .Include(x => x.PaymentState).FirstOrDefaultAsync();

            payment.PaymentState.State = "processed";
            _ctx.Update(payment);
            await _ctx.SaveChangesAsync();
            return payment;
        }

    }
}
