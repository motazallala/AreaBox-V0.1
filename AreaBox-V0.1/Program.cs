using AreaBox_V0._1.Data;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Data.Seeders;
using AreaBox_V0._1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AreaBoxDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AreaBoxDb"));
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AreaBoxDbContext>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;

    try
    {
        var dbContext = serviceProvider.GetRequiredService<AreaBoxDbContext>();
        dbContext.Database.Migrate();

        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        new RolesSeeder(roleManager)
            .SeedAsync(dbContext, loggerFactory.CreateLogger<RolesSeeder>())
            .GetAwaiter()
            .GetResult();

        new UsersSeeder(userManager)
            .SeedAsync(dbContext, loggerFactory.CreateLogger<UsersSeeder>())
            .GetAwaiter()
            .GetResult();
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
