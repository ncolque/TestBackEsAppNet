using Application.Abstractions.Messaging;
namespace Application.UseCases.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : ICommand<bool>
    {
        public Guid CustomerId { get; set; }
        public string ServiceProvider { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
