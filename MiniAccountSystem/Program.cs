using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniAccountSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DB context with SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add Identity services with Role support
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // You can change this if needed
})
.AddRoles<IdentityRole>() // Add Role management
.AddEntityFrameworkStores<ApplicationDbContext>();

// Add Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Use strict transport security
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Enable login, logout, register
app.UseAuthorization();  // Enable role-based access

app.MapRazorPages();

app.Run();
