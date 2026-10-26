using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;

namespace QuanLyBanQuanAo.GUI
{
    /// <summary>
    /// Quản lý đơn hàng & hóa đơn: xem danh sách (kèm thông tin khách), lọc, xem chi tiết,
    /// xác nhận đơn khách đặt, hủy đơn (hoàn kho), in hóa đơn ra Excel, bán tại quầy.
    /// </summary>
    public class frmQuanLyDonHang : Form
    {
        private readonly DataProcesser db = new DataProcesser();
        private readonly Function func = new Function();

        private ComboBox cboTT, cboNV;
        private DateTimePicker dtpTu, dtpDen;
        private TextBox txtTim;
        private DataGridView dgvDon, dgvCT;
        private Label lblCho, lblInfo, lblTong;
        private Button btnXacNhan, btnHuy, btnIn, btnXemCK;
        private DataTable ctHienTai;

        public frmQuanLyDonHang()
        {
            this.Name = "frmQuanLyDonHang";
            this.Text = "Quản lý đơn hàng & hóa đơn";
            this.ClientSize = new Size(1000, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            BuildUI();
            this.Load += frmQuanLyDonHang_Load;
        }

        // ================================================================ giao diện
        private void BuildUI()
        {
            Label tieuDe = new Label();
            tieuDe.Text = "QUẢN LÝ ĐƠN HÀNG & HÓA ĐƠN";
            tieuDe.Font = new Font("Arial", 14, FontStyle.Bold);
            tieuDe.ForeColor = Color.Firebrick;
            tieuDe.Left = 10; tieuDe.Top = 8; tieuDe.Width = 430; tieuDe.Height = 28;
            Controls.Add(tieuDe);

            lblCho = new Label();
            lblCho.Font = new Font("Arial", 10, FontStyle.Bold);
            lblCho.Left = 450; lblCho.Top = 13; lblCho.Width = 400; lblCho.Height = 22;
            Controls.Add(lblCho);

            // ---- bộ lọc
            GroupBox gb = new GroupBox();
            gb.Text = "Lọc đơn hàng";
            gb.Left = 10; gb.Top = 40; gb.Width = 970; gb.Height = 62;
            gb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(gb);

            UI.Lbl(gb, "Trạng thái", 15, 22, 70);
            cboTT = UI.Cbo(gb, 90, 22, 130);
            cboTT.Items.AddRange(new object[] { "Tất cả", "Chờ xác nhận", "Đã thanh toán", "Đã hủy" });
            cboTT.SelectedIndex = 0;

            UI.Lbl(gb, "Từ ngày", 235, 22, 55);
            dtpTu = new DateTimePicker();
            dtpTu.Format = DateTimePickerFormat.Short;
            dtpTu.Left = 295; dtpTu.Top = 22; dtpTu.Width = 105;
            dtpTu.Value = new DateTime(DateTime.Today.Year, 1, 1);
            gb.Controls.Add(dtpTu);

            UI.Lbl(gb, "Đến", 410, 22, 30);
            dtpDen = new DateTimePicker();
            dtpDen.Format = DateTimePickerFormat.Short;
            dtpDen.Left = 445; dtpDen.Top = 22; dtpDen.Width = 105;
            dtpDen.Value = DateTime.Today;
            gb.Controls.Add(dtpDen);

            UI.Lbl(gb, "Tìm (mã/tên/SĐT)", 565, 22, 105);
            txtTim = UI.Txt(gb, 675, 22, 170, false);
            txtTim.KeyDown += delegate (object s, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) LocDon(); };
            UI.Btn(gb, "Lọc", 855, 19, 95, delegate { LocDon(); });

            // ---- danh sách đơn
            dgvDon = UI.Grid(this, 10, 110, 970, 230);
            dgvDon.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            lblInfo = new Label();
            lblInfo.Font = new Font("Arial", 9.5f, FontStyle.Bold);
            lblInfo.ForeColor = Color.DarkBlue;
            lblInfo.Left = 10; lblInfo.Top = 348; lblInfo.Width = 970; lblInfo.Height = 22;
            lblInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(lblInfo);

            // ---- chi tiết đơn
            dgvCT = UI.Grid(this, 10, 374, 970, 190);

            lblTong = new Label();
            lblTong.Font = new Font("Arial", 10, FontStyle.Bold);
            lblTong.Left = 10; lblTong.Top = 572; lblTong.Width = 850; lblTong.Height = 22;
            lblTong.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(lblTong);

            btnXemCK = UI.Btn(this, "Xem ảnh CK", 870, 568, 110, delegate { XemAnhChuyenKhoan(); });
            btnXemCK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXemCK.Enabled = false;

            // ---- nút thao tác
            Label l1 = UI.Lbl(this, "Nhân viên xử lý", 10, 618, 105);
            cboNV = UI.Cbo(this, 120, 618, 200);
            btnXacNhan = UI.Btn(this, "Xác nhận đơn", 335, 616, 115, delegate { XacNhanDon(); });
            btnHuy = UI.Btn(this, "Hủy đơn", 455, 616, 90, delegate { HuyDon(); });
            btnIn = UI.Btn(this, "In hóa đơn Excel", 550, 616, 130, delegate { InHoaDon(); });
            Button btnQuay = UI.Btn(this, "Bán tại quầy (lập HĐ mới)", 690, 616, 190, delegate { BanTaiQuay(); });
            Button btnDong = UI.Btn(this, "Đóng", 890, 616, 90, delegate { Close(); });
            foreach (Control c in new Control[] { l1, cboNV, btnXacNhan, btnHuy, btnIn, btnQuay })
                c.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDong.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        }

        // ================================================================ nạp dữ liệu
        private void frmQuanLyDonHang_Load(object sender, EventArgs e)
        {
            func.FillCombo("SELECT MaNhanVien, MaNhanVien + N' - ' + TenNhanVien AS Ten FROM tblNhanVien ORDER BY MaNhanVien",
                cboNV, "MaNhanVien", "Ten");

            // có đơn chờ xác nhận thì mặc định lọc đúng các đơn đó cho admin xử lý ngay
            int cho;
            int.TryParse(func.GetFieldValues("SELECT COUNT(*) FROM tblHDBan WHERE TrangThai = N'Chờ xác nhận'"), out cho);
            cboTT.SelectedIndex = cho > 0 ? 1 : 0;

            LocDon();
            cboTT.SelectedIndexChanged += delegate { LocDon(); };
            dgvDon.SelectionChanged += delegate { HienChiTiet(); };
        }

        private void CapNhatSoDonCho()
        {
            int n;
            int.TryParse(func.GetFieldValues("SELECT COUNT(*) FROM tblHDBan WHERE TrangThai = N'Chờ xác nhận'"), out n);
            if (n > 0) { lblCho.Text = "Có " + n + " đơn đang chờ xác nhận!"; lblCho.ForeColor = Color.Red; }
            else { lblCho.Text = "Không có đơn nào chờ xác nhận"; lblCho.ForeColor = Color.DarkGreen; }
        }

        private void LocDon()
        {
            if (dtpTu.Value.Date > dtpDen.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "SELECT h.MaHDBan, h.NgayBan, k.TenKhach, k.DienThoai, k.DiaChi, " +
                         "ISNULL(n.TenNhanVien, N'(Chưa phân công)') AS TenNV, h.HinhThucThanhToan, h.TrangThai, h.TongTien, h.GhiChu, h.AnhChuyenKhoan " +
                         "FROM tblHDBan h INNER JOIN tblKhach k ON h.MaKhach = k.MaKhach " +
                         "LEFT JOIN tblNhanVien n ON h.MaNhanVien = n.MaNhanVien " +
                         "WHERE h.NgayBan >= @tu AND h.NgayBan < @den";
            List<SqlParameter> ps = new List<SqlParameter>();
            ps.Add(DataProcesser.P("@tu", dtpTu.Value.Date));
            ps.Add(DataProcesser.P("@den", dtpDen.Value.Date.AddDays(1)));
            if (cboTT.SelectedIndex > 0)
            {
                sql += " AND h.TrangThai = @tt";
                ps.Add(DataProcesser.P("@tt", cboTT.Text));
            }
            string tk = txtTim.Text.Trim();
            if (tk != "")
            {
                sql += " AND (h.MaHDBan LIKE @k OR k.TenKhach LIKE @k OR k.DienThoai LIKE @k)";
                ps.Add(DataProcesser.P("@k", "%" + tk + "%"));
            }
            sql += " ORDER BY h.NgayBan DESC";

            dgvDon.DataSource = db.DocBang(sql, ps.ToArray());
            UI.Headers(dgvDon, "Mã hóa đơn", "Ngày bán", "Khách hàng", "Điện thoại", "Địa chỉ",
                "Nhân viên xử lý", "Thanh toán", "Trạng thái", "Tổng tiền (VNĐ)");
            UI.Format(dgvDon, "dd/MM/yyyy HH:mm", 1);
            UI.Format(dgvDon, "N0", 8);
            if (dgvDon.Columns.Count > 9) dgvDon.Columns[9].Visible = false;   // cột Ghi chú (hiện ở dòng thông tin)
            if (dgvDon.Columns.Count > 10) dgvDon.Columns[10].Visible = false; // cột tên file ảnh chuyển khoản

            foreach (DataGridViewRow r in dgvDon.Rows)
            {
                string t = Convert.ToString(r.Cells[7].Value);
                if (t == "Chờ xác nhận") r.DefaultCellStyle.BackColor = Color.FromArgb(255, 236, 200);
                else if (t == "Đã hủy") r.DefaultCellStyle.ForeColor = Color.Gray;
            }

            CapNhatSoDonCho();
            HienChiTiet();
        }

        private void HienChiTiet()
        {
            DataGridViewRow r = dgvDon.CurrentRow;
            if (r == null || dgvDon.Rows.Count == 0 || r.Cells.Count < 9)
            {
                ctHienTai = null;
                dgvCT.DataSource = null;
                lblInfo.Text = "Chọn một đơn ở bảng trên để xem chi tiết.";
                lblTong.Text = "";
                btnXacNhan.Enabled = btnHuy.Enabled = btnIn.Enabled = btnXemCK.Enabled = false;
                return;
            }

            string ma = Convert.ToString(r.Cells[0].Value);
            ctHienTai = db.DocBang("SELECT ct.MaQuanAo, qa.TenQuanAo, qa.KichCo, qa.MauSac, ct.SoLuong, ct.DonGia, ct.GiamGia, ct.ThanhTien " +
                                   "FROM tblChiTietHDBan ct INNER JOIN tblQuanAo qa ON ct.MaQuanAo = qa.MaQuanAo WHERE ct.MaHDBan = @m",
                DataProcesser.P("@m", ma));
            dgvCT.DataSource = ctHienTai;
            UI.Headers(dgvCT, "Mã hàng", "Tên quần áo", "Size", "Màu", "Số lượng", "Đơn giá", "Giảm giá %", "Thành tiền");
            UI.Format(dgvCT, "N0", 5, 7);

            string gc = Convert.ToString(r.Cells[9].Value);
            lblInfo.Text = "Đơn " + ma + "  |  Khách: " + Convert.ToString(r.Cells[2].Value) +
                           "  |  ĐT: " + Convert.ToString(r.Cells[3].Value) +
                           "  |  Địa chỉ: " + Convert.ToString(r.Cells[4].Value) +
                           (gc != "" ? "  |  Ghi chú: " + gc : "");

            double tong = Convert.ToDouble(r.Cells[8].Value);
            lblTong.Text = "Tổng tiền: " + tong.ToString("N0") + " VNĐ  -  " + func.DocSoThanhChu((long)Math.Round(tong));

            string tt = Convert.ToString(r.Cells[7].Value);
            btnXacNhan.Enabled = tt == "Chờ xác nhận";
            btnHuy.Enabled = tt != "Đã hủy";
            btnIn.Enabled = true;
            btnXemCK.Enabled = r.Cells.Count > 10 && Convert.ToString(r.Cells[10].Value) != "";
        }

        // ================================================================ thao tác
        private void XacNhanDon()
        {
            DataGridViewRow r = dgvDon.CurrentRow;
            if (r == null) return;
            string ma = Convert.ToString(r.Cells[0].Value);

            string nv = (cboNV.SelectedIndex >= 0 && cboNV.SelectedValue is string) ? (string)cboNV.SelectedValue : null;
            if (nv == null)
            {
                MessageBox.Show("Hãy chọn \"Nhân viên xử lý\" trước khi xác nhận đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNV.Focus();
                return;
            }
            if (MessageBox.Show("Xác nhận đơn " + ma + " của khách " + Convert.ToString(r.Cells[2].Value) +
                                "?\nĐơn sẽ chuyển sang \"Đã thanh toán\" và tính vào doanh thu.", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (db.CapNhatDuLieu("UPDATE tblHDBan SET TrangThai = N'Đã thanh toán', MaNhanVien = @nv " +
                                 "WHERE MaHDBan = @m AND TrangThai = N'Chờ xác nhận'",
                    DataProcesser.P("@nv", nv), DataProcesser.P("@m", ma)))
            {
                MessageBox.Show("Đã xác nhận đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LocDon();
            }
        }

        private void HuyDon()
        {
            DataGridViewRow r = dgvDon.CurrentRow;
            if (r == null) return;
            string ma = Convert.ToString(r.Cells[0].Value);
            if (MessageBox.Show("Hủy đơn " + ma + "?\nSố lượng hàng sẽ được cộng trả lại kho.", "Xác nhận hủy",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            string loi;
            bool ok = db.ThucThiGiaoDich(delegate (GiaoDich gd)
            {
                int n = gd.Chay("UPDATE tblHDBan SET TrangThai = N'Đã hủy' WHERE MaHDBan = @m AND TrangThai <> N'Đã hủy'",
                    DataProcesser.P("@m", ma));
                if (n == 0) throw new Exception("Đơn này đã bị hủy trước đó.");
                gd.Chay("UPDATE qa SET qa.SoLuong = qa.SoLuong + ct.SoLuong " +
                        "FROM tblQuanAo qa INNER JOIN tblChiTietHDBan ct ON qa.MaQuanAo = ct.MaQuanAo WHERE ct.MaHDBan = @m",
                    DataProcesser.P("@m", ma));
            }, out loi);

            if (!ok) { MessageBox.Show(loi, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            MessageBox.Show("Đã hủy đơn và hoàn trả hàng về kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LocDon();
        }

        /// <summary>Mở ảnh chuyển khoản mà khách đã tải lên cho đơn đang chọn.</summary>
        private void XemAnhChuyenKhoan()
        {
            DataGridViewRow r = dgvDon.CurrentRow;
            if (r == null || r.Cells.Count < 11) return;
            string ten = Convert.ToString(r.Cells[10].Value);
            string duongDan = System.IO.Path.Combine(Application.StartupPath, "Images", "ChuyenKhoan", ten);
            if (ten == "" || !System.IO.File.Exists(duongDan))
            {
                MessageBox.Show("Không tìm thấy file ảnh chuyển khoản của đơn này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (Form f = new Form())
            {
                f.Text = "Ảnh chuyển khoản - đơn " + Convert.ToString(r.Cells[0].Value) +
                         " - cần thu " + Convert.ToDouble(r.Cells[8].Value).ToString("N0") + " VNĐ";
                f.ClientSize = new Size(560, 720);
                f.StartPosition = FormStartPosition.CenterParent;
                PictureBox pic = new PictureBox();
                pic.Dock = DockStyle.Fill;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Image = Image.FromStream(new System.IO.MemoryStream(System.IO.File.ReadAllBytes(duongDan)));
                f.Controls.Add(pic);
                f.ShowDialog(this);
            }
        }

        private void InHoaDon()
        {
            DataGridViewRow r = dgvDon.CurrentRow;
            if (r == null || ctHienTai == null) return;
            double tong = Convert.ToDouble(r.Cells[8].Value);
            ExcelHelper.InHoaDon(Convert.ToString(r.Cells[0].Value), Convert.ToDateTime(r.Cells[1].Value),
                Convert.ToString(r.Cells[5].Value), Convert.ToString(r.Cells[2].Value), Convert.ToString(r.Cells[3].Value),
                Convert.ToString(r.Cells[4].Value), Convert.ToString(r.Cells[6].Value),
                ctHienTai, tong, func.DocSoThanhChu((long)Math.Round(tong)));
        }

        private void BanTaiQuay()
        {
            using (frmHoaDonBan f = new frmHoaDonBan())
            {
                f.ShowDialog(this);
            }
            LocDon();
        }
    }
}
