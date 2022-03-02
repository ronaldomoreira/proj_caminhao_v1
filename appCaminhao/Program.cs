using BusinessSevice;
using DomainApp.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Context;

var builder = WebApplication.CreateBuilder(args);


string conn = builder.Configuration.GetConnectionString("DefaultConnection");
if (conn.Contains("%DataDirectory%"))
{
    conn = conn.Replace("%DataDirectory%", builder.Environment.ContentRootPath);
}

// Add services to the container.
var connectionString = conn; //builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<global::Repository.Context.IdentityAppDbContext>((global::Microsoft.EntityFrameworkCore.DbContextOptionsBuilder options) =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<global::Repository.Context.AppMainDbContext>((global::Microsoft.EntityFrameworkCore.DbContextOptionsBuilder options) =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>( (IdentityOptions options) =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<IdentityAppDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICaminhaoBusinessService, CaminhaoBusinessService>();
builder.Services.AddScoped<IRepositoryCaminhao, RepositoryCaminhao>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
