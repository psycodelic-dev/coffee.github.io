using Microsoft.EntityFrameworkCore;
using MelikeCoffee.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// SQLite bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=MelikeCoffee.db")); // veritabanı dosyası proje kökünde olacak

builder.Services.AddSession();


var app = builder.Build();

app.UseSession(); //middlewareler arasına ekle

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

app.Run();
