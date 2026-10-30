# HANDOFF REPORT — EXPLORER 1: DATABASE, SQL SCHEMA & DAL SURVEY

## 1. Observation
1. **File paths and structure in `Database/`**:
   - `Database/1_csdl_quanlybanquanao.sql`: Exists (348 lines). Lines 63–78 define table `tblQuanAo`:
     ```sql
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
     ```
   - Lines 34–37 define `tblLoaiThoiTrang` (`MaLoai NVARCHAR(20) PRIMARY KEY`, `TenLoai NVARCHAR(100)`).
   - Lines 40–43 define `tblChatLieu` (`MaChatLieu NVARCHAR(20) PRIMARY KEY`, `TenChatLieu NVARCHAR(100)`).
   - `Database/2_patch_csdl.sql`: Exists (31 lines), alters `tblHDBan` adding `HinhThucThanhToan` and `GhiChu`.
   - `Database/3_patch_chuyenkhoan.sql`: Exists (8 lines), alters `tblHDBan` adding `AnhChuyenKhoan`.
   - `Database/UpdateAnh.sql`: Does NOT exist yet in `Database/`.
2. **Column Names in `tblQuanAo`**:
   - Direct inspection via `sqlcmd -S "(localdb)\MSSQLLocalDB" -d QuanLyBanQuanAo -Q "SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblQuanAo'"` returned 11 columns: `MaQuanAo`, `TenQuanAo`, `MaLoai`, `MaChatLieu`, `KichCo` (nvarchar 10), `MauSac` (nvarchar 50), `SoLuong` (int), `DonGiaNhap` (float), `DonGiaBan` (float), `Anh` (nvarchar 255), `GhiChu` (nvarchar 255).
   - The size column is `KichCo`, NOT `KichSize`.
   - In code: `GUI/frmQuanAo.cs` line 64: `qa.KichCo`, `GUI/frmDatHang.cs` line 52: `qa.KichCo`, `GUI/frmCuaHang.cs` line 85: `qa.KichCo`.
3. **Database Data Values**:
   - Querying `(localdb)\MSSQLLocalDB`:
     `SELECT COUNT(*) AS TongSo, COUNT(CASE WHEN Anh LIKE 'QA%.jpg' THEN 1 END) AS DaDongBoAnh FROM tblQuanAo` returned `TongSo = 200`, `DaDongBoAnh = 0`.
   - All 200 rows currently have static image names such as `somi_trang.jpg`, `quankaki.jpg`, `pijama.jpg`.
   - `SELECT DISTINCT MauSac FROM tblQuanAo` returned 10 colors: `Trắng`, `Đen`, `Xanh Navy`, `Be (Kem)`, `Xám khói`, `Đỏ đô`, `Xanh rêu`, `Nâu Tây`, `Hồng Pastel`, `Xanh Dương`.
   - Notice the color "Be" is recorded as `"Be (Kem)"`.
   - `SELECT DISTINCT KichCo FROM tblQuanAo` returned 5 values: `S`, `M`, `L`, `XL`, `XXL`.
4. **DAL Classes and Methods**:
   - `DAL/DataProcesser.cs` defines `QuanLyBanQuanAo.DAL.DataProcesser`.
   - Method for SELECT queries: `public DataTable DocBang(string sql, params SqlParameter[] ps)`.
   - Method for INSERT/UPDATE/DELETE: `public bool CapNhatDuLieu(string sql, params SqlParameter[] ps)`.
   - Transaction method: `public bool ThucThiGiaoDich(Action<GiaoDich> work, out string loi)`.
   - Connection string logic: `ConfigurationManager.ConnectionStrings["QuanLyBanQuanAo"]` from `App.config`, fallback to `"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuanLyBanQuanAo;Integrated Security=True"`.
   - There is NO method named `ThucThiNonQuery`, `RunSql`, or `GetDataToTable`.
   - In `BLL/Function.cs` line 57: `private DataProcesser dtBase = new DataProcesser();`.
5. **Image Loading in GUI**:
   - `GUI/frmCuaHang.cs` line 150 uses safe loading:
     `img = Image.FromStream(new MemoryStream(File.ReadAllBytes(duongDan)));`
   - `GUI/frmDatHang.cs` line 113 uses locking method:
     `picXemTruoc.Image = Image.FromFile(duongDanAnh);`
   - `GUI/frmQuanAo.cs` lines 111–114 uses `FileStream` inside `using`:
     `using (FileStream fs = new FileStream(duongDan, FileMode.Open, FileAccess.Read)) { picAnh.Image = Image.FromStream(fs); }`
6. **Build Tooling**:
   - Running `dotnet msbuild` fails with:
     `error MSB4803: The task "ResolveComReference" is not supported on the .NET Core version of MSBuild. Please use the .NET Framework version of MSBuild.`
   - Running Visual Studio MSBuild:
     `& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" QuanLyBanQuanAo.csproj /p:Configuration=Debug /v:m`
     succeeds with 0 errors and generates `QuanLyBanQuanAo.exe`.

---

## 2. Logic Chain
1. From Observation 1 & 2: Table `tblQuanAo` defines `KichCo NVARCHAR(10)` and every SQL query in `frmQuanAo.cs`, `frmDatHang.cs`, and `frmCuaHang.cs` selects `qa.KichCo`. Therefore, all queries and models must reference column name `KichCo`. Attempting to query `KichSize` will result in SQL Server error `Invalid column name 'KichSize'`.
2. From Observation 3: Exactly 200 product records exist in `tblQuanAo`, none of which currently follow the `QA%.jpg` naming scheme. Running `UPDATE tblQuanAo SET Anh = MaQuanAo + N'.jpg' WHERE Anh IS NULL OR Anh = '' OR Anh NOT LIKE 'QA%.jpg'` will update all 200 rows to `QA001.jpg`..`QA200.jpg`. Rerunning it is idempotent because subsequent runs will match 0 rows.
3. From Observation 3: The distinct color list in `tblQuanAo` contains `"Be (Kem)"`. The color palette mapper must check `.Contains("be")` or handle `"be (kem)"` so that items with `"Be (Kem)"` map correctly to `Color.FromArgb(235, 220, 195)` rather than falling back to default gray.
4. From Observation 4: The DAL class is `DataProcesser`, providing `DocBang` for queries and `CapNhatDuLieu` for non-queries. The draft snippet in `implementation_plan.md` called `dtBase.ThucThiNonQuery(sqlSync)`. Because `DataProcesser` does not define `ThucThiNonQuery`, implementing that snippet verbatim would fail compilation with CS1061. The implementation must call `dtBase.CapNhatDuLieu(sqlSync)`.
5. From Observation 5: `frmDatHang.cs` locks image files on disk via `Image.FromFile`, preventing file operations. Adopting `MemoryStream` with `File.ReadAllBytes` (matching `frmCuaHang.cs`) resolves file locking across all forms.
6. From Observation 6: Due to COM reference `Microsoft.Office.Interop.Excel`, `dotnet msbuild` (.NET Core) cannot be used. The build verification step must invoke full Visual Studio MSBuild (`MSBuild.exe`).

---

## 3. Caveats
- The live database `(localdb)\MSSQLLocalDB` currently has `QuanLyBanQuanAo` with 200 rows. If tests are run on a machine without `(localdb)\MSSQLLocalDB` started or without the database created, `1_csdl_quanlybanquanao.sql`, `2_patch_csdl.sql`, and `3_patch_chuyenkhoan.sql` must be executed first.
- The `TaoTuDong200AnhSanPham()` method writes images to `Path.Combine(Application.StartupPath, "Images", "QuanAo")`. At runtime, `Application.StartupPath` is `bin\Debug`. If the project source directory `Images\QuanAo` must also reflect the 200 generated images, the generator should optionally write to both locations or copy them to `..\..\Images\QuanAo`.

---

## 4. Conclusion
1. Create `Database/UpdateAnh.sql` containing:
   ```sql
   USE QuanLyBanQuanAo;
   GO

   UPDATE tblQuanAo 
   SET Anh = MaQuanAo + N'.jpg' 
   WHERE Anh IS NULL OR Anh = '' OR Anh NOT LIKE 'QA%.jpg';
   GO
   ```
2. In `BLL/Function.cs`, implement `TaoTuDong200AnhSanPham()` using:
   - `dtBase.CapNhatDuLieu(sqlSync);`
   - `dtBase.DocBang(sqlQuery);`
   - Column `KichCo` (NOT `KichSize`).
   - Color mapping with `"be"` matching `"Be (Kem)"` -> `RGB(235, 220, 195)`.
3. Standardize image preview loading in `frmQuanAo.cs` and `frmDatHang.cs` to use safe `MemoryStream(File.ReadAllBytes(path))` to prevent file locks.
4. Verify build using Visual Studio MSBuild (`MSBuild.exe`).

---

## 5. Verification Method
1. **Verify column name and database schema**:
   ```powershell
   sqlcmd -S "(localdb)\MSSQLLocalDB" -d QuanLyBanQuanAo -Q "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblQuanAo' AND COLUMN_NAME IN ('KichCo', 'KichSize', 'Anh', 'MauSac')"
   ```
   *Expected output*: `KichCo`, `Anh`, `MauSac` appear; `KichSize` does not exist.
2. **Verify SQL update statement**:
   ```powershell
   sqlcmd -S "(localdb)\MSSQLLocalDB" -d QuanLyBanQuanAo -Q "SELECT COUNT(*) FROM tblQuanAo WHERE Anh NOT LIKE 'QA%.jpg'"
   ```
   *Before update*: returns 200.
   *After executing update*: returns 0.
3. **Verify build command**:
   ```powershell
   [Console]::OutputEncoding = [System.Text.Encoding]::UTF8; cd "d:\lập trình trực quan\QuanLyBanQuanAo_24_10\QuanLyBanQuanAo"; & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" QuanLyBanQuanAo.csproj /p:Configuration=Debug /v:m
   ```
   *Expected output*: Build succeeded with 0 errors.
