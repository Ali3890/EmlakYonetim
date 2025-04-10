using EmlakYonetim.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Kimlik doðrulama ekle
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Login/Index"; // Giriþ yapýlmamýþsa yönlendirme
        
    });

// Yetkilendirme ekle
builder.Services.AddAuthorization();

/*builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter()); // Global yetkilendirme
});*/

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VeriTabani>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("BaglantiCumlesi"));
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EmlakYonetim}/{action=Index}/{ID?}");

app.Run();
