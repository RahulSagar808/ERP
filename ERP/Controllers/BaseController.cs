using ERP.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly UserManager<ApplicationUser> _userManager;

        public BaseController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // ✅ Get current logged-in user
        protected async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(User);
        }

        // ✅ Get current user Id
        protected string? CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        // ✅ Get current user Email
        protected string? CurrentUserEmail => User.FindFirstValue(ClaimTypes.Email);

        // ✅ Example: Get custom property (FullName)
        protected async Task<string?> GetCurrentUserFullNameAsync()
        {
            var user = await GetCurrentUserAsync();
            return user?.FirstName + " " + user?.LastName;
        }
    }
}

