using ExpenseTracker.Application.Common.Interfaces;
using ExpenseTracker.Application.Services.Interfaces;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services.Implementation
{
    public class ExpenseService : IExpenseService
    {
        // inject UnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        public ExpenseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateExpense(Expense expense)
        {
            ArgumentNullException.ThrowIfNull(expense);

            _unitOfWork.Expense.Add(expense);
            _unitOfWork.Save();
        }

        public bool DeleteExpense(int id, string userId)
        {
            try
            {
                // get expense by id
                var expense = _unitOfWork.Expense.Get(c => c.Id == id && c.UserId == userId);
                if (expense != null)
                {
                    _unitOfWork.Expense.Remove(expense);
                    _unitOfWork.Save();
                    return true;
                }
                else
                {
                    throw new InvalidOperationException($"Expense with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public IEnumerable<Expense> GetAllExpenses(string userId)
        {
            return _unitOfWork.Expense.GetAll(e => e.UserId == userId);
        }

        public Expense GetExpenseById(int id, string userId)
        {
            return _unitOfWork.Expense.Get(e => e.Id == id && e.UserId == userId);
        }

        public IEnumerable<Expense> GetFilteredExpenses(string userId, DateTime? startDate, DateTime? endDate)
        {
            var query = _unitOfWork.Expense.GetAll(e => e.UserId == userId);

            if (startDate.HasValue)
                query = query.Where(e => e.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(e => e.Date <= endDate.Value);

            return query.ToList();
        }

        public void UpdateExpense (Expense expense)
        {
            _unitOfWork.Expense.Update(expense);
            _unitOfWork.Save();
        }
    }
}
