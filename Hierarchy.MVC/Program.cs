using Hierarchy.Application;
using Hierarchy.Domain.Repositories;
using Hierarchy.Infrastructure.Persistence.Entities;
using Hierarchy.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HierarchyDatabase");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HierarchyDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IHierarchyTableItemsRepository, HierarchyTableItemsRepository>();
builder.Services.AddScoped<HierarchyTableItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HierarchyTableItems}/{action=Parents}/{id?}")
    .WithStaticAssets();

app.UseStaticFiles();

app.Run();
