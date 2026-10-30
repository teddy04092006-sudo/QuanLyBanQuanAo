# BÁO CÁO KHẢO SÁT KIẾN TRÚC BLL VÀ BỘ SINH ẢNH TỰ ĐỘNG (R2)
**Dự án**: QuanLyBanQuanAo (WinForms C# / .NET Framework 4.7.2)  
**Người thực hiện**: Explorer 2  
**Ngày thực hiện**: 2026-10-30  
**Thư mục làm việc**: `d:\lập trình trực quan\QuanLyBanQuanAo_24_10\QuanLyBanQuanAo\.agents\teamwork\explorer_survey_2\`

---

## 1. TÓM TẮT ĐIỀU HÀNH (EXECUTIVE SUMMARY)

1. **Kiến trúc tầng BLL & Lớp `Function`**:
   - `QuanLyBanQuanAo.BLL.Function` (`BLL/Function.cs`) là một **lớp thông thường (instantiated class)**, không phải `static class`. Lớp này sở hữu một trường riêng `private DataProcesser dtBase = new DataProcesser();`.
   - **CẢNH BÁO LỆCH GIAO DIỆN DAL**: Bản kế hoạch ban đầu (`implementation_plan.md`) viết giả mã `dtBase.ThucThiNonQuery(sqlSync);`. Tuy nhiên, trong `DAL/DataProcesser.cs`, hàm thực thi SQL Non-Query là **`CapNhatDuLieu(string sql, params SqlParameter[] ps)`** và hàm truy vấn bảng là **`DocBang(string sql, params SqlParameter[] ps)`**. Nếu gọi `ThucThiNonQuery` sẽ gây lỗi biên dịch ngay lập tức.
   - Cột kích thước trong CSDL là **`KichCo`** (kiểu `NVARCHAR(10)`), **KHÔNG PHẢI** `KichSize`.

2. **Dữ liệu màu sắc thực tế trong CSDL**:
   - Kiểm tra trực tiếp trên CSDL `QuanLyBanQuanAo` (`(localdb)\MSSQLLocalDB`): Cột `MauSac` của 200 sản phẩm chỉ nhận đúng 10 giá trị chuẩn, nhưng màu Be được lưu dưới dạng chuỗi: **`"Be (Kem)"`**.
   - Do đó, bộ ánh xạ màu (palette mapping) bắt buộc phải xử lý chuỗi `"Be (Kem)"` (so khớp không phân biệt hoa thường hoặc kiểm tra `Contains("be")` / `Contains("kem")`), nếu không sẽ bị gán nhầm sang màu xám mặc định.

3. **Cơ chế lưu trữ ảnh và đường dẫn (`Images/QuanAo`)**:
   - Hiện tại thư mục `Images\QuanAo` chỉ chứa 8 file ảnh mẫu cũ (`aothun.jpg`..`thethao.jpg`). Chưa có các file `QA001.jpg` đến `QA200.jpg`.
   - File cấu hình dự án `QuanLyBanQuanAo.csproj` có cấu hình:
     ```xml
     <Content Include="Images\QuanAo\*.jpg">
       <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
     </Content>
     ```
   - Khi chạy ứng dụng, `Application.StartupPath` trỏ tới `bin\Debug`. Để đảm bảo kiểm tra nghiệm thu (Acceptance Criteria) thành công ở cả thư mục gốc của dự án và thư mục `bin\Debug`, bộ sinh ảnh cần tạo ảnh tại `Application.StartupPath\Images\QuanAo` và đồng thời đồng bộ/sao chép sang `<ProjectRoot>\Images\QuanAo` (hoặc ngược lại).

4. **Thư mục Logs và Cơ chế Ghi Log (`Logs/image_gen.log`)**:
   - Hiện tại dự án **chưa có thư mục `Logs\`**.
   - Bộ sinh ảnh cần tự động tạo thư mục `Logs\` nếu chưa tồn tại và ghi log vào `image_gen.log` với dấu thời gian và số lượng ảnh đã tạo thành công.

5. **Giải pháp Đồ họa System.Drawing & Chống Khóa File**:
   - Dự án tham chiếu trực tiếp `System.Drawing.dll` (hỗ trợ đầy đủ `System.Drawing.Drawing2D`, `System.Drawing.Imaging`, `System.Drawing.Text`).
   - Không sử dụng cú pháp C# 8.0 switch expressions (`switch { ... }`) vì dự án đang biên dịch ở chế độ mặc định C# 7.3 của .NET Framework 4.7.2. Cần dùng cấu trúc `switch-case` cổ điển hoặc chuỗi `if-else` an toàn 100%.
   - Để tránh khóa file trên đĩa, phương thức xuất ảnh phải lưu thông qua bộ nhớ đệm `MemoryStream` rồi gọi `File.WriteAllBytes()`.
   - Cung cấp một phương thức dùng chung `Function.LayAnhKhongKhoaFile(string duongDan)` sử dụng `new Bitmap(img)` bên trong khối `using` để triệt tiêu vĩnh viễn lỗi file lock và giải phóng handle file an toàn.

---

## 2. KHẢO SÁT CHI TIẾT TẦNG BLL (`BLL/Function.cs`)

### 2.1. Cấu trúc hiện tại của `BLL/Function.cs`
- **Namespace**: `QuanLyBanQuanAo.BLL`
- **Định nghĩa lớp**: `public class Function` (dòng 10, file `BLL/Function.cs`)
- **Thuộc tính & Hằng số static**:
  - `TenShop`, `DiaChiShop`, `DienThoaiShop` (chuỗi hằng)
  - `MaNganHangQR`, `TenNganHang`, `SoTaiKhoan`, `TenChuTaiKhoan`, `ThongTinChuyenKhoan`, `MaBinNganHang`
  - Các hàm tiện ích static: `Tlv()`, `Crc16()`, `TaoChuoiVietQR(double soTien, string noiDung)`
- **Thực thể dữ liệu**:
  - `private DataProcesser dtBase = new DataProcesser();` (dòng 57)
- **Phương thức instance hiện có**:
  - `FillCombo(string sql, ComboBox cbo, string ma, string ten)` (dòng 60-67)
  - `GetFieldValues(string sql)` (dòng 70-79)
  - `CheckKey(string sql)` (dòng 82-86)
  - `CreateKey(string tiento)` (dòng 89-93)
  - `ChiNhapSoNguyen(object sender, KeyPressEventArgs e)` (dòng 98-104)
  - `KiemTraSoDienThoai(string sdt)` (dòng 107-110)
  - `DocSoThanhChu(long so)` (dòng 141-161)

### 2.2. Cách các Form GUI sử dụng `Function`
- Hầu hết các form GUI khởi tạo một đối tượng instance của `Function`:
  - `GUI/frmQuanAo.cs`: `Function func = new Function();` (dòng 15)
  - `GUI/frmDatHang.cs`: `Function func = new Function();` (dòng 19)
  - `GUI/frmKhachHang.cs`: `Function func = new Function();` (dòng 12)
  - `GUI/frmNhanVien.cs`: `Function func = new Function();` (dòng 12)
  - `GUI/frmDanhMuc.cs`: `private readonly Function func = new Function();` (dòng 34)
  - `GUI/frmHoaDonBan.cs`: `private readonly Function func = new Function();` (dòng 14)
  - `GUI/frmQuanLyDonHang.cs`: `private readonly Function func = new Function();` (dòng 20)
- Vì vậy, phương thức `TaoTuDong200AnhSanPham()` nên là một **instance method** (hoặc cung cấp cả overload / static wrapper) để có thể được gọi linh hoạt:
  `func.TaoTuDong200AnhSanPham();` hoặc `new Function().TaoTuDong200AnhSanPham();` hoặc `Function.TaoTuDong200AnhSanPham();`.

### 2.3. Cảnh báo lỗi đối với hàm của `DAL/DataProcesser`
Trong `implementation_plan.md` có đoạn mã đề xuất:
```csharp
// ĐOẠN MÃ LỖI TRONG PLAN:
dtBase.ThucThiNonQuery(sqlSync);
DataTable dt = dtBase.DocBang(sql);
```
**Đối chiếu thực tế trong `DAL/DataProcesser.cs`**:
- Lớp `DataProcesser` **KHÔNG CÓ** phương thức nào tên là `ThucThiNonQuery`.
- Phương thức thực thi câu lệnh SQL cập nhật dữ liệu là:
  ```csharp
  public bool CapNhatDuLieu(string sql, params SqlParameter[] ps) // Dòng 82, DAL/DataProcesser.cs
  ```
- Phương thức truy vấn dữ liệu là:
  ```csharp
  public DataTable DocBang(string sql, params SqlParameter[] ps) // Dòng 62, DAL/DataProcesser.cs
  ```
- **Kết luận bắt buộc**: Phải sử dụng `dtBase.CapNhatDuLieu(sqlSync);` khi chạy lệnh đồng bộ SQL.

---

## 3. KHẢO SÁT THƯ MỤC `Images/QuanAo` VÀ `Logs/`

### 3.1. Thư mục `Images/QuanAo`
- Đường dẫn trong mã nguồn: `d:\lập trình trực quan\QuanLyBanQuanAo_24_10\QuanLyBanQuanAo\Images\QuanAo\`
- Đường dẫn khi chạy Debug: `d:\lập trình trực quan\QuanLyBanQuanAo_24_10\QuanLyBanQuanAo\bin\Debug\Images\QuanAo\`
- Danh sách 8 file ảnh mẫu hiện có:
  1. `aothun.jpg` (19,356 bytes)
  2. `dam_lua.jpg` (6,347 bytes)
  3. `hoodie_den.jpg` (5,736 bytes)
  4. `jeans_nam.jpg` (16,679 bytes)
  5. `pijama.jpg` (7,737 bytes)
  6. `quankaki.jpg` (7,391 bytes)
  7. `somi_trang.jpg` (8,266 bytes)
  8. `thethao.jpg` (14,161 bytes)
- Cột `Anh` trong `tblQuanAo` hiện tại trỏ tới 8 file trên cho tất cả 200 sản phẩm.
- Chưa có bất kỳ file nào từ `QA001.jpg` đến `QA200.jpg`.

### 3.2. Quản lý đường dẫn đa môi trường (StartupPath vs Project Root)
Khi chạy từ Visual Studio hoặc `bin\Debug\QuanLyBanQuanAo.exe`:
- `Application.StartupPath` = `...\QuanLyBanQuanAo\bin\Debug`
- Thư mục ảnh chạy thực tế: `Path.Combine(Application.StartupPath, "Images", "QuanAo")`
- Thư mục ảnh trong source code: `Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Images\QuanAo"))`
- **Chiến lược an toàn**:
  Trong `TaoTuDong200AnhSanPham()`, xác định thư mục chính là `Path.Combine(Application.StartupPath, "Images", "QuanAo")`. Nếu thư mục mã nguồn `..\..\Images\QuanAo` cũng tồn tại (khi chạy trong môi trường phát triển), lưu hoặc sao chép ảnh vào cả hai thư mục. Điều này đảm bảo:
  1. Ứng dụng đang chạy hiển thị ảnh ngay lập tức.
  2. Quá trình kiểm tra tệp tin trên mã nguồn hoặc lệnh git/msbuild đều thấy đủ 200 tệp `QA001.jpg` đến `QA200.jpg`.

### 3.3. Thư mục `Logs/`
- Hiện tại chưa tồn tại thư mục `Logs` trong dự án.
- Tương tự như ảnh, vị trí ghi log:
  1. Thư mục `Logs` tại `Application.StartupPath`: `Path.Combine(Application.StartupPath, "Logs")`
  2. Thư mục `Logs` tại thư mục gốc dự án: `Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Logs"))` (nếu tồn tại hoặc tạo mới).
  File đích: `image_gen.log`.

---

## 4. PHÂN TÍCH VÀ THIẾT KẾ CHI TIẾT BỘ SINH ẢNH (`TaoTuDong200AnhSanPham`)

### 4.1. Quy trình xử lý (Flow of Execution)
1. Kiểm tra và đảm bảo thư mục đích `Images\QuanAo` tồn tại.
2. Thực thi câu lệnh SQL đồng bộ cột `Anh` trong CSDL:
   ```sql
   UPDATE tblQuanAo SET Anh = MaQuanAo + N'.jpg' WHERE Anh IS NULL OR Anh = '' OR Anh NOT LIKE 'QA%.jpg'
   ```
3. Truy vấn toàn bộ 200 sản phẩm với thông tin loại và chất liệu:
   ```sql
   SELECT qa.MaQuanAo, qa.TenQuanAo, qa.MaLoai, l.TenLoai, cl.TenChatLieu, 
          qa.KichCo, qa.MauSac, qa.DonGiaBan, qa.Anh
   FROM tblQuanAo qa
   INNER JOIN tblLoaiThoiTrang l ON qa.MaLoai = l.MaLoai
   INNER JOIN tblChatLieu cl ON qa.MaChatLieu = cl.MaChatLieu
   ORDER BY qa.MaQuanAo
   ```
4. Duyệt qua từng sản phẩm:
   - Kiểm tra xem file `<MaQuanAo>.jpg` đã tồn tại trong thư mục ảnh chưa.
   - Nếu chưa tồn tại: tiến hành vẽ thẻ đồ họa 500x600 px bằng GDI+ và lưu file.
5. Ghi log kết quả hoàn thành vào file `Logs/image_gen.log`.

---

### 4.2. Bảng ánh xạ màu sắc chuẩn (10-Color Palette Mapping)
Theo yêu cầu R2 và dữ liệu thực tế tại CSDL:

| STT | Tên màu (`MauSac`) | Tên trong CSDL | Mã RGB | Mã Hex | Nhóm độ tương phản |
|---|---|---|---|---|---|
| 1 | Trắng | `Trắng` | RGB(245, 247, 250) | `#F5F7FA` | Sáng (Chữ đen `#1E293B`) |
| 2 | Đen | `Đen` | RGB(30, 30, 36) | `#1E1E24` | Tối (Chữ trắng `#FFFFFF`) |
| 3 | Xanh Navy | `Xanh Navy` | RGB(30, 58, 138) | `#1E3A8A` | Tối (Chữ trắng `#FFFFFF`) |
| 4 | Be | **`Be (Kem)`** | RGB(235, 220, 195) | `#EBDCC3` | Sáng (Chữ đen `#1E293B`) |
| 5 | Xám khói | `Xám khói` | RGB(148, 163, 184) | `#94A3B8` | Tối (Chữ trắng `#FFFFFF`) |
| 6 | Đỏ đô | `Đỏ đô` | RGB(153, 27, 27) | `#991B1B` | Tối (Chữ trắng `#FFFFFF`) |
| 7 | Xanh rêu | `Xanh rêu` | RGB(63, 98, 18) | `#3F6212` | Tối (Chữ trắng `#FFFFFF`) |
| 8 | Nâu Tây | `Nâu Tây` | RGB(120, 53, 15) | `#78350F` | Tối (Chữ trắng `#FFFFFF`) |
| 9 | Hồng Pastel | `Hồng Pastel` | RGB(244, 180, 200) | `#F4B4C8` | Sáng (Chữ đen `#1E293B`) |
| 10 | Xanh Dương | `Xanh Dương` | RGB(37, 99, 235) | `#2563EB` | Tối (Chữ trắng `#FFFFFF`) |

#### Thuật toán ánh xạ màu an toàn (tương thích C# 7.3):
```csharp
public static Color LayMauTheoMauSac(string mauSac)
{
    if (string.IsNullOrWhiteSpace(mauSac)) return Color.FromArgb(148, 163, 184);
    string m = mauSac.Trim().ToLowerInvariant();

    if (m.Contains("trắng") || m.Contains("trang"))
        return Color.FromArgb(245, 247, 250);
    if (m.Contains("đen") || m.Contains("den"))
        return Color.FromArgb(30, 30, 36);
    if (m.Contains("navy"))
        return Color.FromArgb(30, 58, 138);
    if (m.Contains("be") || m.Contains("kem"))
        return Color.FromArgb(235, 220, 195);
    if (m.Contains("xám") || m.Contains("xam"))
        return Color.FromArgb(148, 163, 184);
    if (m.Contains("đỏ") || m.Contains("do"))
        return Color.FromArgb(153, 27, 27);
    if (m.Contains("rêu") || m.Contains("reu"))
        return Color.FromArgb(63, 98, 18);
    if (m.Contains("nâu") || m.Contains("nau"))
        return Color.FromArgb(120, 53, 15);
    if (m.Contains("hồng") || m.Contains("hong"))
        return Color.FromArgb(244, 180, 200);
    if (m.Contains("dương") || m.Contains("duong") || m.Contains("xanh"))
        return Color.FromArgb(37, 99, 235);

    return Color.FromArgb(148, 163, 184); // Mặc định xám khói nếu không khớp
}
```

---

### 4.3. Thuật toán tương phản chữ trên áo (Text Contrast)
Yêu cầu R2:
- Chữ tối màu (Navy Đậm `RGB(30, 41, 59)` / `#1E293B`) nếu màu áo là `Trắng`, `Be` (hoặc `Be (Kem)`), hoặc `Hồng Pastel`.
- Chữ sáng màu (`Color.White` / `#FFFFFF`) cho tất cả các màu sắc còn lại.

```csharp
public static Color LayMauChuTrenAo(string mauSac)
{
    if (string.IsNullOrWhiteSpace(mauSac)) return Color.White;
    string m = mauSac.Trim().ToLowerInvariant();
    if (m.Contains("trắng") || m.Contains("trang") ||
        m.Contains("be") || m.Contains("kem") ||
        m.Contains("hồng") || m.Contains("hong"))
    {
        return Color.FromArgb(30, 41, 59); // Màu Navy đậm #1E293B
    }
    return Color.White;
}
```

---

### 4.4. Thiết kế hình bóng trang phục (Vector Silhouettes)
Trong 8 loại thời trang (`L01` đến `L08`), phân loại theo 3 nhóm hình dáng:
1. **Quần** (`TenLoai.Contains("Quần")`):
   - Bao gồm: `L02` (Quần Jeans & Denim), `L07` (Quần âu & Kaki).
   - Đặc điểm: Cạp quần ngang trên, chia 2 ống quần đối xứng xuống dưới, có đường chỉ đũng quần.
2. **Váy / Đầm** (`TenLoai.Contains("Váy")` hoặc `TenLoai.Contains("Đầm")`):
   - Bao gồm: `L03` (Váy đầm thời trang).
   - Đặc điểm: Cổ áo khoét sâu, thân trên thon gọn chiết eo, tùng váy xòe chữ A dáng lượn sóng tự nhiên ở gấu váy.
3. **Áo** (Các loại còn lại):
   - Bao gồm: `L01` (Áo sơ mi), `L04` (Áo khoác & Hoodie), `L05` (Đồ thể thao), `L06` (Áo thun & Polo), `L08` (Đồ mặc nhà & Pijama).
   - Đặc điểm: Cổ áo (collar / neckline), 2 tay áo vát chéo sang hai bên, thân áo hình hộp chữ nhật có bo gấu nhẹ.

#### Tọa độ vector mẫu cho khung hình Body (Y: 75 -> 425):
```csharp
private static void VeHinhBong(Graphics g, string tenLoai, Color mauTrangPhuc)
{
    using (SolidBrush brush = new SolidBrush(mauTrangPhuc))
    using (Pen penVien = new Pen(Color.FromArgb(120, 30, 41, 59), 2f)) // Viền tinh tế
    {
        if (tenLoai.IndexOf("Quần", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            // Vector QUẦN
            PointF[] ptsQuan = new PointF[]
            {
                new PointF(150, 85),  // Cạp trái
                new PointF(350, 85),  // Cạp phải
                new PointF(355, 140), // Hông phải
                new PointF(335, 415), // Gấu ống phải ngoài
                new PointF(265, 415), // Gấu ống phải trong
                new PointF(250, 240), // Đũng quần
                new PointF(235, 415), // Gấu ống trái trong
                new PointF(165, 415), // Gấu ống trái ngoài
                new PointF(145, 140), // Hông trái
            };
            g.FillPolygon(brush, ptsQuan);
            g.DrawPolygon(penVien, ptsQuan);
            // Đường may cạp và đũng
            g.DrawLine(penVien, 150, 115, 350, 115);
            g.DrawLine(penVien, 250, 115, 250, 240);
        }
        else if (tenLoai.IndexOf("Váy", StringComparison.OrdinalIgnoreCase) >= 0 ||
                 tenLoai.IndexOf("Đầm", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            // Vector VÁY CHỮ A
            PointF[] ptsVay = new PointF[]
            {
                new PointF(190, 85),  // Vai trái
                new PointF(225, 105), // Khoét cổ trái
                new PointF(275, 105), // Khoét cổ phải
                new PointF(310, 85),  // Vai phải
                new PointF(315, 140), // Nách phải
                new PointF(295, 205), // Eo phải
                new PointF(395, 415), // Gấu váy phải (xòe chữ A)
                new PointF(250, 425), // Đáy gấu cong giữa
                new PointF(105, 415), // Gấu váy trái (xòe chữ A)
                new PointF(205, 205), // Eo trái
                new PointF(185, 140), // Nách trái
            };
            g.FillPolygon(brush, ptsVay);
            g.DrawPolygon(penVien, ptsVay);
            // Đường thắt lưng eo
            g.DrawLine(penVien, 205, 205, 295, 205);
        }
        else
        {
            // Vector ÁO (Áo thun, sơ mi, khoác, thể thao...)
            PointF[] ptsAo = new PointF[]
            {
                new PointF(190, 85),  // Cổ trái
                new PointF(250, 105), // Đáy khoét cổ
                new PointF(310, 85),  // Cổ phải
                new PointF(415, 155), // Đầu tay phải
                new PointF(380, 205), // Gấu tay phải
                new PointF(335, 185), // Nách phải
                new PointF(335, 415), // Gấu áo phải
                new PointF(165, 415), // Gấu áo trái
                new PointF(165, 185), // Nách trái
                new PointF(120, 205), // Gấu tay trái
                new PointF(85, 155),  // Đầu tay trái
            };
            g.FillPolygon(brush, ptsAo);
            g.DrawPolygon(penVien, ptsAo);
            // Cổ áo V-neck hoặc đường cổ
            g.DrawLine(penVien, 190, 85, 250, 105);
            g.DrawLine(penVien, 250, 105, 310, 85);
        }
    }
}
```

---

### 4.5. Thiết kế bố cục thẻ sản phẩm (Card Layout)
Kích thước tổng thể: **500 px (rộng) x 600 px (cao)**.

1. **Phần Đầu (Header - Y: 0 -> 60)**:
   - Màu nền: `#1E293B` (RGB: 30, 41, 59 - Xanh Navy đậm hiện đại).
   - Nội dung hiển thị: `$"#{maQuanAo} {tenLoai.ToUpper()}"` (ví dụ: `#QA001 ÁO SƠ MI CÔNG SỞ`).
   - Font chữ: Arial 16pt Bold, màu trắng `#FFFFFF`, căn giữa tuyệt đối theo chiều ngang và dọc.

2. **Phần Thân (Body - Y: 60 -> 435)**:
   - Màu nền card: `#F8FAFC` (RGB: 248, 250, 252 - Slate 50 siêu sáng sạch sẽ).
   - Vẽ hình bóng trang phục (`VeHinhBong`) với màu sắc lấy từ `LayMauTheoMauSac(mauSac)`.
   - Chữ thông số in trên ngực/thân trang phục:
     - Dòng 1: `$"SIZE {kichCo}"` (ví dụ: `SIZE XL`)
     - Dòng 2: `$"Màu: {mauSac}"` (ví dụ: `Màu: Be (Kem)`)
     - Font: Arial 18pt Bold, căn giữa, màu chữ theo `LayMauChuTrenAo(mauSac)`.
     - Vị trí: Đặt tại tâm vùng ngực/bụng trang phục (Y = 240 đến 300).

3. **Phần Chân Thẻ (Footer - Y: 435 -> 600)**:
   - Màu nền: Trắng tinh `#FFFFFF` với 1 đường kẻ viền phân cách phía trên (Pen `#E2E8F0`, Y = 435).
   - Nội dung 3 dòng thông tin:
     - Dòng 1 (Tên sản phẩm): `tenQuanAo`, font Arial 12pt Bold, màu tối `#0F172A`. (Dùng `StringFormat` bọc chữ để tránh tràn nếu tên dài).
     - Dòng 2 (Chất liệu): `$"Chất liệu: {tenChatLieu}"`, font Arial 11pt Regular, màu xám thanh lịch `#475569`.
     - Dòng 3 (Đơn giá bán): `string.Format("{0:N0} VNĐ", donGiaBan)`, font Arial 16pt Bold, màu hồng đỏ nổi bật **`#E11D48`** (RGB: 225, 29, 72).

---

### 4.6. Xuất ảnh JPEG Không Khóa File & Ghi Log
- **Không khóa file**:
  ```csharp
  using (MemoryStream ms = new MemoryStream())
  {
      bmp.Save(ms, ImageFormat.Jpeg);
      File.WriteAllBytes(fullPath, ms.ToArray());
  }
  ```
- **Ghi log hoàn thành**:
  ```csharp
  string logDir = Path.Combine(Application.StartupPath, "Logs");
  if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);
  string logFile = Path.Combine(logDir, "image_gen.log");
  File.AppendAllText(logFile, string.Format("[{0:yyyy-MM-dd HH:mm:ss}] Generated {1} product images successfully.\r\n", DateTime.Now, count));
  ```
  *(Đồng thời ghi vào thư mục gốc `..\..\Logs\image_gen.log` nếu phát hiện thư mục mã nguồn).*

---

### 4.7. Hàm Nạp Ảnh An Toàn Dùng Chung Cho GUI (`LayAnhKhongKhoaFile`)
Để hỗ trợ yêu cầu R3, lớp `Function` nên cung cấp phương thức static:
```csharp
/// <summary>
/// Nạp ảnh từ đường dẫn tệp tin mà không giữ khóa file trên ổ cứng.
/// </summary>
public static Image LayAnhKhongKhoaFile(string duongDan)
{
    if (string.IsNullOrEmpty(duongDan) || !File.Exists(duongDan)) return null;
    try
    {
        using (FileStream fs = new FileStream(duongDan, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (Image img = Image.FromStream(fs))
        {
            return new Bitmap(img); // Clone độc lập vào bộ nhớ RAM
        }
    }
    catch
    {
        return null;
    }
}
```

---

## 5. KHẢO SÁT CẤU HÌNH BIÊN DỊCH & THAM CHIẾU

### 5.1. Tham chiếu System.Drawing
- File `QuanLyBanQuanAo.csproj` đã khai báo tại dòng 49:
  ```xml
  <Reference Include="System.Drawing" />
  ```
- Không cần bổ sung tham chiếu riêng cho `System.Drawing.Drawing2D`, vì namespace này đã nằm sẵn trong `System.Drawing.dll`.
- Trong file `BLL/Function.cs`, chỉ cần bổ sung các lệnh `using`:
  ```csharp
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;
  using System.Drawing.Text;
  using System.IO;
  ```

### 5.2. Công cụ Biên dịch MSBuild
- Lệnh biên dịch chuẩn đã được thử nghiệm và vượt qua với **0 Cảnh báo, 0 Lỗi**:
  ```powershell
  & "C:\Program Files\Microsoft SQL Server Management Studio 22\Release\MSBuild\Current\Bin\amd64\MSBuild.exe" "d:\lập trình trực quan\QuanLyBanQuanAo_24_10\QuanLyBanQuanAo\QuanLyBanQuanAo.csproj" /t:Build /p:Configuration=Debug
  ```
- Hoặc sử dụng MSBuild của Visual Studio 18:
  ```powershell
  & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\lập trình trực quan\QuanLyBanQuanAo_24_10\QuanLyBanQuanAo\QuanLyBanQuanAo.csproj" /p:Configuration=Debug
  ```

---

## 6. TỔNG HỢP & PHỐI HỢP VỚI CÁC KHẢO SÁT KHÁC (SYNTHESIS)

| Nội dung | Kết quả Explorer 1 (DB/DAL) | Kết quả Explorer 2 (BLL/Image Gen) | Kết quả Explorer 3 (UI/Forms) | Thống nhất (Consensus) |
|---|---|---|---|---|
| **Cột Size trong CSDL** | `KichCo` (dòng 69 `csdl.sql`) | `KichCo` | `KichCo` (dòng 102 `frmDatHang.cs`) | Dùng duy nhất `qa.KichCo`, không dùng `KichSize`. |
| **Giá trị màu Be trong DB** | `Be (Kem)` | `Be (Kem)` (10 distinct rows) | Không khảo sát màu DB | Bộ ánh xạ màu bắt buộc so khớp chuỗi `"be"` hoặc `"kem"`. |
| **Tên hàm thực thi SQL** | `CapNhatDuLieu` & `DocBang` | `CapNhatDuLieu` & `DocBang` | `CapNhatDuLieu` & `DocBang` | Sửa giả mã trong plan: dùng `CapNhatDuLieu`, không gọi `ThucThiNonQuery`. |
| **Cơ chế chống khóa file** | Không khảo sát UI | Đề xuất `new Bitmap(img)` qua MemoryStream/FileStream | `LayAnh` dùng `MemoryStream(File.ReadAllBytes)` | Thống nhất cung cấp `Function.LayAnhKhongKhoaFile` cho toàn bộ các Form. |
| **PictureBox SizeMode** | Không khảo sát UI | Phân tích 500x600 tỷ lệ 5:6 | `picAnh` và `picXemTruoc` đang là `StretchImage`, cần chuyển `Zoom` | Cập nhật `SizeMode = PictureBoxSizeMode.Zoom` ở `frmQuanAo` và `frmDatHang`. |
| **Công cụ Build** | MSBuild của VS 18 | MSBuild SSMS 22 / VS 18 | MSBuild 18 | Sử dụng đường dẫn MSBuild đầy đủ đã kiểm chứng. |

---

## 7. ĐỀ XUẤT MÃ NGUỒN HOÀN CHỈNH CHO `TaoTuDong200AnhSanPham`

Đoạn mã sau sẵn sàng để Implementer đưa vào `BLL/Function.cs`:

```csharp
/// <summary>
/// Tự động đồng bộ CSDL và tạo 200 ảnh sản phẩm chất lượng cao chuẩn Vector Silhouette.
/// Không gây khóa file và lưu log vào Logs/image_gen.log.
/// </summary>
public void TaoTuDong200AnhSanPham()
{
    try
    {
        // 1. Đảm bảo thư mục Images\QuanAo tồn tại
        string imgDir = Path.Combine(Application.StartupPath, "Images", "QuanAo");
        if (!Directory.Exists(imgDir)) Directory.CreateDirectory(imgDir);

        // Đồng bộ thư mục nguồn (nếu đang chạy môi trường lập trình)
        string srcImgDir = "";
        try
        {
            string candidate = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Images\QuanAo"));
            if (Directory.Exists(Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\.."))))
            {
                srcImgDir = candidate;
                if (!Directory.Exists(srcImgDir)) Directory.CreateDirectory(srcImgDir);
            }
        }
        catch { }

        // 2. Chạy câu lệnh đồng bộ tên ảnh trong CSDL
        string sqlSync = "UPDATE tblQuanAo SET Anh = MaQuanAo + N'.jpg' WHERE Anh IS NULL OR Anh = '' OR Anh NOT LIKE 'QA%.jpg'";
        dtBase.CapNhatDuLieu(sqlSync);

        // 3. Lấy thông tin 200 sản phẩm
        string sql = @"SELECT qa.MaQuanAo, qa.TenQuanAo, qa.MaLoai, l.TenLoai, cl.TenChatLieu, 
                              qa.KichCo, qa.MauSac, qa.DonGiaBan, qa.Anh
                       FROM tblQuanAo qa
                       INNER JOIN tblLoaiThoiTrang l ON qa.MaLoai = l.MaLoai
                       INNER JOIN tblChatLieu cl ON qa.MaChatLieu = cl.MaChatLieu
                       ORDER BY qa.MaQuanAo";
        DataTable dt = dtBase.DocBang(sql);
        if (dt == null || dt.Rows.Count == 0) return;

        int taoMoi = 0;

        // 4. Vẽ ảnh cho từng sản phẩm nếu chưa có
        foreach (DataRow row in dt.Rows)
        {
            string ma = Convert.ToString(row["MaQuanAo"]).Trim();
            if (string.IsNullOrEmpty(ma)) continue;

            string fileName = ma + ".jpg";
            string fullPath = Path.Combine(imgDir, fileName);

            // Nếu đã tồn tại ở cả hai nơi thì bỏ qua
            bool coOStartup = File.Exists(fullPath);
            bool coOSrc = !string.IsNullOrEmpty(srcImgDir) && File.Exists(Path.Combine(srcImgDir, fileName));

            if (coOStartup && (string.IsNullOrEmpty(srcImgDir) || coOSrc))
            {
                continue;
            }

            string tenQA = Convert.ToString(row["TenQuanAo"]);
            string tenLoai = Convert.ToString(row["TenLoai"]);
            string chatLieu = Convert.ToString(row["TenChatLieu"]);
            string kichCo = Convert.ToString(row["KichCo"]);
            string mauSac = Convert.ToString(row["MauSac"]);
            double giaBan = 0;
            if (row["DonGiaBan"] != DBNull.Value) double.TryParse(row["DonGiaBan"].ToString(), out giaBan);

            Color mauTrangPhuc = LayMauTheoMauSac(mauSac);
            Color mauChuAo = LayMauChuTrenAo(mauSac);

            using (Bitmap bmp = new Bitmap(500, 600))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    // 4.1. Nền tổng thể
                    g.Clear(Color.FromArgb(248, 250, 252)); // Slate-50

                    // 4.2. Header Navy #1E293B
                    Rectangle hdrRect = new Rectangle(0, 0, 500, 60);
                    using (SolidBrush bHdr = new SolidBrush(Color.FromArgb(30, 41, 59)))
                    {
                        g.FillRectangle(bHdr, hdrRect);
                    }
                    using (Font fHdr = new Font("Arial", 16, FontStyle.Bold))
                    using (StringFormat sfHdr = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        string hdrText = $"#{ma}  {tenLoai.ToUpper()}";
                        g.DrawString(hdrText, fHdr, Brushes.White, hdrRect, sfHdr);
                    }

                    // 4.3. Vẽ Body (Hình bóng trang phục)
                    VeHinhBong(g, tenLoai, mauTrangPhuc);

                    // Chữ in trên áo (SIZE & Màu)
                    using (Font fSize = new Font("Arial", 20, FontStyle.Bold))
                    using (Font fMau = new Font("Arial", 15, FontStyle.Bold))
                    using (SolidBrush bTextAo = new SolidBrush(mauChuAo))
                    using (StringFormat sfAo = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.DrawString($"SIZE {kichCo}", fSize, bTextAo, new RectangleF(100, 240, 300, 35), sfAo);
                        g.DrawString($"Màu: {mauSac}", fMau, bTextAo, new RectangleF(100, 280, 300, 30), sfAo);
                    }

                    // 4.4. Footer (Y: 435 -> 600)
                    Rectangle footRect = new Rectangle(0, 435, 500, 165);
                    using (SolidBrush bFoot = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(bFoot, footRect);
                    }
                    using (Pen pLine = new Pen(Color.FromArgb(226, 232, 240), 1f))
                    {
                        g.DrawLine(pLine, 0, 435, 500, 435);
                    }

                    // Nội dung footer
                    using (Font fTen = new Font("Arial", 12, FontStyle.Bold))
                    using (Font fCL = new Font("Arial", 11, FontStyle.Regular))
                    using (Font fGia = new Font("Arial", 16, FontStyle.Bold))
                    using (SolidBrush bTen = new SolidBrush(Color.FromArgb(15, 23, 42)))
                    using (SolidBrush bCL = new SolidBrush(Color.FromArgb(71, 85, 105)))
                    using (SolidBrush bGia = new SolidBrush(Color.FromArgb(225, 29, 72))) // Hồng đỏ #E11D48
                    {
                        // Tên sản phẩm
                        RectangleF rTen = new RectangleF(20, 445, 460, 48);
                        g.DrawString(tenQA, fTen, bTen, rTen);

                        // Chất liệu
                        g.DrawString($"Chất liệu: {chatLieu}", fCL, bCL, 20, 498);

                        // Giá bán định dạng {0:N0} VNĐ
                        string giaText = string.Format("{0:N0} VNĐ", giaBan);
                        g.DrawString(giaText, fGia, bGia, 20, 525);
                    }
                }

                // Lưu không khóa file qua MemoryStream
                byte[] imgBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                    imgBytes = ms.ToArray();
                }

                File.WriteAllBytes(fullPath, imgBytes);
                if (!string.IsNullOrEmpty(srcImgDir))
                {
                    try { File.WriteAllBytes(Path.Combine(srcImgDir, fileName), imgBytes); } catch { }
                }
                taoMoi++;
            }
        }

        // 5. Ghi log kết quả
        GhiLogTienTrinh(string.Format("[{0:yyyy-MM-dd HH:mm:ss}] TaoTuDong200AnhSanPham completed. Newly created: {1} images, Total items checked: {2}.\r\n",
            DateTime.Now, taoMoi, dt.Rows.Count));
    }
    catch (Exception ex)
    {
        GhiLogTienTrinh(string.Format("[{0:yyyy-MM-dd HH:mm:ss}] ERROR in TaoTuDong200AnhSanPham: {1}\r\n", DateTime.Now, ex.Message));
    }
}

private static void GhiLogTienTrinh(string msg)
{
    try
    {
        // Ghi vào StartupPath\Logs
        string logDir = Path.Combine(Application.StartupPath, "Logs");
        if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);
        File.AppendAllText(Path.Combine(logDir, "image_gen.log"), msg);

        // Ghi vào SourceRoot\Logs nếu có
        try
        {
            string srcLogDir = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Logs"));
            if (Directory.Exists(Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\.."))))
            {
                if (!Directory.Exists(srcLogDir)) Directory.CreateDirectory(srcLogDir);
                File.AppendAllText(Path.Combine(srcLogDir, "image_gen.log"), msg);
            }
        }
        catch { }
    }
    catch { }
}
```
