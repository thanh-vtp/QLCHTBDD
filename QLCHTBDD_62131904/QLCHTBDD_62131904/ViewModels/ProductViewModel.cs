using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class ProductViewModel
    {
        public string TenSanPham { get; set; }
        public string DoPhanGiaiManHinh { get; set; }
        public string KichThuocManHinh { get; set; }
        public string HinhAnh { get; set; }
        public List<VariantViewModel> Variants { get; set; }
    }
}