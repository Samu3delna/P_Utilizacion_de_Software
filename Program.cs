using Microsoft.EntityFrameworkCore;
using P_Utilizacion_de_Software.Data;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------------
// BLOQUE DE CONEXIÓN A LA BASE DE DATOS
// -----------------------------------------------------------

// 1. Obtiene la cadena de conexión del appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Registra ApplicationDbContext para usar el proveedor SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// -----------------------------------------------------------

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
Add-Migration InitialCreate
Update-Database
