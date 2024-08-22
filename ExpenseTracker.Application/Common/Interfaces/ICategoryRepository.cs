using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Common.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category entity);
    }
}
