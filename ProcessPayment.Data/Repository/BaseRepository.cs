using Microsoft.EntityFrameworkCore;
using ProcessPayment.Domain.IRepository;
using System.Threading.Tasks;

namespace ProcessPayment.Data.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly PaymentContext _ctx;
        private readonly DbSet<T> _entitySet;

        protected BaseRepository(PaymentContext ctx)
        {
            _ctx = ctx;
            _entitySet = ctx.Set<T>();
        }

        public async Task<T> Find(string id)
        {
            var entity = await _entitySet.FindAsync(id);
            return entity;
        }
        public async Task Save(T t)
        {
            if (t != null)
            {
                await _entitySet.AddAsync(t);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task Update(T t)
        {
            if (t != null)
            {
                _ctx.Update(t);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
