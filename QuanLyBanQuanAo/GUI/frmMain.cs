using System;
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
