using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniAccountSystem.Data;
using MiniAccountSystem.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

// Step 1: Configure SQL Server DB context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Step 2: Configure Identity with Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to true if you enable email confirmation
})
.AddRoles<IdentityRole>() // Role support
.AddEntityFrameworkStores<ApplicationDbContext>();

// Step 3: Add Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// ? Step 4: Seed Roles (Admin, Accountant, Viewer) -- Use GetAwaiter().GetResult()
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    RoleSeeder.SeedRolesAsync(roleManager).GetAwaiter().GetResult();
}

// Step 5: Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
