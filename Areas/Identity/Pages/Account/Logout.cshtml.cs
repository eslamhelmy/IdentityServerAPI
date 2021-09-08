using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using UserIdentity.Entities;
using IdentityServer4.Services;

namespace UserIdentity.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private IIdentityServerInteractionService _interaction;
        public LogoutModel(SignInManager<ApplicationUser> signInManager,IIdentityServerInteractionService interaction, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
            _interaction = interaction;
        }

        public async Task<IActionResult> OnGet(string logoutId)
        {
           var context =  await _interaction.GetLogoutContextAsync(logoutId);
           if(context?.ShowSignoutPrompt == false)
           {
               return await this.OnPost(context.PostLogoutRedirectUri);
           }
           return Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("http://localhost:4200/home");
                return RedirectToPage();
            }
        }
    }
}
