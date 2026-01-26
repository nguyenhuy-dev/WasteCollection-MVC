using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WasteCollection.Services.HuyNQ;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.MVCWebApp.HuyNQ.Controllers;

public class AuthController(SystemUserAccountService userService) : Controller
{
    private readonly SystemUserAccountService _userService = userService;

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userService.LoginAsync(request);

        if (user == null)
        {
            ViewBag.Error = "Invalid email or password.";
            return View(request);
        }

        // Create Claims
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserAccountId.ToString()),
            new(ClaimTypes.Name, user.FullName),
        };

        // Create Identity
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Sign-in user
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }

    public IActionResult Denied() => View();
}
