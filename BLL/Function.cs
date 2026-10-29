using System;
using System.Data;
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
    }
}
