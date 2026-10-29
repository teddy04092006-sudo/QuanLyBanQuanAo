using System.Data;

namespace QuanLyBanQuanAo.BLL
{
    /// <summary>Giỏ hàng dùng chung giữa form xem sản phẩm (frmCuaHang) và form đặt hàng (frmDatHang).</summary>
    public static class GioHang
    {
        /// <summary>Tăng lên mỗi khi đặt hàng thành công (tồn kho thay đổi) để form xem sản phẩm tự nạp lại.</summary>
        public static int PhienBanKho = 0;

        public static readonly DataTable Bang = TaoBang();

        private static DataTable TaoBang()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaQuanAo", typeof(string));
            dt.Columns.Add("TenQuanAo", typeof(string));
            dt.Columns.Add("KichCo", typeof(string));
            dt.Columns.Add("MauSac", typeof(string));
            dt.Columns.Add("SoLuong", typeof(int));
            dt.Columns.Add("DonGia", typeof(double));
            dt.Columns.Add("ThanhTien", typeof(double));
            return dt;
        }

        /// <summary>Số lượng của một mã hàng đang có trong giỏ.</summary>
        public static int SoLuongTrongGio(string maQuanAo)
        {
            foreach (DataRow r in Bang.Rows)
                if (r["MaQuanAo"].ToString() == maQuanAo) return System.Convert.ToInt32(r["SoLuong"]);
            return 0;
        }

        /// <summary>Tổng số sản phẩm trong giỏ.</summary>
        public static int TongSoLuong()
        {
            int tong = 0;
            foreach (DataRow r in Bang.Rows) tong += System.Convert.ToInt32(r["SoLuong"]);
            return tong;
        }

        public static void Them(string maQuanAo, string ten, string size, string mau, int soLuong, double donGia)
        {
            foreach (DataRow r in Bang.Rows)
            {
                if (r["MaQuanAo"].ToString() == maQuanAo)
                {
                    int moi = System.Convert.ToInt32(r["SoLuong"]) + soLuong;
                    r["SoLuong"] = moi;
                    r["ThanhTien"] = moi * donGia;
                    return;
                }
            }
            Bang.Rows.Add(maQuanAo, ten, size, mau, soLuong, donGia, soLuong * donGia);
        }
    }
}
