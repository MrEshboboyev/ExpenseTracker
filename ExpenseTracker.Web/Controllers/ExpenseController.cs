using ExpenseTracker.Application.Services.Interfaces;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        // inject IExpenseService
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public IActionResult GetAllExpenses()
        {
            var expenses = _expenseService.GetAllExpenses();
            return Ok(expenses);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetExpenseById(int id)
        {
            var expense = _expenseService.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpPost]
        public IActionResult CreateExpense([FromBody] Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _expenseService.CreateExpense(expense);
            return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, expense);
        }

        [HttpPut]
        public IActionResult UpdateExpense(int id, [FromBody] Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest("Id and Expense id must be equal!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _expenseService.UpdateExpense(expense);

            return NoContent(); 
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult DeleteExpense(int id)
        {
            _expenseService.DeleteExpense(id);
            return NoContent(); 
        }
    }
}
