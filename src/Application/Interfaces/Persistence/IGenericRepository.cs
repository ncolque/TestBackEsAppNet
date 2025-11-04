using Domain.Entities;

namespace Application.Interfaces.Persistence
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        //IQueryable<T> GetAllQueryable();
        Task CreateAsync(T entity, CancellationToken cancellationToken);        
        Task<IEnumerable<T>> GetAllPaymentsByCustomer(Guid customerId, CancellationToken cancellationToken);
    }
}
