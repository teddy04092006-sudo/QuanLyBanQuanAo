-- CHẠY SAU script csdl_quanlybanquanao.sql (chạy lại nhiều lần vẫn an toàn)
USE QuanLyBanQuanAo;
GO
IF COL_LENGTH('tblHDBan','HinhThucThanhToan') IS NULL
    ALTER TABLE tblHDBan ADD HinhThucThanhToan NVARCHAR(30) NOT NULL
        CONSTRAINT DF_HDBan_HinhThuc DEFAULT N'Tiền mặt';
GO
IF COL_LENGTH('tblHDBan','GhiChu') IS NULL
    ALTER TABLE tblHDBan ADD GhiChu NVARCHAR(255) NULL;
GO
-- Hóa đơn mẫu cũ: gán hình thức thanh toán
UPDATE tblHDBan SET HinhThucThanhToan = N'Chuyển khoản' WHERE MaHDBan = N'HDB_20261022_090000';
GO
-- Thêm vài hóa đơn mẫu để báo cáo doanh thu có số liệu
IF NOT EXISTS (SELECT 1 FROM tblHDBan WHERE MaHDBan = N'HDB_20261021_101500000')
BEGIN
    INSERT INTO tblHDBan(MaHDBan,MaNhanVien,NgayBan,MaKhach,TongTien,TrangThai,HinhThucThanhToan)
    VALUES (N'HDB_20261021_101500000', N'NV02', '2026-10-21 10:15:00', N'KH02', 1094000, N'Đã thanh toán', N'Tiền mặt');
    INSERT INTO tblChiTietHDBan VALUES (N'HDB_20261021_101500000', N'QA051', 1, 550000, 0, 550000);
    INSERT INTO tblChiTietHDBan VALUES (N'HDB_20261021_101500000', N'QA076', 1, 390000, 0, 390000);
    INSERT INTO tblChiTietHDBan VALUES (N'HDB_20261021_101500000', N'QA126', 1, 220000, 30, 154000);
END
IF NOT EXISTS (SELECT 1 FROM tblHDBan WHERE MaHDBan = N'HDB_20261023_160000000')
BEGIN
    INSERT INTO tblHDBan(MaHDBan,MaNhanVien,NgayBan,MaKhach,TongTien,TrangThai,HinhThucThanhToan)
    VALUES (N'HDB_20261023_160000000', N'NV01', '2026-10-23 16:00:00', N'KH03', 820000, N'Đã thanh toán', N'Chuyển khoản');
    INSERT INTO tblChiTietHDBan VALUES (N'HDB_20261023_160000000', N'QA101', 2, 280000, 0, 560000);
    INSERT INTO tblChiTietHDBan VALUES (N'HDB_20261023_160000000', N'QA176', 1, 260000, 0, 260000);
END
GO
