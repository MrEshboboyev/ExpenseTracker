using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<string> LoginAsync(LoginModel model);
        string GenerateJwtToken(ApplicationUser user);
    }
}
