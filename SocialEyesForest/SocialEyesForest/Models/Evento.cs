//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocialEyesForest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Evento
    {
        public int Id { get; set; }
        public System.DateTime FechaEvento { get; set; }
        public System.Data.Entity.Spatial.DbGeography Localizacion { get; set; }
        public string Ubicacion { get; set; }
        public int IdTipoEvento { get; set; }
        public int IdMotivo { get; set; }
        public string SubMotivo { get; set; }
        public string Observaciones { get; set; }
    
        public virtual Motivo Motivo { get; set; }
        public virtual TipoEvento TipoEvento { get; set; }
    }
}
