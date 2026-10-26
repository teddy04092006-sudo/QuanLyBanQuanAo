-- CHẠY SAU 1_csdl_quanlybanquanao.sql VÀ 2_patch_csdl.sql (chạy lại nhiều lần vẫn an toàn)
-- Thêm cột lưu tên file ảnh chuyển khoản mà khách tải lên khi đặt hàng
USE QuanLyBanQuanAo;
GO
IF COL_LENGTH('tblHDBan','AnhChuyenKhoan') IS NULL
    ALTER TABLE tblHDBan ADD AnhChuyenKhoan NVARCHAR(255) NULL;
GO
