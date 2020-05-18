using System.Collections.Generic;

namespace kalum2020_v1.Model
{
    public class CarreraTecnica
    {
        public int CarreraTecnicaId { get; set; }
        public string NombreCarrera { get; set; }
        public virtual List<Clase>  Clase { get; set; }        
    }
}