using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;

namespace QuanLyBanQuanAo.GUI
{
    public partial class frmChatLieu : Form
    {
        DataProcesser dtBase = new DataProcesser();

        public frmChatLieu()
        {
            InitializeComponent();
        }

        private void frmChatLieu_Load(object sender, EventArgs e)
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
                dt = dtBase.DocBang("SELECT MaChatLieu, TenChatLieu FROM tblChatLieu ORDER BY MaChatLieu");
            }
            else
            {
                dt = dtBase.DocBang("SELECT MaChatLieu, TenChatLieu FROM tblChatLieu WHERE MaChatLieu LIKE @k OR TenChatLieu LIKE @k ORDER BY MaChatLieu",
                    DataProcesser.P("@k", "%" + tk + "%"));
            }
            dgvChatLieu.DataSource = dt;
            if (dgvChatLieu.Columns.Count >= 2)
            {
                dgvChatLieu.Columns[0].HeaderText = "Mã chất liệu";
                dgvChatLieu.Columns[1].HeaderText = "Tên chất liệu";
            }
        }

        private void LamMoi()
        {
            txtMaChatLieu.Clear();
            txtTenChatLieu.Clear();
            txtMaChatLieu.ReadOnly = false;
            txtTim.Clear();
            LoadDataGridView();
            txtMaChatLieu.Focus();
        }

        private bool KiemTraNhap()
        {
            if (txtMaChatLieu.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập mã chất liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaChatLieu.Focus();
                return false;
            }
            if (txtTenChatLieu.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenChatLieu.Focus();
                return false;
            }
            return true;
        }

        private void dgvChatLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow r = dgvChatLieu.Rows[e.RowIndex];
            txtMaChatLieu.Text = Convert.ToString(r.Cells[0].Value);
            txtTenChatLieu.Text = Convert.ToString(r.Cells[1].Value);
            txtMaChatLieu.ReadOnly = true;
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaChatLieu.ReadOnly)
            {
                MessageBox.Show("Bạn đang chọn một dòng trong danh sách. Hãy bấm Làm mới để nhập chất liệu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraNhap()) return;

            string ma = txtMaChatLieu.Text.Trim();
            string ten = txtTenChatLieu.Text.Trim();

            DataTable dt = dtBase.DocBang("SELECT MaChatLieu FROM tblChatLieu WHERE MaChatLieu = @ma", DataProcesser.P("@ma", ma));
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Mã này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaChatLieu.Focus();
                return;
            }

            if (dtBase.CapNhatDuLieu("INSERT INTO tblChatLieu(MaChatLieu, TenChatLieu) VALUES(@ma, @ten)",
                DataProcesser.P("@ma", ma), DataProcesser.P("@ten", ten)))
            {
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!txtMaChatLieu.ReadOnly)
            {
                MessageBox.Show("Hãy chọn một dòng trong danh sách để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraNhap()) return;

            if (dtBase.CapNhatDuLieu("UPDATE tblChatLieu SET TenChatLieu = @ten WHERE MaChatLieu = @ma",
                DataProcesser.P("@ten", txtTenChatLieu.Text.Trim()), DataProcesser.P("@ma", txtMaChatLieu.Text.Trim())))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!txtMaChatLieu.ReadOnly)
            {
                MessageBox.Show("Hãy chọn một dòng trong danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn xóa chất liệu \"" + txtTenChatLieu.Text.Trim() + "\" không?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (dtBase.CapNhatDuLieu("DELETE FROM tblChatLieu WHERE MaChatLieu = @ma", DataProcesser.P("@ma", txtMaChatLieu.Text.Trim())))
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
            DataTable dt = dgvChatLieu.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ExcelHelper.XuatBang(dt, "DANH MỤC CHẤT LIỆU", new string[] { "Mã chất liệu", "Tên chất liệu" }, "ChatLieu");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
