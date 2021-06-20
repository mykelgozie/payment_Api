using System.Threading.Tasks;

namespace ProcessPayment.Domain.IRepository
{
    public interface IRepository<T>
    {
        Task Save(T t);
        Task<T> Find(string id);
        Task Update(T t);
    }
}
