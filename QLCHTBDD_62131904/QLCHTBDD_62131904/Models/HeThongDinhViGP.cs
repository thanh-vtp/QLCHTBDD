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
    
    public partial class HeThongDinhViGP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HeThongDinhViGP()
        {
            this.ThongTinHeThongDinhViDienThoais = new HashSet<ThongTinHeThongDinhViDienThoai>();
        }
    
        public int MaHeThongDinhViGPS { get; set; }
        public string TenHeThongDinhViGPS { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinHeThongDinhViDienThoai> ThongTinHeThongDinhViDienThoais { get; set; }
    }
}
