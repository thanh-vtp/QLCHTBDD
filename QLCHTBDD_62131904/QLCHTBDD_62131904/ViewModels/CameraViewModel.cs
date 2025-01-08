using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class CameraViewModel
    {
        // Thông tin Camera sau
        public List<string> DoPhanGiaiCameraSau { get; set; }
        public List<string> DoPhanGiaiCameraTruoc { get; set; }
        public List<string> QuayPhimCameraSau { get; set; }
        public List<string> TinhNangCameraSau { get; set; }
        public List<string> TinhNangCameraTruoc { get; set; }
    }
}