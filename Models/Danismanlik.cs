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
    
    public partial class Danismanlik
    {
        public int DanismanlikID { get; set; }
        public int OgrElmID { get; set; }
        public int OgrenciID { get; set; }
    
        public virtual Ogrenci Ogrenci { get; set; }
        public virtual OgretimElemani OgretimElemani { get; set; }
    }
}
