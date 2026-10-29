namespace QuanLyBanQuanAo.GUI
{
    partial class frmLoaiThoiTrang
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
            this.lblMa = new System.Windows.Forms.Label();
            this.txtMaLoai = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTenLoai = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.lblTim = new System.Windows.Forms.Label();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.gbDanhSach = new System.Windows.Forms.GroupBox();
            this.dgvLoaiThoiTrang = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiThoiTrang)).BeginInit();
            this.gbThongTin.SuspendLayout();
            this.gbDanhSach.SuspendLayout();
            this.SuspendLayout();

            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTieuDe.Location = new System.Drawing.Point(12, 10);
            this.lblTieuDe.Size = new System.Drawing.Size(260, 22);
            this.lblTieuDe.Text = "DANH MỤC LOẠI THỜI TRANG";
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;

            this.gbThongTin.Controls.Add(this.lblMa);
            this.gbThongTin.Controls.Add(this.txtMaLoai);
            this.gbThongTin.Controls.Add(this.lblTen);
            this.gbThongTin.Controls.Add(this.txtTenLoai);
            this.gbThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.gbThongTin.Location = new System.Drawing.Point(12, 44);
            this.gbThongTin.Size = new System.Drawing.Size(776, 70);
            this.gbThongTin.Text = "Thông tin loại thời trang";
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.TabIndex = 1;
            this.gbThongTin.TabStop = false;

            this.lblMa.AutoSize = true;
            this.lblMa.Location = new System.Drawing.Point(20, 33);
            this.lblMa.Size = new System.Drawing.Size(80, 13);
            this.lblMa.Text = "Mã loại";
            this.lblMa.Name = "lblMa";
            this.lblMa.TabIndex = 0;

            this.txtMaLoai.Location = new System.Drawing.Point(130, 30);
            this.txtMaLoai.Size = new System.Drawing.Size(200, 20);
            this.txtMaLoai.MaxLength = 20;
            this.txtMaLoai.Name = "txtMaLoai";
            this.txtMaLoai.TabIndex = 1;

            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(360, 33);
            this.lblTen.Size = new System.Drawing.Size(80, 13);
            this.lblTen.Text = "Tên loại thời trang";
            this.lblTen.Name = "lblTen";
            this.lblTen.TabIndex = 2;

            this.txtTenLoai.Location = new System.Drawing.Point(465, 30);
            this.txtTenLoai.Size = new System.Drawing.Size(290, 20);
            this.txtTenLoai.Name = "txtTenLoai";
            this.txtTenLoai.TabIndex = 3;

            this.btnThem.Location = new System.Drawing.Point(12, 126);
            this.btnThem.Size = new System.Drawing.Size(100, 32);
            this.btnThem.Text = "Thêm";
            this.btnThem.Name = "btnThem";
            this.btnThem.TabIndex = 2;
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Location = new System.Drawing.Point(122, 126);
            this.btnSua.Size = new System.Drawing.Size(100, 32);
            this.btnSua.Text = "Sửa";
            this.btnSua.Name = "btnSua";
            this.btnSua.TabIndex = 3;
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Location = new System.Drawing.Point(232, 126);
            this.btnXoa.Size = new System.Drawing.Size(100, 32);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.TabIndex = 4;
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Location = new System.Drawing.Point(342, 126);
            this.btnLamMoi.Size = new System.Drawing.Size(100, 32);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            this.btnExcel.Location = new System.Drawing.Point(452, 126);
            this.btnExcel.Size = new System.Drawing.Size(100, 32);
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.TabIndex = 6;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);

            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.Location = new System.Drawing.Point(688, 126);
            this.btnThoat.Size = new System.Drawing.Size(100, 32);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 7;
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            this.lblTim.AutoSize = true;
            this.lblTim.Location = new System.Drawing.Point(12, 175);
            this.lblTim.Size = new System.Drawing.Size(55, 13);
            this.lblTim.Text = "Tìm kiếm";
            this.lblTim.Name = "lblTim";
            this.lblTim.TabIndex = 8;

            this.txtTim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTim.Location = new System.Drawing.Point(75, 172);
            this.txtTim.Size = new System.Drawing.Size(300, 20);
            this.txtTim.Name = "txtTim";
            this.txtTim.TabIndex = 9;
            this.txtTim.TextChanged += new System.EventHandler(this.txtTim_TextChanged);

            this.gbDanhSach.Controls.Add(this.dgvLoaiThoiTrang);
            this.gbDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDanhSach.Location = new System.Drawing.Point(12, 204);
            this.gbDanhSach.Size = new System.Drawing.Size(776, 264);
            this.gbDanhSach.Text = "Danh sách loại thời trang";
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.TabIndex = 10;
            this.gbDanhSach.TabStop = false;

            this.dgvLoaiThoiTrang.AllowUserToAddRows = false;
            this.dgvLoaiThoiTrang.AllowUserToDeleteRows = false;
            this.dgvLoaiThoiTrang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoaiThoiTrang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoaiThoiTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLoaiThoiTrang.Location = new System.Drawing.Point(3, 16);
            this.dgvLoaiThoiTrang.MultiSelect = false;
            this.dgvLoaiThoiTrang.ReadOnly = true;
            this.dgvLoaiThoiTrang.RowHeadersVisible = false;
            this.dgvLoaiThoiTrang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoaiThoiTrang.Size = new System.Drawing.Size(770, 245);
            this.dgvLoaiThoiTrang.Name = "dgvLoaiThoiTrang";
            this.dgvLoaiThoiTrang.TabIndex = 0;
            this.dgvLoaiThoiTrang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLoaiThoiTrang_CellClick);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục loại thời trang";
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
            this.Name = "frmLoaiThoiTrang";
            this.Load += new System.EventHandler(this.frmLoaiThoiTrang_Load);
            this.gbDanhSach.ResumeLayout(false);
            this.gbDanhSach.PerformLayout();
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiThoiTrang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.TextBox txtMaLoai;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTenLoai;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.GroupBox gbDanhSach;
        private System.Windows.Forms.DataGridView dgvLoaiThoiTrang;
    }
}