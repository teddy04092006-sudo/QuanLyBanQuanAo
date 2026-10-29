using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;

namespace QuanLyBanQuanAo.GUI
{
    public partial class frmNhanVien : Form
    {
        DataProcesser dtBase = new DataProcesser();
        Function func = new Function();

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            dtpNgaySinh.MaxDate = DateTime.Today;
            bool coQuyenSua = PhienDangNhap.Quyen == "Admin";
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
                dt = dtBase.DocBang("SELECT MaNhanVien, TenNhanVien, GioiTinh, NgaySinh, DienThoai, DiaChi FROM tblNhanVien ORDER BY MaNhanVien");
            }
            else
            {
                dt = dtBase.DocBang("SELECT MaNhanVien, TenNhanVien, GioiTinh, NgaySinh, DienThoai, DiaChi FROM tblNhanVien " +
                                    "WHERE MaNhanVien LIKE @k OR TenNhanVien LIKE @k OR DienThoai LIKE @k OR DiaChi LIKE @k ORDER BY MaNhanVien",
                    DataProcesser.P("@k", "%" + tk + "%"));
            }
            dgvNhanVien.DataSource = dt;
            if (dgvNhanVien.Columns.Count >= 6)
            {
                dgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
                dgvNhanVien.Columns[1].HeaderText = "Tên nhân viên";
                dgvNhanVien.Columns[2].HeaderText = "Giới tính";
                dgvNhanVien.Columns[3].HeaderText = "Ngày sinh";
                dgvNhanVien.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvNhanVien.Columns[4].HeaderText = "Điện thoại";
                dgvNhanVien.Columns[5].HeaderText = "Địa chỉ";
            }
        }

        private void LamMoi()
        {
            txtMaNhanVien.Clear();
            txtTenNhanVien.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            rdoNam.Checked = true;
            dtpNgaySinh.Value = DateTime.Today.AddYears(-18);
            txtMaNhanVien.ReadOnly = false;
            txtTim.Clear();
            LoadDataGridView();
            txtMaNhanVien.Focus();
        }

        private bool KiemTraNhap()
        {
            if (txtMaNhanVien.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return false;
            }
            if (txtTenNhanVien.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return false;
            }
            if (dtpNgaySinh.Value.Date > DateTime.Today.AddYears(-15))
            {
                MessageBox.Show("Nhân viên phải từ 15 tuổi trở lên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }
            if (txtDienThoai.Text.Trim() != "" && !func.KiemTraSoDienThoai(txtDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại phải bắt đầu bằng 0 và gồm đúng 10 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return false;
            }
            return true;
        }

        private object GiaTri(string s)
        {
            s = s.Trim();
            if (s == "") return null;
            return s;
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow r = dgvNhanVien.Rows[e.RowIndex];
            txtMaNhanVien.Text = Convert.ToString(r.Cells[0].Value);
            txtTenNhanVien.Text = Convert.ToString(r.Cells[1].Value);
            if (Convert.ToString(r.Cells[2].Value) == "Nữ") rdoNu.Checked = true; else rdoNam.Checked = true;
            if (r.Cells[3].Value is DateTime) dtpNgaySinh.Value = (DateTime)r.Cells[3].Value;
            else dtpNgaySinh.Value = DateTime.Today.AddYears(-18);
            txtDienThoai.Text = Convert.ToString(r.Cells[4].Value);
            txtDiaChi.Text = Convert.ToString(r.Cells[5].Value);
            txtMaNhanVien.ReadOnly = true;
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaNhanVien.ReadOnly)
            {
                MessageBox.Show("Bạn đang chọn một dòng trong danh sách. Hãy bấm Làm mới để nhập nhân viên mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraNhap()) return;

            DataTable dt = dtBase.DocBang("SELECT MaNhanVien FROM tblNhanVien WHERE MaNhanVien = @ma", DataProcesser.P("@ma", txtMaNhanVien.Text.Trim()));
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }

            if (dtBase.CapNhatDuLieu("INSERT INTO tblNhanVien(MaNhanVien, TenNhanVien, GioiTinh, DiaChi, DienThoai, NgaySinh) " +
                                     "VALUES(@ma, @ten, @gt, @dc, @sdt, @ns)",
                DataProcesser.P("@ma", txtMaNhanVien.Text.Trim()), DataProcesser.P("@ten", txtTenNhanVien.Text.Trim()),
                DataProcesser.P("@gt", rdoNam.Checked ? "Nam" : "Nữ"), DataProcesser.P("@dc", GiaTri(txtDiaChi.Text)),
                DataProcesser.P("@sdt", GiaTri(txtDienThoai.Text)), DataProcesser.P("@ns", dtpNgaySinh.Value.Date)))
            {
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!txtMaNhanVien.ReadOnly)
            {
                MessageBox.Show("Hãy chọn một dòng trong danh sách để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraNhap()) return;

            if (dtBase.CapNhatDuLieu("UPDATE tblNhanVien SET TenNhanVien = @ten, GioiTinh = @gt, DiaChi = @dc, DienThoai = @sdt, NgaySinh = @ns " +
                                     "WHERE MaNhanVien = @ma",
                DataProcesser.P("@ten", txtTenNhanVien.Text.Trim()), DataProcesser.P("@gt", rdoNam.Checked ? "Nam" : "Nữ"),
                DataProcesser.P("@dc", GiaTri(txtDiaChi.Text)), DataProcesser.P("@sdt", GiaTri(txtDienThoai.Text)),
                DataProcesser.P("@ns", dtpNgaySinh.Value.Date), DataProcesser.P("@ma", txtMaNhanVien.Text.Trim())))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!txtMaNhanVien.ReadOnly)
            {
                MessageBox.Show("Hãy chọn một dòng trong danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên \"" + txtTenNhanVien.Text.Trim() + "\" không?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (dtBase.CapNhatDuLieu("DELETE FROM tblNhanVien WHERE MaNhanVien = @ma", DataProcesser.P("@ma", txtMaNhanVien.Text.Trim())))
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
            DataTable dt = dgvNhanVien.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ExcelHelper.XuatBang(dt, "DANH MỤC NHÂN VIÊN",
                new string[] { "Mã nhân viên", "Tên nhân viên", "Giới tính", "Ngày sinh", "Điện thoại", "Địa chỉ" }, "NhanVien");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
