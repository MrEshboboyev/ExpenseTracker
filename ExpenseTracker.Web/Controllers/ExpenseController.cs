using ExpenseTracker.Application.Common.Models;
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
        public IActionResult CreateExpense([FromBody] CreateExpenseDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var expense = new Expense()
            {
                Amount = model.Amount,
                CategoryId = model.CategoryId,
                Date = model.Date,
                Description = model.Description,
                UserId = userId
            };

            _expenseService.CreateExpense(expense);
            return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, expense);
        }

        [HttpPut]
        public IActionResult UpdateExpense(int id, [FromBody] UpdateExpenseDto model)
        {
            if (id != model.Id)
            {
                return BadRequest("Id and Expense id must be equal!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var expense = new Expense()
            {
                Id = model.Id,
                Amount = model.Amount,
                CategoryId = model.CategoryId,
                Date = model.Date,
                Description = model.Description,
                UserId = userId
            };

            _expenseService.UpdateExpense(expense); 

            return NoContent(); 
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult DeleteExpense(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expense = _expenseService.GetExpenseById(id, userId);
            if (expense == null)
            {
                return NotFound();
            }
            _expenseService.DeleteExpense(id, userId);
            return NoContent(); 
        }
    }
}
