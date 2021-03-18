using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpinningFilm.Data;
using SpinningFilm.Models;
using SpinningFilm.ViewModels;
using SpinningFilm.Extensions;

namespace SpinningFilm.Controllers
{
    //[Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SpinningFilmContext _context;

        public AccountController(UserManager<AppUser> userManager, SpinningFilmContext context)
        {
            this._userManager = userManager;
            _context = context;
        }

        public IActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.AppUserId.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.NormalizeEmail));
                    identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }

                ViewBag.Message = "Invalid username/password";
                return View();
            }

            var errors = ModelState.Keys.Select(e => "<li>" + e + "</li>");
            ViewBag.Message = errors;
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = null;
                var user = await _userManager.FindByNameAsync(model.Email);

                //if (user != null)
                //{
                //    return new ResultViewModel
                //    {
                //        Status = Status.Error,
                //        Message = "Invalid data",
                //        Data = "<li>User already exists</li>"
                //    };
                //}

                user = new AppUser
                {
                    AppUserId = Guid.NewGuid(),
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var resultErrors = result.Errors.Select(e => "<li>" + e.Description + "</li>");
                    //return new ResultViewModel
                    //{
                    //    Status = Status.Error,
                    //    Message = "Invalid data",
                    //    Data = string.Join("", resultErrors)
                    //};
                    return View(model);
                }
            }

            var errors = ModelState.Keys.Select(e => "<li>" + e + "</li>");
            return View(model);
            //return new ResultViewModel
            //{
            //    Status = Status.Error,
            //    Message = "Invalid data",
            //    Data = string.Join("", errors)
            //};
        }

        public IActionResult Manage()
        {
            AppUser user = _context.AppUsers.SingleOrDefault(m => m.AppUserId == Guid.Parse(User.Identity.NameId()));

            return View(user);
        }

        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(forgotPassword.Email);
                if(user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    string token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    string passwordResetLink = Url.Action("ResetPassword", "Account", new { email = forgotPassword.Email, token }, Request.Scheme);
                }

                return View("ForgotPasswordConfirmation");
            }

            return View(forgotPassword);
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel reset)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(reset.Email);
                if(user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, reset.Token, reset.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(reset);
                }
                return View("ResetPasswordConfirmation");
            }
            return View(reset);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}