using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MiniAccountSystem.Models;
using System.Data;

namespace MiniAccountSystem.Pages.ChartOfAccounts
{
    [Authorize(Roles = "Accountant,Admin")] // Only accessible by Accountant and Admin
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string AccountName { get; set; }

        [BindProperty]
        public int? ParentId { get; set; }

        public List<ChartOfAccount> AllAccounts { get; set; }
        public List<ChartOfAccount> HierarchicalAccounts { get; set; }

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

            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@AccountName", AccountName);
                cmd.Parameters.AddWithValue("@ParentId", (object?)ParentId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", DBNull.Value); // Not used for INSERT

                cmd.ExecuteNonQuery();
            }

            return RedirectToPage();
        }

        private void LoadAccounts()
        {
            AllAccounts = new List<ChartOfAccount>();
            HierarchicalAccounts = new List<ChartOfAccount>();

            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@Id", DBNull.Value);
                cmd.Parameters.AddWithValue("@AccountName", DBNull.Value);
                cmd.Parameters.AddWithValue("@ParentId", DBNull.Value);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var account = new ChartOfAccount
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        AccountName = reader["AccountName"].ToString(),
                        ParentId = reader["ParentId"] == DBNull.Value ? null : Convert.ToInt32(reader["ParentId"])
                    };

                    AllAccounts.Add(account);
                }
            }

            // Optional: Load as flat list for now
            HierarchicalAccounts = AllAccounts;
        }
    }
}
