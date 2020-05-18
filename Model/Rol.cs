using System.Collections.Generic;

namespace kalum2020_v1.Model
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<UsuarioRol> UsuariosRoles;          
    }
}