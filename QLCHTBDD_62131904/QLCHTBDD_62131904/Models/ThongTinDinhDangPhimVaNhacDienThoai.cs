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
    
    public partial class ThongTinDinhDangPhimVaNhacDienThoai
    {
        public int MaThongTinDinhDang { get; set; }
        public Nullable<int> MaTSKT { get; set; }
        public Nullable<int> MaDinhDangPhim { get; set; }
        public Nullable<int> MaDinhDangNhac { get; set; }
    
        public virtual DinhDangNhac DinhDangNhac { get; set; }
        public virtual DinhDangPhim DinhDangPhim { get; set; }
        public virtual ThongSoKTDienThoai ThongSoKTDienThoai { get; set; }
    }
}
