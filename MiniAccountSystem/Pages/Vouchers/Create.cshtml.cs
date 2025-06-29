using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MiniAccountSystem.Models;

namespace MiniAccountSystem.Pages.Vouchers
{
    [Authorize(Roles = "Accountant")]
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _config;

        public CreateModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty]
        public string VoucherType { get; set; } = string.Empty;

        [BindProperty]
        public DateTime VoucherDate { get; set; } = DateTime.Today;

        [BindProperty]
        public string ReferenceNo { get; set; } = string.Empty;

        [BindProperty]
        public List<VoucherItem> VoucherItems { get; set; } = new();

        public List<ChartOfAccount> AccountList { get; set; } = new();

        public void OnGet()
        {
            LoadAccounts();
        }

        public IActionResult OnPost()
        {
            LoadAccounts();

            if (!ModelState.IsValid || VoucherItems.Count == 0)
            {
                ModelState.AddModelError("", "At least one item required.");
                return Page();
            }

            var cs = _config.GetConnectionString("DefaultConnection")!;
            using var conn = new SqlConnection(cs);
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var insertHeader = @"INSERT INTO VoucherHeaders (VoucherType, VoucherDate, ReferenceNo)
                                     OUTPUT INSERTED.Id VALUES (@VoucherType, @VoucherDate, @ReferenceNo)";
                using var cmdHeader = new SqlCommand(insertHeader, conn, tx);
                cmdHeader.Parameters.AddWithValue("@VoucherType", VoucherType);
                cmdHeader.Parameters.AddWithValue("@VoucherDate", VoucherDate);
                cmdHeader.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);

                int headerId = (int)cmdHeader.ExecuteScalar();

                foreach (var item in VoucherItems)
                {
                    if ((item.Debit > 0 && item.Credit > 0) || (item.Debit == 0 && item.Credit == 0))
                    {
                        ModelState.AddModelError("", "Each item must have either Debit OR Credit.");
                        tx.Rollback();
                        return Page();
                    }

                    var insertItem = @"INSERT INTO Vouchers (VoucherId, AccountId, Debit, Credit)
                                       VALUES (@VoucherId, @AccountId, @Debit, @Credit)";
                    using var cmdItem = new SqlCommand(insertItem, conn, tx);
                    cmdItem.Parameters.AddWithValue("@VoucherId", headerId);
                    cmdItem.Parameters.AddWithValue("@AccountId", item.AccountId);
                    cmdItem.Parameters.AddWithValue("@Debit", item.Debit);
                    cmdItem.Parameters.AddWithValue("@Credit", item.Credit);
                    cmdItem.ExecuteNonQuery();
                }

                tx.Commit();
                return RedirectToPage("/Vouchers/Index");
            }
            catch (Exception ex)
            {
                tx.Rollback();
                ModelState.AddModelError("", "ERROR: " + ex.Message);
                return Page();
            }
        }

        private void LoadAccounts()
        {
            AccountList.Clear();
            var cs = _config.GetConnectionString("DefaultConnection")!;
            using var conn = new SqlConnection(cs);
            conn.Open();

            using var cmd = new SqlCommand("SELECT Id, AccountName FROM ChartOfAccounts WHERE IsActive = 1", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                AccountList.Add(new ChartOfAccount
                {
                    Id = rdr.GetInt32(0),
                    AccountName = rdr.GetString(1)
                });
            }
        }
    }
}
