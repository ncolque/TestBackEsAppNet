using Application.Abstractions.Messaging;
using Application.Dtos.Payments;

namespace Application.UseCases.Payments.Queries.GetPaymentsByCustomer
{
    public class GetPaymentsByCustomerQuery : IQuery<List<PaymentResponseDto>>
    {
        public Guid CustomerId { get; set; }
    }
}
