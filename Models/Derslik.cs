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
    
    public partial class Derslik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Derslik()
        {
            this.DersProgrami = new HashSet<DersProgrami>();
            this.Sinav = new HashSet<Sinav>();
        }
    
        public int DerslikID { get; set; }
        public string DerslikAdi { get; set; }
        public string DerslikTuru { get; set; }
        public int Kapasite { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DersProgrami> DersProgrami { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sinav> Sinav { get; set; }
    }
}
