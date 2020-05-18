namespace kalum2020_v1.Model
{
    public class UsuarioRol
    {
        public int UsuarioId { get; set; }
        public int RoleId { get; set; }
        public Usuario Usuario;
        public Rol Rol;
    }
}