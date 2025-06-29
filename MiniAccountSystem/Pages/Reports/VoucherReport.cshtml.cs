using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAccountSystem.Pages.Reports
{
    [Authorize(Roles = "Viewer,Admin")]
    public class VoucherReportModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public VoucherReportModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty(SupportsGet = true)]
        public DateTime? FromDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? ToDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? VoucherType { get; set; }

        public List<VoucherDisplay> VoucherReport { get; set; } = new();

        public void OnGet()
        {
            LoadReportData();
        }

        public IActionResult OnGetExport()
        {
            LoadReportData();

            var csv = new StringBuilder();
            csv.AppendLine("Voucher ID,Voucher Type,Date,Reference No,Account,Debit,Credit");

            foreach (var v in VoucherReport)
            {
                csv.AppendLine($"{v.VoucherId},{v.VoucherType},{v.VoucherDate:yyyy-MM-dd},{v.ReferenceNo},{v.AccountName},{v.Debit},{v.Credit}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            var fileName = $"VoucherReport_{DateTime.Now:yyyyMMddHHmmss}.csv";

            return File(bytes, "text/csv", fileName);
        }

        private void LoadReportData()
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection")!;
            using SqlConnection conn = new(connStr);
            conn.Open();

            string sql = @"
                SELECT vh.Id AS VoucherId, vh.VoucherType, vh.VoucherDate, vh.ReferenceNo,
                       coa.AccountName, v.Debit, v.Credit
                FROM VoucherHeaders vh
                JOIN Vouchers v ON vh.Id = v.VoucherId
                JOIN ChartOfAccounts coa ON v.AccountId = coa.Id
                WHERE 1=1";

            if (FromDate.HasValue)
                sql += " AND vh.VoucherDate >= @FromDate";
            if (ToDate.HasValue)
                sql += " AND vh.VoucherDate <= @ToDate";
            if (!string.IsNullOrEmpty(VoucherType))
                sql += " AND vh.VoucherType = @VoucherType";

            using SqlCommand cmd = new(sql, conn);

            if (FromDate.HasValue)
                cmd.Parameters.AddWithValue("@FromDate", FromDate.Value);
            if (ToDate.HasValue)
                cmd.Parameters.AddWithValue("@ToDate", ToDate.Value);
            if (!string.IsNullOrEmpty(VoucherType))
                cmd.Parameters.AddWithValue("@VoucherType", VoucherType);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                VoucherReport.Add(new VoucherDisplay
                {
                    VoucherId = reader.GetInt32(reader.GetOrdinal("VoucherId")),
                    VoucherType = reader.GetString(reader.GetOrdinal("VoucherType")),
                    VoucherDate = reader.GetDateTime(reader.GetOrdinal("VoucherDate")),
                    ReferenceNo = reader.GetString(reader.GetOrdinal("ReferenceNo")),
                    AccountName = reader.GetString(reader.GetOrdinal("AccountName")),
                    Debit = reader.GetDecimal(reader.GetOrdinal("Debit")),
                    Credit = reader.GetDecimal(reader.GetOrdinal("Credit"))
                });
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
}
