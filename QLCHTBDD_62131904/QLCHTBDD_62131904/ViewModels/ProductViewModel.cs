using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class ProductViewModel
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public string DoPhanGiaiManHinh { get; set; }
        public string KichThuocManHinh { get; set; } 
        public string ManHinhCongNghe { get; set; }
        public string DungLuongPin { get; set; } 
        public string HinhAnh { get; set; }
        public List<string> HinhAnhPhu { get; set; } 
        public List<VariantViewModel> Variants { get; set; }
        public string HeDieuHanh { get; set; }
        public string DanhBa { get; set; }
        public string DenFlash { get; set; } 
        public string KhacNuocBui { get; set; }
        public string CongNgheManHinh { get; set; }
        public string DoSangToiDa { get; set; }
        public string LoaiKinh { get; set; }
        public string LoaiPin { get; set; } 
        public string HoTroSac { get; set; }
        public string TinhNangBaoMat { get; set; }
        public string DinhDangGhiAm { get; set; }
        public string MangDiDong { get; set; }
        public string Sim { get; set; }
        public string Bluetooth { get; set; }
        public string CongKetNoiSac { get; set; }
        public string JackTaiNghe { get; set; }
        public string CongKetNoiKhac { get; set; }
        public string ThietKe { get; set; }
        public string ChatLieu { get; set; }
        public DateTime ThoiDiemRaMat { get; set; }
        public CameraViewModel Camera { get; set; } // Thông tin Camera
    }
}
