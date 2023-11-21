using ContractManagementValue.Interfaces;
using ContractManagementValue.Models;
using ContractManagementValue.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Debugger.Contracts.HotReload;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connection);
    options.EnableSensitiveDataLogging();
}, ServiceLifetime.Scoped);
//builder.Services.AddScoped<ILogin, UserRepository>();

builder.Services.AddScoped<ProjectRepo>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddScoped(typeof(IFileUpload), typeof(FileUploadRepo));
builder.Services.AddMvc();

builder.Services.AddMvc();
builder.Services.AddSignalR();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;

});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseWebSockets();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();
app.UseCookiePolicy();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();
//app.MapHub<SiteHub>("/siteHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");


app.Run();



