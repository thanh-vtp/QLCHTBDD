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
    
    public partial class ThietKe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThietKe()
        {
            this.ThongSoKTDienThoais = new HashSet<ThongSoKTDienThoai>();
        }
    
        public int MaThietKe { get; set; }
        public string TenThietKe { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongSoKTDienThoai> ThongSoKTDienThoais { get; set; }
    }
}
