namespace Application.Dtos.Payments
{
    public class PaymentResponseDto
    {
        public Guid PaymentId { get; set; }
        public string ServiceProvider { get; set; }
        public decimal Amount { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
