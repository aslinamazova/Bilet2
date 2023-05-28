using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bizland.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Bizland.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM register)
    {
        if (!ModelState.IsValid) return View(register);
        IdentityUser identityUser = new IdentityUser
        {
            UserName = register.UserName,
            Email = register.Email,
        };

        var registerResult=await _userManager.CreateAsync(identityUser,register.Password);
        if (!registerResult.Succeeded)
        {
            foreach (var error in registerResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(register);
        }

        return RedirectToAction(nameof(Login));
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM login,string? ReturnUrl)
    {
        if (!ModelState.IsValid) return View(login);
        IdentityUser user = await _userManager.FindByNameAsync(login.UserName);
        if (user==null)
        {
            ModelState.AddModelError("", "Email or password wrong");
        }

        var sigingResult = await _signInManager.CheckPasswordSignInAsync(user, login.Password,true);
        if (!sigingResult.Succeeded)
        {
            ModelState.AddModelError("", "Email or password is wrong");
            return View(login);
        }
         await _signInManager.SignInAsync(user, login.RememberMe);
        if (Url.IsLocalUrl(ReturnUrl))
        {
            return RedirectToAction(ReturnUrl);
        }
        return RedirectToAction("Index","Home");
    }

    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> AddRoles()
    {
        foreach (var role in Enum.GetValues(typeof(UserRoles)))
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
            }
        }
        return Json("Ok");
    }

    enum UserRoles
    {
        Admin,
        User,
        Moderator
    }
}

