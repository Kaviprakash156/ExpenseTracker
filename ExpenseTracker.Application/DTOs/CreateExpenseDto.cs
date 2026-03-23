namespace ExpenseTracker.Application.DTOs
{
    public class CreateExpenseDto   // ✅ FIXED
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}