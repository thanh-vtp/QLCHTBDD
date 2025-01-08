CREATE DATABASE QLCHTBDD_62131904
GO
USE QLCHTBDD_62131904
GO
-- Bảng QUOC_GIA
CREATE TABLE QuocGia (
    MaQG int IDENTITY(1,1) PRIMARY KEY,
		TenQuocGia nvarchar(100) NOT NULL, -- Tên quốc gia (Ví dụ: Việt Nam, Hoa Kỳ)
		IsActive bit NOT NULL DEFAULT 1
)
GO
-- Bảng HANG_SAN_XUAT
CREATE TABLE HangSanXuat (
    MaHang int IDENTITY(1,1) PRIMARY KEY,
		TenHang nvarchar(100) NOT NULL, -- Tên hãng (Ví dụ: Apple, Samsung)
		MaQG int FOREIGN KEY REFERENCES QuocGia(MaQG) 
        ON UPDATE CASCADE 
        ON DELETE SET NULL, -- Nếu quốc gia bị xóa, thông tin hãng vẫn được giữ lại
		IsActive bit NOT NULL DEFAULT 1
)
GO
-- Bảng LOAI_SAN_PHAM
CREATE TABLE LoaiSanPham (
    MaLSP int IDENTITY(1,1) PRIMARY KEY,
		TenLSP nvarchar(100) NOT NULL,	-- Ví dụ: Điện thoại, Laptop
		IsActive bit NOT NULL DEFAULT 1
)
GO
-- Bảng SAN_PHAM
CREATE TABLE SanPham (
    MaSP int IDENTITY(1,1) PRIMARY KEY,
		TenSP nvarchar(100) NOT NULL,
		MoTa nvarchar(max),
		MaLSP int FOREIGN KEY REFERENCES LoaiSanPham(MaLSP)
		ON UPDATE CASCADE
		ON DELETE CASCADE,
		MaHang int FOREIGN KEY REFERENCES HangSanXuat(MaHang) 
        ON UPDATE CASCADE 
        ON DELETE SET NULL -- Hãng sản xuất
)
GO
-- Bảng MAU_SAC
CREATE TABLE MauSac (
    MaMau int IDENTITY(1,1) PRIMARY KEY,
		TenMau nvarchar(50) NOT NULL,
		IsActive bit NOT NULL DEFAULT 1
)
GO
-- Bảng RAM
CREATE TABLE RAM (
    MaRAM int IDENTITY(1,1) PRIMARY KEY,
		DungLuong nvarchar(50) NOT NULL, -- Ví dụ: 4GB, 6GB, 8GB, 12GB, 16GB
		IsActive bit NOT NULL DEFAULT 1
)
GO
-- Bảng ROM
CREATE TABLE ROM (
    MaROM int IDENTITY(1,1) PRIMARY KEY,
		DungLuong nvarchar(50) NOT NULL,	-- Ví dụ: 64GB, 128GB, 256GB, 512GB, 1024GB
		IsActive bit NOT NULL DEFAULT 1
)
GO
-- Bảng Trạng thái sản phẩm
CREATE TABLE TrangThaiSanPham (
    MaTrangThai INT IDENTITY(1,1) PRIMARY KEY,
    TenTrangThai NVARCHAR(50) NOT NULL UNIQUE,
    IsActive BIT NOT NULL DEFAULT 1
)
GO
-- Bảng Biến Thể Sản Phẩm
CREATE TABLE BienTheSanPham (
    MaBT int IDENTITY(1,1) PRIMARY KEY,
		MaSP int FOREIGN KEY REFERENCES SanPham(MaSP)
		ON UPDATE CASCADE 
		ON DELETE CASCADE,
		MaMau int FOREIGN KEY REFERENCES MauSac(MaMau) 
		ON UPDATE CASCADE 
		ON DELETE CASCADE,
		MaRAM int FOREIGN KEY REFERENCES RAM(MaRAM) 
		ON UPDATE CASCADE 
		ON DELETE CASCADE,
		MaROM int FOREIGN KEY REFERENCES ROM(MaROM) 
		ON UPDATE CASCADE 
		ON DELETE CASCADE,
		SoLuong int NOT NULL,
		DonViTinh nvarchar(30),
		DonGia int NOT NULL,
		SKU VARCHAR(50) UNIQUE, -- Thêm SKU
		MaTrangThai int FOREIGN KEY REFERENCES TrangThaiSanPham(MaTrangThai)
		ON UPDATE CASCADE 
		ON DELETE CASCADE,
)
GO
-- Bảng HINH_ANH_SAN_PHAM
CREATE TABLE HinhAnhSanPham (
    MaHA int IDENTITY(1,1) PRIMARY KEY,
		MaBT int FOREIGN KEY REFERENCES BienTheSanPham(MaBT) 
		ON UPDATE CASCADE 
		ON DELETE CASCADE,
  		DuongDanAnh nvarchar(max),
		TenAnh nvarchar(max),
		AnhChinh bit DEFAULT 0
)
GO
-- Bảng phiên bản hệ điều hành
CREATE TABLE PhienBanHDH (
    MaPhienBanHDH INT IDENTITY(1,1) PRIMARY KEY,
		TenPhienBanHDH NVARCHAR(50) NOT NULL UNIQUE,   -- Ví dụ: "iOS 18", "Android 13"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng Chip Xử Lý
CREATE TABLE ChipXuLy (
    MaChip int IDENTITY(1,1) PRIMARY KEY,
		TenChip nvarchar(100) NOT NULL,
		IsActive bit NOT NULL DEFAULT 1
)
GO
-- Bảng tốc độ CPU 
CREATE TABLE TocDoCPU (
	MaTocDoCPU INT IDENTITY(1,1) PRIMARY KEY,
		TocDo NVARCHAR(50) NOT NULL UNIQUE,  -- Ví dụ: 2.8 GHz, 3.2 GHz, Hãng không công bố
		IsActive BIT DEFAULT 1
)
GO
-- Bảng chip đồ họa
CREATE TABLE ChipDoHoaGPU (
    MaChipDoHoaGPU INT IDENTITY(1,1) PRIMARY KEY,
		TenChipDoHoaGPU NVARCHAR(100) NOT NULL UNIQUE,    -- Ví dụ: "Apple GPU 5 nhân", "Adreno 740"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng chuẩn kháng nước, bụi
CREATE TABLE ChuanKhangBuiNuoc (
    MaChuanKhangBuiNuoc INT IDENTITY(1,1) PRIMARY KEY,
		TenChuanKhangBuiNuoc NVARCHAR(50) NOT NULL UNIQUE,  -- Ví dụ: "IP68", "IP67"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng định dạng ghi âm
CREATE TABLE DinhDangGhiAm (
    MaDinhDangGhiAm INT IDENTITY(1,1) PRIMARY KEY,
		TenDinhDangGhiAm NVARCHAR(100) NOT NULL UNIQUE, -- Ví dụ: "Ghi âm mặc định"
		IsActive BIT DEFAULT 1
)
-- Bảng loại SIM
CREATE TABLE SIM (
    MaSIM INT IDENTITY(1,1) PRIMARY KEY,
		TenSIM NVARCHAR(50) NOT NULL UNIQUE,        -- Ví dụ: "1 Nano SIM & 1 eSIM", "Dual Nano SIM"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng bluetooth
CREATE TABLE Bluetooth (
    MaBluetooth INT IDENTITY(1,1) PRIMARY KEY,
		TenBluetooth NVARCHAR(50) NOT NULL UNIQUE,       -- Ví dụ: "v5.3", "v5.2"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng cổng kết nối/sạc
CREATE TABLE CongKetNoiSac (
    MaCongKetNoiSac INT IDENTITY(1,1) PRIMARY KEY,
		TenCongKetNoiSac NVARCHAR(50) NOT NULL UNIQUE,   -- Ví dụ: "Type-C", "Lightning"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng jack tai nghe
CREATE TABLE JackTaiNghe (
    MaJackTaiNghe INT IDENTITY(1,1) PRIMARY KEY,
		TenJackTaiNghe NVARCHAR(50) NOT NULL UNIQUE,      -- Ví dụ: "Type-C", "3.5mm", "Không có"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng các kết nối khác
CREATE TABLE CongKetNoiKhac (
    MaCongKetNoiKhac INT IDENTITY(1,1) PRIMARY KEY,
		TenCongKetNoiKhac NVARCHAR(50) NOT NULL UNIQUE,     -- Ví dụ: "NFC", "IR Blaster"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng thiết kế
CREATE TABLE ThietKe (
    MaThietKe INT IDENTITY(1,1) PRIMARY KEY,
		TenThietKe NVARCHAR(100) NOT NULL UNIQUE,     -- Ví dụ: "Nguyên khối", "Gập"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng chất liệu
CREATE TABLE ChatLieu (
    MaChatLieu INT IDENTITY(1,1) PRIMARY KEY,
		TenChatLieu NVARCHAR(100) NOT NULL UNIQUE,  -- Ví dụ: "Khung nhôm & Mặt lưng kính cường lực", "Kim loại"
		IsActive BIT DEFAULT 1
)
GO
-- Fieds Công nghệ màn hình
-- Bảng công nghệ màn hình
CREATE TABLE CongNgheManHinh (
    MaCongNgheManHinh INT IDENTITY(1,1) PRIMARY KEY,
		TenCongNgheManHinh NVARCHAR(100) NOT NULL UNIQUE,
		IsActive BIT DEFAULT 1
)
GO
-- Fieds Độ phân giải màn hình
-- Bảng độ phân giải màn hình
CREATE TABLE DoPhanGiaiManHinh (
    MaDoPhanGiai INT IDENTITY(1,1) PRIMARY KEY,
		TenDoPhanGiai NVARCHAR(100) NOT NULL UNIQUE,  -- Ví dụ: "Super Retina XDR"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng chiều ngang độ phân giải
CREATE TABLE PixelNgang (
    MaPixelNgang INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính
    GTPixelNgang INT NOT NULL UNIQUE,              -- Giá trị pixel chiều ngang
	IsActive BIT DEFAULT 1
)
GO
-- Bảng chiều dọc độ phân giải
CREATE TABLE PixelDoc (
    MaPixelDoc INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính
    GTPixelDoc INT NOT NULL UNIQUE,             -- Giá trị pixel chiều dọc
	IsActive BIT DEFAULT 1
)
GO
-- Fieds Màn hình rộng
-- Bảng kích thước màn hình
CREATE TABLE KichThuocManHinh (
    MaKichThuocManHinh INT IDENTITY(1,1) PRIMARY KEY,
		KichThuoc NVARCHAR(50) NOT NULL UNIQUE,    -- Ví dụ: "6.7 inch"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng tần số quét
CREATE TABLE TanSoQuetManHinh (
    MaTanSoQuet INT IDENTITY(1,1) PRIMARY KEY,
		TanSo INT NOT NULL UNIQUE,          -- Ví dụ: 60 (Hz)
		IsActive BIT DEFAULT 1
)
GO
-- Fieds Độ sáng tối đa
-- Bảng độ sáng tối đa
CREATE TABLE DoSangToiDa (
    MaDoSang INT IDENTITY(1,1) PRIMARY KEY,
		DoSang INT NOT NULL UNIQUE,           -- Ví dụ: 2000 (nits)
		IsActive BIT DEFAULT 1
)
GO
-- Fieds Mặt kính cảm ứng
-- Bảng loại kính cường lực
CREATE TABLE LoaiKinhCuongLuc (
    MaLoaiKinh INT IDENTITY(1,1) PRIMARY KEY,
		TenLoaiKinh NVARCHAR(100) NOT NULL UNIQUE,  -- Ví dụ: "Kính cường lực Ceramic Shield"
		IsActive BIT DEFAULT 1
)
GO
-- PIN & SẠC
-- Fieds Dung lượng pin
-- Bảng dung lượng pin
CREATE TABLE DungLuongPin (
    MaDungLuongPin INT IDENTITY(1,1) PRIMARY KEY,
		DungLuong NVARCHAR(50) NOT NULL UNIQUE,  -- Ví dụ: "4000 mAh", "27 giờ"
		IsActive BIT DEFAULT 1
)
GO
-- Fieds Loại pin
-- Bảng loại pin
CREATE TABLE LoaiPin (
    MaLoaiPin INT IDENTITY(1,1) PRIMARY KEY,
		TenLoaiPin NVARCHAR(50) NOT NULL UNIQUE,  -- Ví dụ: "Li-Ion", "Li-Po"
		IsActive BIT DEFAULT 1
)
GO
-- Fieds Hỗ trợ sạc tối đa
-- Bảng hỗ trợ sạc tối đa
CREATE TABLE HoTroSacToiDa (
    MaHoTroSac INT IDENTITY(1,1) PRIMARY KEY,
		CongSuat NVARCHAR(50) NOT NULL UNIQUE,  -- Ví dụ: "20 W", "45 W"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng tính năng bảo mật
CREATE TABLE TinhNangBaoMat (
    MaTinhNangBaoMat INT IDENTITY(1,1) PRIMARY KEY,
		TenTinhNangBaoMat NVARCHAR(100) NOT NULL UNIQUE,  -- Ví dụ: "Mở khóa khuôn mặt Face ID", "Vân tay"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng công nghệ mạng di động
CREATE TABLE MangDiDong (
    MaMangDiDong INT IDENTITY(1,1) PRIMARY KEY,
		TenMangDiDong NVARCHAR(50) NOT NULL UNIQUE,     -- Ví dụ: "Hỗ trợ 5G", "4G LTE"
		IsActive BIT DEFAULT 1
)
GO
--- THIẾT KẾ & CHẤT LIỆU
-- Bảng Kích thước
CREATE TABLE ChieuDai (
    MaChieuDai INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính
    GTChieuDai FLOAT NOT NULL UNIQUE,           -- Giá trị chiều dài
    IsActive BIT DEFAULT 1                    -- Trạng thái kích hoạt
)
GO
CREATE TABLE ChieuNgang (
    MaChieuNgang INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính
    GTChieuNgang FLOAT NOT NULL UNIQUE,           -- Giá trị chiều ngang
    IsActive BIT DEFAULT 1                      -- Trạng thái kích hoạt
)
GO
CREATE TABLE DoDay (
    MaDoDay INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính
    GTDoDay FLOAT NOT NULL UNIQUE,           -- Giá trị độ dày
    IsActive BIT DEFAULT 1                 -- Trạng thái kích hoạt
)
GO
CREATE TABLE KhoiLuong (
    MaKhoiLuong INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính
    GTKhoiLuong FLOAT NOT NULL UNIQUE,           -- Giá trị khối lượng
    IsActive BIT DEFAULT 1                     -- Trạng thái kích hoạt
)
GO
-- Bảng Thông số kĩ thuật điện thoại
CREATE TABLE ThongSoDienThoai (
	MaTSDT int IDENTITY(1,1) PRIMARY KEY,
		MaSP int FOREIGN KEY REFERENCES SanPham(MaSP) ON UPDATE CASCADE ON DELETE CASCADE,
		MaPhienBanHDH int FOREIGN KEY REFERENCES PhienBanHDH(MaPhienBanHDH) ON UPDATE CASCADE ON DELETE CASCADE,						
		DanhBa bit DEFAULT 1, -- Danh Bạ (1: Có, 2: Không , mặc định có)					
		DenFlashCameraSau bit DEFAULT 1, -- Đèn flash (1: Có, 2: Không , mặc định có)												
		MaChuanKhangBuiNuoc int FOREIGN KEY REFERENCES ChuanKhangBuiNuoc(MaChuanKhangBuiNuoc) ON UPDATE CASCADE ON DELETE CASCADE,
		MaCongNgheManHinh INT FOREIGN KEY REFERENCES CongNgheManHinh(MaCongNgheManHinh) ON UPDATE CASCADE ON DELETE CASCADE,
		MaDoSang INT FOREIGN KEY REFERENCES DoSangToiDa(MaDoSang) ON UPDATE CASCADE ON DELETE CASCADE,
		MaLoaiKinh INT FOREIGN KEY REFERENCES LoaiKinhCuongLuc(MaLoaiKinh) ON UPDATE CASCADE ON DELETE CASCADE,
		MaLoaiPin INT FOREIGN KEY REFERENCES LoaiPin(MaLoaiPin) ON UPDATE CASCADE ON DELETE CASCADE,
		MaHoTroSac INT FOREIGN KEY REFERENCES HoTroSacToiDa(MaHoTroSac) ON UPDATE CASCADE ON DELETE CASCADE,
		MaTinhNangBaoMat INT FOREIGN KEY REFERENCES TinhNangBaoMat(MaTinhNangBaoMat) ON UPDATE CASCADE ON DELETE CASCADE,
		MaDinhDangGhiAm int FOREIGN KEY REFERENCES DinhDangGhiAm(MaDinhDangGhiAm) ON UPDATE CASCADE ON DELETE CASCADE, 
		MaMangDiDong int FOREIGN KEY REFERENCES MangDiDong(MaMangDiDong) ON UPDATE CASCADE ON DELETE CASCADE, 
		MaSIM INT FOREIGN KEY REFERENCES SIM(MaSIM) ON UPDATE CASCADE ON DELETE CASCADE,
		MaBluetooth INT FOREIGN KEY REFERENCES Bluetooth(MaBluetooth) ON UPDATE CASCADE ON DELETE CASCADE,		-- Phiên bản Bluetooth								
		MaCongKetNoiSac INT FOREIGN KEY REFERENCES CongKetNoiSac(MaCongKetNoiSac) ON UPDATE CASCADE ON DELETE CASCADE,
		MaJackTaiNghe INT FOREIGN KEY REFERENCES JackTaiNghe(MaJackTaiNghe) ON UPDATE CASCADE ON DELETE CASCADE,
		MaCongKetNoiKhac int FOREIGN KEY REFERENCES CongKetNoiKhac(MaCongKetNoiKhac) ON UPDATE CASCADE ON DELETE CASCADE,
		MaThietKe INT FOREIGN KEY REFERENCES ThietKe(MaThietKe) ON UPDATE CASCADE ON DELETE CASCADE,
		MaChatLieu INT FOREIGN KEY REFERENCES ChatLieu(MaChatLieu) ON UPDATE CASCADE ON DELETE CASCADE,
		ThoiDiemRaMat date -- Ngày ra mắt sản phẩm
)
GO
-- Bảng Thông số kĩ thuật biến thể điện thọai
CREATE TABLE ThongSoBienTheDienThoai (
	MaTSBTDT int IDENTITY(1,1) PRIMARY KEY,
		MaBT int FOREIGN KEY REFERENCES BienTheSanPham(MaBT) ON UPDATE CASCADE ON DELETE CASCADE,			
		MaChip int FOREIGN KEY REFERENCES ChipXuLy(MaChip) ON UPDATE CASCADE ON DELETE CASCADE,	
		MaTocDoCPU INT FOREIGN KEY REFERENCES TocDoCPU(MaTocDoCPU) ON UPDATE CASCADE ON DELETE CASCADE,
		MaChipDoHoaGPU INT FOREIGN KEY REFERENCES ChipDoHoaGPU(MaChipDoHoaGPU) ON UPDATE CASCADE ON DELETE CASCADE,
		DungLuongConLai nvarchar(50), -- Dung lượng khả dụng (ROM khả dụng)
		MaDoPhanGiai INT FOREIGN KEY REFERENCES DoPhanGiaiManHinh(MaDoPhanGiai) ON UPDATE CASCADE ON DELETE CASCADE,
		MaPixelNgang INT FOREIGN KEY REFERENCES PixelNgang(MaPixelNgang) ON UPDATE CASCADE ON DELETE CASCADE,
		MaPixelDoc INT FOREIGN KEY REFERENCES PixelDoc(MaPixelDoc) ON UPDATE CASCADE ON DELETE CASCADE,
		MaKichThuocManHinh INT FOREIGN KEY REFERENCES KichThuocManHinh(MaKichThuocManHinh) ON UPDATE CASCADE ON DELETE CASCADE,
		MaTanSoQuet INT FOREIGN KEY REFERENCES TanSoQuetManHinh(MaTanSoQuet) ON UPDATE CASCADE ON DELETE CASCADE, 
		MaDungLuongPin INT FOREIGN KEY REFERENCES DungLuongPin(MaDungLuongPin) ON UPDATE CASCADE ON DELETE CASCADE,
		MaChieuDai INT FOREIGN KEY REFERENCES ChieuDai(MaChieuDai) ON UPDATE CASCADE ON DELETE CASCADE,
		MaChieuNgang INT FOREIGN KEY REFERENCES ChieuNgang(MaChieuNgang) ON UPDATE CASCADE ON DELETE CASCADE,
		MaDoDay INT FOREIGN KEY REFERENCES DoDay(MaDoDay) ON UPDATE CASCADE ON DELETE CASCADE,
		MaKhoiLuong INT FOREIGN KEY REFERENCES KhoiLuong(MaKhoiLuong) ON UPDATE CASCADE ON DELETE CASCADE,
)
GO
-- CAMERA & MÀN HÌNH
-- Fieds Độ phân giải camera sau, Độ phân giải camera trước
-- Bảng Độ phân giải camera
CREATE TABLE DoPhanGiaiCamera (
    MaDoPhanGiai int IDENTITY(1,1) PRIMARY KEY, -- Mã duy nhất
		DoPhanGiai nvarchar(50) NOT NULL UNIQUE, -- Độ phân giải (ví dụ: "50 MP")
		IsActive bit NOT NULL DEFAULT 1
)
GO
-- Bảng loại camera
CREATE TABLE LoaiCamera (
    MaLoaiCamera int IDENTITY(1,1) PRIMARY KEY,
		TenLoaiCamera nvarchar(50) NOT NULL UNIQUE,
		IsActive bit DEFAULT 1
)
GO
-- Bảng thông số camera (liên kết với ThongSoKTDienThoai)
CREATE TABLE ThongSoCamera (
	MaThongSoCamera INT IDENTITY(1,1) PRIMARY KEY,
		MaTSBTDT INT FOREIGN KEY REFERENCES ThongSoBienTheDienThoai(MaTSBTDT) ON UPDATE CASCADE ON DELETE CASCADE,
		MaLoaiCamera INT FOREIGN KEY REFERENCES LoaiCamera(MaLoaiCamera) ON UPDATE CASCADE ON DELETE CASCADE,
		UNIQUE(MaTSBTDT, MaLoaiCamera)
)
GO
-- Bảng chi tiết độ phân giải camera
CREATE TABLE ChiTietDoPhanGiaiCamera (
    MaChiTietDoPhanGiaiCamera INT IDENTITY(1,1) PRIMARY KEY,
		MaThongSoCamera INT FOREIGN KEY REFERENCES ThongSoCamera(MaThongSoCamera) ON UPDATE CASCADE ON DELETE CASCADE,
		MaDoPhanGiai INT FOREIGN KEY REFERENCES DoPhanGiaiCamera(MaDoPhanGiai) ON UPDATE CASCADE ON DELETE CASCADE,
		UNIQUE(MaThongSoCamera, MaDoPhanGiai)
)
GO
-- Fieds Quay phim camera sau, Quay phim camera trước
-- Bảng độ phân giải cho quay phim
CREATE TABLE DoPhanGiaiQuayPhim (
    MaDoPhanGiaiQuayPhim int IDENTITY(1,1) PRIMARY KEY,
		TenDoPhanGiaiQuayPhim nvarchar(100) NOT NULL UNIQUE,
		IsActive bit DEFAULT 1
)
GO
-- Bảng tốc độ khung hình
CREATE TABLE TocDoKhungHinh (
    MaTocDoKhungHinh int IDENTITY(1,1) PRIMARY KEY,
		TocDo int NOT NULL UNIQUE,
		IsActive bit DEFAULT 1
)
GO
-- Bảng chi tiết thông số quay phim (liên kết với ThongSoKTDienThoai)
CREATE TABLE ThongSoQuayPhim (
    MaThongSoQuayPhim int IDENTITY(1,1) PRIMARY KEY,
		MaTSBTDT int FOREIGN KEY REFERENCES ThongSoBienTheDienThoai(MaTSBTDT) ON UPDATE CASCADE ON DELETE CASCADE,
		MaDoPhanGiaiQuayPhim int FOREIGN KEY REFERENCES DoPhanGiaiQuayPhim(MaDoPhanGiaiQuayPhim) ON UPDATE CASCADE ON DELETE CASCADE,
		MaTocDoKhungHinh int FOREIGN KEY REFERENCES TocDoKhungHinh(MaTocDoKhungHinh) ON UPDATE CASCADE ON DELETE CASCADE,
		UNIQUE(MaTSBTDT, MaDoPhanGiaiQuayPhim, MaTocDoKhungHinh) -- Đảm bảo không có sự trùng lặp
)
GO
-- Fields Tính năng camera sau, Tính năng camera trước
-- Bảng tính năng camera
CREATE TABLE TinhNang (
  	MaTinhNang int IDENTITY(1,1) PRIMARY KEY,
  		TenTinhNang nvarchar(100) NOT NULL UNIQUE,
		IsActive bit DEFAULT 1 
)
GO
-- Bảng chi tiết tính năng camera (liên kết với ThongSoKTDienThoai)
CREATE TABLE ChiTietTinhNangCamera (
    MaChiTietTinhNangCamera INT IDENTITY(1,1) PRIMARY KEY,
		MaTSBTDT INT FOREIGN KEY REFERENCES ThongSoBienTheDienThoai(MaTSBTDT) ON UPDATE CASCADE ON DELETE CASCADE,
		MaLoaiCamera INT FOREIGN KEY REFERENCES LoaiCamera(MaLoaiCamera) ON UPDATE CASCADE ON DELETE CASCADE,
		MaTinhNang INT FOREIGN KEY REFERENCES TinhNang(MaTinhNang) ON UPDATE CASCADE ON DELETE CASCADE,
		UNIQUE(MaTSBTDT, MaLoaiCamera, MaTinhNang) -- Đảm bảo không có sự trùng lặp
)
GO
-- Fieds công nghệ pin
-- Bảng công nghệ pin
CREATE TABLE CongNghePin (
    MaCongNghePin INT IDENTITY(1,1) PRIMARY KEY,
		TenCongNghePin NVARCHAR(100) NOT NULL UNIQUE,
		IsActive BIT DEFAULT 1
)
GO
-- Bảng liên kết công nghệ pin (chinh)
CREATE TABLE ThongTinCongNghePinDienThoai (
	MaThongTinCongNghePin INT IDENTITY(1,1) PRIMARY KEY,
		MaTSBTDT INT FOREIGN KEY REFERENCES ThongSoBienTheDienThoai(MaTSBTDT) ON UPDATE CASCADE ON DELETE CASCADE,
		MaCongNghePin INT FOREIGN KEY REFERENCES CongNghePin(MaCongNghePin) ON UPDATE CASCADE ON DELETE CASCADE,
		UNIQUE(MaTSBTDT, MaCongNghePin)
)
GO
-- TIỆN ÍCH
-- Bảng tính năng đặc biệt
CREATE TABLE TinhNangDacBiet (
    MaTinhNangDacBiet INT IDENTITY(1,1) PRIMARY KEY,
		TenTinhNangDacBiet NVARCHAR(100) NOT NULL UNIQUE,  -- Ví dụ: "Âm thanh Dolby Atmos", "Phát hiện va chạm"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng thông tin tính năng đặc biệt của điện thoại (chinh)
CREATE TABLE ThongTinTinhNangDacBietDienThoai (
    MaThongTinTinhNangDacBiet INT IDENTITY(1,1) PRIMARY KEY,
		MaTSBTDT INT FOREIGN KEY REFERENCES ThongSoBienTheDienThoai(MaTSBTDT) ON UPDATE CASCADE ON DELETE CASCADE,
		MaTinhNangDacBiet INT FOREIGN KEY REFERENCES TinhNangDacBiet(MaTinhNangDacBiet) ON UPDATE CASCADE ON DELETE CASCADE,
		UNIQUE(MaTSBTDT, MaTinhNangDacBiet)
)
GO
-- Bảng định dạng phim
CREATE TABLE DinhDangPhim (
    MaDinhDangPhim INT IDENTITY(1,1) PRIMARY KEY,
		TenDinhDangPhim NVARCHAR(50) NOT NULL UNIQUE,   -- Ví dụ: "MP4", "HEVC"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng định dạng nhạc
CREATE TABLE DinhDangNhac (
    MaDinhDangNhac INT IDENTITY(1,1) PRIMARY KEY,
		TenDinhDangNhac NVARCHAR(50) NOT NULL UNIQUE,    -- Ví dụ: "MP3", "FLAC", "Apple Lossless"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng thông tin định dạng phim và nhạc (chinh)
CREATE TABLE ThongTinDinhDangPhimVaNhacDienThoai (
	MaThongTinDinhDang INT IDENTITY(1,1) PRIMARY KEY,
		MaTSBTDT INT FOREIGN KEY REFERENCES ThongSoBienTheDienThoai(MaTSBTDT) ON UPDATE CASCADE ON DELETE CASCADE,
		MaDinhDangPhim INT FOREIGN KEY REFERENCES DinhDangPhim(MaDinhDangPhim) ON UPDATE CASCADE ON DELETE CASCADE,
		MaDinhDangNhac INT FOREIGN KEY REFERENCES DinhDangNhac(MaDinhDangNhac) ON UPDATE CASCADE ON DELETE CASCADE,
		UNIQUE(MaTSBTDT, MaDinhDangPhim, MaDinhDangNhac)
)
GO
-- KẾT NỐI
-- Bảng chuẩn wifi
CREATE TABLE Wifi (
    MaWifi INT IDENTITY(1,1) PRIMARY KEY,
		TenWifi NVARCHAR(50) NOT NULL UNIQUE,         -- Ví dụ: "Wi-Fi MIMO", "Wi-Fi 7"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng thông tin wifi của điện thoại (chinh)
CREATE TABLE ThongTinWifiDienThoai (
    MaThongTinWifi INT IDENTITY(1,1) PRIMARY KEY,
		MaTSBTDT INT FOREIGN KEY REFERENCES ThongSoBienTheDienThoai(MaTSBTDT) ON UPDATE CASCADE ON DELETE CASCADE,
		MaWifi INT FOREIGN KEY REFERENCES Wifi(MaWifi) ON UPDATE CASCADE ON DELETE CASCADE,
		UNIQUE(MaTSBTDT, MaWifi)
)
GO
-- Bảng hệ thống định vị GPS
CREATE TABLE HeThongDinhViGPS (
    MaHeThongDinhViGPS INT IDENTITY(1,1) PRIMARY KEY,
		TenHeThongDinhViGPS NVARCHAR(50) NOT NULL UNIQUE,    -- Ví dụ: "GPS", "GLONASS", "BEIDOU"
		IsActive BIT DEFAULT 1
)
GO
-- Bảng thông tin hệ thống định vị của điện thoại (chinh)
CREATE TABLE ThongTinHeThongDinhViDienThoai (
	MaThongTinHeThongDinhVi INT IDENTITY(1,1) PRIMARY KEY,
		MaTSBTDT INT FOREIGN KEY REFERENCES ThongSoBienTheDienThoai(MaTSBTDT) ON UPDATE CASCADE ON DELETE CASCADE,
		MaHeThongDinhViGPS INT FOREIGN KEY REFERENCES HeThongDinhViGPS(MaHeThongDinhViGPS) ON UPDATE CASCADE ON DELETE CASCADE,
		UNIQUE(MaTSBTDT, MaHeThongDinhViGPS)
)
-------------------------------------------------------------------------
-- Bảng quyền toàn hệ thống
CREATE TABLE Role (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    TenRole NVARCHAR(50) NOT NULL UNIQUE
)
GO
-- ADMIN
-- Bảng QUAN_TRI_HE_THONG
GO
CREATE TABLE QuanTriVien (
	MaQT INT PRIMARY KEY CHECK (MaQT = 1),
	Username NVARCHAR(50) NOT NULL UNIQUE,
	Password NVARCHAR(255) NOT NULL,
	RoleId INT NOT NULL DEFAULT 2,
	FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
)
GO
-- KHÁCH HÀNG
CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    HoTen NVARCHAR(100),
    SoDienThoai NVARCHAR(15),
	DiaChi NVARCHAR(MAX),
	NgaySinh DATETIME NULL,
    RoleId INT NOT NULL DEFAULT 2,
	FOREIGN KEY (RoleId) REFERENCES Role(RoleId),
	CreatedOn DATETIME DEFAULT GETDATE(),
	UpdatedOn DATETIME NULL, 
	ResetPasswordToken NVARCHAR(255) NULL,            -- Token đặt lại mật khẩu
    TokenExpiration DATETIME NULL,						-- Thời gian hết hạn của token
    IsActive BIT DEFAULT 1
)
GO
-- GIỎ HÀNG
CREATE TABLE GioHang (
    MaGioHang INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính tự tăng
    MaKH INT FOREIGN KEY REFERENCES KhachHang(MaKH) 
            ON DELETE CASCADE ON UPDATE CASCADE, -- Liên kết với KhachHang
    CreatedOn DATETIME DEFAULT GETDATE() -- Ngày tạo giỏ hàng
)
GO
-- CHI TIẾT GIỎ HÀNG
CREATE TABLE ChiTietGioHang (
    MaCTGH INT IDENTITY(1,1) PRIMARY KEY,
    MaGioHang INT FOREIGN KEY REFERENCES GioHang(MaGioHang) 
                 ON DELETE CASCADE ON UPDATE CASCADE,
    MaBT INT FOREIGN KEY REFERENCES BienTheSanPham(MaBT) 
             ON DELETE CASCADE ON UPDATE CASCADE,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    Gia DECIMAL(18, 2) NOT NULL
)
GO
-- TRẠNG THÁI ĐƠN HÀNG
CREATE TABLE TrangThaiDonHang (
    MaTrangThai INT PRIMARY KEY, -- Khóa chính
    TenTrangThai NVARCHAR(50) NOT NULL, -- Tên trạng thái
	IsActive BIT DEFAULT 1
)
GO
-- ĐƠN HÀNG
 CREATE TABLE DonHang (
      MaDonHang INT PRIMARY KEY IDENTITY(1,1),
      MaKH INT FOREIGN KEY REFERENCES KhachHang(MaKH),
      NgayDatHang DATETIME NOT NULL,
      TongTien DECIMAL(18, 2) NOT NULL,
      MaTrangThai INT FOREIGN KEY REFERENCES TrangThaiDonHang(MaTrangThai)
  )
GO
-- CHI TIẾT ĐƠN HÀNG
CREATE TABLE ChiTietDonHang (
    MaChiTiet INT PRIMARY KEY IDENTITY(1,1), -- Khóa chính tự tăng
    MaDonHang INT FOREIGN KEY REFERENCES DonHang(MaDonHang) 
                 ON DELETE CASCADE ON UPDATE CASCADE, -- Liên kết với DonHang
    MaBT INT FOREIGN KEY REFERENCES BienTheSanPham(MaBT) 
             ON DELETE CASCADE ON UPDATE CASCADE, -- Liên kết với biến thể sản phẩm
    SoLuong INT NOT NULL CHECK (SoLuong > 0), -- Số lượng
    DonGia DECIMAL(18, 2) NOT NULL -- Giá tại thời điểm mua
)
GO
-------------------------------------------------------------------------
-- Thêm data
-- Thêm dữ liệu tĩnh
-- Bảng Quyền
INSERT INTO Role (TenRole)
VALUES 
    (N'Admin'),
    (N'Khách hàng');
GO
-- Bảng quản trị viên
INSERT INTO QuanTriVien (MaQT,Username, Password, RoleId)
VALUES 
    (1,N'thanh62131904', '123',1);
GO
-- Thêm dữ liệu vào bảng TrangThaiSanPham
INSERT INTO TrangThaiSanPham (TenTrangThai, IsActive)
VALUES 
    (N'Còn hàng', 1),
    (N'Hết hàng', 1),
    (N'Ngừng kinh doanh', 1),
    (N'Đặt hàng trước', 1),
    (N'Đang nhập hàng', 1);
GO
-- Thêm dữ liệu vào bảng Trạng thái đơn hàng
INSERT INTO TrangThaiDonHang (MaTrangThai, TenTrangThai, IsActive)
VALUES 
    (1, N'Chờ xác nhận', 1),      -- Đơn hàng vừa được tạo, cần xác nhận
    (2, N'Đang xử lý', 1),        -- Đơn hàng đang được xử lý
    (3, N'Đang giao hàng', 1),    -- Đơn hàng đang được vận chuyển
    (4, N'Giao hàng thành công', 1), -- Đơn hàng đã được giao thành công
    (5, N'Đã hủy', 1),            -- Đơn hàng đã bị hủy
    (6, N'Hoàn trả', 1);          -- Đơn hàng đã được hoàn trả
-- Thêm dữ liệu vào bảng Quốc gia
INSERT INTO QuocGia (TenQuocGia, IsActive)
VALUES 
(N'Việt Nam', 1),
(N'Hoa Kỳ', 1),
(N'Hàn Quốc', 1),
(N'Nhật Bản', 1),
(N'Trung Quốc', 1),
(N'Đức', 1),
(N'Ấn Độ', 1),
(N'Pháp', 1),
(N'Phần Lan', 1);
GO
-- Thêm dữ liệu vào bảng Hãng sản xuất
INSERT INTO HangSanXuat (TenHang, MaQG, IsActive)
VALUES 
(N'Apple', (SELECT MaQG FROM QuocGia WHERE TenQuocGia = N'Hoa Kỳ'), 1),
(N'Samsung', (SELECT MaQG FROM QuocGia WHERE TenQuocGia = N'Hàn Quốc'), 1),
(N'Sony', (SELECT MaQG FROM QuocGia WHERE TenQuocGia = N'Nhật Bản'), 1),
(N'Xiaomi', (SELECT MaQG FROM QuocGia WHERE TenQuocGia = N'Trung Quốc'), 1),
(N'LG', (SELECT MaQG FROM QuocGia WHERE TenQuocGia = N'Hàn Quốc'), 1),
(N'Nokia', (SELECT MaQG FROM QuocGia WHERE TenQuocGia = N'Phần Lan'), 1),
(N'Huawei', (SELECT MaQG FROM QuocGia WHERE TenQuocGia = N'Trung Quốc'), 1),
(N'Asus', (SELECT MaQG FROM QuocGia WHERE TenQuocGia = N'Đài Loan'), 1);
GO
-- Thêm dữ liệu vào bảng Loại sản phẩm
INSERT INTO LoaiSanPham (TenLSP, IsActive)
VALUES 
(N'Điện thoại', 1),
(N'Laptop', 0),
(N'Máy tính bảng', 0),
(N'Đồng hồ thông minh', 0),
(N'Phụ kiện', 0),
(N'Máy ảnh', 0),
(N'TV', 0);
GO
-- Thêm dữ liệu vào bảng Màu sắc
INSERT INTO MauSac (TenMau, IsActive) 
VALUES 
(N'Đen', 1),
(N'Trắng', 1),
(N'Hồng', 1),
(N'Xanh Lá', 1),
(N'Xanh Dương', 1),
(N'Vàng', 1),
(N'Xám', 1),
(N'Tím', 1),
(N'Đỏ', 1),
(N'Nâu', 1),
(N'Bạc', 1),
(N'Xanh Ngọc', 1),
(N'Xanh Dương Đậm', 1),
(N'Cam', 1),
(N'Vàng Gold', 1);
GO
-- Thêm dữ liệu vào bảng RAM
INSERT INTO RAM (DungLuong, IsActive)
VALUES 
(N'4GB', 1),
(N'6GB', 1),
(N'8GB', 1),
(N'12GB', 1),
(N'16GB', 1),
(N'32GB', 1);
GO
-- Thêm dữ liệu vào bảng ROM
INSERT INTO ROM (DungLuong, IsActive)
VALUES 
(N'64GB', 1),
(N'128GB', 1),
(N'256GB', 1),
(N'512GB', 1),
(N'1024GB', 1); -- 1TB
GO
-- CẤU HÌNH & BỘ NHỚ
-- Thêm dữ liệu vào bảng PhienBanHDH
INSERT INTO PhienBanHDH (TenPhienBanHDH, IsActive) VALUES
(N'iOS 18', 1),
(N'iOS 17', 1),
(N'iOS 16', 1),
(N'Android 14', 1),
(N'Android 13', 1),
(N'Android 12', 1),
(N'Windows Phone 8.1', 0),
(N'Symbian OS', 0);
GO
-- Thêm dữ liệu vào bảng ChipXuLy
INSERT INTO ChipXuLy (TenChip, IsActive) VALUES
(N'Apple A16 Bionic', 1),
(N'Apple A18 6 nhân', 1),
(N'Apple A17 6 nhân', 1),
(N'Snapdragon 8 Gen 2', 1),
(N'Snapdragon 8 Gen 1', 1),
(N'Dimensity 9200+', 1),
(N'Dimensity 9000', 1),
(N'Exynos 2300', 1),
(N'Exynos 2200', 1),
(N'Kirin 9000', 0),
(N'Kirin 990', 0);
GO
-- Thêm dữ liệu vào bảng TocDoCPU
INSERT INTO TocDoCPU (TocDo, IsActive) VALUES
(N'Hãng không công bố', 1),
(N'2.8 GHz', 1),
(N'3.0 GHz', 1),
(N'3.2 GHz', 1),
(N'2.5 GHz', 1),
(N'2.9 GHz', 1),
(N'1.8 GHz', 0),
(N'2.0 GHz', 0),
(N'3.46 GHz', 1);
GO
-- Thêm dữ liệu vào bảng ChipDoHoaGPU
INSERT INTO ChipDoHoaGPU (TenChipDoHoaGPU, IsActive) VALUES
(N'Apple GPU 5 nhân', 1),
(N'Apple GPU 4 nhân', 1),
(N'Adreno 740', 1),
(N'Adreno 730', 1),
(N'Mali-G715 Immortalis', 1),
(N'Mali-G710', 1),
(N'Mali-G78', 0),
(N'PowerVR GX6250', 0);
GO
-- CAMERA & MÀN HÌNH
-- Thêm dữ liệu vào bảng DoPhanGiaiCamera
INSERT INTO DoPhanGiaiCamera (DoPhanGiai, IsActive) VALUES
(N'12 MP', 1),
(N'48 MP', 1),
(N'50 MP', 1),
(N'64 MP', 1),
(N'108 MP', 1),
(N'200 MP', 1),
(N'8 MP', 0),    -- Không hoạt động
(N'2 MP', 0);   -- Không hoạt động
GO
-- Thêm dữ liệu vào bảng LoaiCamera
INSERT INTO LoaiCamera (TenLoaiCamera, IsActive) VALUES
(N'Trước', 1),
(N'Sau', 1);
GO
-- Thêm dữ liệu vào bảng DoPhanGiaiQuayPhim
INSERT INTO DoPhanGiaiQuayPhim (TenDoPhanGiaiQuayPhim, IsActive) VALUES
(N'HD 720p', 1),
(N'FullHD 1080p', 1),
(N'4K 2160p', 1),
(N'8K', 1);
GO
-- Thêm dữ liệu vào bảng TocDoKhungHinh
INSERT INTO TocDoKhungHinh (TocDo, IsActive) VALUES
(24, 1),
(30, 1),
(60, 1),
(120, 1),
(240, 1);
GO
-- Thêm dữ liệu vào bảng TinhNang
INSERT INTO TinhNang (TenTinhNang, IsActive) VALUES
(N'Smart HDR 5', 1),
(N'Xóa phông', 1),
(N'Trôi nhanh thời gian (Time Lapse)', 1),
(N'Retina Flash', 0),  -- Không còn active
(N'Quay video Full HD', 1),
(N'Quay video 4K', 1),
(N'Quay chậm (Slow Motion)', 1),
(N'Nhãn dán (AR Stickers)', 1),
(N'Live Photos', 1),
(N'Deep Fusion', 1),
(N'Cinematic', 1),
(N'Chụp ảnh liên tục', 1),
(N'Chụp đêm', 1),
(N'Chống rung', 1),
(N'Bộ lọc màu', 1),
(N'TrueDepth', 0), -- Không còn active
(N'Photonic Engine', 1),
(N'Điều khiển camera (Camera Control)', 1),
(N'Zoom quang học', 1),
(N'Zoom kỹ thuật số', 1),
(N'Toàn cảnh (Panorama)', 1),
(N'Siêu độ phân giải', 1),
(N'Siêu cận (Macro)', 1),
(N'Góc siêu rộng (Ultrawide)', 1),
(N'Dolby Vision HDR', 1),
(N'Chống rung quang học (OIS)', 1),
(N'Chế độ hành động (Action Mode)', 1),
(N'Ban đêm (Night Mode)', 1),
(N'Chụp chân dung', 1),
(N'Tự động lấy nét', 1),
(N'Nhận diện khuôn mặt', 1),
(N'Làm đẹp', 1),
(N'Chống mắt đỏ', 0); -- Không còn active
GO
-- Thêm dữ liệu vào bảng CongNgheManHinh
INSERT INTO CongNgheManHinh (TenCongNgheManHinh, IsActive) VALUES
(N'OLED', 1),          -- OLED (active)
(N'LCD', 1),           -- LCD (active)
(N'AMOLED', 1),        -- AMOLED (active)
(N'IPS LCD', 1),       -- IPS LCD (active)
(N'Super AMOLED', 1),  -- Super AMOLED (active)
(N'Dynamic AMOLED', 1), -- Dynamic AMOLED (active)
(N'Retina IPS LCD', 1), -- Retina IPS LCD (active)
(N'LTPO AMOLED', 1),   -- LTPO AMOLED (active)
(N'LED', 0),           -- LED (không active)
(N'TFT LCD', 0),        -- TFT LCD (không active)
(N'POLED', 0);        -- POLED (không active)
GO
-- Thêm dữ liệu vào bảng DoPhanGiaiManHinh
INSERT INTO DoPhanGiaiManHinh (TenDoPhanGiai, IsActive) VALUES
(N'Super Retina XDR', 1),     -- Super Retina XDR (active)
(N'Full HD+', 1),             -- Full HD+ (active)
(N'Quad HD+', 1),             -- Quad HD+ (active)
(N'HD+', 1),                  -- HD+ (active)
(N'Retina', 1),               -- Retina (active)
(N'Dynamic AMOLED', 1),       -- Dynamic AMOLED (active)
(N'Super AMOLED', 1),        -- Super AMOLED (active)
(N'LCD', 1),                 -- LCD (active)
(N'Retina IPS LCD', 1),   -- Retina IPS LCD (active)
(N'WQHD+', 0),              -- WQHD+ (không active)
(N'Full HD', 0),              -- Full HD (không active)
(N'UHD', 0);              -- UHD (không active)
GO
-- Thêm dữ liệu vào bảng PixelNgang
INSERT INTO PixelNgang (GTPixelNgang, IsActive)
VALUES
    (1206, 1), -- Super Retina XDR
    (1080, 1), -- Full HD+
    (1440, 1), -- Quad HD+
    (720, 1),  -- HD
    (480, 0),
	(1179, 1);
GO
-- Thêm dữ liệu vào bảng PixelDoc
INSERT INTO PixelDoc (GTPixelDoc, IsActive)
VALUES
    (2622, 1), -- Super Retina XDR
    (2400, 1), -- Full HD+
    (3200, 1), -- Quad HD+
    (1280, 1), -- HD
    (854, 0),
	(2556, 1);
GO
-- Thêm dữ liệu vào bảng KichThuocManHinh
INSERT INTO KichThuocManHinh (KichThuoc, IsActive) VALUES
(N'6.0 inch', 1),      -- Kích thước 6.0 inch (active)
(N'6.1 inch', 1),      -- Kích thước 6.1 inch (active)
(N'6.2 inch', 1),      -- Kích thước 6.2 inch (active)
(N'6.4 inch', 1),      -- Kích thước 6.4 inch (active)
(N'6.5 inch', 1),      -- Kích thước 6.5 inch (active)
(N'6.6 inch', 1),      -- Kích thước 6.6 inch (active)
(N'6.7 inch', 1),      -- Kích thước 6.7 inch (active)
(N'6.8 inch', 1),       -- Kích thước 6.8 inch (active)
(N'5.5 inch', 0),        -- Kích thước 5.5 inch (không active)
(N'7.0 inch', 0);       -- Kích thước 7.0 inch (không active)
GO
-- Thêm dữ liệu vào bảng TanSoQuetManHinh
INSERT INTO TanSoQuetManHinh (TanSo, IsActive) VALUES
(60, 1),        -- Tần số quét 60 Hz (active)
(90, 1),        -- Tần số quét 90 Hz (active)
(120, 1),       -- Tần số quét 120 Hz (active)
(144, 1),      -- Tần số quét 144 Hz (active)
(240, 0),       -- Tần số quét 240 Hz (không active)
(75,0);    -- Tần số quét 75 Hz (không active)
GO
-- Thêm dữ liệu vào bảng DoSangToiDa
INSERT INTO DoSangToiDa (DoSang, IsActive) VALUES
(500, 1),      -- 500 nits (active)
(800, 1),      -- 800 nits (active)
(1000, 1),    -- 1000 nits (active)
(1200, 1),    -- 1200 nits (active)
(1500, 1),    -- 1500 nits (active)
(1600, 1),    -- 1600 nits (active)
(2000, 1),   -- 2000 nits (active)
(2500, 1),   -- 2500 nits (active)
(3000, 0);     -- 3000 nits (không active)
GO
-- Thêm dữ liệu vào bảng LoaiKinhCuongLuc
INSERT INTO LoaiKinhCuongLuc (TenLoaiKinh, IsActive) VALUES
(N'Gorilla Glass', 1),            -- Gorilla Glass (active)
(N'Gorilla Glass 3', 1),           -- Gorilla Glass 3 (active)
(N'Gorilla Glass 5', 1),           -- Gorilla Glass 5 (active)
(N'Gorilla Glass Victus', 1),      -- Gorilla Glass Victus (active)
(N'Ceramic Shield', 1),           -- Ceramic Shield (active)
(N'Dragontrail Glass', 1),         -- Dragontrail Glass (active)
(N'Sapphire Glass', 1),          -- Sapphire Glass (active)
(N'Panda Glass', 0),       -- Panda Glass (không active)
(N'Tempered Glass', 0);  -- Tempered Glass (không active)
GO
-- Thêm dữ liệu vào bảng LoaiPin
INSERT INTO LoaiPin (TenLoaiPin, IsActive) VALUES
(N'Li-Ion', 1),        -- Li-Ion (active)
(N'Li-Po', 1),         -- Li-Po (active)
(N'Li-Polymer', 1),      -- Li-Polymer (active)
(N'NiMH', 0),        -- NiMH (không active)
(N'NiCd', 0);        -- NiCd (không active)
GO
-- Thêm dữ liệu vào bảng DungLuongPin
INSERT INTO DungLuongPin (DungLuong, IsActive) VALUES
(N'3000 mAh', 1),      -- 3000 mAh (active)
(N'4000 mAh', 1),      -- 4000 mAh (active)
(N'4500 mAh', 1),      -- 4500 mAh (active)
(N'5000 mAh', 1),      -- 5000 mAh (active)
(N'5500 mAh', 1),      -- 5500 mAh (active)
(N'27 giờ', 1),        -- 27 giờ (active)
(N'20 giờ', 0);       -- 20 giờ (không active)
GO
-- Thêm dữ liệu vào bảng HoTroSacToiDa
INSERT INTO HoTroSacToiDa (CongSuat, IsActive) VALUES
(N'10 W', 1),       -- 10 W (active)
(N'15 W', 1),       -- 15 W (active)
(N'18 W', 1),       -- 18 W (active)
(N'20 W', 1),       -- 20 W (active)
(N'25 W', 1),       -- 25 W (active)
(N'45 W', 1),       -- 45 W (active)
(N'65 W', 1),       -- 65 W (active)
(N'120 W', 0);      -- 120 W (không active)
GO
-- Thêm dữ liệu vào bảng CongNghePin
INSERT INTO CongNghePin (TenCongNghePin, IsActive) VALUES
(N'Tiết kiệm pin', 1),            -- Tiết kiệm pin (active)
(N'Sạc pin nhanh', 1),            -- Sạc pin nhanh (active)
(N'Sạc ngược qua cáp', 1),        -- Sạc ngược qua cáp (active)
(N'Sạc không dây MagSafe', 1),    -- Sạc không dây MagSafe (active)
(N'Sạc không dây', 1),            -- Sạc không dây (active)
(N'Sạc ngược không dây', 1),       -- Sạc ngược không dây (active)
(N'Sạc siêu nhanh', 0),    -- Sạc siêu nhanh (không active)
(N'Sạc chậm', 0);            -- Sạc chậm (không active)
GO
-- TIỆN ÍCH
-- Thêm dữ liệu vào bảng TinhNangBaoMat
INSERT INTO TinhNangBaoMat (TenTinhNangBaoMat, IsActive) VALUES
(N'Mở khóa khuôn mặt Face ID', 1),
(N'Vân tay', 1),
(N'Mật khẩu', 1),
(N'Mã PIN', 1),
(N'Mở khóa bằng giọng nói', 0), -- Không còn active
(N'Mở khóa bằng mống mắt', 0); -- Không còn active
GO
-- Thêm dữ liệu vào bảng TinhNangDacBiet
INSERT INTO TinhNangDacBiet (TenTinhNangDacBiet, IsActive) VALUES
(N'Âm thanh Dolby Atmos', 1),
(N'Phát hiện va chạm (Crash Detection)', 1),
(N'Loa kép', 1),
(N'HDR10+', 1),
(N'HDR10', 1),
(N'DCI-P3', 1),
(N'Công nghệ âm thanh Dolby Digital Plus', 1),
(N'Công nghệ True Tone', 1),
(N'Công nghệ hình ảnh Dolby Vision', 1),
(N'Công nghệ HLG', 1),
(N'Công nghệ âm thanh Dolby Digital', 1),
(N'Chạm 2 lần sáng màn hình', 1),
(N'Apple Pay', 1),
(N'Nhận diện khuôn mặt', 0),    -- Không còn active
(N'Cảm biến vân tay dưới màn hình', 0);    -- Không còn active
GO
-- Thêm dữ liệu vào bảng ChuanKhangBuiNuoc
INSERT INTO ChuanKhangBuiNuoc (TenChuanKhangBuiNuoc, IsActive) VALUES
(N'IP68', 1),
(N'IP67', 1),
(N'IP53', 1),
(N'IP66', 0),  -- Không còn active
(N'Không kháng nước', 0);   -- Không còn active
GO
-- Thêm dữ liệu vào bảng DinhDangGhiAm
INSERT INTO DinhDangGhiAm (TenDinhDangGhiAm, IsActive) VALUES
(N'Ghi âm mặc định', 1),
(N'MP3', 1),
(N'WAV', 1),
(N'AAC', 1),
(N'AMR', 0);
GO
-- Thêm dữ liệu vào bảng DinhDangPhim
INSERT INTO DinhDangPhim (TenDinhDangPhim, IsActive) VALUES
(N'MP4', 1),
(N'HEVC', 1),
(N'AVI', 1),
(N'MKV', 1),
(N'WMV', 0); -- Không còn active
GO
-- Thêm dữ liệu vào bảng DinhDangNhac
INSERT INTO DinhDangNhac (TenDinhDangNhac, IsActive) VALUES
(N'MP3', 1),
(N'FLAC', 1),
(N'Apple Lossless', 1),
(N'APAC', 1),
(N'AAC', 1),
(N'WAV', 0),  -- Không còn active
(N'OGG', 0);  -- Không còn active
GO
-- KẾT NỐI
-- Thêm dữ liệu vào bảng MangDiDong
INSERT INTO MangDiDong (TenMangDiDong, IsActive) VALUES
(N'Hỗ trợ 5G', 1),
(N'4G LTE', 1),
(N'3G', 1),
(N'2G', 0),
(N'HSDPA', 0);
GO
-- Thêm dữ liệu vào bảng SIM
INSERT INTO SIM (TenSIM, IsActive) VALUES
(N'1 Nano SIM & 1 eSIM', 1),
(N'Dual Nano SIM', 1),
(N'Nano SIM', 1),
(N'eSIM', 1),
(N'Micro SIM', 0);
GO
-- Thêm dữ liệu vào bảng Wifi
INSERT INTO Wifi (TenWifi, IsActive) VALUES
(N'Wi-Fi MIMO', 1),
(N'Wi-Fi 7', 1),
(N'Wi-Fi 6E', 1),
(N'Wi-Fi 6', 1),
(N'Wi-Fi 5', 1),
(N'Wi-Fi 4', 0);
GO
-- Thêm dữ liệu vào bảng HeThongDinhViGPS
INSERT INTO HeThongDinhViGPS (TenHeThongDinhViGPS, IsActive) VALUES
(N'QZSS', 1),
(N'iBeacon', 1),
(N'GPS', 1),
(N'GLONASS', 1),
(N'GALILEO', 1),
(N'BEIDOU', 1),
(N'A-GPS', 0);
GO
-- Thêm dữ liệu vào bảng Bluetooth
INSERT INTO Bluetooth (TenBluetooth, IsActive) VALUES
(N'v5.3', 1),
(N'v5.2', 1),
(N'v5.1', 1),
(N'v5.0', 1),
(N'v4.2', 0),
(N'v4.0', 0);
GO
-- Thêm dữ liệu vào bảng CongKetNoiSac
INSERT INTO CongKetNoiSac (TenCongKetNoiSac, IsActive) VALUES
(N'Type-C', 1),
(N'Lightning', 1),
(N'Micro USB', 0);
GO
-- Thêm dữ liệu vào bảng JackTaiNghe
INSERT INTO JackTaiNghe (TenJackTaiNghe, IsActive) VALUES
(N'Type-C', 1),
(N'3.5mm', 1),
(N'Không có', 1),
(N'Micro USB', 0),
(N'Mini HDMI', 0);
GO
-- Thêm dữ liệu vào bảng CongKetNoiKhac
INSERT INTO CongKetNoiKhac (TenCongKetNoiKhac, IsActive) VALUES
(N'NFC', 1),
(N'IR Blaster', 1),
(N'NFC + IR Blaster', 1),
(N'NFC + Bluetooth', 0),
(N'Không có', 0);
GO
-- THIẾT KẾ & CHẤT LƯỢNG
-- Thêm dữ liệu vào bảng ThietKe
INSERT INTO ThietKe (TenThietKe, IsActive) VALUES
(N'Nguyên khối', 1),
(N'Gập', 1),
(N'Trượt', 0),       -- Không còn active
(N'Vỏ sò', 0),       -- Không còn active
(N'Bàn phím QWERTY', 0);      -- Không còn active
GO
-- Thêm dữ liệu vào bảng ChatLieu
INSERT INTO ChatLieu (TenChatLieu, IsActive) VALUES
(N'Khung nhôm & Mặt lưng kính cường lực', 1),
(N'Kim loại', 1),
(N'Nhựa', 1),
(N'Gốm', 1),
(N'Titan', 1),
(N'Kính', 0),   -- Không còn active
(N'Nhôm', 0);     -- Không còn active
GO
-- Thêm dữ liệu vào bảng KichThuoc
INSERT INTO ChieuDai (GTChieuDai,IsActive) VALUES
    (160.8, 1),
    (158.2, 1),
    (165.2, 1),
    (159.9, 1),
    (162.7, 1),
    (164.0, 1),
    (157.8, 1);
GO
INSERT INTO ChieuNgang (GTChieuNgang, IsActive) 
VALUES
    (78.1, 1),
    (72.6, 1),
    (75.7, 1),
    (74.2, 1),
    (75.1, 1),
    (76.8, 1),
    (72.2, 1);
GO
INSERT INTO DoDay (GTDoDay, IsActive) 
VALUES
    (7.8, 1),
    (8.0, 1),
    (8.9, 1),
    (8.8, 1),
    (8.6, 1),
    (8.2, 1),
    (7.9, 1);
GO
INSERT INTO KhoiLuong (GTKhoiLuong, IsActive) 
VALUES
    (180.0, 1),
    (200.0, 1),
    (210.0, 1),
    (195.0, 1),
    (220.0, 1),
    (230.0, 1),
    (190.0, 1);
GO

----------------------------------------------------------------------------------------
-- Thêm sản phẩm
INSERT INTO SanPham (TenSP, MoTa, MaLSP, MaHang)
VALUES
(N'iPhone 13 Pro Max', N'Mẫu cao cấp với chip A15 Bionic', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Apple')),
(N'iPhone 14 Pro', N'Mẫu cao cấp với chip A16 Bionic', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Apple')),
(N'iPhone 14 Pro Max', N'Màn hình lớn với camera cao cấp', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Apple')),
(N'Samsung Galaxy S23 Ultra', N'Dòng flagship với camera 200MP', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Samsung')),
(N'Samsung Galaxy Z Fold 5', N'Mẫu điện thoại gập cao cấp', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Samsung')),
(N'Xiaomi 13 Ultra', N'Mẫu flagship với camera Leica', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Xiaomi')),
(N'Sony Xperia 1 V', N'Màn hình 4K OLED với camera chuyên nghiệp', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Sony')),
(N'Nokia X30 5G', N'Mẫu tầm trung với 5G', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Nokia')),
(N'Huawei P60 Pro', N'Camera tiên tiến và thiết kế cao cấp', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Huawei')),
(N'Asus ROG Phone 6', N'Dòng gaming phone hiệu năng cao', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Asus')),
(N'LG Wing', N'Mẫu thiết kế xoay độc đáo', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'LG')),
(N'iPhone 14 Plus', N'Màn hình lớn, hiệu năng cao', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Apple')),
(N'Samsung Galaxy A54 5G', N'Tầm trung với 5G', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Samsung')),
(N'Xiaomi Redmi Note 12', N'Tầm trung với camera cao cấp', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Xiaomi')),
(N'Sony Xperia 10 V', N'Mẫu tầm trung với thiết kế nhỏ gọn', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Sony')),
(N'Nokia G60 5G', N'Mẫu giá rẻ với 5G', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Nokia')),
(N'Asus Zenfone 10', N'Mẫu flagship nhỏ gọn', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Asus')),
(N'LG Velvet', N'Thiết kế thời trang, hiệu năng cao', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'LG')),
(N'Xiaomi Black Shark 5', N'Mẫu gaming phone với hiệu năng cao', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Xiaomi')),
(N'Samsung Galaxy M14', N'Pin lớn, giá rẻ', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Samsung')),
(N'Huawei Mate X3', N'Mẫu điện thoại gập với camera cao cấp', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Huawei')),
(N'Sony Xperia Pro-I', N'Mẫu chuyên nghiệp dành cho nhiếp ảnh', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Sony')),
(N'iPhone SE 2023', N'Mẫu nhỏ gọn với chip mạnh', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Apple')),
(N'Samsung Galaxy S21 FE', N'Mẫu giá tốt với hiệu năng cao', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Samsung')),
(N'Xiaomi Mi 12', N'Mẫu flagship giá rẻ', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Xiaomi')),
(N'Nokia XR21', N'Mẫu bền bỉ dành cho môi trường khắc nghiệt', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Nokia')),
(N'Asus ROG Phone 7', N'Gaming phone với cấu hình cao', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Asus')),
(N'Huawei Nova 11', N'Mẫu tầm trung với thiết kế đẹp', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Huawei')),
(N'LG G8 ThinQ', N'Mẫu cao cấp với âm thanh chất lượng', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'LG')),
(N'Xiaomi Poco F5', N'Mẫu tầm trung với hiệu năng gaming vượt trội', 1, (SELECT MaHang FROM HangSanXuat WHERE TenHang = N'Xiaomi'));
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Thêm biến thể cho các sản phẩm
INSERT INTO BienTheSanPham (MaSP, MaMau, MaRAM, MaROM, SoLuong, DonViTinh, DonGia, SKU, MaTrangThai)
VALUES
-- iPhone 13 Pro Max
((SELECT MaSP FROM SanPham WHERE TenSP = N'iPhone 13 Pro Max'), 1, 3, 4, 100, N'Chiếc', 30000000, N'IP13-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'iPhone 13 Pro Max'), 2, 4, 5, 50, N'Chiếc', 35000000, N'IP13-WHITE-12-512', 1),

-- iPhone 14 Pro
((SELECT MaSP FROM SanPham WHERE TenSP = N'iPhone 14 Pro'), 1, 3, 4, 120, N'Chiếc', 32000000, N'IP14-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'iPhone 14 Pro'), 2, 4, 5, 80, N'Chiếc', 37000000, N'IP14-WHITE-12-512', 1),

-- iPhone 14 Pro Max
((SELECT MaSP FROM SanPham WHERE TenSP = N'iPhone 14 Pro Max'), 1, 3, 5, 90, N'Chiếc', 40000000, N'IP14MAX-BLACK-8-512', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'iPhone 14 Pro Max'), 3, 4, 5, 70, N'Chiếc', 42000000, N'IP14MAX-HONG-12-512', 1),

-- Samsung Galaxy S23 Ultra
((SELECT MaSP FROM SanPham WHERE TenSP = N'Samsung Galaxy S23 Ultra'), 1, 3, 4, 110, N'Chiếc', 30000000, N'SG23U-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Samsung Galaxy S23 Ultra'), 4, 4, 5, 90, N'Chiếc', 34000000, N'SG23U-XANH-12-512', 1),

-- Samsung Galaxy Z Fold 5
((SELECT MaSP FROM SanPham WHERE TenSP = N'Samsung Galaxy Z Fold 5'), 1, 4, 4, 60, N'Chiếc', 50000000, N'SZF5-BLACK-12-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Samsung Galaxy Z Fold 5'), 2, 4, 5, 40, N'Chiếc', 55000000, N'SZF5-WHITE-12-512', 1),

-- Xiaomi 13 Ultra
((SELECT MaSP FROM SanPham WHERE TenSP = N'Xiaomi 13 Ultra'), 5, 2, 3, 150, N'Chiếc', 25000000, N'X13U-VANG-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Xiaomi 13 Ultra'), 6, 3, 4, 130, N'Chiếc', 27000000, N'X13U-XAM-8-256', 1),

-- Sony Xperia 1 V
((SELECT MaSP FROM SanPham WHERE TenSP = N'Sony Xperia 1 V'), 1, 3, 4, 70, N'Chiếc', 28000000, N'X1V-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Sony Xperia 1 V'), 2, 4, 5, 50, N'Chiếc', 32000000, N'X1V-WHITE-12-512', 1),

-- Nokia X30 5G
((SELECT MaSP FROM SanPham WHERE TenSP = N'Nokia X30 5G'), 1, 2, 3, 120, N'Chiếc', 15000000, N'NX30-BLACK-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Nokia X30 5G'), 2, 3, 4, 80, N'Chiếc', 17000000, N'NX30-WHITE-8-256', 1),

-- Huawei P60 Pro
((SELECT MaSP FROM SanPham WHERE TenSP = N'Huawei P60 Pro'), 1, 3, 4, 60, N'Chiếc', 30000000, N'P60-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Huawei P60 Pro'), 3, 4, 5, 40, N'Chiếc', 35000000, N'P60-HONG-12-512', 1),

-- Asus ROG Phone 6
((SELECT MaSP FROM SanPham WHERE TenSP = N'Asus ROG Phone 6'), 1, 3, 4, 90, N'Chiếc', 22000000, N'ROG6-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Asus ROG Phone 6'), 6, 4, 5, 70, N'Chiếc', 25000000, N'ROG6-XAM-12-512', 1),

-- LG Wing
((SELECT MaSP FROM SanPham WHERE TenSP = N'LG Wing'), 1, 3, 4, 50, N'Chiếc', 20000000, N'LGW-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'LG Wing'), 5, 4, 5, 40, N'Chiếc', 23000000, N'LGW-VANG-12-512', 1),

-- iPhone 14 Plus
((SELECT MaSP FROM SanPham WHERE TenSP = N'iPhone 14 Plus'), 1, 3, 4, 80, N'Chiếc', 27000000, N'IP14P-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'iPhone 14 Plus'), 2, 4, 5, 60, N'Chiếc', 30000000, N'IP14P-WHITE-12-512', 1),

-- Samsung Galaxy A54 5G
((SELECT MaSP FROM SanPham WHERE TenSP = N'Samsung Galaxy A54 5G'), 1, 2, 3, 150, N'Chiếc', 11000000, N'SGA54-BLACK-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Samsung Galaxy A54 5G'), 2, 3, 4, 100, N'Chiếc', 13000000, N'SGA54-WHITE-8-256', 1),

-- Xiaomi Redmi Note 12
((SELECT MaSP FROM SanPham WHERE TenSP = N'Xiaomi Redmi Note 12'), 1, 2, 3, 180, N'Chiếc', 9000000, N'RN12-BLACK-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Xiaomi Redmi Note 12'), 3, 3, 4, 120, N'Chiếc', 12000000, N'RN12-HONG-8-256', 1),

-- Sony Xperia 10 V
((SELECT MaSP FROM SanPham WHERE TenSP = N'Sony Xperia 10 V'), 1, 2, 3, 100, N'Chiếc', 12000000, N'X10V-BLACK-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Sony Xperia 10 V'), 2, 3, 4, 80, N'Chiếc', 15000000, N'X10V-WHITE-8-256', 1),

-- Nokia G60 5G
((SELECT MaSP FROM SanPham WHERE TenSP = N'Nokia G60 5G'), 1, 2, 3, 140, N'Chiếc', 11000000, N'NG60-BLACK-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Nokia G60 5G'), 5, 3, 4, 100, N'Chiếc', 13000000, N'NG60-VANG-8-256', 1),

-- Asus Zenfone 10
((SELECT MaSP FROM SanPham WHERE TenSP = N'Asus Zenfone 10'), 1, 3, 4, 90, N'Chiếc', 22000000, N'AZ10-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Asus Zenfone 10'), 6, 4, 5, 70, N'Chiếc', 25000000, N'AZ10-XAM-12-512', 1),

-- LG Velvet
((SELECT MaSP FROM SanPham WHERE TenSP = N'LG Velvet'), 1, 2, 3, 70, N'Chiếc', 20000000, N'LGV-BLACK-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'LG Velvet'), 2, 3, 4, 50, N'Chiếc', 23000000, N'LGV-WHITE-8-256', 1),

-- Xiaomi Black Shark 5
((SELECT MaSP FROM SanPham WHERE TenSP = N'Xiaomi Black Shark 5'), 1, 3, 4, 100, N'Chiếc', 17000000, N'BS5-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Xiaomi Black Shark 5'), 4, 4, 5, 80, N'Chiếc', 20000000, N'BS5-XANH-12-512', 1),

-- Samsung Galaxy M14
((SELECT MaSP FROM SanPham WHERE TenSP = N'Samsung Galaxy M14'), 1, 2, 3, 160, N'Chiếc', 7000000, N'SGM14-BLACK-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Samsung Galaxy M14'), 4, 3, 4, 140, N'Chiếc', 9000000, N'SGM14-XANH-8-256', 1),

-- Huawei Mate X3
((SELECT MaSP FROM SanPham WHERE TenSP = N'Huawei Mate X3'), 1, 4, 5, 50, N'Chiếc', 45000000, N'MX3-BLACK-12-512', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Huawei Mate X3'), 2, 4, 5, 30, N'Chiếc', 48000000, N'MX3-WHITE-12-512', 1),

-- Sony Xperia Pro-I
((SELECT MaSP FROM SanPham WHERE TenSP = N'Sony Xperia Pro-I'), 1, 3, 4, 40, N'Chiếc', 35000000, N'XPROI-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Sony Xperia Pro-I'), 3, 4, 5, 20, N'Chiếc', 38000000, N'XPROI-HONG-12-512', 1),

-- iPhone SE 2023
((SELECT MaSP FROM SanPham WHERE TenSP = N'iPhone SE 2023'), 1, 2, 3, 150, N'Chiếc', 15000000, N'IPSE23-BLACK-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'iPhone SE 2023'), 2, 3, 4, 100, N'Chiếc', 17000000, N'IPSE23-WHITE-8-256', 1),

-- Samsung Galaxy S21 FE
((SELECT MaSP FROM SanPham WHERE TenSP = N'Samsung Galaxy S21 FE'), 1, 3, 4, 130, N'Chiếc', 16000000, N'SG21FE-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Samsung Galaxy S21 FE'), 4, 4, 5, 90, N'Chiếc', 19000000, N'SG21FE-XANH-12-512', 1),

-- Xiaomi Mi 12
((SELECT MaSP FROM SanPham WHERE TenSP = N'Xiaomi Mi 12'), 1, 3, 4, 100, N'Chiếc', 20000000, N'MI12-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Xiaomi Mi 12'), 6, 4, 5, 80, N'Chiếc', 24000000, N'MI12-XAM-12-512', 1),

-- Nokia XR21
((SELECT MaSP FROM SanPham WHERE TenSP = N'Nokia XR21'), 1, 2, 3, 120, N'Chiếc', 14000000, N'NX21-BLACK-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Nokia XR21'), 2, 3, 4, 100, N'Chiếc', 16000000, N'NX21-WHITE-8-256', 1),

-- Asus ROG Phone 7
((SELECT MaSP FROM SanPham WHERE TenSP = N'Asus ROG Phone 7'), 1, 3, 4, 100, N'Chiếc', 22000000, N'ROG7-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Asus ROG Phone 7'), 6, 4, 5, 70, N'Chiếc', 27000000, N'ROG7-XAM-12-512', 1),

-- Huawei Nova 11
((SELECT MaSP FROM SanPham WHERE TenSP = N'Huawei Nova 11'), 1, 2, 3, 90, N'Chiếc', 12000000, N'HN11-BLACK-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Huawei Nova 11'), 3, 3, 4, 70, N'Chiếc', 15000000, N'HN11-HONG-8-256', 1),

-- LG G8 ThinQ
((SELECT MaSP FROM SanPham WHERE TenSP = N'LG G8 ThinQ'), 1, 3, 4, 50, N'Chiếc', 18000000, N'LGG8-BLACK-8-256', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'LG G8 ThinQ'), 5, 4, 5, 30, N'Chiếc', 22000000, N'LGG8-VANG-12-512', 1),

-- Xiaomi Poco F5
((SELECT MaSP FROM SanPham WHERE TenSP = N'Xiaomi Poco F5'), 1, 2, 3, 140, N'Chiếc', 10000000, N'POCOF5-BLACK-6-128', 1),
((SELECT MaSP FROM SanPham WHERE TenSP = N'Xiaomi Poco F5'), 4, 3, 4, 100, N'Chiếc', 13000000, N'POCOF5-XANH-8-256', 1);
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Thêm hình ảnh cho từng biến thể sản phẩm
INSERT INTO HinhAnhSanPham (MaBT, DuongDanAnh, TenAnh, AnhChinh)
VALUES

    -- iPhone 13 Pro Max (IP13-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP13-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính IP13-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP13-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 IP13-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP13-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 IP13-BLACK-8-256', 0),
    
    -- iPhone 13 Pro Max (IP13-WHITE-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP13-WHITE-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính IP13-WHITE-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP13-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 IP13-WHITE-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP13-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 IP13-WHITE-12-512', 0),
    
    -- iPhone 14 Pro (IP14-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính IP14-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 IP14-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 IP14-BLACK-8-256', 0),
    
    -- iPhone 14 Pro (IP14-WHITE-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14-WHITE-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính IP14-WHITE-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 IP14-WHITE-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 IP14-WHITE-12-512', 0),
    
    -- iPhone 14 Pro Max (IP14MAX-BLACK-8-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14MAX-BLACK-8-512'), N'/Images/image-test-primary.png', N'Ảnh chính IP14MAX-BLACK-8-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14MAX-BLACK-8-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 IP14MAX-BLACK-8-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14MAX-BLACK-8-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 IP14MAX-BLACK-8-512', 0),
    
    -- iPhone 14 Pro Max (IP14MAX-HONG-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14MAX-HONG-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính IP14MAX-HONG-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14MAX-HONG-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 IP14MAX-HONG-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14MAX-HONG-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 IP14MAX-HONG-12-512', 0),
    
    -- Samsung Galaxy S23 Ultra (SG23U-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG23U-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính SG23U-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG23U-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 SG23U-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG23U-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 SG23U-BLACK-8-256', 0),
    
    -- Samsung Galaxy S23 Ultra (SG23U-XANH-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG23U-XANH-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính SG23U-XANH-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG23U-XANH-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 SG23U-XANH-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG23U-XANH-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 SG23U-XANH-12-512', 0),
    
    -- Samsung Galaxy Z Fold 5 (SZF5-BLACK-12-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SZF5-BLACK-12-256'), N'/Images/image-test-primary.png', N'Ảnh chính SZF5-BLACK-12-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SZF5-BLACK-12-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 SZF5-BLACK-12-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SZF5-BLACK-12-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 SZF5-BLACK-12-256', 0),
    
    -- Samsung Galaxy Z Fold 5 (SZF5-WHITE-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SZF5-WHITE-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính SZF5-WHITE-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SZF5-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 SZF5-WHITE-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SZF5-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 SZF5-WHITE-12-512', 0),
    
    -- Xiaomi 13 Ultra (X13U-VANG-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X13U-VANG-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính X13U-VANG-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X13U-VANG-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 X13U-VANG-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X13U-VANG-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 X13U-VANG-6-128', 0),
    
    -- Xiaomi 13 Ultra (X13U-XAM-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X13U-XAM-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính X13U-XAM-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X13U-XAM-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 X13U-XAM-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X13U-XAM-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 X13U-XAM-8-256', 0),
    
    -- Sony Xperia 1 V (X1V-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X1V-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính X1V-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X1V-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 X1V-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X1V-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 X1V-BLACK-8-256', 0),
    
    -- Sony Xperia 1 V (X1V-WHITE-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X1V-WHITE-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính X1V-WHITE-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X1V-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 X1V-WHITE-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X1V-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 X1V-WHITE-12-512', 0),
    
    -- Nokia X30 5G (NX30-BLACK-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX30-BLACK-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính NX30-BLACK-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX30-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 NX30-BLACK-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX30-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 NX30-BLACK-6-128', 0),
    
    -- Nokia X30 5G (NX30-WHITE-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX30-WHITE-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính NX30-WHITE-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX30-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 NX30-WHITE-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX30-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 NX30-WHITE-8-256', 0),
    
    -- Huawei P60 Pro (P60-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'P60-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính P60-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'P60-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 P60-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'P60-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 P60-BLACK-8-256', 0),
    
    -- Huawei P60 Pro (P60-HONG-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'P60-HONG-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính P60-HONG-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'P60-HONG-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 P60-HONG-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'P60-HONG-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 P60-HONG-12-512', 0),
    
    -- Asus ROG Phone 6 (ROG6-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG6-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính ROG6-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG6-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 ROG6-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG6-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 ROG6-BLACK-8-256', 0),
    
    -- Asus ROG Phone 6 (ROG6-XAM-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG6-XAM-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính ROG6-XAM-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG6-XAM-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 ROG6-XAM-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG6-XAM-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 ROG6-XAM-12-512', 0),
    
    -- LG Wing (LGW-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGW-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính LGW-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGW-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 LGW-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGW-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 LGW-BLACK-8-256', 0),
    
    -- LG Wing (LGW-VANG-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGW-VANG-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính LGW-VANG-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGW-VANG-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 LGW-VANG-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGW-VANG-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 LGW-VANG-12-512', 0),
    
    -- iPhone 14 Plus (IP14P-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14P-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính IP14P-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14P-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 IP14P-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14P-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 IP14P-BLACK-8-256', 0),
    
    -- iPhone 14 Plus (IP14P-WHITE-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14P-WHITE-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính IP14P-WHITE-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14P-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 IP14P-WHITE-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IP14P-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 IP14P-WHITE-12-512', 0),
    
    -- Samsung Galaxy A54 5G (SGA54-BLACK-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGA54-BLACK-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính SGA54-BLACK-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGA54-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 SGA54-BLACK-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGA54-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 SGA54-BLACK-6-128', 0),
    
    -- Samsung Galaxy A54 5G (SGA54-WHITE-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGA54-WHITE-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính SGA54-WHITE-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGA54-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 SGA54-WHITE-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGA54-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 SGA54-WHITE-8-256', 0),
    
    -- Xiaomi Redmi Note 12 (RN12-BLACK-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'RN12-BLACK-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính RN12-BLACK-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'RN12-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 RN12-BLACK-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'RN12-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 RN12-BLACK-6-128', 0),
    
    -- Xiaomi Redmi Note 12 (RN12-HONG-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'RN12-HONG-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính RN12-HONG-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'RN12-HONG-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 RN12-HONG-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'RN12-HONG-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 RN12-HONG-8-256', 0),
    
    -- Sony Xperia 10 V (X10V-BLACK-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X10V-BLACK-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính X10V-BLACK-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X10V-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 X10V-BLACK-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X10V-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 X10V-BLACK-6-128', 0),
    
    -- Sony Xperia 10 V (X10V-WHITE-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X10V-WHITE-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính X10V-WHITE-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X10V-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 X10V-WHITE-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'X10V-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 X10V-WHITE-8-256', 0),
    
    -- Nokia G60 5G (NG60-BLACK-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NG60-BLACK-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính NG60-BLACK-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NG60-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 NG60-BLACK-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NG60-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 NG60-BLACK-6-128', 0),
    
    -- Nokia G60 5G (NG60-VANG-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NG60-VANG-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính NG60-VANG-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NG60-VANG-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 NG60-VANG-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NG60-VANG-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 NG60-VANG-8-256', 0),
    
    -- Asus Zenfone 10 (AZ10-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'AZ10-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính AZ10-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'AZ10-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 AZ10-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'AZ10-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 AZ10-BLACK-8-256', 0),
    
    -- Asus Zenfone 10 (AZ10-XAM-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'AZ10-XAM-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính AZ10-XAM-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'AZ10-XAM-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 AZ10-XAM-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'AZ10-XAM-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 AZ10-XAM-12-512', 0),
    
    -- LG Velvet (LGV-BLACK-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGV-BLACK-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính LGV-BLACK-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGV-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 LGV-BLACK-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGV-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 LGV-BLACK-6-128', 0),
    
    -- LG Velvet (LGV-WHITE-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGV-WHITE-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính LGV-WHITE-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGV-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 LGV-WHITE-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGV-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 LGV-WHITE-8-256', 0),
    
    -- Xiaomi Black Shark 5 (BS5-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'BS5-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính BS5-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'BS5-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 BS5-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'BS5-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 BS5-BLACK-8-256', 0),
    
    -- Xiaomi Black Shark 5 (BS5-XANH-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'BS5-XANH-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính BS5-XANH-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'BS5-XANH-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 BS5-XANH-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'BS5-XANH-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 BS5-XANH-12-512', 0),
    
    -- Samsung Galaxy M14 (SGM14-BLACK-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGM14-BLACK-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính SGM14-BLACK-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGM14-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 SGM14-BLACK-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGM14-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 SGM14-BLACK-6-128', 0),
    
    -- Samsung Galaxy M14 (SGM14-XANH-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGM14-XANH-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính SGM14-XANH-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGM14-XANH-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 SGM14-XANH-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SGM14-XANH-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 SGM14-XANH-8-256', 0),
    
    -- Huawei Mate X3 (MX3-BLACK-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MX3-BLACK-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính MX3-BLACK-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MX3-BLACK-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 MX3-BLACK-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MX3-BLACK-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 MX3-BLACK-12-512', 0),
    
    -- Huawei Mate X3 (MX3-WHITE-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MX3-WHITE-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính MX3-WHITE-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MX3-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 MX3-WHITE-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MX3-WHITE-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 MX3-WHITE-12-512', 0),
    
    -- Sony Xperia Pro-I (XPROI-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'XPROI-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính XPROI-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'XPROI-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 XPROI-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'XPROI-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 XPROI-BLACK-8-256', 0),
    
    -- Sony Xperia Pro-I (XPROI-HONG-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'XPROI-HONG-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính XPROI-HONG-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'XPROI-HONG-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 XPROI-HONG-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'XPROI-HONG-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 XPROI-HONG-12-512', 0),
    
    -- iPhone SE 2023 (IPSE23-BLACK-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IPSE23-BLACK-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính IPSE23-BLACK-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IPSE23-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 IPSE23-BLACK-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IPSE23-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 IPSE23-BLACK-6-128', 0),
    
    -- iPhone SE 2023 (IPSE23-WHITE-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IPSE23-WHITE-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính IPSE23-WHITE-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IPSE23-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 IPSE23-WHITE-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'IPSE23-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 IPSE23-WHITE-8-256', 0),
    
    -- Samsung Galaxy S21 FE (SG21FE-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG21FE-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính SG21FE-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG21FE-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 SG21FE-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG21FE-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 SG21FE-BLACK-8-256', 0),
    
    -- Samsung Galaxy S21 FE (SG21FE-XANH-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG21FE-XANH-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính SG21FE-XANH-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG21FE-XANH-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 SG21FE-XANH-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'SG21FE-XANH-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 SG21FE-XANH-12-512', 0),
    
    -- Xiaomi Mi 12 (MI12-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MI12-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính MI12-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MI12-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 MI12-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MI12-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 MI12-BLACK-8-256', 0),
    
    -- Xiaomi Mi 12 (MI12-XAM-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MI12-XAM-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính MI12-XAM-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MI12-XAM-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 MI12-XAM-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'MI12-XAM-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 MI12-XAM-12-512', 0),
    
    -- Nokia XR21 (NX21-BLACK-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX21-BLACK-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính NX21-BLACK-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX21-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 NX21-BLACK-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX21-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 NX21-BLACK-6-128', 0),
    
    -- Nokia XR21 (NX21-WHITE-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX21-WHITE-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính NX21-WHITE-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX21-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 NX21-WHITE-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'NX21-WHITE-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 NX21-WHITE-8-256', 0),
    
    -- Asus ROG Phone 7 (ROG7-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG7-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính ROG7-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG7-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 ROG7-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG7-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 ROG7-BLACK-8-256', 0),
    
    -- Asus ROG Phone 7 (ROG7-XAM-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG7-XAM-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính ROG7-XAM-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG7-XAM-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 ROG7-XAM-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'ROG7-XAM-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 ROG7-XAM-12-512', 0),
    
    -- Huawei Nova 11 (HN11-BLACK-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'HN11-BLACK-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính HN11-BLACK-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'HN11-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 HN11-BLACK-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'HN11-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 HN11-BLACK-6-128', 0),
    
    -- Huawei Nova 11 (HN11-HONG-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'HN11-HONG-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính HN11-HONG-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'HN11-HONG-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 HN11-HONG-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'HN11-HONG-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 HN11-HONG-8-256', 0),
    
    -- LG G8 ThinQ (LGG8-BLACK-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGG8-BLACK-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính LGG8-BLACK-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGG8-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 LGG8-BLACK-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGG8-BLACK-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 LGG8-BLACK-8-256', 0),
    
    -- LG G8 ThinQ (LGG8-VANG-12-512)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGG8-VANG-12-512'), N'/Images/image-test-primary.png', N'Ảnh chính LGG8-VANG-12-512', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGG8-VANG-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 LGG8-VANG-12-512', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'LGG8-VANG-12-512'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 LGG8-VANG-12-512', 0),
    
    -- Xiaomi Poco F5 (POCOF5-BLACK-6-128)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'POCOF5-BLACK-6-128'), N'/Images/image-test-primary.png', N'Ảnh chính POCOF5-BLACK-6-128', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'POCOF5-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 POCOF5-BLACK-6-128', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'POCOF5-BLACK-6-128'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 POCOF5-BLACK-6-128', 0),
    
    -- Xiaomi Poco F5 (POCOF5-XANH-8-256)
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'POCOF5-XANH-8-256'), N'/Images/image-test-primary.png', N'Ảnh chính POCOF5-XANH-8-256', 1),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'POCOF5-XANH-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 1 POCOF5-XANH-8-256', 0),
    ((SELECT MaBT FROM BienTheSanPham WHERE SKU = N'POCOF5-XANH-8-256'), N'/Images/image-test-secondary.png', N'Ảnh phụ 2 POCOF5-XANH-8-256', 0);
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Truy vấn mẫu
Select * from QuanTriVien
Select * from LoaiCamera
Select * from KhachHang
Select * from GioHang
Select * from ChiTietGioHang
Select * from SanPham
Select * from HinhAnhSanPham
select * from BienTheSanPham

delete HinhAnhSanPham
DELETE KHACHHANG

DROP TABLE KhachHang

SELECT 
    sp.TenSP AS TenSanPham,
    dpmh.TenDoPhanGiai AS DoPhanGiaiManHinh,
    ktmh.KichThuoc AS KichThuocManHinh,
    (
        SELECT 
            rom.DungLuong AS ROM,
            bt.DonGia AS DonGia,
            bt.MaBT AS BienTheID
        FROM 
            BienTheSanPham bt
        INNER JOIN 
            ROM rom ON bt.MaROM = rom.MaROM
        WHERE 
            bt.MaSP = sp.MaSP -- Sử dụng MaSP để liên kết
        FOR JSON PATH
    ) AS Variants
FROM 
    SanPham sp
INNER JOIN 
    BienTheSanPham bt ON sp.MaSP = bt.MaSP
INNER JOIN 
    ThongSoBienTheDienThoai tsbt ON bt.MaBT = tsbt.MaBT
INNER JOIN 
    DoPhanGiaiManHinh dpmh ON tsbt.MaDoPhanGiai = dpmh.MaDoPhanGiai
INNER JOIN 
    KichThuocManHinh ktmh ON tsbt.MaKichThuocManHinh = ktmh.MaKichThuocManHinh
GROUP BY 
    sp.TenSP, dpmh.TenDoPhanGiai, ktmh.KichThuoc, sp.MaSP;
------------------------------------------------------
SELECT 
    sp.MaSP, 
    sp.TenSP, 
    sp.MoTa,
    bt.MaBT, 
    bt.SKU, -- Thêm mã SKU của biến thể
    ms.TenMau AS MauSac, -- Lấy tên màu từ bảng MauSac
    ram.DungLuong AS RAM, -- Lấy dung lượng RAM từ bảng RAM
    rom.DungLuong AS ROM, -- Lấy dung lượng ROM từ bảng ROM
    bt.DonGia, 
    bt.SoLuong,
    ha.TenAnh,
    ha.DuongDanAnh,
    ha.AnhChinh -- Xác định ảnh chính hay ảnh phụ
FROM 
    SanPham AS sp
LEFT JOIN 
    BienTheSanPham AS bt ON sp.MaSP = bt.MaSP
LEFT JOIN 
    MauSac AS ms ON bt.MaMau = ms.MaMau -- Join với bảng MauSac
LEFT JOIN 
    RAM AS ram ON bt.MaRAM = ram.MaRAM -- Join với bảng RAM
LEFT JOIN 
    ROM AS rom ON bt.MaROM = rom.MaROM -- Join với bảng ROM
LEFT JOIN 
    HinhAnhSanPham AS ha ON bt.MaBT = ha.MaBT
WHERE 
    sp.MaSP = 10;

-------------------------------------
-- độ phân giải camera sau 
SELECT 
    sp.MaSP AS MaSanPham,
    sp.TenSP AS TenSanPham,
    bt.MaBT AS MaBienThe,
    lc.TenLoaiCamera AS LoaiCamera,
    dpg.DoPhanGiai AS DoPhanGiaiCameraSau
FROM 
    SanPham AS sp
LEFT JOIN 
    BienTheSanPham AS bt ON sp.MaSP = bt.MaSP
LEFT JOIN 
    ThongSoBienTheDienThoai AS tsbt ON bt.MaBT = tsbt.MaBT
LEFT JOIN 
    ThongSoCamera AS tsc ON tsbt.MaTSBTDT = tsc.MaTSBTDT
LEFT JOIN 
    LoaiCamera AS lc ON tsc.MaLoaiCamera = lc.MaLoaiCamera
LEFT JOIN 
    ChiTietDoPhanGiaiCamera AS ctdpg ON tsc.MaThongSoCamera = ctdpg.MaThongSoCamera
LEFT JOIN 
    DoPhanGiaiCamera AS dpg ON ctdpg.MaDoPhanGiai = dpg.MaDoPhanGiai
WHERE 
    sp.MaSP = 10
    AND lc.TenLoaiCamera = 'Sau'; -- Lọc chỉ lấy thông tin của Camera Sau


