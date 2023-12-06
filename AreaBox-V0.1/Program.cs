using AreaBox_V0._1.Common;
using AreaBox_V0._1.Data;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Repositories;
using AreaBox_V0._1.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


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

builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


/*builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";

});*/


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


        new AreaBoxDbContextSeeder(userManager, roleManager)
            .SeedAsync(dbContext,
                       serviceScope
                       .ServiceProvider
                       .GetService<ILoggerFactory>()
                       .CreateLogger(typeof(AreaBoxDbContextSeeder)))
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chathub");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Chat}/{id?}");
});

app.MapRazorPages();
app.Run();
