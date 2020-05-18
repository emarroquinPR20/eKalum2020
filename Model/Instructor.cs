using System.Collections.Generic;

namespace kalum2020_v1.Model
{
    public class Instructor
    {
        public int InstructorId { get; set; }        
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Estatus { get; set; }
        public string Foto { get; set; }
        public virtual List<Clase>  Clase { get; set; }
    }
}