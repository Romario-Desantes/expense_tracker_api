namespace expense_tracker_api.DTOs
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid CategoryId { get; set; }
    }
}
