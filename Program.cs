using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using PriceTagPrinter.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string goodsConnectionString = builder.Configuration.GetConnectionString("Goods");
string priceTagConnectionString = builder.Configuration.GetConnectionString("PriceTag");

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContextFactory<GoodsContext>(o => o.UseSqlite(goodsConnectionString));
builder.Services.AddDbContextFactory<PriceTagContext>(o => o.UseSqlite(priceTagConnectionString));

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
