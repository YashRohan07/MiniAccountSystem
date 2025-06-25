using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniAccountSystem.Pages.ChartOfAccounts
{
    [Authorize(Roles = "Accountant,Admin")] // Only accessible by Accountant and Admin
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}
