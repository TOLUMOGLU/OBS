//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OgrenciBilgiSistemi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sinav
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sinav()
        {
            this.Degerlendirme = new HashSet<Degerlendirme>();
        }
    
        public int SinavID { get; set; }
        public int DersAcmaID { get; set; }
        public string SinavTuru { get; set; }
        public int EtkiOrani { get; set; }
        public System.DateTime SinavTarihi { get; set; }
        public System.TimeSpan SinavSaati { get; set; }
        public int DerslikID { get; set; }
        public int OgrElmID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Degerlendirme> Degerlendirme { get; set; }
        public virtual DersAcma DersAcma { get; set; }
        public virtual Derslik Derslik { get; set; }
        public virtual OgretimElemani OgretimElemani { get; set; }
    }
}
