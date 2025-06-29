using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MiniAccountSystem.Models;

namespace MiniAccountSystem.Pages.ChartOfAccounts
{
    [Authorize(Roles = "Accountant,Admin")]
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string AccountName { get; set; } = string.Empty;

        [BindProperty]
        public int? ParentId { get; set; }

        public List<ChartOfAccount> AllAccounts { get; set; } = new();
        public List<ChartOfAccount> HierarchicalAccounts { get; set; } = new();

        public void OnGet()
        {
            LoadAccounts();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadAccounts();
                return Page();
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@AccountName", AccountName);
                cmd.Parameters.AddWithValue("@ParentId", (object?)ParentId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", DBNull.Value);

                cmd.ExecuteNonQuery();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDeactivate(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE ChartOfAccounts 
                    SET IsActive = CASE WHEN IsActive = 1 THEN 0 ELSE 1 END
                    WHERE Id = @Id
                ", conn);

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM ChartOfAccounts WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }

            return RedirectToPage();
        }

        private void LoadAccounts()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT c.Id, c.AccountName, c.ParentId, 
                           p.AccountName AS ParentAccountName,
                           c.IsActive
                    FROM ChartOfAccounts c
                    LEFT JOIN ChartOfAccounts p ON c.ParentId = p.Id
                ", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AllAccounts.Add(new ChartOfAccount
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        AccountName = reader["AccountName"].ToString() ?? string.Empty,
                        ParentId = reader["ParentId"] == DBNull.Value ? null : Convert.ToInt32(reader["ParentId"]),
                        ParentAccountName = reader["ParentAccountName"] == DBNull.Value ? null : reader["ParentAccountName"].ToString(),
                        IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"])
                    });
                }
            }

            HierarchicalAccounts = AllAccounts;
        }
    }
}
