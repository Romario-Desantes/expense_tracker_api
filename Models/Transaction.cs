namespace expense_tracker_api.Models
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Title { get; set; }
        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Guid CategoryId { get; set; } 
    }
}
