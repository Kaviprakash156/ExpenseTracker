using AutoMapper;
using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseDto>> GetAllExpenses()
        {
            var expenses = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
        }

        public async Task<ExpenseDto?> GetExpenseById(int id)
        {
            var expense = await _repository.GetByIdAsync(id);
            if (expense == null)
                return null;

            return _mapper.Map<ExpenseDto>(expense);
        }

        public async Task AddExpense(CreateExpenseDto dto)
        {
            var expense = _mapper.Map<Expense>(dto);

            // Set default date
            expense.Date = DateTime.UtcNow;

            await _repository.AddAsync(expense);
        }

        public async Task UpdateExpense(int id, CreateExpenseDto dto)
        {
            var existingExpense = await _repository.GetByIdAsync(id);

            if (existingExpense == null)
                throw new Exception("Expense not found");

            // Map updated fields
            _mapper.Map(dto, existingExpense);

            await _repository.UpdateAsync(existingExpense);
        }

        public async Task DeleteExpense(int id)
        {
            var existingExpense = await _repository.GetByIdAsync(id);

            if (existingExpense == null)
                throw new Exception("Expense not found");

            await _repository.DeleteAsync(id);
        }
    }
}