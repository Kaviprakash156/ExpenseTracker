public class ExpenseDto   // ✅ must be public
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public DateTime Date { get; set; }
}