using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniAccountSystem.Pages.Roles
{
    [Authorize(Roles = "Admin")] // Only accessible by Admin users
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
           
        }
    }
}
