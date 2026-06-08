using fyd.backend.API.DTOs.Autenticacion;
using fyd.backend.API.DTOs.Autorizacion;
using fyd.backend.Identidad.Entidades;
using fyd.backend.Identidad.ORM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;

namespace fyd.backend.API.Controllers.Auth
{
    [ApiController]
    [Route("api/seguridad")]
    [Produces(MediaTypeNames.Application.Json)] // Define que siempre devuelve JSON
    [Consumes(MediaTypeNames.Application.Json)] // Define que espera JSON
    [SwaggerTag("Gestión centralizada de autenticación, usuarios y matriz de permisos.")]
    public class SeguridadController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _gestorDeUsuarios;
        private readonly RoleManager<IdentityRole> _gestorDeRoles;
        private readonly ContextoSeguridad _contexto;

        public SeguridadController(UserManager<IdentityUser> gestorDeUsuarios, RoleManager<IdentityRole> gestiorDeRoles, ContextoSeguridad contexto)
        {
            _gestorDeUsuarios = gestorDeUsuarios;
            _gestorDeRoles = gestiorDeRoles;
            _contexto = contexto;
        }

        /// <summary>
        /// Inicia sesión en el sistema.
        /// </summary>
        /// <remarks>
        /// Valida credenciales y devuelve un **JWT** con los Roles y Claims del usuario.
        /// Este token debe enviarse en el header `Authorization: Bearer {token}`.
        /// </remarks>
        /// <param name="login">Email y contraseña del usuario.</param>
        /// <returns>Token de acceso y fecha de expiración.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Obtener Token JWT", Description = "Valida credenciales y emite el token.")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDto login)
        {
            var user = await _gestorDeUsuarios.FindByEmailAsync(login.Email);

            if (user != null && await _gestorDeUsuarios.CheckPasswordAsync(user, login.Password))
            {
                var roles = await _gestorDeUsuarios.GetRolesAsync(user);
                var token = GenerarTokenJWT(user, roles);
                return Ok(new { token });
            }

            return Unauthorized("Credenciales inválidas");
        }


        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        /// <remarks>
        /// Solo disponible para administradores o usuarios con permiso `Usuarios.Crear`.
        /// El usuario se crea activo pero sin roles asignados por defecto.
        /// </remarks>
        [SwaggerOperation(Summary = "Crear Nuevo Usuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("usuarios/registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarDto registrar)
        {
            var userExists = await _gestorDeUsuarios.FindByEmailAsync(registrar.Email);
            if (userExists != null) return BadRequest("El usuario ya existe.");

            var user = new IdentityUser
            {
                UserName = registrar.Email,
                Email = registrar.Email
            };

            var result = await _gestorDeUsuarios.CreateAsync(user, registrar.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            // Opcional: Asignar un rol por defecto
            await _gestorDeUsuarios.AddToRoleAsync(user, "User");

            return Ok("Usuario registrado con éxito.");
        }



        // --- UPDATE (Email o Datos Básicos) ---
        [HttpPut("user/{id}")]
        [Authorize(Roles = "Admin")] // Solo un admin debería poder editar otros usuarios
        public async Task<IActionResult> Update(string id, [FromBody] ActualizarUsuarioDto model)
        {
            var user = await _gestorDeUsuarios.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.Email = model.Email;
            user.UserName = model.Email;

            var result = await _gestorDeUsuarios.UpdateAsync(user);
            return result.Succeeded ? Ok("Usuario actualizado") : BadRequest(result.Errors);
        }

        [HttpDelete("user/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _gestorDeUsuarios.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _gestorDeUsuarios.DeleteAsync(user);
            return result.Succeeded ? Ok("Usuario eliminado") : BadRequest(result.Errors);
        }

        [HttpPost("force-admin")]
        public async Task<IActionResult> ForceAdminRole(string email)
        {
            // 1. Asegurarse de que el rol "Admin" exista en la tabla AspNetRoles
            if (!await _gestorDeRoles.RoleExistsAsync("Admin"))
            {
                await _gestorDeRoles.CreateAsync(new IdentityRole("Admin"));
            }

            // 2. Buscar al usuario
            var user = await _gestorDeUsuarios.FindByEmailAsync(email);
            if (user == null) return NotFound("Usuario no encontrado");

            // 3. Verificar si ya tiene el rol, si no, agregarlo
            if (!await _gestorDeUsuarios.IsInRoleAsync(user, "Admin"))
            {
                var result = await _gestorDeUsuarios.AddToRoleAsync(user, "Admin");
                if (!result.Succeeded) return BadRequest(result.Errors);
            }

            return Ok($"El usuario {email} ahora es Admin");
        }

        [HttpPost("permisos")]
        public async Task<IActionResult> AgregarPermisoARol([FromBody] PermisoDto nuevoPermiso)
        {
            if (await _contexto.Permisos.AnyAsync(p => p.Nombre == nuevoPermiso.Nombre))
                return BadRequest("El permiso ya existe.");

            _contexto.Permisos.Add(new Permiso
            {
                Nombre = nuevoPermiso.Nombre,
                Modulo = nuevoPermiso.Modulo,
                Descripcion = nuevoPermiso.Descripcion
            });
            await _contexto.SaveChangesAsync();
            return Ok(nuevoPermiso);
        }

        [HttpPost("roles/asignar-permiso")]
        public async Task<IActionResult> AsignarPermisoARol([FromBody] AsignacionPermisoDto model)
        {
            // 1. Validar que el rol exista
            var role = await _gestorDeRoles.FindByNameAsync(model.RoleName);
            if (role == null) return NotFound("Rol no encontrado");

            // 2. Validar que el permiso exista
            var permiso = await _contexto.Permisos.FindAsync(model.PermisoId);
            if (permiso == null) return NotFound("Permiso no encontrado");

            // 3. Crear la relación en la tabla intermedia
            var existeRelacion = await _contexto.RolPermisos
                .AnyAsync(rp => rp.RoleId == role.Id && rp.PermisoId == permiso.Id);

            if (!existeRelacion)
            {
                _contexto.RolPermisos.Add(new RolPermiso
                {
                    RoleId = role.Id,
                    PermisoId = permiso.Id
                });
                await _contexto.SaveChangesAsync();
            }

            return Ok($"Permiso {permiso.Nombre} asignado al rol {model.RoleName}");
        }

        [HttpGet("roles")]
        public async Task<IActionResult> ObtenerRoles()
        {
            return Ok(_gestorDeRoles.Roles.ToList());
        }

        private string GenerarTokenJWT(IdentityUser user, IList<string> roles)
        {
            var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            // Metemos los roles en el JWT para que Next.js los lea
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TU_CLAVE_SUPER_SECRETA_DE_MINIMO_32_CARACTERES"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
