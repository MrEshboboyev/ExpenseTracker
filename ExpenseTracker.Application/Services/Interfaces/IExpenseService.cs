using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services.Interfaces
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetAllExpenses(string userId);
        Expense GetExpenseById(int id, string userId);
        void CreateExpense(Expense expense);
        void UpdateExpense(Expense expense);
        bool DeleteExpense(int id, string userId);
        
        // filtered methods
        IEnumerable<Expense> GetExpensesForPastWeek(string userId);
        IEnumerable<Expense> GetExpensesForLastMonth(string userId);
        IEnumerable<Expense> GetExpensesForLast3Months(string userId);
        IEnumerable<Expense> GetExpensesForDateRange(string userId, DateTime startDate, DateTime endDate);
    }
}
