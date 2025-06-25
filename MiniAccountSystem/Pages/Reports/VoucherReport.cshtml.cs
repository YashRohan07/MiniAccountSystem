using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniAccountSystem.Pages.Reports
{
    [Authorize(Roles = "Viewer,Admin")] // Accessible by Viewer or Admin only
    public class VoucherReportModel : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}
