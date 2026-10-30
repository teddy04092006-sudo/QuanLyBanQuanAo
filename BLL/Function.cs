using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using QuanLyBanQuanAo.DAL;

namespace QuanLyBanQuanAo.BLL
{
    public class Function
    {
        // ====== SỬA CÁC THÔNG TIN NÀY CHO ĐÚNG SHOP CỦA NHÓM (hiện trên hóa đơn Excel) ======
        public const string TenShop = "CỬA HÀNG THỜI TRANG NHÓM 4";
        public const string DiaChiShop = "Địa chỉ: Đại học Giao thông Vận tải, Hà Nội";
        public const string DienThoaiShop = "Điện thoại: 0900 000 000";

        // ====== THÔNG TIN TÀI KHOẢN NHẬN TIỀN (dùng để tạo mã QR VietQR + in lên hóa đơn) ======
        // MaNganHangQR: mã/tên viết tắt ngân hàng theo VietQR, ví dụ: VCB, TCB, MB, ACB, BIDV, VPB, TPB, CTG, STB...
        public const string MaNganHangQR = "NCB";
        public const string TenNganHang = "NCB - Ngân hàng Quốc Dân";
        public const string SoTaiKhoan = "100013747808";
        public const string TenChuTaiKhoan = "Dương Linh Chi";
        public const string ThongTinChuyenKhoan = "Chuyển khoản: STK " + SoTaiKhoan + " - " + TenNganHang + " - Chủ TK: " + TenChuTaiKhoan;

        /// <summary>Link ảnh QR VietQR đã điền sẵn số tiền + nội dung chuyển khoản.</summary>
               // Mã BIN của ngân hàng theo chuẩn VietQR (NCB = 970419)
        public const string MaBinNganHang = "970419";

        private static string Tlv(string id, string value)
        {
            return id + value.Length.ToString("00") + value;
        }

        private static string Crc16(string s)
        {
            ushort crc = 0xFFFF;
            foreach (byte b in Encoding.ASCII.GetBytes(s))
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                    crc = (crc & 0x8000) != 0 ? (ushort)((crc << 1) ^ 0x1021) : (ushort)(crc << 1);
            }
            return crc.ToString("X4");
        }

        /// <summary>Chuỗi nội dung của mã VietQR (đã gồm số tiền + nội dung chuyển khoản).</summary>
        public static string TaoChuoiVietQR(double soTien, string noiDung)
        {
            string taiKhoan = Tlv("00", MaBinNganHang) + Tlv("01", SoTaiKhoan);
            string merchant = Tlv("00", "A000000727") + Tlv("01", taiKhoan) + Tlv("02", "QRIBFTTA");
            string s = Tlv("00", "01") + Tlv("01", "12") + Tlv("38", merchant) + Tlv("53", "704")
                     + Tlv("54", ((long)Math.Round(soTien)).ToString()) + Tlv("58", "VN")
                     + Tlv("62", Tlv("08", noiDung)) + "6304";
            return s + Crc16(s);
        }

        private DataProcesser dtBase = new DataProcesser();

        // Đổ dữ liệu từ câu lệnh SQL vào ComboBox
        public void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            DataTable dt = dtBase.DocBang(sql);
            cbo.DataSource = dt;
            cbo.ValueMember = ma;
            cbo.DisplayMember = ten;
            cbo.SelectedIndex = -1;
        }

        // Lấy 1 giá trị đơn lẻ từ câu lệnh SELECT
        public string GetFieldValues(string sql)
        {
            string ma = "";
            DataTable dt = dtBase.DocBang(sql);
            if (dt.Rows.Count > 0)
            {
                ma = dt.Rows[0][0].ToString();
            }
            return ma;
        }

        // Kiểm tra trùng khóa chính
        public bool CheckKey(string sql)
        {
            DataTable dt = dtBase.DocBang(sql);
            return dt.Rows.Count > 0;
        }

        // Sinh mã theo ngày giờ (có mili-giây để không trùng): TienTo_yyyyMMdd_HHmmssfff
        public string CreateKey(string tiento)
        {
            DateTime now = DateTime.Now;
            return string.Format("{0}_{1:yyyyMMdd}_{1:HHmmssfff}", tiento, now);
        }

        // ================= CÁC HÀM VALIDATE DÙNG CHUNG =================

        // Chỉ cho phép nhập số nguyên dương
        public void ChiNhapSoNguyen(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || Convert.ToInt32(e.KeyChar) == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }

        // Số điện thoại hợp lệ (bắt đầu bằng 0 và có 10 chữ số)
        public bool KiemTraSoDienThoai(string sdt)
        {
            return Regex.IsMatch((sdt ?? "").Trim(), @"^0\d{9}$");
        }

        // ================= ĐỌC SỐ TIỀN THÀNH CHỮ =================
        private static readonly string[] CH = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

        private static string Doc3(int n, bool day)
        {
            int tram = n / 100, chuc = (n % 100) / 10, donvi = n % 10;
            StringBuilder s = new StringBuilder();
            if (tram > 0 || day) s.Append(CH[tram] + " trăm");
            if (chuc > 1)
            {
                s.Append(" " + CH[chuc] + " mươi");
                if (donvi == 1) s.Append(" mốt");
                else if (donvi == 5) s.Append(" lăm");
                else if (donvi > 0) s.Append(" " + CH[donvi]);
            }
            else if (chuc == 1)
            {
                s.Append(" mười");
                if (donvi == 5) s.Append(" lăm");
                else if (donvi > 0) s.Append(" " + CH[donvi]);
            }
            else if (donvi > 0)
            {
                if (tram > 0 || day) s.Append(" lẻ");
                s.Append(" " + CH[donvi]);
            }
            return s.ToString().Trim();
        }

        public string DocSoThanhChu(long so)
        {
            if (so <= 0) return "Không đồng";
            string[] donVi = { "", " nghìn", " triệu", " tỷ" };
            int[] nhom = new int[8];
            int soNhom = 0;
            long t = so;
            while (t > 0) { nhom[soNhom++] = (int)(t % 1000); t /= 1000; }

            StringBuilder kq = new StringBuilder();
            for (int i = soNhom - 1; i >= 0; i--)
            {
                if (nhom[i] == 0) continue;
                bool day = i < soNhom - 1;           // nhóm không đứng đầu thì đọc đủ "không trăm"
                kq.Append(Doc3(nhom[i], day));
                kq.Append(donVi[i % 4]);
                kq.Append(" ");
            }
            string s = kq.ToString().Trim();
            return char.ToUpper(s[0]) + s.Substring(1) + " đồng";
        }

        // ================= XỬ LÝ ẢNH & SINH 200 ẢNH TỰ ĐỘNG =================

        /// <summary>
        /// Lấy thư mục gốc thực thi của ứng dụng (hỗ trợ cả khi chạy bình thường và chạy qua test runner/reflection).
        /// </summary>
        public static string LayThuMucUngDung()
        {
            string dir = Application.StartupPath;
            if (string.IsNullOrEmpty(dir) ||
                dir.IndexOf("WindowsPowerShell", StringComparison.OrdinalIgnoreCase) >= 0 ||
                dir.IndexOf("System32", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                string asmLoc = typeof(Function).Assembly.Location;
                if (!string.IsNullOrEmpty(asmLoc))
                {
                    string asmDir = Path.GetDirectoryName(asmLoc);
                    if (!string.IsNullOrEmpty(asmDir) && Directory.Exists(asmDir))
                    {
                        return asmDir;
                    }
                }
            }
            return dir;
        }

        /// <summary>
        /// Nạp ảnh từ đường dẫn tệp tin mà không gây khóa tệp (file lock) trên ổ đĩa.
        /// Sử dụng MemoryStream và clone ra Bitmap độc lập để giải phóng hoàn toàn luồng tệp.
        /// </summary>
        public static Image LayAnhKhongKhoaFile(string duongDan)
        {
            if (string.IsNullOrEmpty(duongDan))
                return null;

            if (!File.Exists(duongDan))
            {
                // Thử tìm theo thư mục ứng dụng thực tế nếu đường dẫn caller bị lệch
                string fallback = Path.Combine(LayThuMucUngDung(), "Images", "QuanAo", Path.GetFileName(duongDan));
                if (File.Exists(fallback))
                {
                    duongDan = fallback;
                }
                else
                {
                    return null;
                }
            }

            try
            {
                byte[] bytes = File.ReadAllBytes(duongDan);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (Image temp = Image.FromStream(ms))
                    {
                        return new Bitmap(temp);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Tự động đồng bộ cột Anh trong cơ sở dữ liệu và vẽ 200 ảnh sản phẩm (QA001.jpg - QA200.jpg)
        /// nếu chưa tồn tại trên ổ đĩa.
        /// </summary>
        public void TaoTuDong200AnhSanPham()
        {
            try
            {
                // 1. Xác định và khởi tạo các thư mục lưu ảnh (cả thư mục runtime bin\Debug và thư mục source gốc nếu có)
                string appDir = LayThuMucUngDung();
                string binImgDir = Path.Combine(appDir, "Images", "QuanAo");
                if (!Directory.Exists(binImgDir))
                {
                    Directory.CreateDirectory(binImgDir);
                }

                string projectRootDir = Path.GetFullPath(Path.Combine(appDir, "..", ".."));
                string srcImgDir = Path.Combine(projectRootDir, "Images", "QuanAo");
                bool coThuMucSource = Directory.Exists(Path.Combine(projectRootDir, "GUI"));
                if (coThuMucSource && !Directory.Exists(srcImgDir))
                {
                    Directory.CreateDirectory(srcImgDir);
                }

                // 2. Đồng bộ cột Anh trong cơ sở dữ liệu tblQuanAo
                string sqlSync = "UPDATE tblQuanAo SET Anh = MaQuanAo + N'.jpg' WHERE Anh IS NULL OR Anh = '' OR Anh NOT LIKE 'QA%.jpg'";
                dtBase.CapNhatDuLieu(sqlSync);

                // 3. Lấy dữ liệu 200 sản phẩm từ tblQuanAo cùng loại và chất liệu
                string sql = @"SELECT qa.MaQuanAo, qa.TenQuanAo, l.TenLoai, cl.TenChatLieu, 
                                      qa.KichCo, qa.MauSac, qa.DonGiaBan, qa.Anh
                               FROM tblQuanAo qa
                               INNER JOIN tblLoaiThoiTrang l ON qa.MaLoai = l.MaLoai
                               INNER JOIN tblChatLieu cl ON qa.MaChatLieu = cl.MaChatLieu
                               ORDER BY qa.MaQuanAo";
                DataTable dt = dtBase.DocBang(sql);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }

                int soAnhTaoMoi = 0;

                // 4. Lặp qua từng sản phẩm để kiểm tra và sinh ảnh nếu còn thiếu
                foreach (DataRow row in dt.Rows)
                {
                    string maQuanAo = Convert.ToString(row["MaQuanAo"]).Trim();
                    string fileName = maQuanAo + ".jpg";
                    string binFilePath = Path.Combine(binImgDir, fileName);
                    string srcFilePath = coThuMucSource ? Path.Combine(srcImgDir, fileName) : null;

                    bool existsInBin = File.Exists(binFilePath);
                    bool existsInSrc = coThuMucSource && File.Exists(srcFilePath);

                    // Nếu đã có ở cả 2 nơi thì bỏ qua
                    if (existsInBin && (!coThuMucSource || existsInSrc))
                    {
                        continue;
                    }

                    // Nếu đã có ở bin nhưng thiếu ở source, copy sang
                    if (existsInBin && coThuMucSource && !existsInSrc)
                    {
                        try { File.Copy(binFilePath, srcFilePath, true); } catch { }
                        continue;
                    }

                    // Nếu đã có ở source nhưng thiếu ở bin, copy sang
                    if (!existsInBin && coThuMucSource && existsInSrc)
                    {
                        try { File.Copy(srcFilePath, binFilePath, true); } catch { }
                        continue;
                    }

                    // Chưa có ảnh -> Tiến hành vẽ ảnh Bitmap 500x600 chuẩn
                    string tenQuanAo = Convert.ToString(row["TenQuanAo"]).Trim();
                    string tenLoai = Convert.ToString(row["TenLoai"]).Trim();
                    string tenChatLieu = Convert.ToString(row["TenChatLieu"]).Trim();
                    string kichCo = Convert.ToString(row["KichCo"]).Trim();
                    string mauSac = Convert.ToString(row["MauSac"]).Trim();
                    double donGiaBan = 0;
                    if (row["DonGiaBan"] != DBNull.Value)
                    {
                        donGiaBan = Convert.ToDouble(row["DonGiaBan"]);
                    }

                    byte[] jpegBytes = VeAnhSanPham(maQuanAo, tenQuanAo, tenLoai, tenChatLieu, kichCo, mauSac, donGiaBan);

                    // Lưu lock-free bằng File.WriteAllBytes
                    File.WriteAllBytes(binFilePath, jpegBytes);
                    if (coThuMucSource && !string.IsNullOrEmpty(srcFilePath))
                    {
                        File.WriteAllBytes(srcFilePath, jpegBytes);
                    }

                    soAnhTaoMoi++;
                }

                // 5. Ghi log kết quả vào Logs/image_gen.log
                GhiLogImageGen(binImgDir, projectRootDir, coThuMucSource, soAnhTaoMoi, dt.Rows.Count);
            }
            catch (Exception ex)
            {
                GhiLogError(ex);
            }
        }

        private static byte[] VeAnhSanPham(string maQuanAo, string tenQuanAo, string tenLoai, string tenChatLieu, string kichCo, string mauSac, double donGiaBan)
        {
            using (Bitmap bmp = new Bitmap(500, 600))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    // Nền toàn bộ thẻ sản phẩm
                    g.Clear(Color.FromArgb(248, 250, 252));

                    // 1. Header (Navy #1E293B)
                    Rectangle headerRect = new Rectangle(0, 0, 500, 65);
                    using (SolidBrush headerBrush = new SolidBrush(Color.FromArgb(30, 41, 59)))
                    {
                        g.FillRectangle(headerBrush, headerRect);
                    }
                    // Đường chỉ đỏ hồng nhấn dưới header
                    using (Pen accentPen = new Pen(Color.FromArgb(225, 29, 72), 3))
                    {
                        g.DrawLine(accentPen, 0, 65, 500, 65);
                    }

                    using (Font headerFont = new Font("Arial", 16, FontStyle.Bold))
                    using (StringFormat sfHeader = new StringFormat())
                    {
                        sfHeader.Alignment = StringAlignment.Center;
                        sfHeader.LineAlignment = StringAlignment.Center;
                        sfHeader.Trimming = StringTrimming.EllipsisCharacter;
                        string headerText = string.Format("#{0}  •  {1}", maQuanAo, tenLoai.ToUpper());
                        g.DrawString(headerText, headerFont, Brushes.White, new RectangleF(10, 5, 480, 55), sfHeader);
                    }

                    // 2. Body - Silhouette & Size, Màu
                    Color mauSanPham = LayMauTheoTen(mauSac);
                    bool laSang = LaMauSang(mauSac);
                    Color mauChuTrenAo = laSang ? Color.FromArgb(30, 41, 59) : Color.White;

                    Rectangle bodyBounds = new Rectangle(50, 85, 400, 340);
                    using (SolidBrush brushQuanAo = new SolidBrush(mauSanPham))
                    {
                        VeHinhDangQuanAo(g, tenLoai, bodyBounds, brushQuanAo);
                    }

                    // Chữ trên thân sản phẩm: SIZE <KichCo> và Màu: <MauSac>
                    using (Font fontBody = new Font("Arial", 18, FontStyle.Bold))
                    using (SolidBrush brushChu = new SolidBrush(mauChuTrenAo))
                    using (StringFormat sfBody = new StringFormat())
                    {
                        sfBody.Alignment = StringAlignment.Center;
                        sfBody.LineAlignment = StringAlignment.Center;

                        float yOffset = 230;
                        if (tenLoai.IndexOf("Quần", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            yOffset = 155;
                        }
                        else if (tenLoai.IndexOf("Váy", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                 tenLoai.IndexOf("Đầm", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            yOffset = 250;
                        }

                        string sizeText = "SIZE " + kichCo;
                        string mauText = "Màu: " + mauSac;
                        g.DrawString(sizeText, fontBody, brushChu, new RectangleF(60, yOffset, 380, 32), sfBody);
                        g.DrawString(mauText, fontBody, brushChu, new RectangleF(60, yOffset + 38, 380, 32), sfBody);
                    }

                    // 3. Footer
                    Rectangle footerRect = new Rectangle(0, 440, 500, 160);
                    using (SolidBrush footBg = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(footBg, footerRect);
                    }
                    using (Pen divPen = new Pen(Color.FromArgb(226, 232, 240), 2))
                    {
                        g.DrawLine(divPen, 15, 440, 485, 440);
                    }

                    // Tên quần áo (Đậm, Navy đen)
                    using (Font fontTen = new Font("Arial", 15, FontStyle.Bold))
                    using (SolidBrush brushTen = new SolidBrush(Color.FromArgb(15, 23, 42)))
                    using (StringFormat sfTen = new StringFormat())
                    {
                        sfTen.Alignment = StringAlignment.Near;
                        sfTen.LineAlignment = StringAlignment.Near;
                        sfTen.Trimming = StringTrimming.EllipsisCharacter;
                        g.DrawString(tenQuanAo, fontTen, brushTen, new RectangleF(25, 452, 450, 46), sfTen);
                    }

                    // Chất liệu
                    using (Font fontChatLieu = new Font("Arial", 12, FontStyle.Regular))
                    using (SolidBrush brushChatLieu = new SolidBrush(Color.FromArgb(100, 116, 139)))
                    {
                        g.DrawString("Chất liệu: " + tenChatLieu, fontChatLieu, brushChatLieu, 25, 504);
                    }

                    // Đơn giá bán formatted as {0:N0} VNĐ in #E11D48
                    using (Font fontGia = new Font("Arial", 18, FontStyle.Bold))
                    using (SolidBrush brushGia = new SolidBrush(Color.FromArgb(225, 29, 72)))
                    {
                        string priceText = string.Format("{0:N0} VNĐ", donGiaBan);
                        g.DrawString(priceText, fontGia, brushGia, 25, 538);
                    }

                    // Viền bao ngoài thẻ
                    using (Pen borderPen = new Pen(Color.FromArgb(203, 213, 225), 1.5f))
                    {
                        g.DrawRectangle(borderPen, 0, 0, 499, 599);
                    }
                }

                // Xuất JPEG lock-free ra mảng byte
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
        }

        private static void VeHinhDangQuanAo(Graphics g, string tenLoai, Rectangle bounds, SolidBrush brush)
        {
            using (Pen strokePen = new Pen(Color.FromArgb(120, 30, 41, 59), 2f))
            using (Pen detailPen = new Pen(Color.FromArgb(90, 30, 41, 59), 1.5f))
            {
                if (tenLoai.IndexOf("Quần", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Vector Silhouette: Quần (2 legs, waistband, crotch)
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddLine(160, 95, 340, 95);     // Cạp quần trên
                        path.AddLine(340, 95, 355, 145);    // Hông phải
                        path.AddLine(355, 145, 335, 395);   // Gấu ngoài chân phải
                        path.AddLine(335, 395, 270, 395);   // Gấu quần chân phải
                        path.AddLine(270, 395, 250, 220);   // Đũng quần phải
                        path.AddLine(250, 220, 230, 395);   // Đũng quần trái
                        path.AddLine(230, 395, 165, 395);   // Gấu quần chân trái
                        path.AddLine(165, 395, 145, 145);   // Gấu ngoài chân trái
                        path.AddLine(145, 145, 160, 95);    // Hông trái về cạp
                        path.CloseFigure();

                        g.FillPath(brush, path);
                        g.DrawPath(strokePen, path);
                    }
                    // Đường may cạp quần và khóa kéo
                    g.DrawLine(detailPen, 160, 120, 340, 120);
                    g.DrawLine(detailPen, 250, 120, 250, 180);
                }
                else if (tenLoai.IndexOf("Váy", StringComparison.OrdinalIgnoreCase) >= 0 ||
                         tenLoai.IndexOf("Đầm", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Vector Silhouette: Váy (flared dress, scoop neckline, fitted waist, flared skirt)
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddLine(190, 95, 220, 95);     // Quai áo trái
                        path.AddBezier(220, 95, 240, 125, 260, 125, 280, 95); // Cổ áo
                        path.AddLine(280, 95, 310, 95);     // Quai áo phải
                        path.AddBezier(310, 95, 325, 130, 325, 145, 315, 175); // Nách phải
                        path.AddBezier(315, 175, 305, 205, 300, 215, 305, 225); // Eo phải
                        path.AddBezier(305, 225, 350, 300, 390, 360, 415, 395); // Tùng váy xòe phải
                        path.AddBezier(415, 395, 335, 415, 165, 415, 85, 395);  // Gấu váy cong
                        path.AddBezier(85, 395, 110, 360, 150, 300, 195, 225);  // Tùng váy xòe trái
                        path.AddBezier(195, 225, 200, 215, 195, 205, 185, 175); // Eo trái
                        path.AddBezier(185, 175, 175, 145, 175, 130, 190, 95);  // Nách trái
                        path.CloseFigure();

                        g.FillPath(brush, path);
                        g.DrawPath(strokePen, path);
                    }
                    // Thắt eo / đai váy
                    g.DrawBezier(detailPen, 195, 225, 230, 233, 270, 233, 305, 225);
                }
                else
                {
                    // Vector Silhouette: Áo (collar + 2 sleeves, torso, bottom hem)
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddLine(210, 95, 250, 115);    // Cổ áo V trái
                        path.AddLine(250, 115, 290, 95);    // Cổ áo V phải
                        path.AddLine(290, 95, 375, 125);    // Vai phải
                        path.AddLine(375, 125, 435, 195);   // Ống tay ngoài phải
                        path.AddLine(435, 195, 390, 235);   // Cửa tay phải
                        path.AddLine(390, 235, 345, 195);   // Nách phải
                        path.AddLine(345, 195, 345, 390);   // Sườn phải
                        path.AddLine(345, 390, 155, 390);   // Gấu áo
                        path.AddLine(155, 390, 155, 195);   // Sườn trái
                        path.AddLine(155, 195, 110, 235);   // Nách trái
                        path.AddLine(110, 235, 65, 195);    // Cửa tay trái
                        path.AddLine(65, 195, 125, 125);    // Ống tay ngoài trái
                        path.AddLine(125, 125, 210, 95);    // Vai trái
                        path.CloseFigure();

                        g.FillPath(brush, path);
                        g.DrawPath(strokePen, path);
                    }
                    // Đường cổ áo (collar)
                    using (GraphicsPath collarPath = new GraphicsPath())
                    {
                        collarPath.AddLine(210, 95, 240, 140);
                        collarPath.AddLine(240, 140, 250, 115);
                        collarPath.AddLine(250, 115, 260, 140);
                        collarPath.AddLine(260, 140, 290, 95);
                        g.DrawPath(detailPen, collarPath);
                    }
                }
            }
        }

        private static Color LayMauTheoTen(string mauSac)
        {
            if (string.IsNullOrEmpty(mauSac)) return Color.FromArgb(148, 163, 184);
            string m = mauSac.Trim().ToLowerInvariant();
            if (m.Contains("trắng") || m.Contains("trang")) return Color.FromArgb(245, 247, 250);
            if (m.Contains("đen") || m.Contains("den")) return Color.FromArgb(30, 30, 36);
            if (m.Contains("navy")) return Color.FromArgb(30, 58, 138);
            if (m.Contains("be") || m.Contains("kem")) return Color.FromArgb(235, 220, 195);
            if (m.Contains("xám") || m.Contains("xam") || m.Contains("khói") || m.Contains("khoi")) return Color.FromArgb(148, 163, 184);
            if (m.Contains("đỏ") || m.Contains("do")) return Color.FromArgb(153, 27, 27);
            if (m.Contains("rêu") || m.Contains("reu")) return Color.FromArgb(63, 98, 18);
            if (m.Contains("nâu") || m.Contains("nau")) return Color.FromArgb(120, 53, 15);
            if (m.Contains("hồng") || m.Contains("hong")) return Color.FromArgb(244, 180, 200);
            if (m.Contains("dương") || m.Contains("duong") || m.Contains("lam")) return Color.FromArgb(37, 99, 235);
            return Color.FromArgb(148, 163, 184);
        }

        private static bool LaMauSang(string mauSac)
        {
            if (string.IsNullOrEmpty(mauSac)) return false;
            string m = mauSac.Trim().ToLowerInvariant();
            return m.Contains("trắng") || m.Contains("trang") ||
                   m.Contains("be") || m.Contains("kem") ||
                   m.Contains("hồng") || m.Contains("hong");
        }

        private static void GhiLogImageGen(string binImgDir, string projectRootDir, bool coThuMucSource, int soAnhTaoMoi, int tongSo)
        {
            try
            {
                string logContent = string.Format("{0:yyyy-MM-dd HH:mm:ss} [INFO] TaoTuDong200AnhSanPham: Sinh mới {1} ảnh, tổng cộng {2} ảnh đã sẵn sàng trong thư mục Images\\QuanAo.\r\n",
                    DateTime.Now, soAnhTaoMoi, tongSo);

                string binLogDir = Path.Combine(LayThuMucUngDung(), "Logs");
                if (!Directory.Exists(binLogDir)) Directory.CreateDirectory(binLogDir);
                File.AppendAllText(Path.Combine(binLogDir, "image_gen.log"), logContent, Encoding.UTF8);

                if (coThuMucSource)
                {
                    string srcLogDir = Path.Combine(projectRootDir, "Logs");
                    if (!Directory.Exists(srcLogDir)) Directory.CreateDirectory(srcLogDir);
                    File.AppendAllText(Path.Combine(srcLogDir, "image_gen.log"), logContent, Encoding.UTF8);
                }
            }
            catch { }
        }

        private static void GhiLogError(Exception ex)
        {
            try
            {
                string logContent = string.Format("{0:yyyy-MM-dd HH:mm:ss} [ERROR] TaoTuDong200AnhSanPham error: {1}\r\n",
                    DateTime.Now, ex.Message);
                string binLogDir = Path.Combine(LayThuMucUngDung(), "Logs");
                if (!Directory.Exists(binLogDir)) Directory.CreateDirectory(binLogDir);
                File.AppendAllText(Path.Combine(binLogDir, "image_gen.log"), logContent, Encoding.UTF8);
            }
            catch { }
        }
    }
}
