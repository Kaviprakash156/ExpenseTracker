using ExpenseTracker.Application.DTOs;

namespace ExpenseTracker.Application.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDto>> GetAllExpenses();
        Task<ExpenseDto?> GetExpenseById(int id);
        Task AddExpense(CreateExpenseDto expenseDto);
        Task UpdateExpense(int id, CreateExpenseDto expenseDto);
        Task DeleteExpense(int id);
    }
}