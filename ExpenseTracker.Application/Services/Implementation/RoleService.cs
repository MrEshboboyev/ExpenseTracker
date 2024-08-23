using ExpenseTracker.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Application.Services.Implementation
{
    public class RoleService : IRoleService
    {
        // inject roleManager
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is not null) await _roleManager.DeleteAsync(role);
            return IdentityResult.Failed(new IdentityError
            {
                Description = $"Role with ID '{roleId}' not found."
            });
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
        {
            return _roleManager.Roles;
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task<IdentityResult> UpdateRoleAsync(string roleId, string newRoleName)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is not null)
            {
                role.Name = newRoleName;
                await _roleManager.DeleteAsync(role);
            }
            return IdentityResult.Failed(new IdentityError
            {
                Description = $"Role with ID '{roleId}' not found."
            });
        }
    }
}
