namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string ServiceProvider { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pendiente";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
