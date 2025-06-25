using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniAccountSystem.Pages.Users
{
    [Authorize(Roles = "Admin")] // Only users with "Admin" role can access this page
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}
