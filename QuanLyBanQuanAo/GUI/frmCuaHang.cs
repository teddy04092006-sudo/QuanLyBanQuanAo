using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;

namespace QuanLyBanQuanAo.GUI
{
    /// <summary>
    /// Form dành cho khách: xem sản phẩm dạng thẻ (ảnh + giá), lọc theo loại, tìm theo tên, sắp xếp theo giá,
    /// bấm vào thẻ để xem chi tiết rồi mới thêm vào giỏ hàng và sang form Đặt hàng.
    /// </summary>
    public class frmCuaHang : Form
    {
        private readonly DataProcesser db = new DataProcesser();
        private readonly Dictionary<string, Image> cacheAnh = new Dictionary<string, Image>();

        private ComboBox cboLoai, cboSapXep;
        private TextBox txtTim;
        private Button btnGio;
        private Label lblDem;
        private FlowLayoutPanel flow;

        // khung chi tiết bên phải
        private PictureBox picCT;
        private Label lblTenCT, lblGiaCT, lblThongSoCT, lblThongBao;
        private NumericUpDown nudSL;
        private Button btnThem;

        private Panel theChon;
        private DataRow dongChon;
        private int phienBanDaNap = -1;
        private bool dangKhoiTao = true;

        public frmCuaHang()
        {
            this.Name = "frmCuaHang";          // MoFormCon dựa vào Name để không mở trùng form
            this.Text = "Xem sản phẩm";
            this.ClientSize = new Size(1200, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            BuildUI();
            this.Load += frmCuaHang_Load;
            this.Activated += frmCuaHang_Activated;
            this.FormClosed += delegate
            {
                foreach (Image img in cacheAnh.Values) if (img != null) img.Dispose();
                cacheAnh.Clear();
            };
        }

        // ================================================================ giao diện
        private void BuildUI()
        {
            // Thứ tự Add quan trọng với Dock: Fill trước, rồi Right, cuối cùng Top
            flow = new FlowLayoutPanel();
            flow.Dock = DockStyle.Fill;
            flow.AutoScroll = true;
            flow.WrapContents = true;
            flow.Padding = new Padding(8);
            flow.BackColor = Color.FromArgb(238, 238, 238);
            Controls.Add(flow);

            Panel pnlCT = new Panel();
            pnlCT.Dock = DockStyle.Right;
            pnlCT.Width = 370;
            pnlCT.AutoScroll = true;
            pnlCT.BackColor = Color.White;
            pnlCT.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pnlCT);
            BuildChiTiet(pnlCT);

            Panel pnlTop = new Panel();
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 80;
            pnlTop.BackColor = Color.FromArgb(252, 246, 240);
            Controls.Add(pnlTop);

            Label tieuDe = new Label();
            tieuDe.Text = "CỬA HÀNG THỜI TRANG - XEM SẢN PHẨM";
            tieuDe.Font = new Font("Arial", 14, FontStyle.Bold);
            tieuDe.ForeColor = Color.Firebrick;
            tieuDe.Left = 12; tieuDe.Top = 6; tieuDe.Width = 600; tieuDe.Height = 28;
            pnlTop.Controls.Add(tieuDe);

            UI.Lbl(pnlTop, "Loại", 12, 40, 35);
            cboLoai = UI.Cbo(pnlTop, 50, 40, 170);
            UI.Lbl(pnlTop, "Tìm tên", 235, 40, 55);
            txtTim = UI.Txt(pnlTop, 295, 40, 180, false);
            txtTim.KeyDown += delegate (object s, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; NapSanPham(); }
            };
            UI.Btn(pnlTop, "Tìm", 480, 38, 55, delegate { NapSanPham(); });
            UI.Lbl(pnlTop, "Sắp xếp", 550, 40, 55);
            cboSapXep = UI.Cbo(pnlTop, 610, 40, 160);
            cboSapXep.Items.AddRange(new object[] { "Mặc định", "Giá tăng dần", "Giá giảm dần", "Tên A → Z" });
            cboSapXep.SelectedIndex = 0;
            lblDem = new Label();
            lblDem.Left = 785; lblDem.Top = 44; lblDem.Width = 150; lblDem.Height = 20;
            lblDem.ForeColor = Color.DarkBlue;
            pnlTop.Controls.Add(lblDem);

            Panel pnlGio = new Panel();
            pnlGio.Dock = DockStyle.Right;
            pnlGio.Width = 260;
            pnlGio.BackColor = pnlTop.BackColor;
            pnlTop.Controls.Add(pnlGio);
            btnGio = UI.Btn(pnlGio, "Giỏ hàng (0) - Đặt hàng", 15, 34, 235, delegate { MoGioHang(); });
            btnGio.Height = 36;
            btnGio.Font = new Font("Arial", 9.5f, FontStyle.Bold);
        }

        private void BuildChiTiet(Panel p)
        {
            picCT = new PictureBox();
            picCT.Left = 20; picCT.Top = 15; picCT.Width = 320; picCT.Height = 320;
            picCT.SizeMode = PictureBoxSizeMode.Zoom;
            picCT.BorderStyle = BorderStyle.FixedSingle;
            picCT.BackColor = Color.FromArgb(245, 245, 245);
            p.Controls.Add(picCT);

            lblTenCT = new Label();
            lblTenCT.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTenCT.Left = 20; lblTenCT.Top = 345; lblTenCT.Width = 320; lblTenCT.Height = 48;
            p.Controls.Add(lblTenCT);

            lblGiaCT = new Label();
            lblGiaCT.Font = new Font("Arial", 15, FontStyle.Bold);
            lblGiaCT.ForeColor = Color.Firebrick;
            lblGiaCT.Left = 20; lblGiaCT.Top = 396; lblGiaCT.Width = 320; lblGiaCT.Height = 30;
            p.Controls.Add(lblGiaCT);

            lblThongSoCT = new Label();
            lblThongSoCT.Left = 20; lblThongSoCT.Top = 432; lblThongSoCT.Width = 320; lblThongSoCT.Height = 120;
            p.Controls.Add(lblThongSoCT);

            UI.Lbl(p, "Số lượng", 20, 560, 70);
            nudSL = UI.Nud(p, 100, 560, 70, 1, 1, 1);

            btnThem = UI.Btn(p, "Thêm vào giỏ hàng", 20, 600, 320, delegate { ThemVaoGio(); });
            btnThem.Height = 40;
            btnThem.Font = new Font("Arial", 10, FontStyle.Bold);

            lblThongBao = new Label();
            lblThongBao.ForeColor = Color.DarkGreen;
            lblThongBao.Left = 20; lblThongBao.Top = 648; lblThongBao.Width = 320; lblThongBao.Height = 40;
            p.Controls.Add(lblThongBao);

            HienChiTiet(null);
        }

        // ================================================================ nạp dữ liệu
        private void frmCuaHang_Load(object sender, EventArgs e)
        {
            DataTable dt = db.DocBang("SELECT MaLoai, TenLoai FROM tblLoaiThoiTrang ORDER BY TenLoai");
            DataRow tatCa = dt.NewRow();
            tatCa["MaLoai"] = "";
            tatCa["TenLoai"] = "Tất cả";
            dt.Rows.InsertAt(tatCa, 0);
            cboLoai.DisplayMember = "TenLoai";
            cboLoai.ValueMember = "MaLoai";
            cboLoai.DataSource = dt;
            cboLoai.SelectedIndex = 0;

            NapSanPham();
            CapNhatNutGio();
            dangKhoiTao = false;
            cboLoai.SelectedIndexChanged += delegate { NapSanPham(); };
            cboSapXep.SelectedIndexChanged += delegate { NapSanPham(); };
        }

        private void frmCuaHang_Activated(object sender, EventArgs e)
        {
            if (dangKhoiTao) return;
            CapNhatNutGio();
            // vừa có đơn đặt thành công (tồn kho đổi) -> nạp lại danh sách
            if (phienBanDaNap != GioHang.PhienBanKho) NapSanPham();
        }

        private void NapSanPham()
        {
            phienBanDaNap = GioHang.PhienBanKho;
            string maChon = dongChon != null ? Convert.ToString(dongChon["MaQuanAo"]) : null;

            string sql = "SELECT qa.MaQuanAo, qa.TenQuanAo, l.TenLoai, cl.TenChatLieu, qa.KichCo, qa.MauSac, " +
                         "qa.DonGiaBan, qa.SoLuong, qa.Anh, qa.GhiChu " +
                         "FROM tblQuanAo qa " +
                         "INNER JOIN tblLoaiThoiTrang l ON qa.MaLoai = l.MaLoai " +
                         "INNER JOIN tblChatLieu cl ON qa.MaChatLieu = cl.MaChatLieu " +
                         "WHERE qa.SoLuong > 0";
            List<SqlParameter> ps = new List<SqlParameter>();
            if (cboLoai.SelectedIndex > 0)
            {
                DataRowView drv = cboLoai.SelectedItem as DataRowView;
                if (drv != null)
                {
                    sql += " AND qa.MaLoai = @loai";
                    ps.Add(DataProcesser.P("@loai", Convert.ToString(drv["MaLoai"])));
                }
            }
            string tk = txtTim.Text.Trim();
            if (tk != "")
            {
                sql += " AND qa.TenQuanAo LIKE @tk";
                ps.Add(DataProcesser.P("@tk", "%" + tk + "%"));
            }
            switch (cboSapXep.SelectedIndex)
            {
                case 1: sql += " ORDER BY qa.DonGiaBan ASC, qa.TenQuanAo"; break;
                case 2: sql += " ORDER BY qa.DonGiaBan DESC, qa.TenQuanAo"; break;
                case 3: sql += " ORDER BY qa.TenQuanAo"; break;
                default: sql += " ORDER BY l.TenLoai, qa.TenQuanAo"; break;
            }

            DataTable dt = db.DocBang(sql, ps.ToArray());

            flow.SuspendLayout();
            while (flow.Controls.Count > 0)
            {
                Control c = flow.Controls[0];
                flow.Controls.RemoveAt(0);
                c.Dispose();
            }
            theChon = null;
            dongChon = null;
            Panel khoiPhuc = null;
            foreach (DataRow r in dt.Rows)
            {
                Panel the = TaoThe(r);
                flow.Controls.Add(the);
                if (maChon != null && Convert.ToString(r["MaQuanAo"]) == maChon) khoiPhuc = the;
            }
            flow.ResumeLayout();

            lblDem.Text = "Có " + dt.Rows.Count + " sản phẩm";
            if (khoiPhuc != null) ChonThe(khoiPhuc); else HienChiTiet(null);
        }

        // ================================================================ thẻ sản phẩm
        private Image LayAnh(string tenFile)
        {
            if (string.IsNullOrEmpty(tenFile)) return null;
            Image img;
            if (cacheAnh.TryGetValue(tenFile, out img)) return img;

            string duongDan = Path.Combine(Application.StartupPath, "Images", "QuanAo", tenFile);
            img = null;
            if (File.Exists(duongDan))
            {
                try { img = Image.FromStream(new MemoryStream(File.ReadAllBytes(duongDan))); }   // không khóa file
                catch (Exception) { img = null; }
            }
            cacheAnh[tenFile] = img;
            return img;
        }

        private Panel TaoThe(DataRow r)
        {
            Panel p = new Panel();
            p.Width = 200; p.Height = 296;
            p.Margin = new Padding(8);
            p.BackColor = Color.White;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Cursor = Cursors.Hand;
            p.Tag = r;

            PictureBox pic = new PictureBox();
            pic.Left = 9; pic.Top = 8; pic.Width = 180; pic.Height = 170;
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.Image = LayAnh(Convert.ToString(r["Anh"]));
            p.Controls.Add(pic);

            Label ten = new Label();
            ten.Text = Convert.ToString(r["TenQuanAo"]);
            ten.Font = new Font("Arial", 9.5f, FontStyle.Bold);
            ten.Left = 8; ten.Top = 182; ten.Width = 182; ten.Height = 38;
            p.Controls.Add(ten);

            Label gia = new Label();
            gia.Text = Convert.ToDouble(r["DonGiaBan"]).ToString("N0") + " đ";
            gia.Font = new Font("Arial", 11, FontStyle.Bold);
            gia.ForeColor = Color.Firebrick;
            gia.Left = 8; gia.Top = 222; gia.Width = 182; gia.Height = 22;
            p.Controls.Add(gia);

            Label phu = new Label();
            phu.Text = "Size " + Convert.ToString(r["KichCo"]) + "  |  " + Convert.ToString(r["MauSac"]);
            phu.Left = 8; phu.Top = 246; phu.Width = 182; phu.Height = 20;
            p.Controls.Add(phu);

            int ton = Convert.ToInt32(r["SoLuong"]);
            Label kho = new Label();
            kho.Left = 8; kho.Top = 268; kho.Width = 182; kho.Height = 20;
            if (ton <= 5) { kho.Text = "Chỉ còn " + ton + " sản phẩm"; kho.ForeColor = Color.DarkOrange; }
            else { kho.Text = "Còn " + ton + " sản phẩm"; kho.ForeColor = Color.Gray; }
            p.Controls.Add(kho);

            EventHandler chon = delegate { ChonThe(p); };
            p.Click += chon;
            foreach (Control c in p.Controls) { c.Click += chon; c.Cursor = Cursors.Hand; }
            return p;
        }

        private void ChonThe(Panel p)
        {
            if (theChon != null && !theChon.IsDisposed) theChon.BackColor = Color.White;
            theChon = p;
            p.BackColor = Color.FromArgb(255, 240, 200);
            HienChiTiet((DataRow)p.Tag);
        }

        // ================================================================ chi tiết + giỏ hàng
        private void HienChiTiet(DataRow r)
        {
            dongChon = r;
            lblThongBao.Text = "";
            if (r == null)
            {
                picCT.Image = null;
                lblTenCT.Text = "Chọn một sản phẩm bên trái để xem ảnh và thông tin chi tiết.";
                lblTenCT.Font = new Font("Arial", 10, FontStyle.Regular);
                lblGiaCT.Text = "";
                lblThongSoCT.Text = "";
                nudSL.Enabled = false;
                btnThem.Enabled = false;
                return;
            }

            int ton = Convert.ToInt32(r["SoLuong"]);
            picCT.Image = LayAnh(Convert.ToString(r["Anh"]));
            lblTenCT.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTenCT.Text = Convert.ToString(r["TenQuanAo"]);
            lblGiaCT.Text = Convert.ToDouble(r["DonGiaBan"]).ToString("N0") + " VNĐ";

            string gc = Convert.ToString(r["GhiChu"]);
            lblThongSoCT.Text = "Loại: " + Convert.ToString(r["TenLoai"]) +
                                "\nChất liệu: " + Convert.ToString(r["TenChatLieu"]) +
                                "\nKích cỡ: " + Convert.ToString(r["KichCo"]) +
                                "\nMàu sắc: " + Convert.ToString(r["MauSac"]) +
                                "\nCòn lại: " + ton + " sản phẩm" +
                                (gc != "" ? "\nGhi chú: " + gc : "");

            nudSL.Minimum = 1;
            nudSL.Maximum = Math.Max(1, ton);
            nudSL.Value = 1;
            nudSL.Enabled = true;
            btnThem.Enabled = true;
        }

        private void ThemVaoGio()
        {
            if (dongChon == null) return;
            string ma = Convert.ToString(dongChon["MaQuanAo"]);
            int ton = Convert.ToInt32(dongChon["SoLuong"]);
            int sl = (int)nudSL.Value;
            int daCo = GioHang.SoLuongTrongGio(ma);

            if (daCo + sl > ton)
            {
                MessageBox.Show("Sản phẩm này chỉ còn " + ton + " cái" +
                                (daCo > 0 ? " (trong giỏ của bạn đã có " + daCo + ")." : "."),
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GioHang.Them(ma, Convert.ToString(dongChon["TenQuanAo"]), Convert.ToString(dongChon["KichCo"]),
                Convert.ToString(dongChon["MauSac"]), sl, Convert.ToDouble(dongChon["DonGiaBan"]));
            CapNhatNutGio();
            lblThongBao.Text = "Đã thêm " + sl + " x " + Convert.ToString(dongChon["TenQuanAo"]) +
                               " vào giỏ. Bấm \"Giỏ hàng - Đặt hàng\" phía trên để thanh toán.";
        }

        private void CapNhatNutGio()
        {
            btnGio.Text = "Giỏ hàng (" + GioHang.TongSoLuong() + ") - Đặt hàng";
        }

        private void MoGioHang()
        {
            frmMain main = this.MdiParent as frmMain;
            if (main != null) main.MoFormCon(new frmDatHang());
        }
    }
}
