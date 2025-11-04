using Application.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<T> _entity;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
            _entity = _applicationDbContext.Set<T>();
        }

        //public IQueryable<T> GetAllQueryable()
        //{
        //    var response = _entity;
        //    return response;
        //}

        public async Task CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await _applicationDbContext.AddAsync(entity, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllPaymentsByCustomer(Guid id, CancellationToken cancellationToken)
        {            
            if (typeof(T) != typeof(Payment))
                throw new InvalidOperationException("Este método solo aplica para Payment.");
            
            var payments = await _entity
                .Cast<Payment>()
                .Where(p => p.CustomerId == id)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return payments as IEnumerable<T>;
        }
        
    }
}
