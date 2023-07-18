
using ZL.Blog.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("collect.json", optional: true, reloadOnChange: true);

builder.Services.AddOptions()
    .Configure<CollectConfig>(e => builder.Configuration.Bind(e));

builder.Services.AddScoped<CollectConfig>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
