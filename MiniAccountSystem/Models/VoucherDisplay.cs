namespace MiniAccountSystem.Models { }
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
