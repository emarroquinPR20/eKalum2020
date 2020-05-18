using System;
using System.Collections.Generic;

namespace kalum2020_v1.Model
{
    public class AsignacionAlumno
    {
        public int AsignacionAlumnoId{ get; set; }
        public int ClaseId { get; set; }
        public int AlumnoId { get; set; }
        public DateTime FechaAsignacion{ get; set; }
        public string Observaciones { get; set; }
        public virtual Clase Clase { get; set; }
        public virtual Alumno Alumno { get; set; }        
    }
}