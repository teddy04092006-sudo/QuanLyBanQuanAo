using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;

namespace QuanLyBanQuanAo.GUI
{
    public partial class frmLoaiThoiTrang : Form
    {
        DataProcesser dtBase = new DataProcesser();

        public frmLoaiThoiTrang()
        {
            InitializeComponent();
        }

        private void frmLoaiThoiTrang_Load(object sender, EventArgs e)
        {
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
                dt = dtBase.DocBang("SELECT MaLoai, TenLoai FROM tblLoaiThoiTrang ORDER BY MaLoai");
            }
            else
            {
                dt = dtBase.DocBang("SELECT MaLoai, TenLoai FROM tblLoaiThoiTrang WHERE MaLoai LIKE @k OR TenLoai LIKE @k ORDER BY MaLoai",
                    DataProcesser.P("@k", "%" + tk + "%"));
            }
            dgvLoaiThoiTrang.DataSource = dt;
            if (dgvLoaiThoiTrang.Columns.Count >= 2)
            {
                dgvLoaiThoiTrang.Columns[0].HeaderText = "Mã loại";
                dgvLoaiThoiTrang.Columns[1].HeaderText = "Tên loại thời trang";
            }
        }

        private void LamMoi()
        {
            txtMaLoai.Clear();
            txtTenLoai.Clear();
            txtMaLoai.ReadOnly = false;
            txtTim.Clear();
            LoadDataGridView();
            txtMaLoai.Focus();
        }

        private bool KiemTraNhap()
        {
            if (txtMaLoai.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập mã loại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoai.Focus();
                return false;
            }
            if (txtTenLoai.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập tên loại thời trang!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return false;
            }
            return true;
        }

        private void dgvLoaiThoiTrang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow r = dgvLoaiThoiTrang.Rows[e.RowIndex];
            txtMaLoai.Text = Convert.ToString(r.Cells[0].Value);
            txtTenLoai.Text = Convert.ToString(r.Cells[1].Value);
            txtMaLoai.ReadOnly = true;
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaLoai.ReadOnly)
            {
                MessageBox.Show("Bạn đang chọn một dòng trong danh sách. Hãy bấm Làm mới để nhập loại thời trang mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraNhap()) return;

            string ma = txtMaLoai.Text.Trim();
            string ten = txtTenLoai.Text.Trim();

            DataTable dt = dtBase.DocBang("SELECT MaLoai FROM tblLoaiThoiTrang WHERE MaLoai = @ma", DataProcesser.P("@ma", ma));
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Mã này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoai.Focus();
                return;
            }

            if (dtBase.CapNhatDuLieu("INSERT INTO tblLoaiThoiTrang(MaLoai, TenLoai) VALUES(@ma, @ten)",
                DataProcesser.P("@ma", ma), DataProcesser.P("@ten", ten)))
            {
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!txtMaLoai.ReadOnly)
            {
                MessageBox.Show("Hãy chọn một dòng trong danh sách để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraNhap()) return;

            if (dtBase.CapNhatDuLieu("UPDATE tblLoaiThoiTrang SET TenLoai = @ten WHERE MaLoai = @ma",
                DataProcesser.P("@ten", txtTenLoai.Text.Trim()), DataProcesser.P("@ma", txtMaLoai.Text.Trim())))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!txtMaLoai.ReadOnly)
            {
                MessageBox.Show("Hãy chọn một dòng trong danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn xóa loại thời trang \"" + txtTenLoai.Text.Trim() + "\" không?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (dtBase.CapNhatDuLieu("DELETE FROM tblLoaiThoiTrang WHERE MaLoai = @ma", DataProcesser.P("@ma", txtMaLoai.Text.Trim())))
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
            DataTable dt = dgvLoaiThoiTrang.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ExcelHelper.XuatBang(dt, "DANH MỤC LOẠI THỜI TRANG", new string[] { "Mã loại", "Tên loại thời trang" }, "LoaiThoiTrang");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
