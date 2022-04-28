using ElectronNET.API;
using ElectronNET.API.Entities;
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

//Task.Run(async () => await Electron.WindowManager.CreateWindowAsync());
//var display = await Electron.Screen.GetPrimaryDisplayAsync();

/*await Task.Run(async () => await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
{
    Width = display.Size.Width,
    Height = display.Size.Height
}));*/

app.Run();