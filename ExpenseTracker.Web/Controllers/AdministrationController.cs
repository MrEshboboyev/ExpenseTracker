using ExpenseTracker.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        // inject IRoleService
        private readonly IRoleService _roleService;

        public AdministrationController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("get-all-roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _roleService.GetAllRolesAsync());
        }

        [HttpGet("get-role-by-name")]
        public async Task<IActionResult> GetRoleByName(string roleName)
        {
            var role = await _roleService.GetRoleByNameAsync(roleName); 
            if (role is not null) return Ok(role);
            return BadRequest($"Role with Name {roleName} is not found.");
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _roleService.CreateRoleAsync(roleName);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.First().Description);
        }
        
        [HttpPut("update-role")]
        public async Task<IActionResult> UpdateRole(string roleId, string roleName)
        {
            var result = await _roleService.UpdateRoleAsync(roleId, roleName);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.First().Description);
        }
        
        [HttpDelete("delete-role")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var result = await _roleService.DeleteRoleAsync(roleName);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.First().Description);
        }
    }
}
