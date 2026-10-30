# Project: QuanLyBanQuanAo - Automated Product Image Generation & Safe WinForms Integration

## Architecture
- **Layer 1: Database (SQL Server)**
  - Script: `Database/UpdateAnh.sql`
  - Table: `tblQuanAo` (Columns: `MaQuanAo`, `TenQuanAo`, `MaLoai`, `MaChatLieu`, `KichCo`, `MauSac`, `SoLuong`, `DonGiaNhap`, `DonGiaBan`, `Anh`, `GhiChu`)
  - Joined Tables: `tblLoaiThoiTrang` (`TenLoai`), `tblChatLieu` (`TenChatLieu`)
- **Layer 2: Data Access Layer (DAL)**
  - `QuanLyBanQuanAo.DAL.DataProcesser`: `CapNhatDuLieu(sql)` for execution, `DocBang(sql)` for queries.
- **Layer 3: Business Logic Layer (BLL)**
  - `QuanLyBanQuanAo.BLL.Function`:
    - `TaoTuDong200AnhSanPham()`: Syncs DB, queries 200 products, renders 500x600 Bitmaps using GDI+ System.Drawing, saves lock-free JPEGs to `Images\QuanAo`, and logs to `Logs\image_gen.log`.
    - `LayAnhKhongKhoaFile(string duongDan)`: Reads image bytes into MemoryStream and returns an independent Bitmap clone to eliminate disk file locks.
- **Layer 4: Graphical User Interface (GUI / WinForms)**
  - `frmMain`: Auto-invokes image generation on `frmMain_Load`.
  - `frmQuanAo`: Auto-invokes image generation on `frmQuanAo_Load`. `picAnh.SizeMode = PictureBoxSizeMode.Zoom`. Uses safe image loading in `HienThiAnh`.
  - `frmCuaHang`: Auto-invokes image generation on `frmCuaHang_Load`. `picCT.SizeMode = PictureBoxSizeMode.Zoom`. Uses safe image loading in `LayAnh`.
  - `frmDatHang`: Auto-invokes image generation on `frmDatHang_Load`. `picXemTruoc.SizeMode = PictureBoxSizeMode.Zoom`. Uses safe image loading in `HienThiXemTruocSanPham` / `CellClick`.

## Feature Inventory
| # | Feature | Description | Milestone | Source | Status |
|---|---------|-------------|-----------|--------|:------:|
| 1 | SQL Image Sync Script | `Database/UpdateAnh.sql` with idempotent UPDATE statement | M1 | R1, Survey | DONE |
| 2 | BLL Method `TaoTuDong200AnhSanPham` | Core generation method in `BLL/Function.cs` updating DB and rendering 500x600 images | M2 | R2, Survey | DONE |
| 3 | 10-Color Palette & Contrast Logic | Color mapping handling `Be (Kem)` and contrast text colors | M2 | R2, Survey | DONE |
| 4 | Vector Silhouettes Renders | Áo (collared shirt + sleeves), Quần (2 legs), Váy (A-line flared dress) | M2 | R2, Survey | DONE |
| 5 | Card Layout Renders | Header (#MaQuanAo, TenLoai.ToUpper()), Body (SIZE KichCo, Màu), Footer (TenQuanAo, ChatLieu, Price #E11D48) | M2 | R2, Survey | DONE |
| 6 | Lock-Free Image Storage & Logging | Write to `Images\QuanAo` lock-free and log to `Logs\image_gen.log` | M2 | R2, Survey | DONE |
| 7 | Shared Safe Image Loader | `LayAnhKhongKhoaFile(string duongDan)` avoiding GDI+ file lock | M2 | R3, Survey | DONE |
| 8 | UI PictureBox Zoom Configuration | `picAnh`, `picCT`, `picXemTruoc` set to `PictureBoxSizeMode.Zoom` | M3 | R3, Survey | DONE |
| 9 | UI Safe Image Preview Integration | `frmQuanAo`, `frmCuaHang`, `frmDatHang` load preview images using safe loader | M3 | R3, Survey | DONE |
| 10 | Form Load Auto-Invocation | Call `TaoTuDong200AnhSanPham()` in `frmMain_Load`, `frmQuanAo_Load`, `frmCuaHang_Load`, `frmDatHang_Load` | M3 | R4, Survey | DONE |
| 11 | Solution MSBuild & E2E Validation | Clean compilation with 0 warnings/0 errors and 100% verification of generated files and lock freedom | M4 | R5, Survey | DONE |

## Milestones
| # | Name | Scope | Dependencies | Status |
|---|------|-------|-------------|:------:|
| M1 | SQL Script & Database Synchronization | Create `Database/UpdateAnh.sql` and execute sync on database | none | DONE |
| M2 | BLL Image Generation & Safe Loader | Implement `TaoTuDong200AnhSanPham()` and `LayAnhKhongKhoaFile()` in `BLL/Function.cs` | M1 | DONE |
| M3 | UI Integration & Form Load Invocation | Update `frmMain`, `frmQuanAo`, `frmCuaHang`, `frmDatHang` for SizeMode, safe loading, and auto-invocation | M2 | DONE |
| M4 | E2E Testing, Adversarial Verification & Gate | Run MSBuild, verify 200 images, verify log, verify file locks, run challengers and forensic auditor | M3 | DONE |

## Interface Contracts
### `BLL/Function.cs`
- `public void TaoTuDong200AnhSanPham()`
- `public static Image LayAnhKhongKhoaFile(string duongDan)`

### `DAL/DataProcesser.cs`
- `public bool CapNhatDuLieu(string sql, params SqlParameter[] ps)`
- `public DataTable DocBang(string sql, params SqlParameter[] ps)`

## Code Layout
- `Database/UpdateAnh.sql`: SQL sync script (200 rows synchronized)
- `BLL/Function.cs`: Business logic, automated generation, safe image loader
- `GUI/frmMain.cs`: Main form entry
- `GUI/frmQuanAo.cs`: Product management form
- `GUI/frmCuaHang.cs`: Shop display form
- `GUI/frmDatHang.cs`: Order form
- `Images/QuanAo/`: 200 product images (`QA001.jpg` .. `QA200.jpg`) (500x600 px)
- `Logs/image_gen.log`: Generation log file
