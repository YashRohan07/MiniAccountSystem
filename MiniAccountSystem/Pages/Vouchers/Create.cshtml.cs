using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniAccountSystem.Pages.Vouchers
{
    [Authorize(Roles = "Accountant")] // Only accessible by users with Accountant role
    public class CreateModel : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}
