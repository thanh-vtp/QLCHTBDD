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
    
    public partial class ThongTinHeThongDinhViDienThoai
    {
        public int MaThongTinHeThongDinhVi { get; set; }
        public Nullable<int> MaTSBTDT { get; set; }
        public Nullable<int> MaHeThongDinhViGPS { get; set; }
    
        public virtual HeThongDinhViGP HeThongDinhViGP { get; set; }
        public virtual ThongSoBienTheDienThoai ThongSoBienTheDienThoai { get; set; }
    }
}
