using Application.Interfaces.Persistence;
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext applicationDbContext;
        public IGenericRepository<Payment> Payment { get; }

        public UnitOfWork(
            ApplicationDbContext applicationDbContext, 
            IGenericRepository<Payment> paymentRepository)
        {
            this.applicationDbContext = applicationDbContext;
            this.Payment = paymentRepository;
        }


        public IDbTransaction BeginTransaction() => 
            applicationDbContext.Database.BeginTransaction().GetDbTransaction();

        public async Task SaveChangesAsync(CancellationToken cancellation = default) =>
            await applicationDbContext.SaveChangesAsync(cancellation);

        public void Dispose() => applicationDbContext.Dispose();
    }
}
