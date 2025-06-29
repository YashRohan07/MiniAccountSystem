using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MiniAccountSystem.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public List<UserWithRoles> UsersWithRoles { get; set; } = new();

        public class UserWithRoles
        {
            public IdentityUser User { get; set; } = default!;
            public List<string> Roles { get; set; } = new();
        }

        public async Task OnGetAsync()
        {
            var users = _userManager.Users.ToList();
            UsersWithRoles.Clear();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UsersWithRoles.Add(new UserWithRoles
                {
                    User = user,
                    Roles = roles.ToList()
                });
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToPage();
        }
    }
}
