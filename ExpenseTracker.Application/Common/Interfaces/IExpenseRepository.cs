using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Common.Interfaces
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        void Update(Expense entity);
    }
}
