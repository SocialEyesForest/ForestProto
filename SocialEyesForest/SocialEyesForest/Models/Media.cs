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
    
    public partial class Media
    {
        public int Id { get; set; }
        public int IdEvento { get; set; }
        public string NombreArchivo { get; set; }
        public byte TipoMedia { get; set; }
    
        public virtual Evento Evento { get; set; }
    }
}