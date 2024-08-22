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

        public bool DeleteExpense(int id)
        {
            try
            {
                // get expense by id
                var expense = _unitOfWork.Expense.Get(c => c.Id == id);
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

        public IEnumerable<Expense> GetAllExpenses()
        {
            return _unitOfWork.Expense.GetAll();
        }

        public Expense GetExpenseById(int id)
        {
            return _unitOfWork.Expense.Get(c => c.Id == id);
        }

        public void UpdateExpense (Expense expense)
        {
            _unitOfWork.Expense.Update(expense);
            _unitOfWork.Save();
        }
    }
}
