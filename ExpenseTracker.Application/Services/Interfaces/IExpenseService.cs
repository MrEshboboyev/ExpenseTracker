using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services.Interfaces
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetAllExpenses(string userId);
        IEnumerable<Expense> GetFilteredExpenses(string userId, DateTime? startDate, DateTime? endDate);
        Expense GetExpenseById(int id, string userId);
        void CreateExpense(Expense expense);
        void UpdateExpense(Expense expense);
        bool DeleteExpense(int id, string userId);
    }
}
