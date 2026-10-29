-- =========================================================================
-- SCRIPT TẠO CƠ SỞ DỮ LIỆU: QUẢN LÝ BÁN QUẦN ÁO (MÔN LẬP TRÌNH TRỰC QUAN)
-- Hệ quản trị CSDL: Microsoft SQL Server (Bao gồm 200 bộ quần áo mẫu)
-- =========================================================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QuanLyBanQuanAo')
BEGIN
    CREATE DATABASE QuanLyBanQuanAo;
END
GO
USE QuanLyBanQuanAo;
GO

-- Xóa dữ liệu cũ nếu chạy lại script để nạp đủ 200 bộ quần áo mới
IF OBJECT_ID('tblChiTietHDBan', 'U') IS NOT NULL DROP TABLE tblChiTietHDBan;
IF OBJECT_ID('tblHDBan', 'U') IS NOT NULL DROP TABLE tblHDBan;
IF OBJECT_ID('tblQuanAo', 'U') IS NOT NULL DROP TABLE tblQuanAo;
IF OBJECT_ID('tblKhach', 'U') IS NOT NULL DROP TABLE tblKhach;
IF OBJECT_ID('tblNhanVien', 'U') IS NOT NULL DROP TABLE tblNhanVien;
IF OBJECT_ID('tblChatLieu', 'U') IS NOT NULL DROP TABLE tblChatLieu;
IF OBJECT_ID('tblLoaiThoiTrang', 'U') IS NOT NULL DROP TABLE tblLoaiThoiTrang;
IF OBJECT_ID('tblTaiKhoan', 'U') IS NOT NULL DROP TABLE tblTaiKhoan;
GO

-- 1. Bảng Tài khoản & Phân quyền (Admin, QuanLy, NguoiMua)
CREATE TABLE tblTaiKhoan (
    TenDangNhap NVARCHAR(50) NOT NULL PRIMARY KEY,
    MatKhau NVARCHAR(50) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    Quyen NVARCHAR(20) NOT NULL -- 'Admin', 'QuanLy', 'NguoiMua'
);

-- 2. Bảng Loại thời trang (8 dòng thời trang đa dạng)
CREATE TABLE tblLoaiThoiTrang (
    MaLoai NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL
);

-- 3. Bảng Chất liệu (8 chất liệu vải phổ biến)
CREATE TABLE tblChatLieu (
    MaChatLieu NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenChatLieu NVARCHAR(100) NOT NULL
);

-- 4. Bảng Nhân viên
CREATE TABLE tblNhanVien (
    MaNhanVien NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenNhanVien NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(20),
    NgaySinh DATE
);

-- 5. Bảng Khách hàng
CREATE TABLE tblKhach (
    MaKhach NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenKhach NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200) NOT NULL,
    DienThoai NVARCHAR(20) NOT NULL
);

-- 6. Bảng Quần áo (Sản phẩm)
CREATE TABLE tblQuanAo (
    MaQuanAo NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenQuanAo NVARCHAR(150) NOT NULL,
    MaLoai NVARCHAR(20) NOT NULL,
    MaChatLieu NVARCHAR(20) NOT NULL,
    KichCo NVARCHAR(10) NOT NULL,       -- S, M, L, XL, XXL
    MauSac NVARCHAR(50) NOT NULL,       -- Trắng, Đen, Xanh Navy, Be...
    SoLuong INT NOT NULL DEFAULT 0,
    DonGiaNhap FLOAT NOT NULL DEFAULT 0,
    DonGiaBan FLOAT NOT NULL DEFAULT 0,
    Anh NVARCHAR(255),                  -- Tên file ảnh trong thư mục bin\Debug\Images\QuanAo
    GhiChu NVARCHAR(255),
    CONSTRAINT FK_QuanAo_Loai FOREIGN KEY (MaLoai) REFERENCES tblLoaiThoiTrang(MaLoai),
    CONSTRAINT FK_QuanAo_ChatLieu FOREIGN KEY (MaChatLieu) REFERENCES tblChatLieu(MaChatLieu)
);

-- 7. Bảng Hóa đơn bán / Đơn đặt hàng
CREATE TABLE tblHDBan (
    MaHDBan NVARCHAR(30) NOT NULL PRIMARY KEY,
    MaNhanVien NVARCHAR(20) NULL,
    NgayBan DATETIME NOT NULL DEFAULT GETDATE(),
    MaKhach NVARCHAR(20) NOT NULL,
    TongTien FLOAT NOT NULL DEFAULT 0,
    TrangThai NVARCHAR(50) DEFAULT N'Đã thanh toán',
    CONSTRAINT FK_HDBan_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES tblNhanVien(MaNhanVien),
    CONSTRAINT FK_HDBan_Khach FOREIGN KEY (MaKhach) REFERENCES tblKhach(MaKhach)
);

-- 8. Bảng Chi tiết Hóa đơn bán
CREATE TABLE tblChiTietHDBan (
    MaHDBan NVARCHAR(30) NOT NULL,
    MaQuanAo NVARCHAR(20) NOT NULL,
    SoLuong INT NOT NULL,
    DonGia FLOAT NOT NULL,
    GiamGia FLOAT NOT NULL DEFAULT 0,
    ThanhTien FLOAT NOT NULL,
    CONSTRAINT PK_ChiTietHDBan PRIMARY KEY (MaHDBan, MaQuanAo),
    CONSTRAINT FK_ChiTiet_HDBan FOREIGN KEY (MaHDBan) REFERENCES tblHDBan(MaHDBan) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTiet_QuanAo FOREIGN KEY (MaQuanAo) REFERENCES tblQuanAo(MaQuanAo)
);
GO

-- =========================================================================
-- DỮ LIỆU MẪU: TÀI KHOẢN, LOẠI THỜI TRANG, CHẤT LIỆU, NHÂN VIÊN, KHÁCH HÀNG
-- =========================================================================

INSERT INTO tblTaiKhoan VALUES (N'admin', N'123456', N'Quản trị viên Hệ thống', N'Admin');
INSERT INTO tblTaiKhoan VALUES (N'quanly', N'123456', N'Nguyễn Thu Hường (Quản lý)', N'QuanLy');
INSERT INTO tblTaiKhoan VALUES (N'khach', N'123456', N'Khách mua hàng', N'NguoiMua');

INSERT INTO tblLoaiThoiTrang VALUES (N'L01', N'Áo sơ mi công sở');
INSERT INTO tblLoaiThoiTrang VALUES (N'L02', N'Quần Jeans & Denim');
INSERT INTO tblLoaiThoiTrang VALUES (N'L03', N'Váy đầm thời trang');
INSERT INTO tblLoaiThoiTrang VALUES (N'L04', N'Áo khoác & Hoodie');
INSERT INTO tblLoaiThoiTrang VALUES (N'L05', N'Đồ thể thao (Sportswear)');
INSERT INTO tblLoaiThoiTrang VALUES (N'L06', N'Áo thun & Polo');
INSERT INTO tblLoaiThoiTrang VALUES (N'L07', N'Quần âu & Kaki');
INSERT INTO tblLoaiThoiTrang VALUES (N'L08', N'Đồ mặc nhà & Pijama');

INSERT INTO tblChatLieu VALUES (N'CL01', N'Cotton 100%');
INSERT INTO tblChatLieu VALUES (N'CL02', N'Denim / Jeans');
INSERT INTO tblChatLieu VALUES (N'CL03', N'Lụa tơ tằm');
INSERT INTO tblChatLieu VALUES (N'CL04', N'Kaki co giãn');
INSERT INTO tblChatLieu VALUES (N'CL05', N'Nỉ bông cao cấp');
INSERT INTO tblChatLieu VALUES (N'CL06', N'Vải đũi (Linen)');
INSERT INTO tblChatLieu VALUES (N'CL07', N'Thun lạnh thể thao');
INSERT INTO tblChatLieu VALUES (N'CL08', N'Dạ Tweed & Len');

INSERT INTO tblNhanVien VALUES (N'NV01', N'Nguyễn Văn Anh', N'Nam', N'Đông Anh - Hà Nội', N'0912345678', '1998-10-10');
INSERT INTO tblNhanVien VALUES (N'NV02', N'Trần Thị Mai Hoa', N'Nữ', N'Cầu Giấy - Hà Nội', N'0987654321', '2000-05-15');
INSERT INTO tblNhanVien VALUES (N'NV03', N'Lê Hoàng Nam', N'Nam', N'Đống Đa - Hà Nội', N'0905112233', '1999-12-01');

INSERT INTO tblKhach VALUES (N'KH01', N'Vương Tiến Anh', N'Nguyên Khê - Đông Anh - Hà Nội', N'0976967619');
INSERT INTO tblKhach VALUES (N'KH02', N'Nguyễn Thị Hồng Hoa', N'183 Nguyễn Chí Thanh - Hà Nội', N'0986698119');
INSERT INTO tblKhach VALUES (N'KH03', N'Phạm Minh Tuấn', N'Số 3 Cầu Giấy - Hà Nội', N'0913556677');

-- =========================================================================
-- DANH SÁCH 200 BỘ QUẦN ÁO THỜI TRANG (QA001 -> QA200)
-- =========================================================================
INSERT INTO tblQuanAo VALUES (N'QA001', N'Áo sơ mi Oxford dài tay Classic - Màu Trắng (Size S)', N'L01', N'CL01', N'S', N'Trắng', 27, 160000, 290000, N'somi_trang.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA002', N'Áo sơ mi công sở Slimfit chống nhăn - Màu Đen (Size M)', N'L01', N'CL01', N'M', N'Đen', 34, 175000, 315000, N'somi_trang.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA003', N'Áo sơ mi kẻ sọc Hàn Quốc - Màu Xanh Navy (Size L)', N'L01', N'CL01', N'L', N'Xanh Navy', 41, 190000, 340000, N'somi_trang.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA004', N'Áo sơ mi cổ tàu Linen thoáng mát - Màu Be (Kem) (Size XL)', N'L01', N'CL01', N'XL', N'Be (Kem)', 48, 205000, 365000, N'somi_trang.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA005', N'Áo sơ mi nữ cổ nơ thanh lịch - Màu Xám khói (Size XXL)', N'L01', N'CL01', N'XXL', N'Xám khói', 55, 220000, 390000, N'somi_trang.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA006', N'Áo sơ mi Oxford dài tay Classic - Màu Đỏ đô (Size S)', N'L01', N'CL01', N'S', N'Đỏ đô', 62, 160000, 290000, N'somi_trang.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA007', N'Áo sơ mi công sở Slimfit chống nhăn - Màu Xanh rêu (Size M)', N'L01', N'CL01', N'M', N'Xanh rêu', 69, 175000, 315000, N'somi_trang.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA008', N'Áo sơ mi kẻ sọc Hàn Quốc - Màu Nâu Tây (Size L)', N'L01', N'CL01', N'L', N'Nâu Tây', 76, 190000, 340000, N'somi_trang.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA009', N'Áo sơ mi cổ tàu Linen thoáng mát - Màu Hồng Pastel (Size XL)', N'L01', N'CL01', N'XL', N'Hồng Pastel', 83, 205000, 365000, N'somi_trang.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA010', N'Áo sơ mi nữ cổ nơ thanh lịch - Màu Xanh Dương (Size XXL)', N'L01', N'CL01', N'XXL', N'Xanh Dương', 90, 220000, 390000, N'somi_trang.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA011', N'Áo sơ mi Oxford dài tay Classic - Màu Trắng (Size S)', N'L01', N'CL01', N'S', N'Trắng', 97, 160000, 290000, N'somi_trang.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA012', N'Áo sơ mi công sở Slimfit chống nhăn - Màu Đen (Size M)', N'L01', N'CL01', N'M', N'Đen', 24, 175000, 315000, N'somi_trang.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA013', N'Áo sơ mi kẻ sọc Hàn Quốc - Màu Xanh Navy (Size L)', N'L01', N'CL01', N'L', N'Xanh Navy', 31, 190000, 340000, N'somi_trang.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA014', N'Áo sơ mi cổ tàu Linen thoáng mát - Màu Be (Kem) (Size XL)', N'L01', N'CL01', N'XL', N'Be (Kem)', 38, 205000, 365000, N'somi_trang.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA015', N'Áo sơ mi nữ cổ nơ thanh lịch - Màu Xám khói (Size XXL)', N'L01', N'CL01', N'XXL', N'Xám khói', 45, 220000, 390000, N'somi_trang.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA016', N'Áo sơ mi Oxford dài tay Classic - Màu Đỏ đô (Size S)', N'L01', N'CL01', N'S', N'Đỏ đô', 52, 160000, 290000, N'somi_trang.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA017', N'Áo sơ mi công sở Slimfit chống nhăn - Màu Xanh rêu (Size M)', N'L01', N'CL01', N'M', N'Xanh rêu', 59, 175000, 315000, N'somi_trang.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA018', N'Áo sơ mi kẻ sọc Hàn Quốc - Màu Nâu Tây (Size L)', N'L01', N'CL01', N'L', N'Nâu Tây', 66, 190000, 340000, N'somi_trang.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA019', N'Áo sơ mi cổ tàu Linen thoáng mát - Màu Hồng Pastel (Size XL)', N'L01', N'CL01', N'XL', N'Hồng Pastel', 73, 205000, 365000, N'somi_trang.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA020', N'Áo sơ mi nữ cổ nơ thanh lịch - Màu Xanh Dương (Size XXL)', N'L01', N'CL01', N'XXL', N'Xanh Dương', 80, 220000, 390000, N'somi_trang.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA021', N'Áo sơ mi Oxford dài tay Classic - Màu Trắng (Size S)', N'L01', N'CL01', N'S', N'Trắng', 87, 160000, 290000, N'somi_trang.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA022', N'Áo sơ mi công sở Slimfit chống nhăn - Màu Đen (Size M)', N'L01', N'CL01', N'M', N'Đen', 94, 175000, 315000, N'somi_trang.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA023', N'Áo sơ mi kẻ sọc Hàn Quốc - Màu Xanh Navy (Size L)', N'L01', N'CL01', N'L', N'Xanh Navy', 21, 190000, 340000, N'somi_trang.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA024', N'Áo sơ mi cổ tàu Linen thoáng mát - Màu Be (Kem) (Size XL)', N'L01', N'CL01', N'XL', N'Be (Kem)', 28, 205000, 365000, N'somi_trang.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA025', N'Áo sơ mi nữ cổ nơ thanh lịch - Màu Xám khói (Size XXL)', N'L01', N'CL01', N'XXL', N'Xám khói', 35, 220000, 390000, N'somi_trang.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA026', N'Quần Jeans ống đứng Regular Fit - Màu Đen (Size S)', N'L02', N'CL02', N'S', N'Đen', 42, 220000, 420000, N'jeans_nam.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA027', N'Quần Jeans ống rộng Baggy trẻ trung - Màu Xanh Navy (Size M)', N'L02', N'CL02', N'M', N'Xanh Navy', 49, 235000, 445000, N'jeans_nam.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA028', N'Quần Jeans nữ cạp cao tôn dáng - Màu Be (Kem) (Size L)', N'L02', N'CL02', N'L', N'Be (Kem)', 56, 250000, 470000, N'jeans_nam.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA029', N'Quần Jeans rách gối Streetwear - Màu Xám khói (Size XL)', N'L02', N'CL02', N'XL', N'Xám khói', 63, 265000, 495000, N'jeans_nam.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA030', N'Quần Short Jeans năng động mùa hè - Màu Đỏ đô (Size XXL)', N'L02', N'CL02', N'XXL', N'Đỏ đô', 70, 280000, 520000, N'jeans_nam.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA031', N'Quần Jeans ống đứng Regular Fit - Màu Xanh rêu (Size S)', N'L02', N'CL02', N'S', N'Xanh rêu', 77, 220000, 420000, N'jeans_nam.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA032', N'Quần Jeans ống rộng Baggy trẻ trung - Màu Nâu Tây (Size M)', N'L02', N'CL02', N'M', N'Nâu Tây', 84, 235000, 445000, N'jeans_nam.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA033', N'Quần Jeans nữ cạp cao tôn dáng - Màu Hồng Pastel (Size L)', N'L02', N'CL02', N'L', N'Hồng Pastel', 91, 250000, 470000, N'jeans_nam.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA034', N'Quần Jeans rách gối Streetwear - Màu Xanh Dương (Size XL)', N'L02', N'CL02', N'XL', N'Xanh Dương', 98, 265000, 495000, N'jeans_nam.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA035', N'Quần Short Jeans năng động mùa hè - Màu Trắng (Size XXL)', N'L02', N'CL02', N'XXL', N'Trắng', 25, 280000, 520000, N'jeans_nam.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA036', N'Quần Jeans ống đứng Regular Fit - Màu Đen (Size S)', N'L02', N'CL02', N'S', N'Đen', 32, 220000, 420000, N'jeans_nam.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA037', N'Quần Jeans ống rộng Baggy trẻ trung - Màu Xanh Navy (Size M)', N'L02', N'CL02', N'M', N'Xanh Navy', 39, 235000, 445000, N'jeans_nam.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA038', N'Quần Jeans nữ cạp cao tôn dáng - Màu Be (Kem) (Size L)', N'L02', N'CL02', N'L', N'Be (Kem)', 46, 250000, 470000, N'jeans_nam.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA039', N'Quần Jeans rách gối Streetwear - Màu Xám khói (Size XL)', N'L02', N'CL02', N'XL', N'Xám khói', 53, 265000, 495000, N'jeans_nam.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA040', N'Quần Short Jeans năng động mùa hè - Màu Đỏ đô (Size XXL)', N'L02', N'CL02', N'XXL', N'Đỏ đô', 60, 280000, 520000, N'jeans_nam.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA041', N'Quần Jeans ống đứng Regular Fit - Màu Xanh rêu (Size S)', N'L02', N'CL02', N'S', N'Xanh rêu', 67, 220000, 420000, N'jeans_nam.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA042', N'Quần Jeans ống rộng Baggy trẻ trung - Màu Nâu Tây (Size M)', N'L02', N'CL02', N'M', N'Nâu Tây', 74, 235000, 445000, N'jeans_nam.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA043', N'Quần Jeans nữ cạp cao tôn dáng - Màu Hồng Pastel (Size L)', N'L02', N'CL02', N'L', N'Hồng Pastel', 81, 250000, 470000, N'jeans_nam.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA044', N'Quần Jeans rách gối Streetwear - Màu Xanh Dương (Size XL)', N'L02', N'CL02', N'XL', N'Xanh Dương', 88, 265000, 495000, N'jeans_nam.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA045', N'Quần Short Jeans năng động mùa hè - Màu Trắng (Size XXL)', N'L02', N'CL02', N'XXL', N'Trắng', 95, 280000, 520000, N'jeans_nam.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA046', N'Quần Jeans ống đứng Regular Fit - Màu Đen (Size S)', N'L02', N'CL02', N'S', N'Đen', 22, 220000, 420000, N'jeans_nam.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA047', N'Quần Jeans ống rộng Baggy trẻ trung - Màu Xanh Navy (Size M)', N'L02', N'CL02', N'M', N'Xanh Navy', 29, 235000, 445000, N'jeans_nam.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA048', N'Quần Jeans nữ cạp cao tôn dáng - Màu Be (Kem) (Size L)', N'L02', N'CL02', N'L', N'Be (Kem)', 36, 250000, 470000, N'jeans_nam.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA049', N'Quần Jeans rách gối Streetwear - Màu Xám khói (Size XL)', N'L02', N'CL02', N'XL', N'Xám khói', 43, 265000, 495000, N'jeans_nam.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA050', N'Quần Short Jeans năng động mùa hè - Màu Đỏ đô (Size XXL)', N'L02', N'CL02', N'XXL', N'Đỏ đô', 50, 280000, 520000, N'jeans_nam.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA051', N'Đầm lụa dáng xòe dự tiệc - Màu Xanh Navy (Size S)', N'L03', N'CL03', N'S', N'Xanh Navy', 57, 280000, 550000, N'dam_lua.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA052', N'Váy hoa nhí Vintage dáng dài - Màu Be (Kem) (Size M)', N'L03', N'CL03', N'M', N'Be (Kem)', 64, 295000, 575000, N'dam_lua.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA053', N'Đầm công sở chiết eo thanh lịch - Màu Xám khói (Size L)', N'L03', N'CL03', N'L', N'Xám khói', 71, 310000, 600000, N'dam_lua.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA054', N'Chân váy xếp ly dáng chữ A - Màu Đỏ đô (Size XL)', N'L03', N'CL03', N'XL', N'Đỏ đô', 78, 325000, 625000, N'dam_lua.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA055', N'Đầm dạ hội cổ V sang trọng - Màu Xanh rêu (Size XXL)', N'L03', N'CL03', N'XXL', N'Xanh rêu', 85, 340000, 650000, N'dam_lua.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA056', N'Đầm lụa dáng xòe dự tiệc - Màu Nâu Tây (Size S)', N'L03', N'CL03', N'S', N'Nâu Tây', 92, 280000, 550000, N'dam_lua.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA057', N'Váy hoa nhí Vintage dáng dài - Màu Hồng Pastel (Size M)', N'L03', N'CL03', N'M', N'Hồng Pastel', 99, 295000, 575000, N'dam_lua.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA058', N'Đầm công sở chiết eo thanh lịch - Màu Xanh Dương (Size L)', N'L03', N'CL03', N'L', N'Xanh Dương', 26, 310000, 600000, N'dam_lua.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA059', N'Chân váy xếp ly dáng chữ A - Màu Trắng (Size XL)', N'L03', N'CL03', N'XL', N'Trắng', 33, 325000, 625000, N'dam_lua.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA060', N'Đầm dạ hội cổ V sang trọng - Màu Đen (Size XXL)', N'L03', N'CL03', N'XXL', N'Đen', 40, 340000, 650000, N'dam_lua.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA061', N'Đầm lụa dáng xòe dự tiệc - Màu Xanh Navy (Size S)', N'L03', N'CL03', N'S', N'Xanh Navy', 47, 280000, 550000, N'dam_lua.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA062', N'Váy hoa nhí Vintage dáng dài - Màu Be (Kem) (Size M)', N'L03', N'CL03', N'M', N'Be (Kem)', 54, 295000, 575000, N'dam_lua.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA063', N'Đầm công sở chiết eo thanh lịch - Màu Xám khói (Size L)', N'L03', N'CL03', N'L', N'Xám khói', 61, 310000, 600000, N'dam_lua.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA064', N'Chân váy xếp ly dáng chữ A - Màu Đỏ đô (Size XL)', N'L03', N'CL03', N'XL', N'Đỏ đô', 68, 325000, 625000, N'dam_lua.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA065', N'Đầm dạ hội cổ V sang trọng - Màu Xanh rêu (Size XXL)', N'L03', N'CL03', N'XXL', N'Xanh rêu', 75, 340000, 650000, N'dam_lua.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA066', N'Đầm lụa dáng xòe dự tiệc - Màu Nâu Tây (Size S)', N'L03', N'CL03', N'S', N'Nâu Tây', 82, 280000, 550000, N'dam_lua.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA067', N'Váy hoa nhí Vintage dáng dài - Màu Hồng Pastel (Size M)', N'L03', N'CL03', N'M', N'Hồng Pastel', 89, 295000, 575000, N'dam_lua.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA068', N'Đầm công sở chiết eo thanh lịch - Màu Xanh Dương (Size L)', N'L03', N'CL03', N'L', N'Xanh Dương', 96, 310000, 600000, N'dam_lua.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA069', N'Chân váy xếp ly dáng chữ A - Màu Trắng (Size XL)', N'L03', N'CL03', N'XL', N'Trắng', 23, 325000, 625000, N'dam_lua.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA070', N'Đầm dạ hội cổ V sang trọng - Màu Đen (Size XXL)', N'L03', N'CL03', N'XXL', N'Đen', 30, 340000, 650000, N'dam_lua.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA071', N'Đầm lụa dáng xòe dự tiệc - Màu Xanh Navy (Size S)', N'L03', N'CL03', N'S', N'Xanh Navy', 37, 280000, 550000, N'dam_lua.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA072', N'Váy hoa nhí Vintage dáng dài - Màu Be (Kem) (Size M)', N'L03', N'CL03', N'M', N'Be (Kem)', 44, 295000, 575000, N'dam_lua.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA073', N'Đầm công sở chiết eo thanh lịch - Màu Xám khói (Size L)', N'L03', N'CL03', N'L', N'Xám khói', 51, 310000, 600000, N'dam_lua.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA074', N'Chân váy xếp ly dáng chữ A - Màu Đỏ đô (Size XL)', N'L03', N'CL03', N'XL', N'Đỏ đô', 58, 325000, 625000, N'dam_lua.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA075', N'Đầm dạ hội cổ V sang trọng - Màu Xanh rêu (Size XXL)', N'L03', N'CL03', N'XXL', N'Xanh rêu', 65, 340000, 650000, N'dam_lua.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA076', N'Áo Hoodie nỉ bông Unisex - Màu Be (Kem) (Size S)', N'L04', N'CL05', N'S', N'Be (Kem)', 72, 210000, 390000, N'hoodie_den.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA077', N'Áo khoác Bomber dù 2 lớp - Màu Xám khói (Size M)', N'L04', N'CL05', N'M', N'Xám khói', 79, 225000, 415000, N'hoodie_den.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA078', N'Áo khoác Cardigan len mỏng - Màu Đỏ đô (Size L)', N'L04', N'CL05', N'L', N'Đỏ đô', 86, 240000, 440000, N'hoodie_den.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA079', N'Áo khoác dạ dáng dài Hàn Quốc - Màu Xanh rêu (Size XL)', N'L04', N'CL05', N'XL', N'Xanh rêu', 93, 255000, 465000, N'hoodie_den.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA080', N'Áo Sweater nỉ cổ tròn Basic - Màu Nâu Tây (Size XXL)', N'L04', N'CL05', N'XXL', N'Nâu Tây', 20, 270000, 490000, N'hoodie_den.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA081', N'Áo Hoodie nỉ bông Unisex - Màu Hồng Pastel (Size S)', N'L04', N'CL05', N'S', N'Hồng Pastel', 27, 210000, 390000, N'hoodie_den.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA082', N'Áo khoác Bomber dù 2 lớp - Màu Xanh Dương (Size M)', N'L04', N'CL05', N'M', N'Xanh Dương', 34, 225000, 415000, N'hoodie_den.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA083', N'Áo khoác Cardigan len mỏng - Màu Trắng (Size L)', N'L04', N'CL05', N'L', N'Trắng', 41, 240000, 440000, N'hoodie_den.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA084', N'Áo khoác dạ dáng dài Hàn Quốc - Màu Đen (Size XL)', N'L04', N'CL05', N'XL', N'Đen', 48, 255000, 465000, N'hoodie_den.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA085', N'Áo Sweater nỉ cổ tròn Basic - Màu Xanh Navy (Size XXL)', N'L04', N'CL05', N'XXL', N'Xanh Navy', 55, 270000, 490000, N'hoodie_den.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA086', N'Áo Hoodie nỉ bông Unisex - Màu Be (Kem) (Size S)', N'L04', N'CL05', N'S', N'Be (Kem)', 62, 210000, 390000, N'hoodie_den.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA087', N'Áo khoác Bomber dù 2 lớp - Màu Xám khói (Size M)', N'L04', N'CL05', N'M', N'Xám khói', 69, 225000, 415000, N'hoodie_den.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA088', N'Áo khoác Cardigan len mỏng - Màu Đỏ đô (Size L)', N'L04', N'CL05', N'L', N'Đỏ đô', 76, 240000, 440000, N'hoodie_den.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA089', N'Áo khoác dạ dáng dài Hàn Quốc - Màu Xanh rêu (Size XL)', N'L04', N'CL05', N'XL', N'Xanh rêu', 83, 255000, 465000, N'hoodie_den.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA090', N'Áo Sweater nỉ cổ tròn Basic - Màu Nâu Tây (Size XXL)', N'L04', N'CL05', N'XXL', N'Nâu Tây', 90, 270000, 490000, N'hoodie_den.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA091', N'Áo Hoodie nỉ bông Unisex - Màu Hồng Pastel (Size S)', N'L04', N'CL05', N'S', N'Hồng Pastel', 97, 210000, 390000, N'hoodie_den.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA092', N'Áo khoác Bomber dù 2 lớp - Màu Xanh Dương (Size M)', N'L04', N'CL05', N'M', N'Xanh Dương', 24, 225000, 415000, N'hoodie_den.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA093', N'Áo khoác Cardigan len mỏng - Màu Trắng (Size L)', N'L04', N'CL05', N'L', N'Trắng', 31, 240000, 440000, N'hoodie_den.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA094', N'Áo khoác dạ dáng dài Hàn Quốc - Màu Đen (Size XL)', N'L04', N'CL05', N'XL', N'Đen', 38, 255000, 465000, N'hoodie_den.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA095', N'Áo Sweater nỉ cổ tròn Basic - Màu Xanh Navy (Size XXL)', N'L04', N'CL05', N'XXL', N'Xanh Navy', 45, 270000, 490000, N'hoodie_den.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA096', N'Áo Hoodie nỉ bông Unisex - Màu Be (Kem) (Size S)', N'L04', N'CL05', N'S', N'Be (Kem)', 52, 210000, 390000, N'hoodie_den.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA097', N'Áo khoác Bomber dù 2 lớp - Màu Xám khói (Size M)', N'L04', N'CL05', N'M', N'Xám khói', 59, 225000, 415000, N'hoodie_den.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA098', N'Áo khoác Cardigan len mỏng - Màu Đỏ đô (Size L)', N'L04', N'CL05', N'L', N'Đỏ đô', 66, 240000, 440000, N'hoodie_den.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA099', N'Áo khoác dạ dáng dài Hàn Quốc - Màu Xanh rêu (Size XL)', N'L04', N'CL05', N'XL', N'Xanh rêu', 73, 255000, 465000, N'hoodie_den.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA100', N'Áo Sweater nỉ cổ tròn Basic - Màu Nâu Tây (Size XXL)', N'L04', N'CL05', N'XXL', N'Nâu Tây', 80, 270000, 490000, N'hoodie_den.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA101', N'Bộ đồ thể thao nam thoáng khí - Màu Xám khói (Size S)', N'L05', N'CL07', N'S', N'Xám khói', 87, 150000, 280000, N'thethao.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA102', N'Bộ đồ tập Gym & Yoga nữ co giãn - Màu Đỏ đô (Size M)', N'L05', N'CL07', N'M', N'Đỏ đô', 94, 165000, 305000, N'thethao.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA103', N'Áo khoác gió thể thao chống nước - Màu Xanh rêu (Size L)', N'L05', N'CL07', N'L', N'Xanh rêu', 21, 180000, 330000, N'thethao.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA104', N'Quần Jogger thể thao 3 sọc - Màu Nâu Tây (Size XL)', N'L05', N'CL07', N'XL', N'Nâu Tây', 28, 195000, 355000, N'thethao.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA105', N'Bộ quần áo bóng đá thi đấu - Màu Hồng Pastel (Size XXL)', N'L05', N'CL07', N'XXL', N'Hồng Pastel', 35, 210000, 380000, N'thethao.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA106', N'Bộ đồ thể thao nam thoáng khí - Màu Xanh Dương (Size S)', N'L05', N'CL07', N'S', N'Xanh Dương', 42, 150000, 280000, N'thethao.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA107', N'Bộ đồ tập Gym & Yoga nữ co giãn - Màu Trắng (Size M)', N'L05', N'CL07', N'M', N'Trắng', 49, 165000, 305000, N'thethao.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA108', N'Áo khoác gió thể thao chống nước - Màu Đen (Size L)', N'L05', N'CL07', N'L', N'Đen', 56, 180000, 330000, N'thethao.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA109', N'Quần Jogger thể thao 3 sọc - Màu Xanh Navy (Size XL)', N'L05', N'CL07', N'XL', N'Xanh Navy', 63, 195000, 355000, N'thethao.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA110', N'Bộ quần áo bóng đá thi đấu - Màu Be (Kem) (Size XXL)', N'L05', N'CL07', N'XXL', N'Be (Kem)', 70, 210000, 380000, N'thethao.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA111', N'Bộ đồ thể thao nam thoáng khí - Màu Xám khói (Size S)', N'L05', N'CL07', N'S', N'Xám khói', 77, 150000, 280000, N'thethao.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA112', N'Bộ đồ tập Gym & Yoga nữ co giãn - Màu Đỏ đô (Size M)', N'L05', N'CL07', N'M', N'Đỏ đô', 84, 165000, 305000, N'thethao.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA113', N'Áo khoác gió thể thao chống nước - Màu Xanh rêu (Size L)', N'L05', N'CL07', N'L', N'Xanh rêu', 91, 180000, 330000, N'thethao.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA114', N'Quần Jogger thể thao 3 sọc - Màu Nâu Tây (Size XL)', N'L05', N'CL07', N'XL', N'Nâu Tây', 98, 195000, 355000, N'thethao.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA115', N'Bộ quần áo bóng đá thi đấu - Màu Hồng Pastel (Size XXL)', N'L05', N'CL07', N'XXL', N'Hồng Pastel', 25, 210000, 380000, N'thethao.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA116', N'Bộ đồ thể thao nam thoáng khí - Màu Xanh Dương (Size S)', N'L05', N'CL07', N'S', N'Xanh Dương', 32, 150000, 280000, N'thethao.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA117', N'Bộ đồ tập Gym & Yoga nữ co giãn - Màu Trắng (Size M)', N'L05', N'CL07', N'M', N'Trắng', 39, 165000, 305000, N'thethao.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA118', N'Áo khoác gió thể thao chống nước - Màu Đen (Size L)', N'L05', N'CL07', N'L', N'Đen', 46, 180000, 330000, N'thethao.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA119', N'Quần Jogger thể thao 3 sọc - Màu Xanh Navy (Size XL)', N'L05', N'CL07', N'XL', N'Xanh Navy', 53, 195000, 355000, N'thethao.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA120', N'Bộ quần áo bóng đá thi đấu - Màu Be (Kem) (Size XXL)', N'L05', N'CL07', N'XXL', N'Be (Kem)', 60, 210000, 380000, N'thethao.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA121', N'Bộ đồ thể thao nam thoáng khí - Màu Xám khói (Size S)', N'L05', N'CL07', N'S', N'Xám khói', 67, 150000, 280000, N'thethao.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA122', N'Bộ đồ tập Gym & Yoga nữ co giãn - Màu Đỏ đô (Size M)', N'L05', N'CL07', N'M', N'Đỏ đô', 74, 165000, 305000, N'thethao.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA123', N'Áo khoác gió thể thao chống nước - Màu Xanh rêu (Size L)', N'L05', N'CL07', N'L', N'Xanh rêu', 81, 180000, 330000, N'thethao.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA124', N'Quần Jogger thể thao 3 sọc - Màu Nâu Tây (Size XL)', N'L05', N'CL07', N'XL', N'Nâu Tây', 88, 195000, 355000, N'thethao.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA125', N'Bộ quần áo bóng đá thi đấu - Màu Hồng Pastel (Size XXL)', N'L05', N'CL07', N'XXL', N'Hồng Pastel', 95, 210000, 380000, N'thethao.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA126', N'Áo thun cổ tròn Cotton 2 chiều - Màu Đỏ đô (Size S)', N'L06', N'CL01', N'S', N'Đỏ đô', 22, 110000, 220000, N'aothun.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA127', N'Áo Polo nam thêu logo lịch lãm - Màu Xanh rêu (Size M)', N'L06', N'CL01', N'M', N'Xanh rêu', 29, 125000, 245000, N'aothun.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA128', N'Áo thun Oversize in hình Graphic - Màu Nâu Tây (Size L)', N'L06', N'CL01', N'L', N'Nâu Tây', 36, 140000, 270000, N'aothun.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA129', N'Áo Polo nữ phối cổ trẻ trung - Màu Hồng Pastel (Size XL)', N'L06', N'CL01', N'XL', N'Hồng Pastel', 43, 155000, 295000, N'aothun.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA130', N'Áo thun giữ nhiệt dài tay Thu Đông - Màu Xanh Dương (Size XXL)', N'L06', N'CL01', N'XXL', N'Xanh Dương', 50, 170000, 320000, N'aothun.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA131', N'Áo thun cổ tròn Cotton 2 chiều - Màu Trắng (Size S)', N'L06', N'CL01', N'S', N'Trắng', 57, 110000, 220000, N'aothun.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA132', N'Áo Polo nam thêu logo lịch lãm - Màu Đen (Size M)', N'L06', N'CL01', N'M', N'Đen', 64, 125000, 245000, N'aothun.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA133', N'Áo thun Oversize in hình Graphic - Màu Xanh Navy (Size L)', N'L06', N'CL01', N'L', N'Xanh Navy', 71, 140000, 270000, N'aothun.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA134', N'Áo Polo nữ phối cổ trẻ trung - Màu Be (Kem) (Size XL)', N'L06', N'CL01', N'XL', N'Be (Kem)', 78, 155000, 295000, N'aothun.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA135', N'Áo thun giữ nhiệt dài tay Thu Đông - Màu Xám khói (Size XXL)', N'L06', N'CL01', N'XXL', N'Xám khói', 85, 170000, 320000, N'aothun.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA136', N'Áo thun cổ tròn Cotton 2 chiều - Màu Đỏ đô (Size S)', N'L06', N'CL01', N'S', N'Đỏ đô', 92, 110000, 220000, N'aothun.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA137', N'Áo Polo nam thêu logo lịch lãm - Màu Xanh rêu (Size M)', N'L06', N'CL01', N'M', N'Xanh rêu', 99, 125000, 245000, N'aothun.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA138', N'Áo thun Oversize in hình Graphic - Màu Nâu Tây (Size L)', N'L06', N'CL01', N'L', N'Nâu Tây', 26, 140000, 270000, N'aothun.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA139', N'Áo Polo nữ phối cổ trẻ trung - Màu Hồng Pastel (Size XL)', N'L06', N'CL01', N'XL', N'Hồng Pastel', 33, 155000, 295000, N'aothun.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA140', N'Áo thun giữ nhiệt dài tay Thu Đông - Màu Xanh Dương (Size XXL)', N'L06', N'CL01', N'XXL', N'Xanh Dương', 40, 170000, 320000, N'aothun.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA141', N'Áo thun cổ tròn Cotton 2 chiều - Màu Trắng (Size S)', N'L06', N'CL01', N'S', N'Trắng', 47, 110000, 220000, N'aothun.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA142', N'Áo Polo nam thêu logo lịch lãm - Màu Đen (Size M)', N'L06', N'CL01', N'M', N'Đen', 54, 125000, 245000, N'aothun.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA143', N'Áo thun Oversize in hình Graphic - Màu Xanh Navy (Size L)', N'L06', N'CL01', N'L', N'Xanh Navy', 61, 140000, 270000, N'aothun.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA144', N'Áo Polo nữ phối cổ trẻ trung - Màu Be (Kem) (Size XL)', N'L06', N'CL01', N'XL', N'Be (Kem)', 68, 155000, 295000, N'aothun.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA145', N'Áo thun giữ nhiệt dài tay Thu Đông - Màu Xám khói (Size XXL)', N'L06', N'CL01', N'XXL', N'Xám khói', 75, 170000, 320000, N'aothun.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA146', N'Áo thun cổ tròn Cotton 2 chiều - Màu Đỏ đô (Size S)', N'L06', N'CL01', N'S', N'Đỏ đô', 82, 110000, 220000, N'aothun.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA147', N'Áo Polo nam thêu logo lịch lãm - Màu Xanh rêu (Size M)', N'L06', N'CL01', N'M', N'Xanh rêu', 89, 125000, 245000, N'aothun.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA148', N'Áo thun Oversize in hình Graphic - Màu Nâu Tây (Size L)', N'L06', N'CL01', N'L', N'Nâu Tây', 96, 140000, 270000, N'aothun.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA149', N'Áo Polo nữ phối cổ trẻ trung - Màu Hồng Pastel (Size XL)', N'L06', N'CL01', N'XL', N'Hồng Pastel', 23, 155000, 295000, N'aothun.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA150', N'Áo thun giữ nhiệt dài tay Thu Đông - Màu Xanh Dương (Size XXL)', N'L06', N'CL01', N'XXL', N'Xanh Dương', 30, 170000, 320000, N'aothun.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA151', N'Quần âu nam công sở ống côn - Màu Xanh rêu (Size S)', N'L07', N'CL04', N'S', N'Xanh rêu', 37, 190000, 350000, N'quankaki.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA152', N'Quần Kaki co giãn 4 chiều - Màu Nâu Tây (Size M)', N'L07', N'CL04', N'M', N'Nâu Tây', 44, 205000, 375000, N'quankaki.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA153', N'Quần Baggy vải tuyết mưa nữ - Màu Hồng Pastel (Size L)', N'L07', N'CL04', N'L', N'Hồng Pastel', 51, 220000, 400000, N'quankaki.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA154', N'Quần Culottes ống rộng thanh lịch - Màu Xanh Dương (Size XL)', N'L07', N'CL04', N'XL', N'Xanh Dương', 58, 235000, 425000, N'quankaki.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA155', N'Quần Short Kaki nam dạo phố - Màu Trắng (Size XXL)', N'L07', N'CL04', N'XXL', N'Trắng', 65, 250000, 450000, N'quankaki.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA156', N'Quần âu nam công sở ống côn - Màu Đen (Size S)', N'L07', N'CL04', N'S', N'Đen', 72, 190000, 350000, N'quankaki.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA157', N'Quần Kaki co giãn 4 chiều - Màu Xanh Navy (Size M)', N'L07', N'CL04', N'M', N'Xanh Navy', 79, 205000, 375000, N'quankaki.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA158', N'Quần Baggy vải tuyết mưa nữ - Màu Be (Kem) (Size L)', N'L07', N'CL04', N'L', N'Be (Kem)', 86, 220000, 400000, N'quankaki.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA159', N'Quần Culottes ống rộng thanh lịch - Màu Xám khói (Size XL)', N'L07', N'CL04', N'XL', N'Xám khói', 93, 235000, 425000, N'quankaki.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA160', N'Quần Short Kaki nam dạo phố - Màu Đỏ đô (Size XXL)', N'L07', N'CL04', N'XXL', N'Đỏ đô', 20, 250000, 450000, N'quankaki.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA161', N'Quần âu nam công sở ống côn - Màu Xanh rêu (Size S)', N'L07', N'CL04', N'S', N'Xanh rêu', 27, 190000, 350000, N'quankaki.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA162', N'Quần Kaki co giãn 4 chiều - Màu Nâu Tây (Size M)', N'L07', N'CL04', N'M', N'Nâu Tây', 34, 205000, 375000, N'quankaki.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA163', N'Quần Baggy vải tuyết mưa nữ - Màu Hồng Pastel (Size L)', N'L07', N'CL04', N'L', N'Hồng Pastel', 41, 220000, 400000, N'quankaki.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA164', N'Quần Culottes ống rộng thanh lịch - Màu Xanh Dương (Size XL)', N'L07', N'CL04', N'XL', N'Xanh Dương', 48, 235000, 425000, N'quankaki.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA165', N'Quần Short Kaki nam dạo phố - Màu Trắng (Size XXL)', N'L07', N'CL04', N'XXL', N'Trắng', 55, 250000, 450000, N'quankaki.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA166', N'Quần âu nam công sở ống côn - Màu Đen (Size S)', N'L07', N'CL04', N'S', N'Đen', 62, 190000, 350000, N'quankaki.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA167', N'Quần Kaki co giãn 4 chiều - Màu Xanh Navy (Size M)', N'L07', N'CL04', N'M', N'Xanh Navy', 69, 205000, 375000, N'quankaki.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA168', N'Quần Baggy vải tuyết mưa nữ - Màu Be (Kem) (Size L)', N'L07', N'CL04', N'L', N'Be (Kem)', 76, 220000, 400000, N'quankaki.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA169', N'Quần Culottes ống rộng thanh lịch - Màu Xám khói (Size XL)', N'L07', N'CL04', N'XL', N'Xám khói', 83, 235000, 425000, N'quankaki.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA170', N'Quần Short Kaki nam dạo phố - Màu Đỏ đô (Size XXL)', N'L07', N'CL04', N'XXL', N'Đỏ đô', 90, 250000, 450000, N'quankaki.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA171', N'Quần âu nam công sở ống côn - Màu Xanh rêu (Size S)', N'L07', N'CL04', N'S', N'Xanh rêu', 97, 190000, 350000, N'quankaki.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA172', N'Quần Kaki co giãn 4 chiều - Màu Nâu Tây (Size M)', N'L07', N'CL04', N'M', N'Nâu Tây', 24, 205000, 375000, N'quankaki.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA173', N'Quần Baggy vải tuyết mưa nữ - Màu Hồng Pastel (Size L)', N'L07', N'CL04', N'L', N'Hồng Pastel', 31, 220000, 400000, N'quankaki.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA174', N'Quần Culottes ống rộng thanh lịch - Màu Xanh Dương (Size XL)', N'L07', N'CL04', N'XL', N'Xanh Dương', 38, 235000, 425000, N'quankaki.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA175', N'Quần Short Kaki nam dạo phố - Màu Trắng (Size XXL)', N'L07', N'CL04', N'XXL', N'Trắng', 45, 250000, 450000, N'quankaki.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA176', N'Bộ Pijama lụa dài tay cao cấp - Màu Nâu Tây (Size S)', N'L08', N'CL06', N'S', N'Nâu Tây', 52, 140000, 260000, N'pijama.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA177', N'Bộ đồ mặc nhà vải đũi thoáng mát - Màu Hồng Pastel (Size M)', N'L08', N'CL06', N'M', N'Hồng Pastel', 59, 155000, 285000, N'pijama.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA178', N'Bộ ngủ cộc tay họa tiết dễ thương - Màu Xanh Dương (Size L)', N'L08', N'CL06', N'L', N'Xanh Dương', 66, 170000, 310000, N'pijama.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA179', N'Bộ mặc nhà Cotton thu đông - Màu Trắng (Size XL)', N'L08', N'CL06', N'XL', N'Trắng', 73, 185000, 335000, N'pijama.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA180', N'Váy ngủ lụa tơ tằm mềm mại - Màu Đen (Size XXL)', N'L08', N'CL06', N'XXL', N'Đen', 80, 200000, 360000, N'pijama.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA181', N'Bộ Pijama lụa dài tay cao cấp - Màu Xanh Navy (Size S)', N'L08', N'CL06', N'S', N'Xanh Navy', 87, 140000, 260000, N'pijama.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA182', N'Bộ đồ mặc nhà vải đũi thoáng mát - Màu Be (Kem) (Size M)', N'L08', N'CL06', N'M', N'Be (Kem)', 94, 155000, 285000, N'pijama.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA183', N'Bộ ngủ cộc tay họa tiết dễ thương - Màu Xám khói (Size L)', N'L08', N'CL06', N'L', N'Xám khói', 21, 170000, 310000, N'pijama.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA184', N'Bộ mặc nhà Cotton thu đông - Màu Đỏ đô (Size XL)', N'L08', N'CL06', N'XL', N'Đỏ đô', 28, 185000, 335000, N'pijama.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA185', N'Váy ngủ lụa tơ tằm mềm mại - Màu Xanh rêu (Size XXL)', N'L08', N'CL06', N'XXL', N'Xanh rêu', 35, 200000, 360000, N'pijama.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA186', N'Bộ Pijama lụa dài tay cao cấp - Màu Nâu Tây (Size S)', N'L08', N'CL06', N'S', N'Nâu Tây', 42, 140000, 260000, N'pijama.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA187', N'Bộ đồ mặc nhà vải đũi thoáng mát - Màu Hồng Pastel (Size M)', N'L08', N'CL06', N'M', N'Hồng Pastel', 49, 155000, 285000, N'pijama.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA188', N'Bộ ngủ cộc tay họa tiết dễ thương - Màu Xanh Dương (Size L)', N'L08', N'CL06', N'L', N'Xanh Dương', 56, 170000, 310000, N'pijama.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA189', N'Bộ mặc nhà Cotton thu đông - Màu Trắng (Size XL)', N'L08', N'CL06', N'XL', N'Trắng', 63, 185000, 335000, N'pijama.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA190', N'Váy ngủ lụa tơ tằm mềm mại - Màu Đen (Size XXL)', N'L08', N'CL06', N'XXL', N'Đen', 70, 200000, 360000, N'pijama.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA191', N'Bộ Pijama lụa dài tay cao cấp - Màu Xanh Navy (Size S)', N'L08', N'CL06', N'S', N'Xanh Navy', 77, 140000, 260000, N'pijama.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA192', N'Bộ đồ mặc nhà vải đũi thoáng mát - Màu Be (Kem) (Size M)', N'L08', N'CL06', N'M', N'Be (Kem)', 84, 155000, 285000, N'pijama.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA193', N'Bộ ngủ cộc tay họa tiết dễ thương - Màu Xám khói (Size L)', N'L08', N'CL06', N'L', N'Xám khói', 91, 170000, 310000, N'pijama.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA194', N'Bộ mặc nhà Cotton thu đông - Màu Đỏ đô (Size XL)', N'L08', N'CL06', N'XL', N'Đỏ đô', 98, 185000, 335000, N'pijama.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA195', N'Váy ngủ lụa tơ tằm mềm mại - Màu Xanh rêu (Size XXL)', N'L08', N'CL06', N'XXL', N'Xanh rêu', 25, 200000, 360000, N'pijama.jpg', N'Hàng thiết kế mới về');
INSERT INTO tblQuanAo VALUES (N'QA196', N'Bộ Pijama lụa dài tay cao cấp - Màu Nâu Tây (Size S)', N'L08', N'CL06', N'S', N'Nâu Tây', 32, 140000, 260000, N'pijama.jpg', N'Form chuẩn, vải mềm mịn');
INSERT INTO tblQuanAo VALUES (N'QA197', N'Bộ đồ mặc nhà vải đũi thoáng mát - Màu Hồng Pastel (Size M)', N'L08', N'CL06', N'M', N'Hồng Pastel', 39, 155000, 285000, N'pijama.jpg', N'Mẫu bán chạy nhất tháng');
INSERT INTO tblQuanAo VALUES (N'QA198', N'Bộ ngủ cộc tay họa tiết dễ thương - Màu Xanh Dương (Size L)', N'L08', N'CL06', N'L', N'Xanh Dương', 46, 170000, 310000, N'pijama.jpg', N'Co giãn nhẹ, thấm hút mồ hôi');
INSERT INTO tblQuanAo VALUES (N'QA199', N'Bộ mặc nhà Cotton thu đông - Màu Trắng (Size XL)', N'L08', N'CL06', N'XL', N'Trắng', 53, 185000, 335000, N'pijama.jpg', N'Bền màu, không xù lông');
INSERT INTO tblQuanAo VALUES (N'QA200', N'Váy ngủ lụa tơ tằm mềm mại - Màu Đen (Size XXL)', N'L08', N'CL06', N'XXL', N'Đen', 60, 200000, 360000, N'pijama.jpg', N'Hàng thiết kế mới về');

-- Hóa đơn mẫu
INSERT INTO tblHDBan VALUES (N'HDB_20261022_090000', N'NV01', '2026-10-20 09:30:00', N'KH01', 710000, N'Đã thanh toán');
INSERT INTO tblChiTietHDBan VALUES (N'HDB_20261022_090000', N'QA001', 1, 290000, 0, 290000);
INSERT INTO tblChiTietHDBan VALUES (N'HDB_20261022_090000', N'QA026', 1, 420000, 0, 420000);
GO