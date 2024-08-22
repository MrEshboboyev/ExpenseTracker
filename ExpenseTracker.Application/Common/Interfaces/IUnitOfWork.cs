namespace ExpenseTracker.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IExpenseRepository Expense {  get; }
        ICategoryRepository Category {  get; }
        IApplicationUserRepository User { get; }
        void Save();
    }
}
