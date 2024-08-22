using ExpenseTracker.Application.Common.Interfaces;
using ExpenseTracker.Infrastructure.Data;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        // inject DB
        private readonly ApplicationDbContext _db;
        public IExpenseRepository Expense { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IApplicationUserRepository User { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Expense = new ExpenseRepository(db);
            Category = new CategoryRepository(db);
            User = new ApplicationUserRepository(db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
