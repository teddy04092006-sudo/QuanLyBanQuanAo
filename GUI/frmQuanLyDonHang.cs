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
    /// Control được kéo thả trong Designer (chỉ cần đặt Name); Text/Font/thuộc tính/bố cục nằm trong code.
    /// </summary>
    public partial class frmQuanLyDonHang : Form
    {
        private readonly DataProcesser db = new DataProcesser();
        private readonly Function func = new Function();
        private DataTable ctHienTai;

        public frmQuanLyDonHang()
        {
            InitializeComponent();
            CauHinhGiaoDien();
            GanSuKien();
            this.SizeChanged += delegate { CanChinhBoCuc(); };
            Theme.Apply(this);
        }

        // ================================================================ CẤU HÌNH GIAO DIỆN
        private void CauHinhGiaoDien()
        {
            // ----- Form
            this.Text = "Quản lý đơn hàng & hóa đơn";
            this.StartPosition = FormStartPosition.CenterScreen;

            // ----- Tiêu đề và các dòng thông báo
            lblTieuDe.Text = "QUẢN LÝ ĐƠN HÀNG & HÓA ĐƠN";
            lblTieuDe.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.Firebrick;
            lblTieuDe.AutoSize = true;
            lblTieuDe.UseMnemonic = false;   // để dấu & hiển thị đúng, không bị coi là phím tắt

            lblCho.Text = "";
            lblCho.Font = new Font("Arial", 10, FontStyle.Bold);
            lblCho.AutoSize = true;

            lblInfo.Text = "";
            lblInfo.Font = new Font("Arial", 9.5f, FontStyle.Bold);
            lblInfo.ForeColor = Color.DarkBlue;
            lblInfo.AutoSize = false;
            lblInfo.Height = lblInfo.Font.Height + 8;

            lblTong.Text = "";
            lblTong.Font = new Font("Arial", 10, FontStyle.Bold);
            lblTong.AutoSize = false;
            lblTong.Height = lblTong.Font.Height + 8;

            // ----- Bộ lọc
            gbLoc.Text = "Lọc đơn hàng";

            cboTT.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTT.Items.Clear();
            cboTT.Items.AddRange(new object[] { "Tất cả", "Chờ xác nhận", "Đã thanh toán", "Đã hủy" });

            dtpTu.Format = DateTimePickerFormat.Short;
            dtpDen.Format = DateTimePickerFormat.Short;

            btnLoc.Text = "Lọc";

            // ----- Hàng nút dưới cùng
            cboNV.DropDownStyle = ComboBoxStyle.DropDownList;
            btnXemCK.Text = "Xem ảnh CK";
            btnXemCK.Enabled = false;
            btnXacNhan.Text = "Xác nhận đơn";
            btnHuy.Text = "Hủy đơn";
            btnIn.Text = "In hóa đơn Excel";
            btnBanQuay.Text = "Bán tại quầy (lập HĐ mới)";
            btnDong.Text = "Đóng";

            // Nút cao bằng nhau, rộng vừa chữ
            foreach (Button b in new Button[] { btnLoc, btnXemCK, btnXacNhan, btnHuy, btnIn, btnBanQuay, btnDong })
            {
                b.Height = cboNV.Height + 6;
                b.Width = TextRenderer.MeasureText(b.Text, b.Font).Width + b.Height;
            }

            // ----- Hai lưới
            CauHinhLuoi(dgvDon);
            CauHinhLuoi(dgvCT);
        }

        private static void CauHinhLuoi(DataGridView g)
        {
            g.ReadOnly = true;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.MultiSelect = false;
            g.RowHeadersVisible = false;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.BackgroundColor = Color.White;
        }

        // Số dòng muốn hiển thị gọn trong mỗi lưới (đổi số này nếu muốn lưới cao/thấp hơn)
        private const int SoDongDon = 6;
        private const int SoDongCT = 6;

        /// <summary>
        /// Căn lại toàn bộ bố cục theo kích thước form hiện tại (form được Dock = Fill trong frmMain).
        /// Không dùng số pixel cố định nên chạy đúng ở mọi mức scale màn hình.
        /// Các khối xếp từ trên xuống dưới, sát nhau: lọc -> lưới đơn -> thông tin -> lưới chi tiết -> tổng tiền -> hàng nút.
        /// </summary>
        private void CanChinhBoCuc()
        {
            int le = gbLoc.Left;                       // lề trái/phải = lề đã kéo ngoài Designer
            int h = btnDong.Height;                    // chiều cao chuẩn của 1 hàng control
            int gap = Math.Max(6, h / 4);
            int rong = this.ClientSize.Width - 2 * le;
            if (rong < 500) rong = 500;

            // ---------- Khung lọc: xếp "nhãn + ô" nối tiếp nhau trên 1 hàng
            // (chữ tiêu đề GroupBox vẽ nửa trên mép khung -> chừa chỗ dưới tiêu đề form, và hàng control nằm dưới chữ đó)
            int chuGb = gbLoc.Font.Height;
            gbLoc.Top = lblTieuDe.Bottom + chuGb / 2 + gap;
            gbLoc.Left = le;
            gbLoc.Width = rong;
            int dongLoc = chuGb + gap;                       // y của hàng control bên trong khung
            gbLoc.Height = dongLoc + h + gap * 2;

            int sbw = SystemInformation.VerticalScrollBarWidth;       // bề rộng mũi tên xổ xuống
            int rongNgay = TextRenderer.MeasureText("30/12/2026", dtpTu.Font).Width + sbw + gap * 3;
            int rongTT = TextRenderer.MeasureText("Đã thanh toán", cboTT.Font).Width + sbw + gap * 3;

            int x = gap;
            x = XepCap(gbLoc, "Trạng thái", cboTT, x, dongLoc, h, rongTT, gap);
            x = XepCap(gbLoc, "Từ ngày", dtpTu, x, dongLoc, h, rongNgay, gap);
            x = XepCap(gbLoc, "Đến", dtpDen, x, dongLoc, h, rongNgay, gap);
            x = XepCap(gbLoc, "Tìm (mã/tên/SĐT)", txtTim, x, dongLoc, h, txtTim.Font.Height * 10, gap);
            btnLoc.Left = gbLoc.ClientSize.Width - btnLoc.Width - gap;
            btnLoc.Top = dongLoc;

            // ---------- Hai lưới: hiển thị gọn ~6 dòng, nếu form thấp quá thì co lại (có thanh cuộn)
            int dauDon = gbLoc.Bottom + gap;
            int danhCho = this.ClientSize.Height - dauDon - lblInfo.Height - lblTong.Height - h - gap * 5;
            if (danhCho < 200) danhCho = 200;

            int caoDonMong = dgvDon.ColumnHeadersHeight + dgvDon.RowTemplate.Height * SoDongDon + 4;
            int caoCTMong = dgvCT.ColumnHeadersHeight + dgvCT.RowTemplate.Height * SoDongCT + 4;
            int caoDon, caoCT;
            if (caoDonMong + caoCTMong <= danhCho) { caoDon = caoDonMong; caoCT = caoCTMong; }
            else { caoDon = danhCho * 45 / 100; caoCT = danhCho - caoDon; }

            dgvDon.SetBounds(le, dauDon, rong, caoDon);
            lblInfo.SetBounds(le, dgvDon.Bottom + gap / 2, rong, lblInfo.Height);
            dgvCT.SetBounds(le, lblInfo.Bottom + gap / 2, rong, caoCT);

            // ---------- Dòng tổng tiền + nút xem ảnh chuyển khoản
            lblTong.Top = dgvCT.Bottom + gap;
            lblTong.Left = le;
            btnXemCK.Left = le + rong - btnXemCK.Width;
            btnXemCK.Top = lblTong.Top + (lblTong.Height - btnXemCK.Height) / 2;
            lblTong.Width = btnXemCK.Left - le - gap;

            // ---------- Hàng nút dưới cùng
            int hangNut = Math.Max(lblTong.Bottom, btnXemCK.Bottom) + gap;
            int xx = XepCap(this, "Nhân viên xử lý", cboNV, le, hangNut, h, cboNV.Font.Height * 14, gap);
            foreach (Button b in new Button[] { btnXacNhan, btnHuy, btnIn, btnBanQuay })
            {
                b.Left = xx;
                b.Top = hangNut;
                xx = b.Right + gap;
            }
            btnDong.Left = le + rong - btnDong.Width;
            btnDong.Top = hangNut;
        }

        /// <summary>
        /// Đặt 1 cặp "nhãn + ô nhập" trên cùng hàng: nhãn (tìm theo chữ, không cần Name) đứng trước,
        /// ô đứng ngay sau, cả hai căn giữa theo chiều cao h. Trả về vị trí x để đặt cặp tiếp theo.
        /// </summary>
        private int XepCap(Control cha, string chuNhan, Control o, int x, int top, int h, int rongO, int gap)
        {
            Label l = TimNhan(cha, chuNhan);
            if (l != null)
            {
                l.AutoSize = false;
                l.UseMnemonic = false;
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.Height = h;
                l.Width = TextRenderer.MeasureText(l.Text, l.Font).Width + gap;
                l.Left = x;
                l.Top = top;
                x = l.Right;
            }
            o.Width = rongO;
            o.Left = x;
            o.Top = top + (h - o.Height) / 2;
            return o.Right + gap * 2;
        }

        // Tìm 1 Label (không đặt Name) trong container theo nội dung chữ
        private Label TimNhan(Control cha, string text)
        {
            foreach (Control c in cha.Controls)
            {
                Label l = c as Label;
                if (l != null && l.Text == text) return l;
            }
            return null;
        }

        // Gắn sự kiện bằng code (có -= trước để không bị gắn trùng nếu Designer đã gắn)
        private void GanSuKien()
        {
            this.Load -= frmQuanLyDonHang_Load; this.Load += frmQuanLyDonHang_Load;
            btnLoc.Click -= btnLoc_Click; btnLoc.Click += btnLoc_Click;
            txtTim.KeyDown -= txtTim_KeyDown; txtTim.KeyDown += txtTim_KeyDown;
            btnXacNhan.Click -= btnXacNhan_Click; btnXacNhan.Click += btnXacNhan_Click;
            btnHuy.Click -= btnHuy_Click; btnHuy.Click += btnHuy_Click;
            btnIn.Click -= btnIn_Click; btnIn.Click += btnIn_Click;
            btnXemCK.Click -= btnXemCK_Click; btnXemCK.Click += btnXemCK_Click;
            btnBanQuay.Click -= btnBanQuay_Click; btnBanQuay.Click += btnBanQuay_Click;
            btnDong.Click -= btnDong_Click; btnDong.Click += btnDong_Click;
        }

        // ================================================================ nạp dữ liệu
        private void frmQuanLyDonHang_Load(object sender, EventArgs e)
        {
            dtpTu.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtpDen.Value = DateTime.Today;

            func.FillCombo("SELECT MaNhanVien, MaNhanVien + N' - ' + TenNhanVien AS Ten FROM tblNhanVien ORDER BY MaNhanVien",
                cboNV, "MaNhanVien", "Ten");

            // có đơn chờ xác nhận thì mặc định lọc đúng các đơn đó cho admin xử lý ngay
            int cho;
            int.TryParse(func.GetFieldValues("SELECT COUNT(*) FROM tblHDBan WHERE TrangThai = N'Chờ xác nhận'"), out cho);
            cboTT.SelectedIndex = cho > 0 ? 1 : 0;

            CanChinhBoCuc();
            LocDon();

            // gắn sau lần LocDon đầu để không chạy khi form còn đang khởi tạo
            cboTT.SelectedIndexChanged -= cboTT_SelectedIndexChanged;
            cboTT.SelectedIndexChanged += cboTT_SelectedIndexChanged;
            dgvDon.SelectionChanged -= dgvDon_SelectionChanged;
            dgvDon.SelectionChanged += dgvDon_SelectionChanged;
        }

        // ================================================================ xử lý sự kiện
        private void cboTT_SelectedIndexChanged(object sender, EventArgs e) { LocDon(); }
        private void dgvDon_SelectionChanged(object sender, EventArgs e) { HienChiTiet(); }

        private void btnLoc_Click(object sender, EventArgs e) { LocDon(); }

        private void txtTim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LocDon();
        }

        private void btnXacNhan_Click(object sender, EventArgs e) { XacNhanDon(); }
        private void btnHuy_Click(object sender, EventArgs e) { HuyDon(); }
        private void btnIn_Click(object sender, EventArgs e) { InHoaDon(); }
        private void btnXemCK_Click(object sender, EventArgs e) { XemAnhChuyenKhoan(); }
        private void btnBanQuay_Click(object sender, EventArgs e) { BanTaiQuay(); }
        private void btnDong_Click(object sender, EventArgs e) { this.Close(); }

        // ================================================================ dữ liệu & thao tác (giữ nguyên từ bản cũ)
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