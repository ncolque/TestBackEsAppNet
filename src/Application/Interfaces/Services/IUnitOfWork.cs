using Application.Interfaces.Persistence;
using Domain.Entities;
using System.Data;

namespace Application.Interfaces.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Payment> Payment { get; }
        Task SaveChangesAsync(CancellationToken cancellation = default);
        IDbTransaction BeginTransaction();

        //IPaymentRepository PaymentPrivate { get; }
    }
}
