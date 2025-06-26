using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniAccountSystem.Models; 

namespace MiniAccountSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Optional: Use this if you want to use EF Core to query the ChartOfAccounts table directly
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
    }
}
