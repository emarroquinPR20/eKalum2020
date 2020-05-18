using System.Collections.Generic;

namespace kalum2020_v1.Model
{
    public class Salon
    {
        public int SalonId { get; set; }
        public string NombreSalon { get; set; }
        public string Descripcion { get; set; }
        public int Capacidad { get; set; }
        public virtual List<Clase>  Clase { get; set; }
    }
}