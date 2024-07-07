using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverMonitoring.Data.Models;
using RiverMonitoring.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Account
{
    /// <summary>
    /// Page model for user login.
    /// </summary>
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputLoginViewModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet()
        {
            StatusMessage = string.Empty;
        }

        /// <summary>
        /// Handles the user login process.
        /// </summary>
        /// <returns>Redirects to the index page on successful login.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(Input.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, Input.Password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (!roles.Any())
                        {
                            await _userManager.AddToRoleAsync(user, user.AccessLevel);
                        }

                        var userClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName)
                        };

                        foreach (var role in roles)
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var identity = new ClaimsIdentity(userClaims, "User Identity");
                        var principal = new ClaimsPrincipal(identity);

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        StatusMessage = "";
                        return RedirectToPage("/Index");
                    }
                }
                StatusMessage = "Invalid login attempt.";
            }

            return Page();
        }
    }
}
