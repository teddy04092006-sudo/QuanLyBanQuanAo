using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;

namespace QuanLyBanQuanAo.GUI
{
    /// <summary>
    /// Báo cáo doanh thu: lọc theo khoảng ngày, thống kê theo ngày / tháng / năm /
    /// nhân viên / loại thời trang / sản phẩm. Chỉ tính hóa đơn "Đã thanh toán".
    /// Lợi nhuận gộp = Thành tiền - (Số lượng x Đơn giá nhập).
    /// </summary>
    public partial class frmBaoCaoDoanhThu : Form
    {
        private readonly DataProcesser db = new DataProcesser();

        // Bảng đang hiển thị (dùng lại khi xuất Excel)
        private DataTable dtHienTai;
        private string[] tieuDeCot;
        private double tongSoHD;   // tổng số hóa đơn (đếm không trùng) của lần xem gần nhất

        public frmBaoCaoDoanhThu()
        {
            InitializeComponent();
            Theme.Apply(this);
        }

        // ================================================================ LOAD
        private void frmBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {
            gbDieuKien.Text = "Điều kiện báo cáo";

            // Các kiểu thống kê (thứ tự khớp với switch trong TaoCauLenh)
            cboKieu.Items.Clear();
            cboKieu.Items.AddRange(new object[]
            {
                "Theo ngày",
                "Theo tháng",
                "Theo năm",
                "Theo nhân viên",
                "Theo loại thời trang",
                "Theo sản phẩm"
            });
            cboKieu.SelectedIndex = 0;

            // Mặc định: từ đầu tháng đến hôm nay
            DateTime homNay = DateTime.Today;
            dtpTu.Value = new DateTime(homNay.Year, homNay.Month, 1);
            dtpDen.Value = homNay;

            // Tự căn bố cục theo kích thước form (form con được Dock = Fill trong frmMain)
            gbDieuKien.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dgvBaoCao.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.SizeChanged += delegate { CanChinhBoCuc(); };
            CanChinhBoCuc();

            // Gắn sự kiện (Designer chưa gắn)
            btnXem.Click += btnXem_Click;
            btnExcel.Click += btnExcel_Click;
            btnDong.Click += btnDong_Click;

            // Ô tổng hợp: căn phải, chữ to dễ nhìn
            foreach (TextBox t in new TextBox[] { txtSoHD, txtDoanhThu, txtLoiNhuan })
            {
                t.TextAlign = HorizontalAlignment.Right;
                t.Font = new Font("Arial", 10, FontStyle.Bold);
            }

            // Hiện luôn báo cáo mặc định khi mở form
            XemBaoCao();
        }

        // Căn khung điều kiện, lưới và nút Đóng vừa khít chiều ngang/dọc của form
        private void CanChinhBoCuc()
        {
            const int le = 35;
            int rong = this.ClientSize.Width - 2 * le;
            if (rong < 600) rong = 600;

            gbDieuKien.Left = le;
            gbDieuKien.Width = rong;

            dgvBaoCao.Left = le;
            dgvBaoCao.Width = rong;
            int cao = this.ClientSize.Height - dgvBaoCao.Top - 25;
            dgvBaoCao.Height = cao < 150 ? 150 : cao;

            btnDong.Left = le + rong - btnDong.Width;
        }

        // ================================================================ SỰ KIỆN NÚT
        private void btnXem_Click(object sender, EventArgs e)
        {
            XemBaoCao();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtHienTai == null || dtHienTai.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu. Hãy bấm \"Xem báo cáo\" trước khi xuất Excel!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo bảng riêng cho Excel (cột đều là string/double) rồi thêm dòng TỔNG ở cuối
            DataTable xuat = new DataTable();
            xuat.Columns.Add("Nhom", typeof(string));
            xuat.Columns.Add("SoHD", typeof(double));
            xuat.Columns.Add("SoLuong", typeof(double));
            xuat.Columns.Add("DoanhThu", typeof(double));
            xuat.Columns.Add("LoiNhuan", typeof(double));
            foreach (DataRow r in dtHienTai.Rows)
                xuat.Rows.Add(r["Nhom"].ToString(), Convert.ToDouble(r["SoHD"]), Convert.ToDouble(r["SoLuong"]),
                              Convert.ToDouble(r["DoanhThu"]), Convert.ToDouble(r["LoiNhuan"]));
            xuat.Rows.Add("TỔNG", tongSoHD, TongCot(dtHienTai, "SoLuong"),
                          TongCot(dtHienTai, "DoanhThu"), TongCot(dtHienTai, "LoiNhuan"));

            string tieuDe = "BÁO CÁO DOANH THU " + cboKieu.Text.ToUpper() +
                            " (" + dtpTu.Value.ToString("dd/MM/yyyy") + " - " + dtpDen.Value.ToString("dd/MM/yyyy") + ")";
            ExcelHelper.XuatBang(xuat, tieuDe, tieuDeCot, "BaoCaoDoanhThu");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ================================================================ XỬ LÝ CHÍNH
        private void XemBaoCao()
        {
            if (dtpTu.Value.Date > dtpDen.Value.Date)
            {
                MessageBox.Show("\"Từ ngày\" không được lớn hơn \"Đến ngày\"!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpTu.Focus();
                return;
            }
            if (cboKieu.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn kiểu thống kê!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKieu.Focus();
                return;
            }

            DateTime tu = dtpTu.Value.Date;
            DateTime den = dtpDen.Value.Date.AddDays(1);   // lấy trọn ngày cuối: NgayBan < ngày sau

            // 1) Bảng chi tiết theo kiểu thống kê
            string tenNhom;
            string sql = TaoCauLenh(cboKieu.SelectedIndex, out tenNhom);
            DataTable dt = db.DocBang(sql, DataProcesser.P("@tu", tu), DataProcesser.P("@den", den));

            dtHienTai = dt;
            tieuDeCot = new string[] { tenNhom, "Số hóa đơn", "Số lượng bán", "Doanh thu", "Lợi nhuận gộp" };

            dgvBaoCao.DataSource = dt;
            if (dgvBaoCao.Columns.Count >= 5)
            {
                for (int i = 0; i < tieuDeCot.Length; i++)
                    dgvBaoCao.Columns[i].HeaderText = tieuDeCot[i];
                for (int i = 1; i < 5; i++)
                    dgvBaoCao.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBaoCao.Columns["DoanhThu"].DefaultCellStyle.Format = "#,##0";
                dgvBaoCao.Columns["LoiNhuan"].DefaultCellStyle.Format = "#,##0";
                dgvBaoCao.Columns["SoHD"].DefaultCellStyle.Format = "#,##0";
                dgvBaoCao.Columns["SoLuong"].DefaultCellStyle.Format = "#,##0";
            }

            // 2) Ô tổng hợp (đếm hóa đơn không trùng trên toàn khoảng ngày)
            DataTable tong = db.DocBang(
                "SELECT COUNT(DISTINCT h.MaHDBan) AS SoHD, " +
                "ISNULL(SUM(ct.ThanhTien), 0) AS DoanhThu, " +
                "ISNULL(SUM(ct.ThanhTien - ct.SoLuong * q.DonGiaNhap), 0) AS LoiNhuan " +
                "FROM tblHDBan h " +
                "JOIN tblChiTietHDBan ct ON ct.MaHDBan = h.MaHDBan " +
                "JOIN tblQuanAo q ON q.MaQuanAo = ct.MaQuanAo " +
                "WHERE h.TrangThai = N'Đã thanh toán' AND h.NgayBan >= @tu AND h.NgayBan < @den",
                DataProcesser.P("@tu", tu), DataProcesser.P("@den", den));

            if (tong.Rows.Count > 0)
            {
                tongSoHD = Convert.ToDouble(tong.Rows[0]["SoHD"]);
                txtSoHD.Text = string.Format("{0:#,##0}", tongSoHD);
                txtDoanhThu.Text = string.Format("{0:#,##0}", Convert.ToDouble(tong.Rows[0]["DoanhThu"]));
                txtLoiNhuan.Text = string.Format("{0:#,##0}", Convert.ToDouble(tong.Rows[0]["LoiNhuan"]));
            }

            if (dt.Rows.Count == 0)
                MessageBox.Show("Không có hóa đơn nào đã thanh toán trong khoảng thời gian này.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Sinh câu SELECT theo kiểu thống kê. Luôn trả về 5 cột:
        /// Nhom, SoHD, SoLuong, DoanhThu, LoiNhuan.
        /// </summary>
        private string TaoCauLenh(int kieu, out string tenNhom)
        {
            string bieuThuc;                 // biểu thức nhóm (cột đầu)
            string joinThem = "";            // JOIN bổ sung nếu cần
            string orderBy;

            switch (kieu)
            {
                case 0: // theo ngày
                    tenNhom = "Ngày";
                    bieuThuc = "CONVERT(NVARCHAR(10), h.NgayBan, 103)";
                    orderBy = "MIN(h.NgayBan)";
                    break;
                case 1: // theo tháng
                    tenNhom = "Tháng";
                    bieuThuc = "CONVERT(NVARCHAR(7), h.NgayBan, 120)";   // yyyy-MM
                    orderBy = "MIN(h.NgayBan)";
                    break;
                case 2: // theo năm
                    tenNhom = "Năm";
                    bieuThuc = "CAST(YEAR(h.NgayBan) AS NVARCHAR(4))";
                    orderBy = "MIN(h.NgayBan)";
                    break;
                case 3: // theo nhân viên
                    tenNhom = "Nhân viên";
                    joinThem = "LEFT JOIN tblNhanVien n ON n.MaNhanVien = h.MaNhanVien ";
                    bieuThuc = "ISNULL(n.TenNhanVien, N'(Chưa phân công)')";
                    orderBy = "SUM(ct.ThanhTien) DESC";
                    break;
                case 4: // theo loại thời trang
                    tenNhom = "Loại thời trang";
                    joinThem = "JOIN tblLoaiThoiTrang l ON l.MaLoai = q.MaLoai ";
                    bieuThuc = "l.TenLoai";
                    orderBy = "SUM(ct.ThanhTien) DESC";
                    break;
                default: // theo sản phẩm
                    tenNhom = "Sản phẩm";
                    bieuThuc = "ct.MaQuanAo + N' - ' + q.TenQuanAo + N' (' + q.KichCo + N', ' + q.MauSac + N')'";
                    orderBy = "SUM(ct.ThanhTien) DESC";
                    break;
            }

            return "SELECT " + bieuThuc + " AS Nhom, " +
                   "COUNT(DISTINCT h.MaHDBan) AS SoHD, " +
                   "SUM(ct.SoLuong) AS SoLuong, " +
                   "SUM(ct.ThanhTien) AS DoanhThu, " +
                   "SUM(ct.ThanhTien - ct.SoLuong * q.DonGiaNhap) AS LoiNhuan " +
                   "FROM tblHDBan h " +
                   "JOIN tblChiTietHDBan ct ON ct.MaHDBan = h.MaHDBan " +
                   "JOIN tblQuanAo q ON q.MaQuanAo = ct.MaQuanAo " +
                   joinThem +
                   "WHERE h.TrangThai = N'Đã thanh toán' AND h.NgayBan >= @tu AND h.NgayBan < @den " +
                   "GROUP BY " + bieuThuc + " " +
                   "ORDER BY " + orderBy;
        }

        // Cộng một cột số của DataTable
        private static double TongCot(DataTable dt, string tenCot)
        {
            double s = 0;
            foreach (DataRow r in dt.Rows)
                if (r[tenCot] != DBNull.Value) s += Convert.ToDouble(r[tenCot]);
            return s;
        }
    }
}