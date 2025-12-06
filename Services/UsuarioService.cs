using BCrypt.Net; // Necesario para la seguridad
using Microsoft.EntityFrameworkCore;
using P_Utilizacion_de_Software.Data;
using P_Utilizacion_de_Software.Models;
using P_Utilizacion_de_Software.Models.ViewModels;

namespace P_Utilizacion_de_Software.Services
{
    public class UsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- Lógica de REGISTRO ---
        public async Task<bool> RegistrarEstudianteAsync(RegistroViewModel model)
        {
            // 1. Validar si el correo ya existe
            bool existe = await _context.Usuarios.AnyAsync(u => u.Correo == model.Correo);
            if (existe)
            {
                return false; // Retorna falso si ya existe
            }

            // 2. Encriptar la contraseña (Hashing)
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Contrasena);

            // 3. Crear la entidad Usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = model.Nombre,
                Correo = model.Correo,
                ContrasenaHash = passwordHash,
                Rol = RolUsuario.Estudiante // Por defecto se registra como Estudiante
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
            return true;        
        }

        // --- Lógica de LOGIN ---
        public async Task<Usuario?> ValidarCredencialesAsync(LoginViewModel model)
        {
            // 1. Buscar usuario por correo
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == model.Correo);

            if (usuario == null) return null; // Usuario no existe

            // 2. Verificar contraseña (comparar texto plano con Hash)
            bool esValido = BCrypt.Net.BCrypt.Verify(model.Contrasena, usuario.ContrasenaHash);

            if (esValido)
            {
                return usuario; // Credenciales correctas
            }

            return null; // Contraseña incorrecta
        }
    }
}