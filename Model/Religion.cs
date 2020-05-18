using System.Collections.Generic;

namespace kalum2020_v1.Model
{
    public class Religion
    {
        public int ReligionId { get; set; }
        public string Descripcion { get; set; }
        public virtual List<Alumno> Alumnos { get; set; }

        public override string ToString()
        {
            return $"{ReligionId} - {Descripcion}";
        }        
    }
}