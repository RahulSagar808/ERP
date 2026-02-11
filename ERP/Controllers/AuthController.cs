using ERP.ApplicationCore.Models;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ERP.Controllers;
public class AuthController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IToastNotification _toastNotification;
    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IToastNotification toastNotification)
    {
        _userManager = userManager; _signInManager = signInManager;
        _toastNotification = toastNotification;
    }
    public IActionResult ForgotPasswordBasic() => View();
    public IActionResult LoginBasic() => View();
    [HttpPost]

    public async Task<IActionResult> LoginBasic(LoginModel model)
    {
        if (!ModelState.IsValid) return View(model);

        ApplicationUser user;

        // Check if input is an email
        if (model.EmailOrUsername.Contains("@"))
            user = await _userManager.FindByEmailAsync(model.EmailOrUsername);
        else
            user = await _userManager.FindByNameAsync(model.EmailOrUsername);

        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _toastNotification.AddSuccessToastMessage("Logged in successfully");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Invalid login attempt");
            }
        }
        else
        {
            _toastNotification.AddErrorToastMessage("Invalid login attempt");
        }

        return View(model);
    }

    public IActionResult RegisterBasic() => View();
    [HttpPost]
    public async Task<IActionResult> RegisterBasic(RegisterModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.Username,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            _toastNotification.AddSuccessToastMessage("Registration successful");
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("LoginBasic");
    }

    public async Task<IActionResult> ChangePassword()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("LoginBasic");
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        await _signInManager.ForgetTwoFactorClientAsync();
        await _signInManager.RefreshSignInAsync(user);

        _toastNotification.AddSuccessToastMessage("Password changed successfully");
        return RedirectToAction("LoginBasic");
    }
}