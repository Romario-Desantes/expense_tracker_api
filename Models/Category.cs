namespace expense_tracker_api.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
    }
}
