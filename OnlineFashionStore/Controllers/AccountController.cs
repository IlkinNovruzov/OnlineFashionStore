using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using OnlineFashionStore.Extensions;
using OnlineFashionStore.Models.DataModels;
using OnlineFashionStore.Models.ViewModels;
using System.Data;

namespace OnlineFashionStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUserLogin appUserLogin)
        {
            var user = await _userManager.FindByNameAsync(appUserLogin.Username);
            if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");
                return RedirectToAction("Confirmation", "Account");

            }
            var result = await _signInManager.PasswordSignInAsync(appUserLogin.Username, appUserLogin.Password, false, true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserRegister appUserRegister)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = appUserRegister.Username,
                    Email = appUserRegister.Email,
                    Name = appUserRegister.Name,
                    Surname = appUserRegister.Surname,
                };
                var result = await _userManager.CreateAsync(appUser, appUserRegister.Password);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = appUser.Id, token = token }, Request.Scheme);
                    await _emailService.SendEmailAsync(appUserRegister.Email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

                    //var mimeMessage = new MimeMessage();
                    //mimeMessage.From.Add(new MailboxAddress("Ilkin", "uomostore004@gmail.com"));
                    //mimeMessage.To.Add(MailboxAddress.Parse(appUserRegister.Email));
                    //mimeMessage.Subject = "Admin";
                    //mimeMessage.Body = new TextPart("plain") { Text = $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>." };

                    //using var client = new SmtpClient();
                    //await client.ConnectAsync("smtp.gmail.com", 465, true);
                    //await client.AuthenticateAsync("uomostore004@gmail.com", "vzza dzjt aaas okbo");
                    //await client.SendAsync(mimeMessage);
                    //await client.DisconnectAsync(true);
                    //var msg = new MimeMessage();
                    //var mailFrom = new MailboxAddress("Admin", "uomostore004@gmail.com");
                    //var mailTo = new MailboxAddress("User", appUserRegister.Email);
                    // await _emailService.SendEmailAsync(appUser.Email, "Confirm Email", $"{code}");
                    await _userManager.AddToRoleAsync(appUser, "User");

                    return RedirectToAction("Confirmation", "Account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(appUserRegister);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }

            return RedirectToAction("Error", "Home");
        }
        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
        }

    }
}
