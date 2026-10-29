using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;

namespace QuanLyBanQuanAo.GUI
{
    public partial class frmKhachHang : Form
    {
        DataProcesser dtBase = new DataProcesser();
        Function func = new Function();

        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            bool coQuyenSua = PhienDangNhap.Quyen == "Admin" || PhienDangNhap.Quyen == "QuanLy";
            btnThem.Enabled = coQuyenSua;
            btnSua.Enabled = coQuyenSua;
            btnXoa.Enabled = coQuyenSua;
            LamMoi();
        }

        private void LoadDataGridView()
        {
            string tk = txtTim.Text.Trim();
            DataTable dt;
            if (tk == "")
            {
                dt = dtBase.DocBang("SELECT MaKhach, TenKhach, DienThoai, DiaChi FROM tblKhach ORDER BY MaKhach");
            }
            else
            {
                dt = dtBase.DocBang("SELECT MaKhach, TenKhach, DienThoai, DiaChi FROM tblKhach " +
                                    "WHERE MaKhach LIKE @k OR TenKhach LIKE @k OR DienThoai LIKE @k OR DiaChi LIKE @k ORDER BY MaKhach",
                    DataProcesser.P("@k", "%" + tk + "%"));
            }
            dgvKhachHang.DataSource = dt;
            if (dgvKhachHang.Columns.Count >= 4)
            {
                dgvKhachHang.Columns[0].HeaderText = "Mã khách";
                dgvKhachHang.Columns[1].HeaderText = "Tên khách hàng";
                dgvKhachHang.Columns[2].HeaderText = "Điện thoại";
                dgvKhachHang.Columns[3].HeaderText = "Địa chỉ";
            }
        }

        private void LamMoi()
        {
            txtMaKhach.Clear();
            txtTenKhach.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            txtMaKhach.ReadOnly = false;
            txtTim.Clear();
            LoadDataGridView();
            txtMaKhach.Focus();
        }

        private bool KiemTraNhap()
        {
            if (txtMaKhach.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập mã khách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKhach.Focus();
                return false;
            }
            if (txtTenKhach.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhach.Focus();
                return false;
            }
            if (!func.KiemTraSoDienThoai(txtDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại phải bắt đầu bằng 0 và gồm đúng 10 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return false;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }
            DataTable trung = dtBase.DocBang("SELECT MaKhach FROM tblKhach WHERE DienThoai = @sdt AND MaKhach <> @ma",
                DataProcesser.P("@sdt", txtDienThoai.Text.Trim()), DataProcesser.P("@ma", txtMaKhach.Text.Trim()));
            if (trung.Rows.Count > 0)
            {
                MessageBox.Show("Số điện thoại này đã thuộc về khách hàng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return false;
            }
            return true;
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow r = dgvKhachHang.Rows[e.RowIndex];
            txtMaKhach.Text = Convert.ToString(r.Cells[0].Value);
            txtTenKhach.Text = Convert.ToString(r.Cells[1].Value);
            txtDienThoai.Text = Convert.ToString(r.Cells[2].Value);
            txtDiaChi.Text = Convert.ToString(r.Cells[3].Value);
            txtMaKhach.ReadOnly = true;
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaKhach.ReadOnly)
            {
                MessageBox.Show("Bạn đang chọn một dòng trong danh sách. Hãy bấm Làm mới để nhập khách hàng mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraNhap()) return;

            DataTable dt = dtBase.DocBang("SELECT MaKhach FROM tblKhach WHERE MaKhach = @ma", DataProcesser.P("@ma", txtMaKhach.Text.Trim()));
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Mã khách này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKhach.Focus();
                return;
            }

            if (dtBase.CapNhatDuLieu("INSERT INTO tblKhach(MaKhach, TenKhach, DiaChi, DienThoai) VALUES(@ma, @ten, @dc, @sdt)",
                DataProcesser.P("@ma", txtMaKhach.Text.Trim()), DataProcesser.P("@ten", txtTenKhach.Text.Trim()),
                DataProcesser.P("@dc", txtDiaChi.Text.Trim()), DataProcesser.P("@sdt", txtDienThoai.Text.Trim())))
            {
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!txtMaKhach.ReadOnly)
            {
                MessageBox.Show("Hãy chọn một dòng trong danh sách để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraNhap()) return;

            if (dtBase.CapNhatDuLieu("UPDATE tblKhach SET TenKhach = @ten, DiaChi = @dc, DienThoai = @sdt WHERE MaKhach = @ma",
                DataProcesser.P("@ten", txtTenKhach.Text.Trim()), DataProcesser.P("@dc", txtDiaChi.Text.Trim()),
                DataProcesser.P("@sdt", txtDienThoai.Text.Trim()), DataProcesser.P("@ma", txtMaKhach.Text.Trim())))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!txtMaKhach.ReadOnly)
            {
                MessageBox.Show("Hãy chọn một dòng trong danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng \"" + txtTenKhach.Text.Trim() + "\" không?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (dtBase.CapNhatDuLieu("DELETE FROM tblKhach WHERE MaKhach = @ma", DataProcesser.P("@ma", txtMaKhach.Text.Trim())))
            {
                MessageBox.Show("Đã xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = dgvKhachHang.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ExcelHelper.XuatBang(dt, "DANH MỤC KHÁCH HÀNG", new string[] { "Mã khách", "Tên khách hàng", "Điện thoại", "Địa chỉ" }, "KhachHang");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
