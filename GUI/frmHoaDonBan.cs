using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;

namespace QuanLyBanQuanAo.GUI
{
    public partial class frmHoaDonBan : Form
    {
        private readonly DataProcesser db = new DataProcesser();
        private readonly Function func = new Function();
        private readonly DataTable gio = new DataTable();

        private TextBox txtMaHD, txtSDT, txtDiaChi, txtGhiChu, txtTonKho, txtDonGia, txtThanhTien, txtTongTien, txtBangChu;
        private DateTimePicker dtpNgay;
        private ComboBox cboNV, cboKhach, cboThanhToan, cboHang, cboTimHD;
        private NumericUpDown nudSL, nudGG;
        private DataGridView dgv;
        private Label lblTrangThai;
        private Button btnMoi, btnThem, btnLuu, btnIn, btnHuy, btnMo, btnDong, btnXacNhan;

        private bool daLuu = false;            // true: hóa đơn đã lưu/đang xem -> khóa nhập
        private string trangThai = "";
        private double donGiaHienTai = 0;

        public frmHoaDonBan()
        {
            this.Name = "frmHoaDonBan";
            this.Text = "Lập hóa đơn bán hàng";
            this.ClientSize = new Size(990, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            gio.Columns.Add("MaQuanAo", typeof(string));
            gio.Columns.Add("TenQuanAo", typeof(string));
            gio.Columns.Add("KichCo", typeof(string));
            gio.Columns.Add("MauSac", typeof(string));
            gio.Columns.Add("SoLuong", typeof(int));
            gio.Columns.Add("DonGia", typeof(double));
            gio.Columns.Add("GiamGia", typeof(double));
            gio.Columns.Add("ThanhTien", typeof(double));

            BuildUI();
            this.Load += frmHoaDonBan_Load;
            Theme.Apply(this);
        }

        // ================================================================ giao diện
        private void BuildUI()
        {
            Label tieuDe = new Label();
            tieuDe.Text = "HÓA ĐƠN BÁN HÀNG";
            tieuDe.Font = new Font("Arial", 14, FontStyle.Bold);
            tieuDe.ForeColor = Color.Firebrick;
            tieuDe.Left = 10; tieuDe.Top = 6; tieuDe.Width = 400; tieuDe.Height = 28;
            Controls.Add(tieuDe);

            // ---- thông tin chung
            GroupBox gb = new GroupBox();
            gb.Text = "Thông tin chung";
            gb.Left = 10; gb.Top = 36; gb.Width = 965; gb.Height = 150;
            gb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(gb);

            UI.Lbl(gb, "Mã hóa đơn", 15, 20, 85);
            txtMaHD = UI.Txt(gb, 105, 20, 190, true);
            UI.Lbl(gb, "Ngày bán", 320, 20, 70);
            dtpNgay = new DateTimePicker();
            dtpNgay.Left = 395; dtpNgay.Top = 20; dtpNgay.Width = 170;
            dtpNgay.Format = DateTimePickerFormat.Custom; dtpNgay.CustomFormat = "dd/MM/yyyy HH:mm";
            gb.Controls.Add(dtpNgay);
            lblTrangThai = new Label();
            lblTrangThai.Left = 590; lblTrangThai.Top = 24; lblTrangThai.Width = 360; lblTrangThai.Height = 22;
            lblTrangThai.Font = new Font("Arial", 10, FontStyle.Bold);
            lblTrangThai.ForeColor = Color.DarkGreen;
            gb.Controls.Add(lblTrangThai);

            UI.Lbl(gb, "Nhân viên", 15, 52, 85);
            cboNV = UI.Cbo(gb, 105, 52, 190);
            UI.Lbl(gb, "Khách hàng", 320, 52, 75);
            cboKhach = UI.Cbo(gb, 395, 52, 250);
            UI.Lbl(gb, "Thanh toán", 665, 52, 75);
            cboThanhToan = UI.Cbo(gb, 745, 52, 200);
            cboThanhToan.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ" });

            UI.Lbl(gb, "Điện thoại", 15, 84, 85);
            txtSDT = UI.Txt(gb, 105, 84, 190, true);
            UI.Lbl(gb, "Địa chỉ", 320, 84, 75);
            txtDiaChi = UI.Txt(gb, 395, 84, 550, true);

            UI.Lbl(gb, "Ghi chú", 15, 116, 85);
            txtGhiChu = UI.Txt(gb, 105, 116, 840, false);

            // ---- chọn hàng
            GroupBox gh = new GroupBox();
            gh.Text = "Chọn mặt hàng";
            gh.Left = 10; gh.Top = 192; gh.Width = 965; gh.Height = 92;
            gh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(gh);

            UI.Lbl(gh, "Sản phẩm", 15, 20, 70);
            cboHang = UI.Cbo(gh, 90, 20, 340);
            UI.Lbl(gh, "Tồn kho", 445, 20, 55);
            txtTonKho = UI.Txt(gh, 505, 20, 60, true);
            UI.Lbl(gh, "Đơn giá", 580, 20, 55);
            txtDonGia = UI.Txt(gh, 640, 20, 95, true);
            UI.Lbl(gh, "Số lượng", 750, 20, 60);
            nudSL = UI.Nud(gh, 815, 20, 70, 1, 9999, 1);

            UI.Lbl(gh, "Giảm giá %", 15, 54, 75);
            nudGG = UI.Nud(gh, 90, 54, 70, 0, 100, 0);
            UI.Lbl(gh, "Thành tiền", 180, 54, 75);
            txtThanhTien = UI.Txt(gh, 260, 54, 120, true);
            btnThem = UI.Btn(gh, "Thêm vào hóa đơn", 400, 51, 160, btnThem_Click);
            Label goiY = new Label();
            goiY.Text = "(Nhấp đúp vào một dòng bên dưới để xóa khỏi hóa đơn)";
            goiY.Left = 575; goiY.Top = 58; goiY.Width = 380; goiY.ForeColor = Color.Gray;
            gh.Controls.Add(goiY);

            // ---- lưới chi tiết
            dgv = UI.Grid(this, 10, 292, 965, 250);
            dgv.DataSource = gio;
            UI.Headers(dgv, "Mã hàng", "Tên quần áo", "Size", "Màu", "Số lượng", "Đơn giá", "Giảm giá %", "Thành tiền");
            UI.Format(dgv, "N0", 5, 7);
            dgv.CellDoubleClick += dgv_CellDoubleClick;

            // ---- tổng tiền + nút
            Label l1 = UI.Lbl(this, "Tổng tiền", 10, 550, 70);
            txtTongTien = UI.Txt(this, 85, 550, 150, true);
            txtTongTien.Font = new Font("Arial", 10, FontStyle.Bold);
            Label l2 = UI.Lbl(this, "Bằng chữ", 250, 550, 65);
            txtBangChu = UI.Txt(this, 320, 550, 655, true);
            l1.Anchor = l2.Anchor = txtTongTien.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtBangChu.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            btnMoi = UI.Btn(this, "Hóa đơn mới", 10, 600, 110, delegate { HoaDonMoi(true); });
            btnLuu = UI.Btn(this, "Lưu hóa đơn", 125, 600, 110, delegate { LuuHoaDon(); });
            btnIn = UI.Btn(this, "In ra Excel", 240, 600, 110, delegate { InExcel(); });
            btnHuy = UI.Btn(this, "Hủy hóa đơn", 355, 600, 110, delegate { HuyHoaDon(); });
            UI.Lbl(this, "Tìm hóa đơn:", 490, 600, 80);
            cboTimHD = UI.Cbo(this, 575, 600, 130);
            cboTimHD.DropDownWidth = 460;
            btnMo = UI.Btn(this, "Mở", 710, 600, 50, delegate { MoHoaDon(); });
            btnXacNhan = UI.Btn(this, "Xác nhận đơn", 765, 600, 110, delegate { XacNhanDon(); });
            btnDong = UI.Btn(this, "Đóng", 880, 600, 95, delegate { Close(); });
            foreach (Control c in new Control[] { btnMoi, btnLuu, btnIn, btnHuy, cboTimHD, btnMo, btnXacNhan, btnDong })
                c.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDong.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        }

        // ================================================================ nạp dữ liệu
        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            func.FillCombo("SELECT MaNhanVien, MaNhanVien + N' - ' + TenNhanVien AS Ten FROM tblNhanVien ORDER BY MaNhanVien",
                cboNV, "MaNhanVien", "Ten");
            NapKhach();
            NapHang();
            NapDanhSachHD();

            cboKhach.SelectedIndexChanged += cboKhach_SelectedIndexChanged;
            cboHang.SelectedIndexChanged += cboHang_SelectedIndexChanged;
            nudSL.ValueChanged += delegate { TinhThanhTien(); };
            nudGG.ValueChanged += delegate { TinhThanhTien(); };

            HoaDonMoi(false);
        }

        private void NapKhach()
        {
            func.FillCombo("SELECT MaKhach, TenKhach + N' - ' + DienThoai AS Ten FROM tblKhach ORDER BY TenKhach",
                cboKhach, "MaKhach", "Ten");
        }

        private void NapHang()
        {
            func.FillCombo("SELECT MaQuanAo, MaQuanAo + N' - ' + TenQuanAo AS Ten FROM tblQuanAo WHERE SoLuong > 0 ORDER BY MaQuanAo",
                cboHang, "MaQuanAo", "Ten");
        }

        private void NapDanhSachHD()
        {
            func.FillCombo("SELECT h.MaHDBan, h.MaHDBan + N' | ' + k.TenKhach + N' | ' + h.TrangThai AS Ten " +
                           "FROM tblHDBan h INNER JOIN tblKhach k ON h.MaKhach = k.MaKhach ORDER BY h.NgayBan DESC",
                cboTimHD, "MaHDBan", "Ten");
        }

        private static string Chuoi(ComboBox c)
        {
            return (c.SelectedIndex >= 0 && c.SelectedValue is string) ? (string)c.SelectedValue : null;
        }

        private void cboKhach_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ma = Chuoi(cboKhach);
            if (ma == null) { txtSDT.Text = ""; txtDiaChi.Text = ""; return; }
            DataTable dt = db.DocBang("SELECT DienThoai, DiaChi FROM tblKhach WHERE MaKhach = @m", DataProcesser.P("@m", ma));
            if (dt.Rows.Count > 0)
            {
                txtSDT.Text = dt.Rows[0]["DienThoai"].ToString();
                txtDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
            }
        }

        private void cboHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ma = Chuoi(cboHang);
            if (ma == null) { txtTonKho.Text = ""; txtDonGia.Text = ""; donGiaHienTai = 0; TinhThanhTien(); return; }
            DataTable dt = db.DocBang("SELECT SoLuong, DonGiaBan FROM tblQuanAo WHERE MaQuanAo = @m", DataProcesser.P("@m", ma));
            if (dt.Rows.Count > 0)
            {
                txtTonKho.Text = dt.Rows[0]["SoLuong"].ToString();
                donGiaHienTai = Convert.ToDouble(dt.Rows[0]["DonGiaBan"]);
                txtDonGia.Text = donGiaHienTai.ToString("N0");
            }
            TinhThanhTien();
        }

        private static double TinhTien(int sl, double dg, double gg)
        {
            return Math.Round(sl * dg * (100 - gg) / 100.0);
        }

        private void TinhThanhTien()
        {
            txtThanhTien.Text = TinhTien((int)nudSL.Value, donGiaHienTai, (double)nudGG.Value).ToString("N0");
        }

        private void CapNhatTong()
        {
            double tong = 0;
            foreach (DataRow r in gio.Rows) tong += Convert.ToDouble(r["ThanhTien"]);
            txtTongTien.Text = tong.ToString("N0");
            txtBangChu.Text = tong > 0 ? func.DocSoThanhChu((long)Math.Round(tong)) : "";
        }

        private double TongTien()
        {
            double tong = 0;
            foreach (DataRow r in gio.Rows) tong += Convert.ToDouble(r["ThanhTien"]);
            return tong;
        }

        // ================================================================ trạng thái form
        private void DatKhoa(bool khoa)
        {
            daLuu = khoa;
            dtpNgay.Enabled = cboNV.Enabled = cboKhach.Enabled = cboThanhToan.Enabled = txtGhiChu.Enabled = !khoa;
            cboHang.Enabled = nudSL.Enabled = nudGG.Enabled = btnThem.Enabled = btnLuu.Enabled = !khoa;
            btnIn.Enabled = khoa;
            string q = PhienDangNhap.Quyen;
            btnHuy.Enabled = khoa && trangThai != "Đã hủy" && (q == "Admin" || q == "QuanLy");
            btnXacNhan.Enabled = khoa && trangThai == "Chờ xác nhận" && (q == "Admin" || q == "QuanLy");
        }

        private void HoaDonMoi(bool hoiXacNhan)
        {
            if (hoiXacNhan && !daLuu && gio.Rows.Count > 0)
            {
                if (MessageBox.Show("Hóa đơn hiện tại chưa lưu, bạn có muốn bỏ và tạo hóa đơn mới?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            }
            gio.Clear();
            trangThai = "";
            txtMaHD.Text = func.CreateKey("HDB");
            dtpNgay.Value = DateTime.Now;
            cboNV.SelectedIndex = -1;
            cboKhach.SelectedIndex = -1;
            cboThanhToan.SelectedIndex = 0;
            cboHang.SelectedIndex = -1;
            txtGhiChu.Text = "";
            nudSL.Value = 1; nudGG.Value = 0;
            lblTrangThai.Text = "Hóa đơn mới (chưa lưu)";
            lblTrangThai.ForeColor = Color.DarkOrange;
            CapNhatTong();
            DatKhoa(false);
        }

        // ================================================================ thêm / xóa mặt hàng
        private void btnThem_Click(object sender, EventArgs e)
        {
            string ma = Chuoi(cboHang);
            if (ma == null)
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHang.Focus();
                return;
            }
            int sl = (int)nudSL.Value;
            double gg = (double)nudGG.Value;

            // đọc lại tồn kho mới nhất từ CSDL
            DataTable dt = db.DocBang("SELECT TenQuanAo, KichCo, MauSac, SoLuong, DonGiaBan FROM tblQuanAo WHERE MaQuanAo = @m",
                DataProcesser.P("@m", ma));
            if (dt.Rows.Count == 0) return;
            DataRow sp = dt.Rows[0];
            int ton = Convert.ToInt32(sp["SoLuong"]);
            double dg = Convert.ToDouble(sp["DonGiaBan"]);

            DataRow daCo = gio.AsEnumerable().FirstOrDefault(r => r.Field<string>("MaQuanAo") == ma);
            int slDaCo = daCo == null ? 0 : daCo.Field<int>("SoLuong");
            if (slDaCo + sl > ton)
            {
                MessageBox.Show("Không đủ hàng! Tồn kho: " + ton + ", trong hóa đơn đã có: " + slDaCo + ".",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (daCo != null)
            {
                daCo["SoLuong"] = slDaCo + sl;
                daCo["GiamGia"] = gg;
                daCo["DonGia"] = dg;
                daCo["ThanhTien"] = TinhTien(slDaCo + sl, dg, gg);
            }
            else
            {
                DataRow nr = gio.NewRow();
                nr["MaQuanAo"] = ma;
                nr["TenQuanAo"] = sp["TenQuanAo"].ToString();
                nr["KichCo"] = sp["KichCo"].ToString();
                nr["MauSac"] = sp["MauSac"].ToString();
                nr["SoLuong"] = sl;
                nr["DonGia"] = dg;
                nr["GiamGia"] = gg;
                nr["ThanhTien"] = TinhTien(sl, dg, gg);
                gio.Rows.Add(nr);
            }
            CapNhatTong();
            nudSL.Value = 1;
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (daLuu || e.RowIndex < 0) return;
            if (MessageBox.Show("Xóa mặt hàng này khỏi hóa đơn?", "Xác nhận", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gio.Rows.RemoveAt(e.RowIndex);
                CapNhatTong();
            }
        }

        // ================================================================ lưu hóa đơn
        private void LuuHoaDon()
        {
            if (daLuu) return;
            string maNV = Chuoi(cboNV), maKH = Chuoi(cboKhach);
            if (maNV == null) { MessageBox.Show("Bạn phải chọn nhân viên bán hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); cboNV.Focus(); return; }
            if (maKH == null) { MessageBox.Show("Bạn phải chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); cboKhach.Focus(); return; }
            if (gio.Rows.Count == 0) { MessageBox.Show("Hóa đơn chưa có mặt hàng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); cboHang.Focus(); return; }

            string maHD = txtMaHD.Text;
            double tong = TongTien();
            string hinhThuc = cboThanhToan.Text;
            string ghiChu = txtGhiChu.Text.Trim();
            DateTime ngay = dtpNgay.Value;
            string loi;

            bool ok = db.ThucThiGiaoDich(delegate (GiaoDich gd)
            {
                gd.Chay("INSERT INTO tblHDBan(MaHDBan, MaNhanVien, NgayBan, MaKhach, TongTien, TrangThai, HinhThucThanhToan, GhiChu) " +
                        "VALUES(@ma, @nv, @ngay, @kh, @tong, N'Đã thanh toán', @tt, @gc)",
                    DataProcesser.P("@ma", maHD), DataProcesser.P("@nv", maNV), DataProcesser.P("@ngay", ngay),
                    DataProcesser.P("@kh", maKH), DataProcesser.P("@tong", tong), DataProcesser.P("@tt", hinhThuc),
                    DataProcesser.P("@gc", ghiChu == "" ? null : ghiChu));

                foreach (DataRow r in gio.Rows)
                {
                    string maSP = r["MaQuanAo"].ToString();
                    int sl = Convert.ToInt32(r["SoLuong"]);
                    // trừ kho đồng thời kiểm tra đủ hàng (an toàn khi 2 người cùng bán)
                    int n = gd.Chay("UPDATE tblQuanAo SET SoLuong = SoLuong - @sl WHERE MaQuanAo = @ma AND SoLuong >= @sl",
                        DataProcesser.P("@sl", sl), DataProcesser.P("@ma", maSP));
                    if (n == 0) throw new Exception("Sản phẩm " + maSP + " không còn đủ số lượng trong kho. Hóa đơn chưa được lưu.");
                    gd.Chay("INSERT INTO tblChiTietHDBan(MaHDBan, MaQuanAo, SoLuong, DonGia, GiamGia, ThanhTien) " +
                            "VALUES(@ma, @sp, @sl, @dg, @gg, @tt)",
                        DataProcesser.P("@ma", maHD), DataProcesser.P("@sp", maSP), DataProcesser.P("@sl", sl),
                        DataProcesser.P("@dg", Convert.ToDouble(r["DonGia"])), DataProcesser.P("@gg", Convert.ToDouble(r["GiamGia"])),
                        DataProcesser.P("@tt", Convert.ToDouble(r["ThanhTien"])));
                }
            }, out loi);

            if (!ok)
            {
                MessageBox.Show(loi, "Không lưu được hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            trangThai = "Đã thanh toán";
            lblTrangThai.Text = "Đã lưu - Đã thanh toán";
            lblTrangThai.ForeColor = Color.DarkGreen;
            DatKhoa(true);
            NapHang();
            NapDanhSachHD();
            if (MessageBox.Show("Lưu hóa đơn thành công!\nBạn có muốn in hóa đơn ra Excel không?", "Thông báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                InExcel();
        }

        // ================================================================ mở hóa đơn cũ
        private void MoHoaDon()
        {
            string ma = Chuoi(cboTimHD);
            if (ma == null) { MessageBox.Show("Hãy chọn mã hóa đơn cần mở!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            DataTable hd = db.DocBang("SELECT MaHDBan, MaNhanVien, NgayBan, MaKhach, TongTien, TrangThai, HinhThucThanhToan, GhiChu " +
                                      "FROM tblHDBan WHERE MaHDBan = @m", DataProcesser.P("@m", ma));
            if (hd.Rows.Count == 0) return;
            DataRow h = hd.Rows[0];

            NapKhach();   // nạp lại để có cả khách mới tạo từ form Đặt hàng
            gio.Clear();
            DataTable ct = db.DocBang("SELECT ct.MaQuanAo, qa.TenQuanAo, qa.KichCo, qa.MauSac, ct.SoLuong, ct.DonGia, ct.GiamGia, ct.ThanhTien " +
                                      "FROM tblChiTietHDBan ct INNER JOIN tblQuanAo qa ON ct.MaQuanAo = qa.MaQuanAo WHERE ct.MaHDBan = @m",
                DataProcesser.P("@m", ma));
            foreach (DataRow r in ct.Rows)
            {
                DataRow nr = gio.NewRow();
                nr["MaQuanAo"] = r["MaQuanAo"].ToString();
                nr["TenQuanAo"] = r["TenQuanAo"].ToString();
                nr["KichCo"] = r["KichCo"].ToString();
                nr["MauSac"] = r["MauSac"].ToString();
                nr["SoLuong"] = Convert.ToInt32(r["SoLuong"]);
                nr["DonGia"] = Convert.ToDouble(r["DonGia"]);
                nr["GiamGia"] = Convert.ToDouble(r["GiamGia"]);
                nr["ThanhTien"] = Convert.ToDouble(r["ThanhTien"]);
                gio.Rows.Add(nr);
            }

            txtMaHD.Text = h["MaHDBan"].ToString();
            dtpNgay.Value = Convert.ToDateTime(h["NgayBan"]);
            if (h["MaNhanVien"] == DBNull.Value) cboNV.SelectedIndex = -1; else cboNV.SelectedValue = h["MaNhanVien"].ToString();
            cboKhach.SelectedValue = h["MaKhach"].ToString();
            int idx = cboThanhToan.Items.IndexOf(h["HinhThucThanhToan"].ToString());
            cboThanhToan.SelectedIndex = idx;
            txtGhiChu.Text = h["GhiChu"] == DBNull.Value ? "" : h["GhiChu"].ToString();
            trangThai = h["TrangThai"].ToString();
            lblTrangThai.Text = "Đang xem - Trạng thái: " + trangThai;
            lblTrangThai.ForeColor = trangThai == "Đã hủy" ? Color.Red : Color.DarkGreen;
            CapNhatTong();
            DatKhoa(true);
        }

        // ================================================================ xác nhận đơn khách đặt online
        private void XacNhanDon()
        {
            if (!daLuu || trangThai != "Chờ xác nhận") return;
            string ma = txtMaHD.Text;
            if (MessageBox.Show("Xác nhận đơn " + ma + " và chuyển sang \"Đã thanh toán\"?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (db.CapNhatDuLieu("UPDATE tblHDBan SET TrangThai = N'Đã thanh toán' WHERE MaHDBan = @m AND TrangThai = N'Chờ xác nhận'",
                    DataProcesser.P("@m", ma)))
            {
                trangThai = "Đã thanh toán";
                lblTrangThai.Text = "Đang xem - Trạng thái: Đã thanh toán";
                lblTrangThai.ForeColor = Color.DarkGreen;
                DatKhoa(true);
                MessageBox.Show("Đã xác nhận đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ================================================================ hủy hóa đơn (hoàn kho)
        private void HuyHoaDon()
        {
            if (!daLuu || trangThai == "Đã hủy") return;
            string ma = txtMaHD.Text;
            if (MessageBox.Show("Hủy hóa đơn " + ma + "?\nSố lượng hàng sẽ được cộng trả lại kho.", "Xác nhận hủy",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            string loi;
            bool ok = db.ThucThiGiaoDich(delegate (GiaoDich gd)
            {
                int n = gd.Chay("UPDATE tblHDBan SET TrangThai = N'Đã hủy' WHERE MaHDBan = @m AND TrangThai <> N'Đã hủy'",
                    DataProcesser.P("@m", ma));
                if (n == 0) throw new Exception("Hóa đơn đã bị hủy trước đó.");
                gd.Chay("UPDATE qa SET qa.SoLuong = qa.SoLuong + ct.SoLuong " +
                        "FROM tblQuanAo qa INNER JOIN tblChiTietHDBan ct ON qa.MaQuanAo = ct.MaQuanAo WHERE ct.MaHDBan = @m",
                    DataProcesser.P("@m", ma));
            }, out loi);

            if (!ok) { MessageBox.Show(loi, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            trangThai = "Đã hủy";
            lblTrangThai.Text = "Đang xem - Trạng thái: Đã hủy";
            lblTrangThai.ForeColor = Color.Red;
            DatKhoa(true);
            NapHang();
            MessageBox.Show("Đã hủy hóa đơn và hoàn trả hàng về kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ================================================================ in Excel
        private void InExcel()
        {
            if (!daLuu) { MessageBox.Show("Hãy lưu hóa đơn trước khi in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string tenNV = cboNV.SelectedIndex >= 0 ? cboNV.Text : "(Đặt hàng online)";
            string tenKhach = cboKhach.SelectedIndex >= 0 ? cboKhach.Text : "";
            ExcelHelper.InHoaDon(txtMaHD.Text, dtpNgay.Value, tenNV, tenKhach, txtSDT.Text, txtDiaChi.Text,
                cboThanhToan.Text, gio, TongTien(), txtBangChu.Text);
        }
    }
}
