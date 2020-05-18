using System;
using System.Collections.Generic;

namespace kalum2020_v1.Model
{
    public class Horario
    {
        public int HorarioId { get; set; }
        public DateTime HorarioInicio { get; set; }
        public DateTime HorarioFinal { get; set; }
        public virtual List<Clase>  Clase { get; set; }
    }
}