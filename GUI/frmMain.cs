using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;

namespace QuanLyBanQuanAo.GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            new Function().TaoTuDong200AnhSanPham();

            Theme.StyleMenu(menuStrip1);
            TaoNenVaTrangThai();

            GioHang.Bang.Clear();   // phiên mới -> giỏ hàng trống
            this.Text = "CHƯƠNG TRÌNH QUẢN LÝ BÁN QUẦN ÁO - Xin chào: " + PhienDangNhap.HoTen + " (" + PhienDangNhap.Quyen + ")";

            // PHÂN QUYỀN TRÊN MENU THEO VAI TRÒ
            bool admin = PhienDangNhap.Quyen == "Admin";
            bool quanLy = PhienDangNhap.Quyen == "QuanLy";

            // Admin: toàn quyền | Quản lý: xem sản phẩm, quản lý khách, lập hóa đơn, xem doanh thu
            // Người mua: chỉ có chức năng Đặt mua quần áo
            mnuDanhMuc.Visible = admin || quanLy;
            mnuQuanAo.Visible = admin || quanLy;
            mnuKhachHang.Visible = admin || quanLy;
            mnuLoaiThoiTrang.Visible = admin;
            mnuChatLieu.Visible = admin;
            mnuNhanVien.Visible = admin;
            mnuLapHoaDon.Visible = admin || quanLy;
            mnuThongKe.Visible = admin || quanLy;

            // Người mua: menu "Mua hàng" (Xem sản phẩm -> Giỏ hàng & đặt hàng), mở sẵn form xem sản phẩm
            if (!admin && !quanLy)
            {
                mnuHoaDon.Text = "Mua hàng";
                mnuDatHang.Text = "Giỏ hàng && đặt hàng";
                ToolStripMenuItem mnuXemSP = new ToolStripMenuItem("Xem sản phẩm");
                mnuXemSP.Click += delegate { MoFormCon(new frmCuaHang()); };
                mnuHoaDon.DropDownItems.Insert(0, mnuXemSP);
                MoFormCon(new frmCuaHang());
            }
        }

        // ------------------------------------------------------------ GIAO DIỆN: nền gradient + thanh trạng thái
        private void TaoNenVaTrangThai()
        {
            // Nền vùng MDI: gradient xanh ngọc -> tím, kèm tên shop
            foreach (Control c in this.Controls)
            {
                MdiClient mdi = c as MdiClient;
                if (mdi == null) continue;
                Bitmap bmp = new Bitmap(1600, 900);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    Theme.VeGradient(g, new Rectangle(0, 0, 1600, 900), Color.FromArgb(0, 121, 140), Color.FromArgb(106, 76, 147), 45f);
                    using (SolidBrush b1 = new SolidBrush(Color.FromArgb(40, 255, 255, 255)))
                    using (SolidBrush b2 = new SolidBrush(Color.FromArgb(35, 255, 193, 7)))
                    using (SolidBrush b3 = new SolidBrush(Color.FromArgb(30, 255, 107, 107)))
                    {
                        g.FillEllipse(b1, -150, -150, 600, 600);
                        g.FillEllipse(b2, 1150, 450, 650, 650);
                        g.FillEllipse(b3, 900, -200, 450, 450);
                    }
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    using (Font f1 = new Font("Times New Roman", 54, FontStyle.Bold | FontStyle.Italic))
                    using (Font f2 = new Font("Arial", 22, FontStyle.Regular))
                    {
                        g.DrawString(Function.TenShop, f1, Brushes.White, new RectangleF(0, 300, 1600, 150), sf);
                        g.DrawString("Quản lý bán quần áo  -  Xin chào, " + PhienDangNhap.HoTen, f2,
                            new SolidBrush(Color.FromArgb(255, 235, 150)), new RectangleF(0, 450, 1600, 60), sf);
                    }
                }
                mdi.BackgroundImage = bmp;
                mdi.BackgroundImageLayout = ImageLayout.Stretch;
                break;
            }

            // Thanh trạng thái: người dùng - quyền - đồng hồ
            StatusStrip st = new StatusStrip();
            st.BackColor = Theme.PrimaryDark;
            st.SizingGrip = false;
            ToolStripStatusLabel nd = new ToolStripStatusLabel("  Người dùng: " + PhienDangNhap.HoTen + "   |   Quyền: " + PhienDangNhap.Quyen);
            nd.ForeColor = Color.White;
            nd.Spring = true;
            nd.TextAlign = ContentAlignment.MiddleLeft;
            ToolStripStatusLabel dh = new ToolStripStatusLabel(DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss"));
            dh.ForeColor = Color.FromArgb(255, 224, 130);
            st.Items.Add(nd);
            st.Items.Add(dh);
            this.Controls.Add(st);

            Timer tm = new Timer();
            tm.Interval = 1000;
            tm.Tick += delegate { dh.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss"); };
            tm.Start();
            this.FormClosed += delegate { tm.Stop(); tm.Dispose(); };
        }

        // Mở form con trong form cha MDI (nếu đã mở rồi thì chỉ kích hoạt lại)
        public void MoFormCon(Form frmCon)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == frmCon.Name)
                {
                    f.Activate();
                    frmCon.Dispose();
                    return;
                }
            }
            frmCon.MdiParent = this;
            frmCon.Dock = DockStyle.Fill;
            frmCon.Show();
        }

        // ------------------------------------------------------------ DANH MỤC
        private void quầnÁoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmQuanAo());
        }

        private void mnuLoaiThoiTrang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmDanhMuc("Loại thời trang", "tblLoaiThoiTrang", new ColSpec[] {
                new ColSpec("MaLoai", "Mã loại"),
                new ColSpec("TenLoai", "Tên loại thời trang")
            }, PhienDangNhap.Quyen == "Admin"));
        }

        private void mnuChatLieu_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmDanhMuc("Chất liệu", "tblChatLieu", new ColSpec[] {
                new ColSpec("MaChatLieu", "Mã chất liệu"),
                new ColSpec("TenChatLieu", "Tên chất liệu")
            }, PhienDangNhap.Quyen == "Admin"));
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmDanhMuc("Khách hàng", "tblKhach", new ColSpec[] {
                new ColSpec("MaKhach", "Mã khách"),
                new ColSpec("TenKhach", "Tên khách hàng"),
                new ColSpec("DiaChi", "Địa chỉ"),
                new ColSpec("DienThoai", "Điện thoại", "phone", true, null)
            }, PhienDangNhap.Quyen == "Admin" || PhienDangNhap.Quyen == "QuanLy"));
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmDanhMuc("Nhân viên", "tblNhanVien", new ColSpec[] {
                new ColSpec("MaNhanVien", "Mã nhân viên"),
                new ColSpec("TenNhanVien", "Tên nhân viên"),
                new ColSpec("GioiTinh", "Giới tính", "combo", true, new string[] { "Nam", "Nữ" }),
                new ColSpec("DiaChi", "Địa chỉ", "text", false, null),
                new ColSpec("DienThoai", "Điện thoại", "phone", false, null),
                new ColSpec("NgaySinh", "Ngày sinh", "date", false, null)
            }, PhienDangNhap.Quyen == "Admin"));
        }

        // ------------------------------------------------------------ HÓA ĐƠN
        private void mnuLapHoaDon_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmQuanLyDonHang());
        }

        private void mnuDatHang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmDatHang());
        }

        // ------------------------------------------------------------ THỐNG KÊ
        private void mnuDoanhThu_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmBaoCaoDoanhThu());
        }

        // ------------------------------------------------------------ THOÁT
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
