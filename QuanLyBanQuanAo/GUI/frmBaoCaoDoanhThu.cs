using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;

namespace QuanLyBanQuanAo.GUI
{
    /// <summary>Báo cáo doanh thu: lọc theo khoảng ngày, thống kê theo ngày/tháng/nhân viên/loại/sản phẩm.</summary>
    public class frmBaoCaoDoanhThu : Form
    {
        private readonly DataProcesser db = new DataProcesser();

        private DateTimePicker dtpTu, dtpDen;
        private ComboBox cboKieu;
        private DataGridView dgv;
        private TextBox txtSoHD, txtDoanhThu, txtLoiNhuan;

        private string[] headersHienTai = new string[0];
        private string tenKieuHienTai = "";

        // Chỉ tính hóa đơn "Đã thanh toán" (bỏ hóa đơn đã hủy / đơn chờ xác nhận)
        private const string LOC = " h.TrangThai = N'Đã thanh toán' AND h.NgayBan >= @tu AND h.NgayBan < @den ";

        public frmBaoCaoDoanhThu()
        {
            this.Name = "frmBaoCaoDoanhThu";
            this.Text = "Báo cáo doanh thu";
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            BuildUI();
            this.Load += delegate { XemBaoCao(); cboKieu.SelectedIndexChanged += delegate { XemBaoCao(); }; };
        }

        private void BuildUI()
        {
            Label tieuDe = new Label();
            tieuDe.Text = "BÁO CÁO DOANH THU";
            tieuDe.Font = new Font("Arial", 14, FontStyle.Bold);
            tieuDe.ForeColor = Color.Firebrick;
            tieuDe.Left = 10; tieuDe.Top = 8; tieuDe.Width = 500; tieuDe.Height = 28;
            Controls.Add(tieuDe);

            GroupBox gb = new GroupBox();
            gb.Text = "Điều kiện thống kê";
            gb.Left = 10; gb.Top = 42; gb.Width = 880; gb.Height = 62;
            gb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(gb);

            UI.Lbl(gb, "Từ ngày", 15, 22, 60);
            dtpTu = new DateTimePicker();
            dtpTu.Format = DateTimePickerFormat.Short;
            dtpTu.Left = 80; dtpTu.Top = 22; dtpTu.Width = 120;
            dtpTu.Value = new DateTime(DateTime.Today.Year, 1, 1);
            gb.Controls.Add(dtpTu);

            UI.Lbl(gb, "Đến ngày", 215, 22, 65);
            dtpDen = new DateTimePicker();
            dtpDen.Format = DateTimePickerFormat.Short;
            dtpDen.Left = 285; dtpDen.Top = 22; dtpDen.Width = 120;
            dtpDen.Value = DateTime.Today;
            gb.Controls.Add(dtpDen);

            UI.Lbl(gb, "Thống kê", 420, 22, 65);
            cboKieu = UI.Cbo(gb, 490, 22, 190);
            cboKieu.Items.AddRange(new object[] {
                "Theo ngày", "Theo tháng", "Theo nhân viên", "Theo loại thời trang", "Theo sản phẩm (bán chạy)" });
            cboKieu.SelectedIndex = 0;

            UI.Btn(gb, "Xem báo cáo", 700, 19, 95, delegate { XemBaoCao(); });
            UI.Btn(gb, "Xuất Excel", 800, 19, 70, delegate { XuatExcel(); });

            UI.Lbl(this, "Số hóa đơn", 10, 112, 80);
            txtSoHD = UI.Txt(this, 95, 112, 80, true);
            UI.Lbl(this, "Doanh thu", 195, 112, 70);
            txtDoanhThu = UI.Txt(this, 270, 112, 140, true);
            UI.Lbl(this, "Lợi nhuận gộp", 430, 112, 95);
            txtLoiNhuan = UI.Txt(this, 530, 112, 140, true);
            txtDoanhThu.Font = new Font("Arial", 10, FontStyle.Bold);
            txtLoiNhuan.Font = new Font("Arial", 10, FontStyle.Bold);

            Button btnDong = UI.Btn(this, "Đóng", 790, 109, 100, delegate { Close(); });
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            dgv = UI.Grid(this, 10, 150, 880, ClientSize.Height - 160);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmBaoCaoDoanhThu
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "frmBaoCaoDoanhThu";
            this.Load += new System.EventHandler(this.frmBaoCaoDoanhThu_Load);
            this.ResumeLayout(false);

        }

        private void frmBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {

        }

        // Mỗi lần gọi DocBang phải tạo tham số mới
        private SqlParameter[] ThamSo()
        {
            return new SqlParameter[] {
                DataProcesser.P("@tu", dtpTu.Value.Date),
                DataProcesser.P("@den", dtpDen.Value.Date.AddDays(1))
            };
        }

        private void XemBaoCao()
        {
            if (dtpTu.Value.Date > dtpDen.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tuBang = " FROM tblHDBan h ";
            string tuBangCT = " FROM tblHDBan h INNER JOIN tblChiTietHDBan ct ON h.MaHDBan = ct.MaHDBan " +
                              "INNER JOIN tblQuanAo qa ON ct.MaQuanAo = qa.MaQuanAo ";
            string sql;
            int[] cotTien;
            int cotNgay = -1;

            switch (cboKieu.SelectedIndex)
            {
                case 1:
                    sql = "SELECT YEAR(h.NgayBan) AS Nam, MONTH(h.NgayBan) AS Thang, COUNT(*) AS SoHD, SUM(h.TongTien) AS DoanhThu" +
                          tuBang + "WHERE" + LOC +
                          "GROUP BY YEAR(h.NgayBan), MONTH(h.NgayBan) ORDER BY YEAR(h.NgayBan), MONTH(h.NgayBan)";
                    headersHienTai = new string[] { "Năm", "Tháng", "Số hóa đơn", "Doanh thu (VNĐ)" };
                    cotTien = new int[] { 3 };
                    break;
                case 2:
                    sql = "SELECT ISNULL(n.TenNhanVien, N'(Đặt hàng online)') AS TenNV, COUNT(*) AS SoHD, SUM(h.TongTien) AS DoanhThu" +
                          tuBang + "LEFT JOIN tblNhanVien n ON h.MaNhanVien = n.MaNhanVien WHERE" + LOC +
                          "GROUP BY ISNULL(n.TenNhanVien, N'(Đặt hàng online)') ORDER BY SUM(h.TongTien) DESC";
                    headersHienTai = new string[] { "Nhân viên", "Số hóa đơn", "Doanh thu (VNĐ)" };
                    cotTien = new int[] { 2 };
                    break;
                case 3:
                    sql = "SELECT l.TenLoai, SUM(ct.SoLuong) AS SoLuongBan, SUM(ct.ThanhTien) AS DoanhThu" +
                          tuBangCT + "INNER JOIN tblLoaiThoiTrang l ON qa.MaLoai = l.MaLoai WHERE" + LOC +
                          "GROUP BY l.TenLoai ORDER BY SUM(ct.ThanhTien) DESC";
                    headersHienTai = new string[] { "Loại thời trang", "Số lượng bán", "Doanh thu (VNĐ)" };
                    cotTien = new int[] { 2 };
                    break;
                case 4:
                    sql = "SELECT qa.MaQuanAo, qa.TenQuanAo, SUM(ct.SoLuong) AS SoLuongBan, SUM(ct.ThanhTien) AS DoanhThu" +
                          tuBangCT + "WHERE" + LOC +
                          "GROUP BY qa.MaQuanAo, qa.TenQuanAo ORDER BY SUM(ct.SoLuong) DESC, SUM(ct.ThanhTien) DESC";
                    headersHienTai = new string[] { "Mã hàng", "Tên quần áo", "Số lượng bán", "Doanh thu (VNĐ)" };
                    cotTien = new int[] { 3 };
                    break;
                default:
                    sql = "SELECT CONVERT(date, h.NgayBan) AS Ngay, COUNT(*) AS SoHD, SUM(h.TongTien) AS DoanhThu" +
                          tuBang + "WHERE" + LOC +
                          "GROUP BY CONVERT(date, h.NgayBan) ORDER BY CONVERT(date, h.NgayBan)";
                    headersHienTai = new string[] { "Ngày", "Số hóa đơn", "Doanh thu (VNĐ)" };
                    cotTien = new int[] { 2 };
                    cotNgay = 0;
                    break;
            }
            tenKieuHienTai = cboKieu.Text;

            dgv.DataSource = db.DocBang(sql, ThamSo());
            UI.Headers(dgv, headersHienTai);
            UI.Format(dgv, "N0", cotTien);
            if (cotNgay >= 0) UI.Format(dgv, "dd/MM/yyyy", cotNgay);

            // ---- tổng hợp
            DataTable t1 = db.DocBang("SELECT COUNT(*) AS SoHD, ISNULL(SUM(h.TongTien), 0) AS DoanhThu" + tuBang + "WHERE" + LOC, ThamSo());
            DataTable t2 = db.DocBang("SELECT ISNULL(SUM(ct.ThanhTien - ct.SoLuong * qa.DonGiaNhap), 0) AS LoiNhuan" + tuBangCT + "WHERE" + LOC, ThamSo());

            if (t1.Rows.Count > 0)
            {
                txtSoHD.Text = Convert.ToInt32(t1.Rows[0]["SoHD"]).ToString("N0");
                txtDoanhThu.Text = Convert.ToDouble(t1.Rows[0]["DoanhThu"]).ToString("N0");
            }
            else { txtSoHD.Text = "0"; txtDoanhThu.Text = "0"; }
            txtLoiNhuan.Text = t2.Rows.Count > 0 ? Convert.ToDouble(t2.Rows[0]["LoiNhuan"]).ToString("N0") : "0";
        }

        private void XuatExcel()
        {
            string tieuDe = "BÁO CÁO DOANH THU (" + tenKieuHienTai.ToUpper() + ") TỪ " +
                            dtpTu.Value.ToString("dd/MM/yyyy") + " ĐẾN " + dtpDen.Value.ToString("dd/MM/yyyy");
            ExcelHelper.XuatBang((DataTable)dgv.DataSource, tieuDe, headersHienTai, "DoanhThu");
        }
    }
}
