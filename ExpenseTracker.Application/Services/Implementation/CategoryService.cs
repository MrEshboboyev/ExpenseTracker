using ExpenseTracker.Application.Common.Interfaces;
using ExpenseTracker.Application.Services.Interfaces;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        // inject UnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateCategory(Category category)
        {
            ArgumentNullException.ThrowIfNull(category);

            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
        }

        public bool DeleteCategory(int id)
        {
            try
            {
                // get category by id
                var category = _unitOfWork.Category.Get(c => c.Id == id);
                if (category != null)
                {
                    _unitOfWork.Category.Remove(category);
                    _unitOfWork.Save();
                    return true;
                }
                else
                {
                    throw new InvalidOperationException($"Category with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _unitOfWork.Category.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return _unitOfWork.Category.Get(c => c.Id == id);
        }

        public void UpdateCategory(Category category)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
        }
    }
}
