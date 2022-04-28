using ElectronNET.API;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseElectron(args);
var services = builder.Services;
services.AddControllersWithViews();
services.AddElectron();
services.AddTransient<ICsvHelper, CsvHelper>();

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

Task.Run(async () => await Electron.WindowManager.CreateWindowAsync());

app.Run();