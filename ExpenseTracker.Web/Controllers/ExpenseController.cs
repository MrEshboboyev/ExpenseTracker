using ExpenseTracker.Application.Services.Interfaces;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = _expenseService.GetAllExpenses(userId);
            return Ok(expenses);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetExpenseById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expense = _expenseService.GetExpenseById(id, userId);
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _expenseService.DeleteExpense(id, userId);
            return NoContent(); 
        }
    }
}
