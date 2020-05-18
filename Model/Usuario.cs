using System.Collections.Generic;

namespace kalum2020_v1.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Enable { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public List<UsuarioRol> UsuariosRoles;        
    }
}