using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using esencialAdmin.Models.ManageViewModels;
using esencialAdmin.Services;
using esencialAdmin.Data;
using esencialAdmin.Extensions;

namespace esencialAdmin.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var indexModel = new IndexViewModel();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                this.AddNotification("Konnte Daten nicht laden", NotificationType.ERROR);
                return View(indexModel);
            }

            var employeeAccountViewModel = new EmployeeAccountViewModel
            {
                Email = user.Email,
            };
            indexModel.employeeAccountViewModel = employeeAccountViewModel;

            var changePasswordViewModel = new ChangePasswordViewModel { };
            indexModel.changePasswordViewModel = changePasswordViewModel;

            return View(indexModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeAccount(EmployeeAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }

                var setUserNameResult = await _userManager.SetUserNameAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    await _userManager.SetEmailAsync(user, user.UserName);
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
                await _signInManager.RefreshSignInAsync(user);
            }

            this.AddNotification("Dein Profil wurde aktualisiert", NotificationType.SUCCESS);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                if (changePasswordResult.Errors.FirstOrDefault().Code == "PasswordMismatch")
                {
                    this.AddNotification("Das Passwort konnte nicht geändert werden. Überprüfe ob das aktuelle Korrekt ist", NotificationType.ERROR);
                }
                else
                {
                    this.AddNotification("Das Passwort konnte nicht geändert werden.", NotificationType.ERROR);
                }

                return RedirectToAction(nameof(Index));
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            this.AddNotification("Dein Passwort wurde geändert", NotificationType.SUCCESS);

            return RedirectToAction(nameof(Index));
        }

        #region Helpers


        #endregion
    }
}