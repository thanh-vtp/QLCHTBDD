//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLCHTBDD_62131904.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThongSoQuayPhim
    {
        public int MaThongSoQuayPhim { get; set; }
        public Nullable<int> MaTSBTDT { get; set; }
        public Nullable<int> MaDoPhanGiaiQuayPhim { get; set; }
        public Nullable<int> MaTocDoKhungHinh { get; set; }
    
        public virtual DoPhanGiaiQuayPhim DoPhanGiaiQuayPhim { get; set; }
        public virtual ThongSoBienTheDienThoai ThongSoBienTheDienThoai { get; set; }
        public virtual TocDoKhungHinh TocDoKhungHinh { get; set; }
    }
}
