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
    
    public partial class Bluetooth
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bluetooth()
        {
            this.ThongSoDienThoais = new HashSet<ThongSoDienThoai>();
        }
    
        public int MaBluetooth { get; set; }
        public string TenBluetooth { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongSoDienThoai> ThongSoDienThoais { get; set; }
    }
}
