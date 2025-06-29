using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace MiniAccountSystem.Pages.Vouchers
{
    [Authorize(Roles = "Accountant")]
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<VoucherDisplay> Vouchers { get; set; } = new List<VoucherDisplay>();

        public void OnGet()
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM VouchersView", conn);
                // Assuming you have a view or table named VouchersView that contains necessary joined info
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var voucher = new VoucherDisplay
                        {
                            VoucherId = reader.GetInt32(reader.GetOrdinal("VoucherId")),
                            VoucherType = !reader.IsDBNull(reader.GetOrdinal("VoucherType")) ? reader.GetString(reader.GetOrdinal("VoucherType")) : string.Empty,
                            VoucherDate = reader.GetDateTime(reader.GetOrdinal("VoucherDate")),
                            ReferenceNo = !reader.IsDBNull(reader.GetOrdinal("ReferenceNo")) ? reader.GetString(reader.GetOrdinal("ReferenceNo")) : string.Empty,
                            AccountName = !reader.IsDBNull(reader.GetOrdinal("AccountName")) ? reader.GetString(reader.GetOrdinal("AccountName")) : string.Empty,
                            Debit = reader.GetDecimal(reader.GetOrdinal("Debit")),
                            Credit = reader.GetDecimal(reader.GetOrdinal("Credit"))
                        };
                        Vouchers.Add(voucher);
                    }
                }
            }
        }
    }

    public class VoucherDisplay
    {
        public int VoucherId { get; set; }

        public string VoucherType { get; set; } = string.Empty;

        public DateTime VoucherDate { get; set; }

        public string ReferenceNo { get; set; } = string.Empty;

        public string AccountName { get; set; } = string.Empty;

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }
    }
}
