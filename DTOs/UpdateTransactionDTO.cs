namespace expense_tracker_api.DTOs
{
    public class UpdateTransactionDTO
    {
        public string? Title { get; set; }
        public decimal Amount { get; set; }
    }
}
