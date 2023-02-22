using Microsoft.EntityFrameworkCore;
using frushgah.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<frushgahContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("frushgahContext") ?? throw new InvalidOperationException("Connection string 'frushgahContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddProgressiveWebApp();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(100000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//session
app.UseSession();


//app.UseBrowserLink();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=groups}/{action=Index}/{id?}");

app.Run();

