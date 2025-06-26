namespace MiniAccountSystem.Models
{
    public class ChartOfAccount
    {
        public int Id { get; set; }  //Unique identifier of the account (Primary Key).

        public string AccountName { get; set; } = string.Empty;  //Name like "Cash", "Bank"

        public int? ParentId { get; set; }  //Supports hierarchical parent-child relationships.

        public bool IsActive { get; set; } = true;  //You can use this later to deactivate an account instead of deleting.

        public string? ParentAccountName { get; set; }  //showing parent name in the UI, populated when needed (not stored in DB).
    }
}
