using System;

namespace SocialEyesForest.Models
{
    public class EventoDto
    {
        public int Id { get; set; }
        public DateTime FechaEvento { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Ubicacion { get; set; }
        public int IdTipoEvento { get; set; }
        public int IdMotivo { get; set; }
        public string SubMotivo { get; set; }
        public string Observaciones { get; set; }

    }
}