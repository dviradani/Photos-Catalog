using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPhotosCatalog.DAL;
using MyPhotosCatalog.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// adding Serilog
builder.Host.UseSerilog((ctx, lc) =>
        lc.ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddTransient<IRepository, Repository>();
//for sqlite provider
builder.Services.AddDbContext<PhotosCatalogContext>(o => o.UseLazyLoadingProxies().UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));
// for sql server provider
//builder.Services.AddDbContext<PhotosCatalogContext>(o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDbContext<AuthenticationContext>(o => o.UseSqlite(builder.Configuration.GetConnectionString("users")));
builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AuthenticationContext>();
var app = builder.Build();
//delete & create Db`s in every single running
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AuthenticationContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<PhotosCatalogContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}
if (app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error/Index");
}
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute("default", "{controller=account}/{action=register}/{id?}");


app.Run();
