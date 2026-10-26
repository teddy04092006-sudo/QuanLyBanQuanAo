using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyBanQuanAo.GUI
{
    public partial class frmQuanAo : Form
    {
        DataProcesser dtBase = new DataProcesser();
        Function func = new Function();
        string fileAnh = "";
        bool dangThemMoi = false;

        public frmQuanAo()
        {
            InitializeComponent();

            // TỰ ĐỘNG NỐI SỰ KIỆN CHO BẢNG VÀ CÁC NÚT (Không lo quên gắn bên Design)
            this.dgvQuanAo.CellClick += new DataGridViewCellEventHandler(this.dgvQuanAo_CellClick);
            this.btnAnh.Click += new EventHandler(this.btnAnh_Click);
            this.btnThemMoi.Click += new EventHandler(this.btnThemMoi_Click);
            this.btnSua.Click += new EventHandler(this.btnSua_Click);
            this.btnLuu.Click += new EventHandler(this.btnLuu_Click);
            this.btnXoa.Click += new EventHandler(this.btnXoa_Click);
            this.btnBoQua.Click += new EventHandler(this.btnBoQua_Click);
            this.btnExcel.Click += new EventHandler(this.btnExcel_Click);
            this.btnThoat.Click += new EventHandler(this.btnThoat_Click);
        }

        private void frmQuanAo_Load(object sender, EventArgs e)
        {
            // Đổ dữ liệu vào ComboBox Loại thời trang và Chất liệu
            func.FillCombo("SELECT MaLoai, TenLoai FROM tblLoaiThoiTrang", cboLoaiThoiTrang, "MaLoai", "TenLoai");
            func.FillCombo("SELECT MaChatLieu, TenChatLieu FROM tblChatLieu", cboChatLieu, "MaChatLieu", "TenChatLieu");

            cboKichCo.Items.Clear();
            cboKichCo.Items.AddRange(new string[] { "S", "M", "L", "XL", "XXL" });

            picAnh.SizeMode = PictureBoxSizeMode.StretchImage;

            LoadDataGridView();
            ResetValues();

            // Phân quyền: Chỉ Admin mới được Thêm, Sửa, Xóa sản phẩm
            if (PhienDangNhap.Quyen != "Admin")
            {
                btnThemMoi.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnBoQua.Enabled = false;
            }
        }

        private void LoadDataGridView()
        {
            string sql = @"SELECT qa.MaQuanAo, qa.TenQuanAo, l.TenLoai, cl.TenChatLieu, 
                                  qa.KichCo, qa.MauSac, qa.SoLuong, qa.DonGiaNhap, qa.DonGiaBan, qa.Anh, qa.GhiChu
                           FROM tblQuanAo qa
                           INNER JOIN tblLoaiThoiTrang l ON qa.MaLoai = l.MaLoai
                           INNER JOIN tblChatLieu cl ON qa.MaChatLieu = cl.MaChatLieu";
            dgvQuanAo.DataSource = dtBase.DocBang(sql);
            dgvQuanAo.Columns[0].HeaderText = "Mã Quần Áo";
            dgvQuanAo.Columns[1].HeaderText = "Tên Quần Áo";
            dgvQuanAo.Columns[2].HeaderText = "Loại Thời Trang";
            dgvQuanAo.Columns[3].HeaderText = "Chất Liệu";
            dgvQuanAo.Columns[4].HeaderText = "Size";
            dgvQuanAo.Columns[5].HeaderText = "Màu Sắc";
            dgvQuanAo.Columns[6].HeaderText = "Số Lượng";
            dgvQuanAo.Columns[7].HeaderText = "Giá Nhập";
            dgvQuanAo.Columns[8].HeaderText = "Giá Bán";
            dgvQuanAo.Columns[9].HeaderText = "File Ảnh";
            dgvQuanAo.Columns[10].HeaderText = "Ghi Chú";
        }

        private void ResetValues()
        {
            txtMaQuanAo.Text = "";
            txtTenQuanAo.Text = "";
            cboLoaiThoiTrang.SelectedIndex = -1;
            cboChatLieu.SelectedIndex = -1;
            cboKichCo.SelectedIndex = -1;
            txtMauSac.Text = "";
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            txtGhiChu.Text = "";
            picAnh.Image = null;
            fileAnh = "";

            txtMaQuanAo.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            if (PhienDangNhap.Quyen == "Admin")
                btnThemMoi.Enabled = true;
        }

        // Hàm nạp ảnh không gây khóa file trên ổ cứng
        private void HienThiAnh(string duongDan)
        {
            if (!string.IsNullOrEmpty(duongDan) && File.Exists(duongDan))
            {
                using (FileStream fs = new FileStream(duongDan, FileMode.Open, FileAccess.Read))
                {
                    picAnh.Image = Image.FromStream(fs);
                }
            }
            else
            {
                picAnh.Image = null;
            }
        }

        // Click vào 1 dòng trên DataGridView -> Đổ dữ liệu lên các ô và bật nút Sửa, Xóa
        private void dgvQuanAo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvQuanAo.CurrentRow == null) return;

            txtMaQuanAo.Text = dgvQuanAo.CurrentRow.Cells[0].Value.ToString();
            txtTenQuanAo.Text = dgvQuanAo.CurrentRow.Cells[1].Value.ToString();
            cboLoaiThoiTrang.Text = dgvQuanAo.CurrentRow.Cells[2].Value.ToString();
            cboChatLieu.Text = dgvQuanAo.CurrentRow.Cells[3].Value.ToString();
            cboKichCo.Text = dgvQuanAo.CurrentRow.Cells[4].Value.ToString();
            txtMauSac.Text = dgvQuanAo.CurrentRow.Cells[5].Value.ToString();
            txtSoLuong.Text = dgvQuanAo.CurrentRow.Cells[6].Value.ToString();
            txtDonGiaNhap.Text = dgvQuanAo.CurrentRow.Cells[7].Value.ToString();
            txtDonGiaBan.Text = dgvQuanAo.CurrentRow.Cells[8].Value.ToString();
            fileAnh = dgvQuanAo.CurrentRow.Cells[9].Value.ToString();
            txtGhiChu.Text = dgvQuanAo.CurrentRow.Cells[10].Value.ToString();

            string duongDanAnh = Application.StartupPath + "\\Images\\QuanAo\\" + fileAnh;
            HienThiAnh(duongDanAnh);

            if (PhienDangNhap.Quyen == "Admin")
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
                btnThemMoi.Enabled = false;
                btnLuu.Enabled = false;
            }
        }

        // Chọn ảnh sản phẩm từ máy tính và tự động copy vào thư mục dự án
        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Ảnh quần áo (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            dlgOpen.Title = "Chọn ảnh sản phẩm quần áo";

            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                string thuMucAnh = Application.StartupPath + "\\Images\\QuanAo";
                if (!Directory.Exists(thuMucAnh))
                {
                    Directory.CreateDirectory(thuMucAnh);
                }

                fileAnh = Path.GetFileName(dlgOpen.FileName);
                string duongDanDich = Path.Combine(thuMucAnh, fileAnh);
                if (!string.Equals(dlgOpen.FileName, duongDanDich, StringComparison.OrdinalIgnoreCase))
                {
                    File.Copy(dlgOpen.FileName, duongDanDich, true);
                }

                HienThiAnh(duongDanDich);
            }
        }

        // Khi bấm nút Thêm mới
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            dangThemMoi = true;
            ResetValues();
            txtMaQuanAo.Enabled = true;
            txtMaQuanAo.Focus();
            btnThemMoi.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
        }

        // Khi bấm nút Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaQuanAo.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm nào để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dangThemMoi = false;
            txtMaQuanAo.Enabled = false;
            btnThemMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            txtTenQuanAo.Focus();
        }

        // Khi bấm nút Lưu (Thực hiện Thêm mới hoặc Sửa vào SQL Server)
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaQuanAo.Text.Trim() == "" || txtTenQuanAo.Text.Trim() == "" ||
                cboLoaiThoiTrang.SelectedValue == null || cboChatLieu.SelectedValue == null ||
                cboKichCo.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập đầy đủ Mã, Tên, Loại thời trang, Chất liệu và Kích cỡ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong = 0;
            int.TryParse(txtSoLuong.Text.Trim(), out soLuong);
            double giaNhap = 0, giaBan = 0;
            double.TryParse(txtDonGiaNhap.Text.Trim(), out giaNhap);
            double.TryParse(txtDonGiaBan.Text.Trim(), out giaBan);

            if (dangThemMoi)
            {
                string sqlCheck = "SELECT MaQuanAo FROM tblQuanAo WHERE MaQuanAo = N'" + txtMaQuanAo.Text.Trim() + "'";
                if (func.CheckKey(sqlCheck))
                {
                    MessageBox.Show("Mã quần áo này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaQuanAo.Focus();
                    return;
                }

                string sqlInsert = string.Format(@"INSERT INTO tblQuanAo VALUES(N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', {6}, {7}, {8}, N'{9}', N'{10}')",
                    txtMaQuanAo.Text.Trim(), txtTenQuanAo.Text.Trim(),
                    cboLoaiThoiTrang.SelectedValue.ToString(), cboChatLieu.SelectedValue.ToString(),
                    cboKichCo.Text, txtMauSac.Text.Trim(),
                    soLuong, giaNhap, giaBan, fileAnh, txtGhiChu.Text.Trim());

                if (!dtBase.CapNhatDuLieu(sqlInsert)) return;
                MessageBox.Show("Thêm sản phẩm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string sqlUpdate = string.Format(@"UPDATE tblQuanAo SET TenQuanAo = N'{1}', MaLoai = N'{2}', MaChatLieu = N'{3}', 
                                                   KichCo = N'{4}', MauSac = N'{5}', SoLuong = {6}, DonGiaNhap = {7}, DonGiaBan = {8}, 
                                                   Anh = N'{9}', GhiChu = N'{10}' WHERE MaQuanAo = N'{0}'",
                    txtMaQuanAo.Text.Trim(), txtTenQuanAo.Text.Trim(),
                    cboLoaiThoiTrang.SelectedValue.ToString(), cboChatLieu.SelectedValue.ToString(),
                    cboKichCo.Text, txtMauSac.Text.Trim(),
                    soLuong, giaNhap, giaBan, fileAnh, txtGhiChu.Text.Trim());

                if (!dtBase.CapNhatDuLieu(sqlUpdate)) return;
                MessageBox.Show("Cập nhật thông tin sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadDataGridView();
            ResetValues();
        }

        // Khi bấm nút Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaQuanAo.Text.Trim() == "") return;
            if (MessageBox.Show("Bạn có muốn xóa sản phẩm " + txtTenQuanAo.Text + " không?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlDelete = "DELETE FROM tblQuanAo WHERE MaQuanAo = N'" + txtMaQuanAo.Text.Trim() + "'";
                if (!dtBase.CapNhatDuLieu(sqlDelete)) return;
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Xuất ra Excel
        private void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dtHang = (DataTable)dgvQuanAo.DataSource;
            if (dtHang == null || dtHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Excel.Application exApp = new Excel.Application();
            Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];
            Excel.Range exRange = (Excel.Range)exSheet.Cells[1, 1];

            exRange.Range["A2:I2"].MergeCells = true;
            exRange.Range["A2:I2"].Font.Size = 16;
            exRange.Range["A2:I2"].Font.Bold = true;
            exRange.Range["A2:I2"].Font.Color = Color.Red;
            exRange.Range["A2:I2"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:I2"].Value = "DANH SÁCH SẢN PHẨM THỜI TRANG";

            string[] headers = { "Mã Hàng", "Tên Quần Áo", "Loại Thời Trang", "Chất Liệu", "Size", "Màu Sắc", "Số Lượng", "Đơn Giá Nhập", "Đơn Giá Bán" };
            for (int col = 0; col < headers.Length; col++)
            {
                exSheet.Cells[4, col + 1] = headers[col];
            }
            exRange.Range["A4:I4"].Font.Bold = true;
            exRange.Range["A4:I4"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            for (int row = 0; row < dtHang.Rows.Count; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    exSheet.Cells[row + 5, col + 1] = dtHang.Rows[row][col].ToString();
                }
            }

            exSheet.Columns.AutoFit();
            exSheet.Name = "DS_QuanAo";
            exBook.Activate();

            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";
            dlgSave.Title = "Lưu file danh sách sản phẩm";
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                exBook.SaveAs(dlgSave.FileName);
                MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            exApp.Quit();
        }
        private void label6_Click(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
    }
}