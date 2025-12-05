using Microsoft.EntityFrameworkCore;
using P_Utilizacion_de_Software.Controllers;
 // Importa tu contexto

<<<<<<< HEAD
public partial class Program
=======
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

ar builder = WebApplication.CreateBuilder(args);

// ... (Tu bloque de AddDbContext y AddAuthentication) ...

// AÑADIR REGISTRO DE SERVICIOS
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProyectoService>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
>>>>>>> 8aa98172eac0c1ab3fa9f62446475b4ba7fcb215
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // -----------------------------------------------------------
        // BLOQUE DE CONEXIÓN A LA BASE DE DATOS
        // -----------------------------------------------------------

        // 1. Obtiene la cadena de conexión del appsettings.json
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // 2. Registra ApplicationDbContext para usar SQL Server
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // -----------------------------------------------------------

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // ... código de manejo de errores
        }

        // ... resto del código ...

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}