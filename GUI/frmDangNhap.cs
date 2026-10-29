using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;


namespace QuanLyBanQuanAo.GUI
{
    public partial class frmDangNhap : Form
    {
        DataProcesser dtBase = new DataProcesser();
        public frmDangNhap()
        {
            InitializeComponent();
            Theme.Apply(this);
            
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text.Trim() == "" || txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = dtBase.DocBang("SELECT * FROM tblTaiKhoan WHERE TenDangNhap = @u AND MatKhau = @p",
                DataProcesser.P("@u", txtTenDangNhap.Text.Trim()), DataProcesser.P("@p", txtMatKhau.Text.Trim()));

            if (dt.Rows.Count > 0)
            {
                PhienDangNhap.TenDangNhap = dt.Rows[0]["TenDangNhap"].ToString();
                PhienDangNhap.HoTen = dt.Rows[0]["HoTen"].ToString();
                PhienDangNhap.Quyen = dt.Rows[0]["Quyen"].ToString();

                MessageBox.Show("Đăng nhập thành công với quyền: " + PhienDangNhap.Quyen, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmMain f = new frmMain();
                this.Hide();
                f.ShowDialog();
                txtMatKhau.Clear();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnKhachMuaHang_Click(object sender, EventArgs e)
        {
            PhienDangNhap.TenDangNhap = "khach";
            PhienDangNhap.HoTen = "Khách mua hàng";
            PhienDangNhap.Quyen = "NguoiMua";

            frmMain f = new frmMain();
            this.Hide();
            f.ShowDialog();
            this.Show();

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
