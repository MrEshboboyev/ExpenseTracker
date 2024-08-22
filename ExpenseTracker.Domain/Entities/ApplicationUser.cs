using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }    
    }
}
