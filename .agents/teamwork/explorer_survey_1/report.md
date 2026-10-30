# BÁO CÁO KHẢO SÁT CƠ SỞ DỮ LIỆU, SCRIPT SQL VÀ TẦNG TRUY XUẤT DỮ LIỆU (DAL)
**Dự án**: QuanLyBanQuanAo (WinForms C# / .NET Framework 4.7.2)  
**Người thực hiện**: Explorer 1  
**Ngày thực hiện**: 2026-10-30  
**Đường dẫn thư mục làm việc**: `.agents/teamwork/explorer_survey_1/`

---

## 1. TỔNG QUAN & TÓM TẮT KẾT QUẢ KHẢO SÁT

1. **Cấu trúc bảng & Tên cột**:
   - Cột kích cỡ trong bảng `tblQuanAo` chính xác là **`KichCo`** (kiểu `NVARCHAR(10)`), **KHÔNG PHẢI** `KichSize`.
   - Các cột liên quan trong `tblQuanAo`: `MaQuanAo` (`NVARCHAR(20)`), `TenQuanAo` (`NVARCHAR(150)`), `MaLoai` (`NVARCHAR(20)`), `MaChatLieu` (`NVARCHAR(20)`), `KichCo` (`NVARCHAR(10)`), `MauSac` (`NVARCHAR(50)`), `DonGiaBan` (`FLOAT`), `Anh` (`NVARCHAR(255)`), `SoLuong` (`INT`), `DonGiaNhap` (`FLOAT`), `GhiChu` (`NVARCHAR(255)`).
   - Màu sắc trong CSDL thực tế có giá trị `"Be (Kem)"` đại diện cho màu Be. Bộ ánh xạ màu (palette mapper) cần xử lý trường hợp này (so khớp không phân biệt hoa thường hoặc chuỗi chứa `"be"`).
2. **Thư mục `Database/` và Script SQL**:
   - Hiện tại có 3 script: `1_csdl_quanlybanquanao.sql`, `2_patch_csdl.sql`, `3_patch_chuyenkhoan.sql`.
   - File `Database/UpdateAnh.sql` chưa tồn tại và cần được tạo mới theo yêu cầu R1.
   - Bảng `tblQuanAo` hiện có đủ 200 dòng mẫu (`QA001` đến `QA200`). Cột `Anh` hiện chứa tên file tĩnh cũ (như `somi_trang.jpg`, `quankaki.jpg`, `pijama.jpg`), 0/200 sản phẩm có định dạng `QA%.jpg`.
3. **Tầng truy xuất dữ liệu (DAL)**:
   - DAL chính của dự án là lớp `QuanLyBanQuanAo.DAL.DataProcesser` (nằm tại `DAL/DataProcesser.cs`). Không có file `DatabaseFunction.cs`.
   - Hàm thực thi câu lệnh Non-Query (INSERT, UPDATE, DELETE) là:
     `public bool CapNhatDuLieu(string sql, params SqlParameter[] ps)`
     *(Lưu ý quan trọng: Không có hàm `ThucThiNonQuery` hay `RunSql`, cần dùng đúng `CapNhatDuLieu`).*
   - Hàm truy vấn bảng trả về `DataTable` là:
     `public DataTable DocBang(string sql, params SqlParameter[] ps)`
     *(Không có hàm `GetDataToTable`).*
   - Chuỗi kết nối lấy từ `App.config` theo tên `"QuanLyBanQuanAo"`, mặc định trỏ tới `(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyBanQuanAo;Integrated Security=True`.
4. **Trình biên dịch & Build**:
   - Do có COM Reference (`Microsoft.Office.Interop.Excel`), không thể dùng `dotnet msbuild` (báo lỗi `MSB4803 ResolveComReference`).
   - Biên dịch hoàn toàn thành công với 0 lỗi khi sử dụng MSBuild của Visual Studio:  
     `"C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" QuanLyBanQuanAo.csproj /p:Configuration=Debug /v:m`.

---

## 2. KHẢO SÁT CHI TIẾT THƯ MỤC `Database/` VÀ SCHEMA CƠ SỞ DỮ LIỆU

### 2.1. Danh mục các file trong `Database/`
- `Database/1_csdl_quanlybanquanao.sql`: Script khởi tạo toàn bộ CSDL `QuanLyBanQuanAo`, tạo các bảng chính, tài khoản phân quyền, danh mục và nạp 200 bản ghi sản phẩm từ `QA001` đến `QA200`.
- `Database/2_patch_csdl.sql`: Bản vá bổ sung cột `HinhThucThanhToan` và `GhiChu` cho bảng `tblHDBan`.
- `Database/3_patch_chuyenkhoan.sql`: Bản vá bổ sung cột `AnhChuyenKhoan` cho bảng `tblHDBan`.
- `Database/UpdateAnh.sql`: **Chưa tồn tại** (cần tạo mới ở R1).

### 2.2. Chi tiết cấu trúc các bảng liên quan

#### Bảng `tblQuanAo` (Sản phẩm)
Kiểm tra trực tiếp từ `1_csdl_quanlybanquanao.sql` (dòng 64-78) và qua `INFORMATION_SCHEMA.COLUMNS` trên CSDL `(localdb)\MSSQLLocalDB`:

| Tên Cột | Kiểu Dữ Liệu | Nullable | Khóa / Ràng buộc | Ý nghĩa / Ghi chú |
|---|---|---|---|---|
| `MaQuanAo` | `NVARCHAR(20)` | NOT NULL | PRIMARY KEY | Mã sản phẩm (`QA001` -> `QA200`) |
| `TenQuanAo` | `NVARCHAR(150)` | NOT NULL | | Tên sản phẩm đầy đủ |
| `MaLoai` | `NVARCHAR(20)` | NOT NULL | FK -> `tblLoaiThoiTrang` | Mã loại thời trang (L01 -> L08) |
| `MaChatLieu` | `NVARCHAR(20)` | NOT NULL | FK -> `tblChatLieu` | Mã chất liệu (CL01 -> CL08) |
| **`KichCo`** | `NVARCHAR(10)` | NOT NULL | | **Size sản phẩm (S, M, L, XL, XXL)** |
| `MauSac` | `NVARCHAR(50)` | NOT NULL | | Màu sắc (10 màu sắc chuẩn) |
| `SoLuong` | `INT` | NOT NULL | DEFAULT 0 | Số lượng tồn kho |
| `DonGiaNhap` | `FLOAT` | NOT NULL | DEFAULT 0 | Giá vốn nhập |
| `DonGiaBan` | `FLOAT` | NOT NULL | DEFAULT 0 | Giá bán niêm yết |
| `Anh` | `NVARCHAR(255)` | YES | | Tên file ảnh (đang là ảnh mẫu cũ) |
| `GhiChu` | `NVARCHAR(255)` | YES | | Mô tả / ghi chú sản phẩm |

> **Xác nhận tuyệt đối**: Cột là **`KichCo`**, hoàn toàn **KHÔNG CÓ** cột nào tên là `KichSize`.

#### Bảng `tblLoaiThoiTrang` (Loại thời trang)
Tạo tại dòng 34-37 của `1_csdl_quanlybanquanao.sql`:
- `MaLoai NVARCHAR(20) PRIMARY KEY`
- `TenLoai NVARCHAR(100) NOT NULL`

Dữ liệu thực tế gồm 8 loại:
1. `L01`: Áo sơ mi công sở (Silhouette: Áo)
2. `L02`: Quần Jeans & Denim (Silhouette: Quần)
3. `L03`: Váy đầm thời trang (Silhouette: Váy)
4. `L04`: Áo khoác & Hoodie (Silhouette: Áo)
5. `L05`: Đồ thể thao (Sportswear) (Silhouette: Áo)
6. `L06`: Áo thun & Polo (Silhouette: Áo)
7. `L07`: Quần âu & Kaki (Silhouette: Quần)
8. `L08`: Đồ mặc nhà & Pijama (Silhouette: Áo)

#### Bảng `tblChatLieu` (Chất liệu)
Tạo tại dòng 40-43 của `1_csdl_quanlybanquanao.sql`:
- `MaChatLieu NVARCHAR(20) PRIMARY KEY`
- `TenChatLieu NVARCHAR(100) NOT NULL`

Dữ liệu thực tế gồm 8 chất liệu:
1. `CL01`: Cotton 100%
2. `CL02`: Denim / Jeans
3. `CL03`: Lụa tơ tằm
4. `CL04`: Kaki co giãn
5. `CL05`: Nỉ bông cao cấp
6. `CL06`: Vải đũi (Linen)
7. `CL07`: Thun lạnh thể thao
8. `CL08`: Dạ Tweed & Len

### 2.3. Khảo sát dữ liệu thực tế cột `MauSac` và `KichCo` trong `tblQuanAo`
- **10 giá trị màu sắc thực tế**:
  1. `Trắng`
  2. `Đen`
  3. `Xanh Navy`
  4. `Be (Kem)` *(Lưu ý: trong DB lưu là "Be (Kem)", cần ánh xạ sang mã màu Be)*
  5. `Xám khói`
  6. `Đỏ đô`
  7. `Xanh rêu`
  8. `Nâu Tây`
  9. `Hồng Pastel`
  10. `Xanh Dương`
- **5 giá trị kích cỡ thực tế**: `S`, `M`, `L`, `XL`, `XXL`.
- **Thống kê cột `Anh` hiện tại**:
  - Tổng số sản phẩm: 200 bản ghi (`QA001` -> `QA200`).
  - Số bản ghi có `Anh LIKE 'QA%.jpg'`: **0**.
  - Toàn bộ 200 bản ghi hiện đang trỏ tới các file mẫu: `somi_trang.jpg`, `quankaki.jpg`, `pijama.jpg`,...

---

## 3. KHẢO SÁT TẦNG TRUY XUẤT DỮ LIỆU (DAL)

### 3.1. Phân tích lớp `QuanLyBanQuanAo.DAL.DataProcesser` (`DAL/DataProcesser.cs`)
Lớp `DataProcesser` là lớp DAL duy nhất của dự án chịu trách nhiệm giao tiếp với SQL Server:

```csharp
namespace QuanLyBanQuanAo.DAL
{
    public class DataProcesser
    {
        // 1. Quản lý chuỗi kết nối
        private static string LayChuoiKetNoi()
        {
            ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["QuanLyBanQuanAo"];
            if (cs != null && !string.IsNullOrEmpty(cs.ConnectionString)) return cs.ConnectionString;
            return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuanLyBanQuanAo;Integrated Security=True";
        }
        private readonly string strConnect = LayChuoiKetNoi();

        // 2. Tạo SqlParameter an toàn
        public static SqlParameter P(string ten, object giaTri)
        {
            return new SqlParameter(ten, giaTri ?? DBNull.Value);
        }

        // 3. Thực thi SELECT trả về DataTable
        public DataTable DocBang(string sql, params SqlParameter[] ps);

        // 4. Thực thi INSERT / UPDATE / DELETE
        public bool CapNhatDuLieu(string sql, params SqlParameter[] ps);

        // 5. Thực thi Giao dịch (Transaction)
        public bool ThucThiGiaoDich(Action<GiaoDich> work, out string loi);
    }
}
```

### 3.2. So sánh đối chiếu phương thức DAL

| Tên trong Yêu cầu / Kế hoạch phác thảo | Tên thực tế trong codebase | Trạng thái & Hướng dẫn cho Implementer |
|---|---|---|
| `RunSql(...)` / `ThucThiNonQuery(...)` | `CapNhatDuLieu(string sql, ...)` | **Bắt buộc dùng `dtBase.CapNhatDuLieu(sql)`**; gọi các tên khác sẽ gây lỗi biên dịch CS1061. |
| `GetDataToTable(...)` | `DocBang(string sql, ...)` | **Bắt buộc dùng `dtBase.DocBang(sql)`**; |
| `DatabaseFunction.cs` | `DataProcesser.cs` | File DAL thực tế là `DAL/DataProcesser.cs` |

### 3.3. Sử dụng DAL trong `BLL/Function.cs`
Lớp `QuanLyBanQuanAo.BLL.Function` khởi tạo biến thành viên:
```csharp
private DataProcesser dtBase = new DataProcesser();
```
Do đó, việc thêm phương thức `TaoTuDong200AnhSanPham()` vào `BLL/Function.cs` sẽ gọi trực tiếp:
```csharp
dtBase.CapNhatDuLieu(sqlSync);
DataTable dt = dtBase.DocBang(sqlQuery);
```

---

## 4. ĐÁNH GIÁ YÊU CẦU R1: SCRIPT ĐỒNG BỘ ẢNH (`Database/UpdateAnh.sql`)

### 4.1. Câu lệnh SQL đồng bộ
Câu lệnh SQL theo yêu cầu:
```sql
UPDATE tblQuanAo 
SET Anh = MaQuanAo + N'.jpg' 
WHERE Anh IS NULL OR Anh = '' OR Anh NOT LIKE 'QA%.jpg';
```

### 4.2. Phân tích tính đúng đắn và an toàn
- **Điều kiện WHERE**:
  - `Anh IS NULL OR Anh = ''`: Xử lý các dòng chưa có ảnh hoặc rỗng.
  - `Anh NOT LIKE 'QA%.jpg'`: Xử lý các dòng ảnh mẫu cũ (như `somi_trang.jpg`, `quankaki.jpg`).
- **Phép gán**:
  - `Anh = MaQuanAo + N'.jpg'`: Ghép mã sản phẩm (ví dụ `QA001`) với đuôi `.jpg` thành `QA001.jpg`.
- **Tính Idempotent (chạy lặp lại an toàn)**:
  - Khi đã chạy lần đầu, tất cả 200 dòng đều thỏa `Anh LIKE 'QA%.jpg'`.
  - Nếu chạy lại script lần 2, điều kiện WHERE không khớp dòng nào, không gây cập nhật thừa hay lỗi dữ liệu.
- **Quy chuẩn file script SQL**:
  Nội dung khuyến nghị cho file `Database/UpdateAnh.sql`:
  ```sql
  -- =========================================================================
  -- SCRIPT ĐỒNG BỘ TÊN FILE ẢNH TRONG BẢNG tblQuanAo
  -- =========================================================================
  USE QuanLyBanQuanAo;
  GO

  UPDATE tblQuanAo 
  SET Anh = MaQuanAo + N'.jpg' 
  WHERE Anh IS NULL OR Anh = '' OR Anh NOT LIKE 'QA%.jpg';
  GO
  ```

---

## 5. PHÂN TÍCH CÂU TRUY VẤN DỮ LIỆU ĐỂ TẠO 200 ẢNH (R2)

Để vẽ 200 ảnh sản phẩm trong `TaoTuDong200AnhSanPham()`, cần truy vấn kết hợp 3 bảng `tblQuanAo`, `tblLoaiThoiTrang`, `tblChatLieu`.

Câu lệnh SELECT tối ưu và chuẩn xác về tên cột:
```sql
SELECT qa.MaQuanAo, qa.TenQuanAo, qa.MaLoai, l.TenLoai, cl.TenChatLieu, 
       qa.KichCo, qa.MauSac, qa.DonGiaBan, qa.Anh
FROM tblQuanAo qa
INNER JOIN tblLoaiThoiTrang l ON qa.MaLoai = l.MaLoai
INNER JOIN tblChatLieu cl ON qa.MaChatLieu = cl.MaChatLieu
ORDER BY qa.MaQuanAo ASC;
```

### Lưu ý khi ánh xạ thông tin sản phẩm:
1. **Màu sắc & Độ tương phản**:
   - Palette màu theo `MauSac`:
     - Trắng: `Color.FromArgb(245, 247, 250)`
     - Đen: `Color.FromArgb(30, 30, 36)`
     - Xanh Navy: `Color.FromArgb(30, 58, 138)`
     - Be / Be (Kem): `Color.FromArgb(235, 220, 195)` (Cần bắt cả chuỗi chứa `"be"`)
     - Xám khói: `Color.FromArgb(148, 163, 184)`
     - Đỏ đô: `Color.FromArgb(153, 27, 27)`
     - Xanh rêu: `Color.FromArgb(63, 98, 18)`
     - Nâu Tây: `Color.FromArgb(120, 53, 15)`
     - Hồng Pastel: `Color.FromArgb(244, 180, 200)`
     - Xanh Dương: `Color.FromArgb(37, 99, 235)`
   - Chữ trên áo:
     - Nếu màu áo là Trắng, Be, hoặc Hồng Pastel -> Chữ màu tối `Color.FromArgb(30, 41, 59)`.
     - Ngược lại -> Chữ màu trắng `Color.White`.
2. **Hình khối (Silhouette)**:
   - Nếu `TenLoai` chứa "Áo" hoặc "thể thao" hoặc "Pijama": Silhouette áo kèm cổ và 2 tay.
   - Nếu `TenLoai` chứa "Quần" hoặc "Denim" hoặc "Kaki": Silhouette quần dài 2 ống.
   - Nếu `TenLoai` chứa "Váy" hoặc "đầm": Silhouette váy dáng chữ A xòe.
3. **Thư mục lưu ảnh**:
   - `Application.StartupPath` khi chạy ứng dụng là `bin\Debug`.
   - Đường dẫn đích: `Path.Combine(Application.StartupPath, "Images", "QuanAo")`.
   - Khuyến nghị: Để kiểm thử và lưu trữ bền vững, phương thức tạo ảnh nên đảm bảo lưu vào `bin\Debug\Images\QuanAo`, đồng thời nếu thư mục gốc `Images\QuanAo` của project tồn tại (đường dẫn tương đối `..\..\Images\QuanAo`) thì cũng ghi bản sao hoặc tạo tại đó.

---

## 6. KHẢO SÁT VIỆC LOAD ẢNH AN TOÀN TRÊN CÁC FORM (R3 & R4)

1. **`frmCuaHang.cs`**:
   - Đã có sẵn hàm `LayAnh(string tenFile)` dùng `MemoryStream(File.ReadAllBytes(duongDan))` kết hợp cache.
   - `picCT` cần đảm bảo `SizeMode = PictureBoxSizeMode.Zoom`.
2. **`frmDatHang.cs`**:
   - Dòng 113 hiện tại: `picXemTruoc.Image = Image.FromFile(duongDanAnh);` -> **Đang khóa file trên ổ cứng!**
   - Cần thay bằng hàm nạp qua `MemoryStream` hoặc `File.ReadAllBytes`.
   - `picXemTruoc` cần đảm bảo `SizeMode = PictureBoxSizeMode.Zoom`.
3. **`frmQuanAo.cs`**:
   - Dòng 111-114: Đang dùng `FileStream` trong khối `using` rồi gán `Image.FromStream(fs)`. Khi stream đóng có nguy cơ lỗi GDI+ và khóa tài nguyên nếu không cẩn thận.
   - Cần đồng bộ sang cơ chế đọc `MemoryStream` từ `File.ReadAllBytes`.
   - `picAnh` cần đảm bảo `SizeMode = PictureBoxSizeMode.Zoom`.
4. **Tự động gọi khi Form Load (R4)**:
   - Cần gọi `new Function().TaoTuDong200AnhSanPham();` tại:
     - `frmMain_Load`
     - `frmQuanAo_Load`
     - `frmDatHang_Load`
     - `frmCuaHang_Load`

---

## 7. KẾT LUẬN & ĐỀ XUẤT CHO BƯỚC THI CÔNG

1. Tạo file `Database/UpdateAnh.sql` theo đúng nội dung câu lệnh SQL ở mục 4.
2. Viết phương thức `TaoTuDong200AnhSanPham()` trong `BLL/Function.cs` sử dụng `dtBase.CapNhatDuLieu(...)` và `dtBase.DocBang(...)`.
3. Chú ý xử lý màu `"Be (Kem)"` và tên cột **`KichCo`**.
4. Chuẩn hóa hàm nạp ảnh an toàn (Safe Image Loading) trên cả 3 form (`frmQuanAo`, `frmCuaHang`, `frmDatHang`) để tránh file lock.
5. Biên dịch nghiệm thu dự án bằng công cụ `MSBuild.exe` của Visual Studio thay vì `dotnet msbuild`.
