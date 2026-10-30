-- Cập nhật đồng bộ tên file ảnh chuẩn QAxxx.jpg cho toàn bộ 200 sản phẩm
USE QuanLyBanQuanAo;
GO

UPDATE tblQuanAo 
SET Anh = MaQuanAo + N'.jpg' 
WHERE Anh IS NULL OR Anh = '' OR Anh NOT LIKE 'QA%.jpg';
UPDATE tblQuanAo SET Anh = MaQuanAo + N'.jpg' WHERE Anh IS NULL OR Anh = '' OR Anh NOT LIKE 'QA%.jpg';
