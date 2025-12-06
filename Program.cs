using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using P_Utilizacion_de_Software.Data;
using P_Utilizacion_de_Software.Services;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. CONFIGURACIÓN DE BASE DE DATOS
// ==========================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ==========================================
// 2. INYECCIÓN DE SERVICIOS (LOGICA)
// ==========================================
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProyectoService>();
builder.Services.AddScoped<TareaService>();

// ==========================================
// 3. AUTENTICACIÓN (COOKIES)
// ==========================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";         // Si no estás logueado, te manda aquí
        options.AccessDeniedPath = "/Account/AccessDenied"; // Si no tienes permiso, te manda aquí
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

// ==========================================
// 4. SOPORTE PARA MVC
// ==========================================
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ==========================================
// 5. CONFIGURACIÓN DEL PIPELINE
// ==========================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ¡ORDEN IMPORTANTE!
app.UseAuthentication(); // 1. Identificar usuario
app.UseAuthorization();  // 2. Verificar permisos

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();