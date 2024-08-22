using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services.Interfaces
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetAllExpenses();
        Expense GetExpenseById(int id);
        void CreateExpense(Expense expense);
        void UpdateExpense(Expense expense);
        bool DeleteExpense(int id);
    }
}
