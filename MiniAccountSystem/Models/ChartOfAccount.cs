namespace MiniAccountSystem.Models
{
    public class ChartOfAccount
    {
        public int Id { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public bool IsActive { get; set; } = true;
        public string? ParentAccountName { get; set; }
    }
}
