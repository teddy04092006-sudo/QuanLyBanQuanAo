namespace QuanLyBanQuanAo.GUI
{
    partial class frmNhanVien
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.gbThongTin = new System.Windows.Forms.GroupBox();
            this.lblMaNhanVien = new System.Windows.Forms.Label();
            this.txtMaNhanVien = new System.Windows.Forms.TextBox();
            this.lblTenNhanVien = new System.Windows.Forms.Label();
            this.txtTenNhanVien = new System.Windows.Forms.TextBox();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.rdoNam = new System.Windows.Forms.RadioButton();
            this.rdoNu = new System.Windows.Forms.RadioButton();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.lblTim = new System.Windows.Forms.Label();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.gbDanhSach = new System.Windows.Forms.GroupBox();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            this.gbThongTin.SuspendLayout();
            this.gbDanhSach.SuspendLayout();
            this.SuspendLayout();

            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTieuDe.Location = new System.Drawing.Point(12, 10);
            this.lblTieuDe.Size = new System.Drawing.Size(260, 22);
            this.lblTieuDe.Text = "DANH MỤC NHÂN VIÊN";
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;

            this.gbThongTin.Controls.Add(this.lblMaNhanVien);
            this.gbThongTin.Controls.Add(this.txtMaNhanVien);
            this.gbThongTin.Controls.Add(this.lblTenNhanVien);
            this.gbThongTin.Controls.Add(this.txtTenNhanVien);
            this.gbThongTin.Controls.Add(this.lblGioiTinh);
            this.gbThongTin.Controls.Add(this.rdoNam);
            this.gbThongTin.Controls.Add(this.rdoNu);
            this.gbThongTin.Controls.Add(this.lblNgaySinh);
            this.gbThongTin.Controls.Add(this.dtpNgaySinh);
            this.gbThongTin.Controls.Add(this.lblDienThoai);
            this.gbThongTin.Controls.Add(this.txtDienThoai);
            this.gbThongTin.Controls.Add(this.lblDiaChi);
            this.gbThongTin.Controls.Add(this.txtDiaChi);
            this.gbThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.gbThongTin.Location = new System.Drawing.Point(12, 44);
            this.gbThongTin.Size = new System.Drawing.Size(776, 146);
            this.gbThongTin.Text = "Thông tin nhân viên";
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.TabIndex = 1;
            this.gbThongTin.TabStop = false;

            this.lblMaNhanVien.AutoSize = true;
            this.lblMaNhanVien.Location = new System.Drawing.Point(20, 33);
            this.lblMaNhanVien.Size = new System.Drawing.Size(80, 13);
            this.lblMaNhanVien.Text = "Mã nhân viên";
            this.lblMaNhanVien.Name = "lblMaNhanVien";
            this.lblMaNhanVien.TabIndex = 0;

            this.txtMaNhanVien.Location = new System.Drawing.Point(130, 30);
            this.txtMaNhanVien.Size = new System.Drawing.Size(200, 20);
            this.txtMaNhanVien.MaxLength = 20;
            this.txtMaNhanVien.Name = "txtMaNhanVien";
            this.txtMaNhanVien.TabIndex = 1;

            this.lblTenNhanVien.AutoSize = true;
            this.lblTenNhanVien.Location = new System.Drawing.Point(360, 33);
            this.lblTenNhanVien.Size = new System.Drawing.Size(80, 13);
            this.lblTenNhanVien.Text = "Tên nhân viên";
            this.lblTenNhanVien.Name = "lblTenNhanVien";
            this.lblTenNhanVien.TabIndex = 2;

            this.txtTenNhanVien.Location = new System.Drawing.Point(465, 30);
            this.txtTenNhanVien.Size = new System.Drawing.Size(290, 20);
            this.txtTenNhanVien.MaxLength = 100;
            this.txtTenNhanVien.Name = "txtTenNhanVien";
            this.txtTenNhanVien.TabIndex = 3;

            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Location = new System.Drawing.Point(20, 71);
            this.lblGioiTinh.Size = new System.Drawing.Size(80, 13);
            this.lblGioiTinh.Text = "Giới tính";
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.TabIndex = 4;

            this.rdoNam.AutoSize = true;
            this.rdoNam.Checked = true;
            this.rdoNam.Location = new System.Drawing.Point(130, 69);
            this.rdoNam.Size = new System.Drawing.Size(51, 17);
            this.rdoNam.TabStop = true;
            this.rdoNam.Text = "Nam";
            this.rdoNam.Name = "rdoNam";
            this.rdoNam.TabIndex = 5;

            this.rdoNu.AutoSize = true;
            this.rdoNu.Location = new System.Drawing.Point(200, 69);
            this.rdoNu.Size = new System.Drawing.Size(39, 17);
            this.rdoNu.Text = "Nữ";
            this.rdoNu.Name = "rdoNu";
            this.rdoNu.TabIndex = 6;

            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Location = new System.Drawing.Point(360, 71);
            this.lblNgaySinh.Size = new System.Drawing.Size(80, 13);
            this.lblNgaySinh.Text = "Ngày sinh";
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.TabIndex = 7;

            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(465, 67);
            this.dtpNgaySinh.Size = new System.Drawing.Size(120, 20);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.TabIndex = 8;

            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Location = new System.Drawing.Point(20, 109);
            this.lblDienThoai.Size = new System.Drawing.Size(80, 13);
            this.lblDienThoai.Text = "Điện thoại";
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.TabIndex = 9;

            this.txtDienThoai.Location = new System.Drawing.Point(130, 106);
            this.txtDienThoai.Size = new System.Drawing.Size(200, 20);
            this.txtDienThoai.MaxLength = 20;
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.TabIndex = 10;

            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(360, 109);
            this.lblDiaChi.Size = new System.Drawing.Size(80, 13);
            this.lblDiaChi.Text = "Địa chỉ";
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.TabIndex = 11;

            this.txtDiaChi.Location = new System.Drawing.Point(465, 106);
            this.txtDiaChi.Size = new System.Drawing.Size(290, 20);
            this.txtDiaChi.MaxLength = 200;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.TabIndex = 12;

            this.btnThem.Location = new System.Drawing.Point(12, 202);
            this.btnThem.Size = new System.Drawing.Size(100, 32);
            this.btnThem.Text = "Thêm";
            this.btnThem.Name = "btnThem";
            this.btnThem.TabIndex = 2;
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Location = new System.Drawing.Point(122, 202);
            this.btnSua.Size = new System.Drawing.Size(100, 32);
            this.btnSua.Text = "Sửa";
            this.btnSua.Name = "btnSua";
            this.btnSua.TabIndex = 3;
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Location = new System.Drawing.Point(232, 202);
            this.btnXoa.Size = new System.Drawing.Size(100, 32);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.TabIndex = 4;
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Location = new System.Drawing.Point(342, 202);
            this.btnLamMoi.Size = new System.Drawing.Size(100, 32);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            this.btnExcel.Location = new System.Drawing.Point(452, 202);
            this.btnExcel.Size = new System.Drawing.Size(100, 32);
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.TabIndex = 6;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);

            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.Location = new System.Drawing.Point(688, 202);
            this.btnThoat.Size = new System.Drawing.Size(100, 32);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 7;
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            this.lblTim.AutoSize = true;
            this.lblTim.Location = new System.Drawing.Point(12, 251);
            this.lblTim.Size = new System.Drawing.Size(55, 13);
            this.lblTim.Text = "Tìm kiếm";
            this.lblTim.Name = "lblTim";
            this.lblTim.TabIndex = 8;

            this.txtTim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTim.Location = new System.Drawing.Point(75, 248);
            this.txtTim.Size = new System.Drawing.Size(300, 20);
            this.txtTim.Name = "txtTim";
            this.txtTim.TabIndex = 9;
            this.txtTim.TextChanged += new System.EventHandler(this.txtTim_TextChanged);

            this.gbDanhSach.Controls.Add(this.dgvNhanVien);
            this.gbDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDanhSach.Location = new System.Drawing.Point(12, 280);
            this.gbDanhSach.Size = new System.Drawing.Size(776, 268);
            this.gbDanhSach.Text = "Danh sách nhân viên";
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.TabIndex = 10;
            this.gbDanhSach.TabStop = false;

            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhanVien.Location = new System.Drawing.Point(3, 16);
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersVisible = false;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new System.Drawing.Size(770, 249);
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.TabIndex = 0;
            this.dgvNhanVien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhanVien_CellClick);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục nhân viên";
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.gbThongTin);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.lblTim);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.gbDanhSach);
            this.Name = "frmNhanVien";
            this.Load += new System.EventHandler(this.frmNhanVien_Load);
            this.gbDanhSach.ResumeLayout(false);
            this.gbDanhSach.PerformLayout();
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.Label lblMaNhanVien;
        private System.Windows.Forms.TextBox txtMaNhanVien;
        private System.Windows.Forms.Label lblTenNhanVien;
        private System.Windows.Forms.TextBox txtTenNhanVien;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.RadioButton rdoNam;
        private System.Windows.Forms.RadioButton rdoNu;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.GroupBox gbDanhSach;
        private System.Windows.Forms.DataGridView dgvNhanVien;
    }
}