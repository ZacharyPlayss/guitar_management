using System;
using System.Configuration;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GuitarManagement.BL;
using GuitarManagement.DAL;
using GuitarManagement.DAL.EF;
using GuitarManagement.UI.MVC;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GuitarDbContextConnection") ?? throw new InvalidOperationException("Connection string 'GuitarDbContextConnection' not found.");
var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
    
builder.Services.AddDbContext<GuitarDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GuitarDbContext>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IManager, Manager>();

//google sign up/in
/*builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = googleClientId;
    googleOptions.ClientSecret = googleClientSecret;
});*/


builder.Services.ConfigureApplicationCookie (cfg =>
{
    cfg.Events.OnRedirectToLogin += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments ("/api"))
        {
            ctx.Response.StatusCode = 401;
        }
        return Task.CompletedTask;
    };
    cfg.Events.OnRedirectToAccessDenied += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments ("/api"))
        {
            ctx.Response.StatusCode = 403;
        }
        return Task.CompletedTask;
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GuitarDbContext>();
    if (context.CreateDatabase(dropDataBase: true))
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        SeedIdentity(userManager,roleManager);
        DataSeeder.Seed(context);
    }
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();

void SeedIdentity(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    var userRole = new IdentityRole(CustomIdentityConstants.UserRole);
    roleManager.CreateAsync(userRole).Wait();
    var adminRole = new IdentityRole(CustomIdentityConstants.AdminRole);
    roleManager.CreateAsync(adminRole).Wait();
    var user1 = new IdentityUser
    {
        UserName = "admin@guitarmanagement.be",
        Email = "admin@guitarmanagement.be",
        EmailConfirmed = true
    };
    userManager.CreateAsync(user1, "Test1234?");
    var user2 = new IdentityUser
    {
        UserName = "user@guitarmanagement.be",
        Email = "user@guitarmanagement.be",
        EmailConfirmed = true
    };
    userManager.CreateAsync(user2, "Test1234?");

    userManager.AddToRoleAsync(user1, CustomIdentityConstants.AdminRole);
    userManager.AddToRoleAsync(user2, CustomIdentityConstants.UserRole);

}

public partial class Program { };