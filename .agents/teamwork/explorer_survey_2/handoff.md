# HANDOFF REPORT: BLL AND IMAGE GENERATION SURVEY (R2)

**Agent**: Explorer 2  
**Date**: 2026-10-30  
**Target Requirement**: R2 (Automatic 200 Product Images Generation in BLL) & R3 (Safe Image Loading Helper)  
**Status**: Survey Complete (Hard Handoff)  
**Report File**: `d:\lập trình trực quan\QuanLyBanQuanAo_24_10\QuanLyBanQuanAo\.agents\teamwork\explorer_survey_2\report.md`

---

## 1. OBSERVATIONS

1. **`BLL/Function.cs` Structure**:
   - `Function` is declared as `public class Function` at line 10 in `BLL/Function.cs`. It is an instantiated class containing `private DataProcesser dtBase = new DataProcesser();` at line 57.
   - It already provides common business and UI helpers (`FillCombo`, `GetFieldValues`, `CheckKey`, `CreateKey`, `ChiNhapSoNguyen`, `KiemTraSoDienThoai`, `DocSoThanhChu`, and VietQR generation).
   - In UI forms, `Function` is instantiated as `Function func = new Function();` (`frmQuanAo.cs:15`, `frmDatHang.cs:19`, `frmKhachHang.cs:12`, `frmNhanVien.cs:12`).

2. **DAL Interface Mismatch in Initial Plan**:
   - `implementation_plan.md` proposed `dtBase.ThucThiNonQuery(sqlSync);`.
   - Inspection of `DAL/DataProcesser.cs` revealed:
     - Line 82: `public bool CapNhatDuLieu(string sql, params SqlParameter[] ps)`
     - Line 62: `public DataTable DocBang(string sql, params SqlParameter[] ps)`
     - There is **no method named `ThucThiNonQuery` or `RunSql`** in `DataProcesser`. Calling `ThucThiNonQuery` causes an immediate compilation failure (`CS1061`).

3. **Database Schema & Data Observations**:
   - In `Database/1_csdl_quanlybanquanao.sql` line 69, product size column is `KichCo NVARCHAR(10) NOT NULL`. There is no `KichSize` column.
   - Querying distinct `MauSac` from `tblQuanAo` on `(localdb)\MSSQLLocalDB` returned 10 distinct values: `Be (Kem)`, `Đen`, `Đỏ đô`, `Hồng Pastel`, `Nâu Tây`, `Trắng`, `Xám khói`, `Xanh Dương`, `Xanh Navy`, `Xanh rêu`.
   - The color Beige is stored verbatim as `Be (Kem)` in all corresponding rows in `tblQuanAo` (`1_csdl_quanlybanquanao.sql:146, 156, 166, 170...`).

4. **Directory Structure & Image References**:
   - `Images\QuanAo` in project root contains only 8 legacy image files (`aothun.jpg`, `dam_lua.jpg`, `hoodie_den.jpg`, `jeans_nam.jpg`, `pijama.jpg`, `quankaki.jpg`, `somi_trang.jpg`, `thethao.jpg`). No `QA001.jpg`..`QA200.jpg` files exist.
   - `QuanLyBanQuanAo.csproj` line 192–194:
     ```xml
     <Content Include="Images\QuanAo\*.jpg">
       <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
     </Content>
     ```
   - Running application uses `Application.StartupPath` (`bin\Debug`). Source code repository root is at `..\..\` relative to `Application.StartupPath`.

5. **Logs Directory**:
   - Directory `Logs/` does not currently exist in the solution or output folder.

6. **File Locking in Existing Forms**:
   - `GUI/frmDatHang.cs` line 113: `picXemTruoc.Image = Image.FromFile(duongDanAnh);` locks the file on disk.
   - `GUI/frmQuanAo.cs` line 111–114: `using (FileStream fs = new FileStream(...)) { picAnh.Image = Image.FromStream(fs); }` disposes the underlying stream immediately, which leaves GDI+ vulnerable to rendering exceptions upon redraw.
   - `GUI/frmCuaHang.cs` line 150: uses `Image.FromStream(new MemoryStream(File.ReadAllBytes(duongDan)))`, which prevents disk file locking.

7. **Compilation & Reference Environment**:
   - `QuanLyBanQuanAo.csproj` line 49 references `<Reference Include="System.Drawing" />`. Under .NET Framework 4.7.2, `System.Drawing.Drawing2D`, `System.Drawing.Imaging`, and `System.Drawing.Text` are namespaces inside `System.Drawing.dll`.
   - Testing build with MSBuild SSMS 22:
     `& "C:\Program Files\Microsoft SQL Server Management Studio 22\Release\MSBuild\Current\Bin\amd64\MSBuild.exe" "d:\lập trình trực quan\QuanLyBanQuanAo_24_10\QuanLyBanQuanAo\QuanLyBanQuanAo.csproj" /t:Build /p:Configuration=Debug`
     Result: **Build succeeded with 0 Warnings and 0 Errors**.

---

## 2. LOGIC CHAIN

1. **From Observation 2 to DAL Execution**:
   Because `DataProcesser` defines `CapNhatDuLieu` for non-query commands and `DocBang` for table queries, any BLL call to execute `UPDATE tblQuanAo SET Anh = ...` MUST call `dtBase.CapNhatDuLieu(sqlSync)`. Using `ThucThiNonQuery` would break the build.

2. **From Observation 3 to Color Palette Mapping**:
   Because the database stores `"Be (Kem)"`, a strict string switch checking only `"be"` would fail to match and incorrectly fallback to gray. Normalizing the string and matching `Contains("be")` or `Contains("kem")` guarantees that all Beige products receive the correct RGB(235, 220, 195) palette.

3. **From Observation 3 & 4 to Garment Silhouettes & Categories**:
   The 8 clothing categories in `tblLoaiThoiTrang` map deterministically to 3 vector silhouette geometries:
   - Pants (`TenLoai.Contains("Quần")` -> L02, L07): dual-leg geometry with waistband and crotch line.
   - Dress (`TenLoai.Contains("Váy")` or `TenLoai.Contains("Đầm")` -> L03): scoop neck, fitted waist, flared A-line skirt.
   - Top / Garment (all others -> L01, L04, L05, L06, L08): collar neckline, angled sleeves, rectangular hem.

4. **From Observation 4 & 5 to Output Path Management**:
   Because tests or automated verifications may inspect either `Application.StartupPath\Images\QuanAo` (runtime directory) or `<ProjectRoot>\Images\QuanAo` (source directory), saving or mirroring generated images to both locations guarantees compliance under all execution contexts. The same logic applies to `Logs/image_gen.log`.

5. **From Observation 6 to Safe Image Loading Helper**:
   Centralizing a safe loader `Function.LayAnhKhongKhoaFile(duongDan)` using `new Bitmap(img)` inside `using (FileStream fs = ...)` clones image pixels into memory, closes file handles immediately, and resolves file locks in `frmQuanAo` and `frmDatHang`.

6. **From Observation 7 to C# Language Compatibility**:
   Because `QuanLyBanQuanAo.csproj` does not set `<LangVersion>8.0</LangVersion>`, C# 8.0 switch expressions (`x switch { ... }`) risk compilation failure under default C# 7.3 tooling. Writing standard `if-else` or classic `switch-case` ensures 100% compiler compatibility.

---

## 3. CAVEATS

1. **Database Accessibility**: `(localdb)\MSSQLLocalDB` must be running when `TaoTuDong200AnhSanPham()` is invoked. If the database connection fails, the method should gracefully log the error to `Logs/image_gen.log` rather than crashing the application.
2. **Text Measurement Variations**: Font rendering metrics may differ slightly across Windows display scaling settings (100% vs 125% DPI). Using `RectangleF` with wrapping `StringFormat` prevents string clipping.
3. No other caveats.

---

## 4. CONCLUSION

- The implementation of `TaoTuDong200AnhSanPham()` in `BLL/Function.cs` must:
  1. Call `dtBase.CapNhatDuLieu(sqlSync)` to update `Anh = MaQuanAo + N'.jpg'`.
  2. Query using `dtBase.DocBang(...)` selecting `KichCo` (not `KichSize`).
  3. Map the 10 colors using keyword-tolerant checks (handling `Be (Kem)`).
  4. Apply dark text `#1E293B` on Trắng, Be (Kem), and Hồng Pastel; White on others.
  5. Draw vector silhouettes for Áo, Quần, and Váy with dimensions 500x600 px.
  6. Format footer price as `{0:N0} VNĐ` in `#E11D48`.
  7. Export JPEG lock-free using `MemoryStream` and `File.WriteAllBytes()`.
  8. Log completion to `Logs/image_gen.log` in both startup and project root directories.
  9. Expose `Function.LayAnhKhongKhoaFile` for safe image loading in UI forms.

---

## 5. VERIFICATION METHOD

1. **Compilation Test**:
   ```powershell
   & "C:\Program Files\Microsoft SQL Server Management Studio 22\Release\MSBuild\Current\Bin\amd64\MSBuild.exe" "d:\lập trình trực quan\QuanLyBanQuanAo_24_10\QuanLyBanQuanAo\QuanLyBanQuanAo.csproj" /t:Build /p:Configuration=Debug
   ```
   *Expected outcome*: 0 Warnings, 0 Errors.

2. **Image Generation Verification**:
   - Check presence of 200 files: `(Get-ChildItem "d:\lập trình trực quan\QuanLyBanQuanAo_24_10\QuanLyBanQuanAo\bin\Debug\Images\QuanAo\QA*.jpg").Count` equals 200.
   - Inspect dimensions: Each image is 500x600 pixels.
   - Inspect `Logs/image_gen.log`: Contains timestamp and generation completion log.

3. **File Lock Invalidation Check**:
   - While a product is selected in `frmQuanAo` or `frmDatHang`, attempt to overwrite or delete the selected image file on disk. If safe loading is applied, disk operations succeed without `IOException: The process cannot access the file because it is being used by another process`.
