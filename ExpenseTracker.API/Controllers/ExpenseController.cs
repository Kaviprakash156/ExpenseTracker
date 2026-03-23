using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service)
        {
            _service = service;
        }

        // GET: api/expense
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllExpenses();
            return Ok(data);
        }

        // GET: api/expense/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var expense = await _service.GetExpenseById(id);

            if (expense == null)
                return NotFound(new { message = "Expense not found" });

            return Ok(expense);
        }

        // POST: api/expense
        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseDto dto)
        {
            await _service.AddExpense(dto);
            return Ok(new { message = "Expense created successfully" });
        }

        // PUT: api/expense/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateExpenseDto dto)
        {
            await _service.UpdateExpense(id, dto);
            return Ok(new { message = "Expense updated successfully" });
        }

        // DELETE: api/expense/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteExpense(id);
            return Ok(new { message = "Expense deleted successfully" });
        }
    }
}