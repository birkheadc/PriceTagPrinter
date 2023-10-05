using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using PriceTagPrinter.Config;
using PriceTagPrinter.Contexts;
using PriceTagPrinter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string goodsConnectionString = builder.Configuration.GetConnectionString("Goods");
string priceTagConnectionString = builder.Configuration.GetConnectionString("PriceTag");

BackendUrlConfig urlConfig = new()
{
  Url = Environment.GetEnvironmentVariable("ASPNETCORE_BACKENDURL") ?? ""
};

if (urlConfig.Url is null || urlConfig.Url == "")
{
  Console.WriteLine("Backend URL not set, aborting.");
  throw new NullReferenceException();
}

builder.Services.AddSingleton(urlConfig);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<IUploadService, UploadService>();

builder.Services.AddDbContextFactory<GoodsContext>(o => o.UseSqlite(goodsConnectionString));
builder.Services.AddDbContextFactory<PriceTagContext>(o => o.UseSqlite(priceTagConnectionString));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment() == false)
{
  app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers();
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
