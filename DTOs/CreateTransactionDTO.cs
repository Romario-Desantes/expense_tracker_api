namespace expense_tracker_api.DTOs
{
    public class CreateTransactionDTO
    {
        public string? Title { get; set; }
        public decimal Amount { get; set; }
        public Guid CategoryId { get; set; }
    }
}
