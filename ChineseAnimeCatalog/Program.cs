using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using ChineseAnimeCatalog.Services;


var builder = WebApplication.CreateBuilder(args);

// Добавляем конфигурацию
builder.Configuration.AddJsonFile("appsettings.json");

// Добавляем MVC и Razor
builder.Services.AddControllersWithViews();

// Регистрируем наши сервисы
builder.Services.AddSingleton<DatabaseContext>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new DatabaseContext(config);
});

builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<AnimeService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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