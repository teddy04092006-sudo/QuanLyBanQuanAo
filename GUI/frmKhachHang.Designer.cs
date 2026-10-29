namespace QuanLyBanQuanAo.GUI
{
    partial class frmKhachHang
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
            this.lblMaKhach = new System.Windows.Forms.Label();
            this.txtMaKhach = new System.Windows.Forms.TextBox();
            this.lblTenKhach = new System.Windows.Forms.Label();
            this.txtTenKhach = new System.Windows.Forms.TextBox();
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
            this.dgvKhachHang = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            this.gbThongTin.SuspendLayout();
            this.gbDanhSach.SuspendLayout();
            this.SuspendLayout();

            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTieuDe.Location = new System.Drawing.Point(12, 10);
            this.lblTieuDe.Size = new System.Drawing.Size(260, 22);
            this.lblTieuDe.Text = "DANH MỤC KHÁCH HÀNG";
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;

            this.gbThongTin.Controls.Add(this.lblMaKhach);
            this.gbThongTin.Controls.Add(this.txtMaKhach);
            this.gbThongTin.Controls.Add(this.lblTenKhach);
            this.gbThongTin.Controls.Add(this.txtTenKhach);
            this.gbThongTin.Controls.Add(this.lblDienThoai);
            this.gbThongTin.Controls.Add(this.txtDienThoai);
            this.gbThongTin.Controls.Add(this.lblDiaChi);
            this.gbThongTin.Controls.Add(this.txtDiaChi);
            this.gbThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.gbThongTin.Location = new System.Drawing.Point(12, 44);
            this.gbThongTin.Size = new System.Drawing.Size(776, 108);
            this.gbThongTin.Text = "Thông tin khách hàng";
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.TabIndex = 1;
            this.gbThongTin.TabStop = false;

            this.lblMaKhach.AutoSize = true;
            this.lblMaKhach.Location = new System.Drawing.Point(20, 33);
            this.lblMaKhach.Size = new System.Drawing.Size(80, 13);
            this.lblMaKhach.Text = "Mã khách";
            this.lblMaKhach.Name = "lblMaKhach";
            this.lblMaKhach.TabIndex = 0;

            this.txtMaKhach.Location = new System.Drawing.Point(130, 30);
            this.txtMaKhach.Size = new System.Drawing.Size(200, 20);
            this.txtMaKhach.MaxLength = 20;
            this.txtMaKhach.Name = "txtMaKhach";
            this.txtMaKhach.TabIndex = 1;

            this.lblTenKhach.AutoSize = true;
            this.lblTenKhach.Location = new System.Drawing.Point(360, 33);
            this.lblTenKhach.Size = new System.Drawing.Size(80, 13);
            this.lblTenKhach.Text = "Tên khách hàng";
            this.lblTenKhach.Name = "lblTenKhach";
            this.lblTenKhach.TabIndex = 2;

            this.txtTenKhach.Location = new System.Drawing.Point(465, 30);
            this.txtTenKhach.Size = new System.Drawing.Size(290, 20);
            this.txtTenKhach.MaxLength = 100;
            this.txtTenKhach.Name = "txtTenKhach";
            this.txtTenKhach.TabIndex = 3;

            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Location = new System.Drawing.Point(20, 71);
            this.lblDienThoai.Size = new System.Drawing.Size(80, 13);
            this.lblDienThoai.Text = "Điện thoại";
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.TabIndex = 4;

            this.txtDienThoai.Location = new System.Drawing.Point(130, 68);
            this.txtDienThoai.Size = new System.Drawing.Size(200, 20);
            this.txtDienThoai.MaxLength = 20;
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.TabIndex = 5;

            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(360, 71);
            this.lblDiaChi.Size = new System.Drawing.Size(80, 13);
            this.lblDiaChi.Text = "Địa chỉ";
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.TabIndex = 6;

            this.txtDiaChi.Location = new System.Drawing.Point(465, 68);
            this.txtDiaChi.Size = new System.Drawing.Size(290, 20);
            this.txtDiaChi.MaxLength = 200;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.TabIndex = 7;

            this.btnThem.Location = new System.Drawing.Point(12, 164);
            this.btnThem.Size = new System.Drawing.Size(100, 32);
            this.btnThem.Text = "Thêm";
            this.btnThem.Name = "btnThem";
            this.btnThem.TabIndex = 2;
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Location = new System.Drawing.Point(122, 164);
            this.btnSua.Size = new System.Drawing.Size(100, 32);
            this.btnSua.Text = "Sửa";
            this.btnSua.Name = "btnSua";
            this.btnSua.TabIndex = 3;
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Location = new System.Drawing.Point(232, 164);
            this.btnXoa.Size = new System.Drawing.Size(100, 32);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.TabIndex = 4;
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Location = new System.Drawing.Point(342, 164);
            this.btnLamMoi.Size = new System.Drawing.Size(100, 32);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            this.btnExcel.Location = new System.Drawing.Point(452, 164);
            this.btnExcel.Size = new System.Drawing.Size(100, 32);
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.TabIndex = 6;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);

            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.Location = new System.Drawing.Point(688, 164);
            this.btnThoat.Size = new System.Drawing.Size(100, 32);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 7;
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            this.lblTim.AutoSize = true;
            this.lblTim.Location = new System.Drawing.Point(12, 213);
            this.lblTim.Size = new System.Drawing.Size(55, 13);
            this.lblTim.Text = "Tìm kiếm";
            this.lblTim.Name = "lblTim";
            this.lblTim.TabIndex = 8;

            this.txtTim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTim.Location = new System.Drawing.Point(75, 210);
            this.txtTim.Size = new System.Drawing.Size(300, 20);
            this.txtTim.Name = "txtTim";
            this.txtTim.TabIndex = 9;
            this.txtTim.TextChanged += new System.EventHandler(this.txtTim_TextChanged);

            this.gbDanhSach.Controls.Add(this.dgvKhachHang);
            this.gbDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDanhSach.Location = new System.Drawing.Point(12, 242);
            this.gbDanhSach.Size = new System.Drawing.Size(776, 266);
            this.gbDanhSach.Text = "Danh sách khách hàng";
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.TabIndex = 10;
            this.gbDanhSach.TabStop = false;

            this.dgvKhachHang.AllowUserToAddRows = false;
            this.dgvKhachHang.AllowUserToDeleteRows = false;
            this.dgvKhachHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhachHang.Location = new System.Drawing.Point(3, 16);
            this.dgvKhachHang.MultiSelect = false;
            this.dgvKhachHang.ReadOnly = true;
            this.dgvKhachHang.RowHeadersVisible = false;
            this.dgvKhachHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhachHang.Size = new System.Drawing.Size(770, 247);
            this.dgvKhachHang.Name = "dgvKhachHang";
            this.dgvKhachHang.TabIndex = 0;
            this.dgvKhachHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKhachHang_CellClick);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục khách hàng";
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
            this.Name = "frmKhachHang";
            this.Load += new System.EventHandler(this.frmKhachHang_Load);
            this.gbDanhSach.ResumeLayout(false);
            this.gbDanhSach.PerformLayout();
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.Label lblMaKhach;
        private System.Windows.Forms.TextBox txtMaKhach;
        private System.Windows.Forms.Label lblTenKhach;
        private System.Windows.Forms.TextBox txtTenKhach;
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
        private System.Windows.Forms.DataGridView dgvKhachHang;
    }
}