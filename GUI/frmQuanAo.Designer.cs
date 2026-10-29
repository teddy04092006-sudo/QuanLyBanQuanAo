namespace QuanLyBanQuanAo.GUI
{
    partial class frmQuanAo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMaQuanAo = new System.Windows.Forms.TextBox();
            this.txtTenQuanAo = new System.Windows.Forms.TextBox();
            this.txtMauSac = new System.Windows.Forms.TextBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.txtDonGiaNhap = new System.Windows.Forms.TextBox();
            this.txtDonGiaBan = new System.Windows.Forms.TextBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.cboLoaiThoiTrang = new System.Windows.Forms.ComboBox();
            this.cboChatLieu = new System.Windows.Forms.ComboBox();
            this.cboKichCo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.picAnh = new System.Windows.Forms.PictureBox();
            this.dgvQuanAo = new System.Windows.Forms.DataGridView();
            this.btnAnh = new System.Windows.Forms.Button();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuanAo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã quần áo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên quần áo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 160);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Màu sắc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 235);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Số lượng tồn kho";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(232, 296);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Giá nhập";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(220, 364);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "Giá bán ra";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(243, 425);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 25);
            this.label7.TabIndex = 6;
            this.label7.Text = "Ghi Chu";
            // 
            // txtMaQuanAo
            // 
            this.txtMaQuanAo.Location = new System.Drawing.Point(363, 15);
            this.txtMaQuanAo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtMaQuanAo.Name = "txtMaQuanAo";
            this.txtMaQuanAo.Size = new System.Drawing.Size(460, 31);
            this.txtMaQuanAo.TabIndex = 7;
            // 
            // txtTenQuanAo
            // 
            this.txtTenQuanAo.Location = new System.Drawing.Point(363, 85);
            this.txtTenQuanAo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtTenQuanAo.Name = "txtTenQuanAo";
            this.txtTenQuanAo.Size = new System.Drawing.Size(460, 31);
            this.txtTenQuanAo.TabIndex = 8;
            // 
            // txtMauSac
            // 
            this.txtMauSac.Location = new System.Drawing.Point(363, 154);
            this.txtMauSac.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtMauSac.Name = "txtMauSac";
            this.txtMauSac.Size = new System.Drawing.Size(460, 31);
            this.txtMauSac.TabIndex = 9;
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(363, 221);
            this.txtSoLuong.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(460, 31);
            this.txtSoLuong.TabIndex = 10;
            // 
            // txtDonGiaNhap
            // 
            this.txtDonGiaNhap.Location = new System.Drawing.Point(363, 290);
            this.txtDonGiaNhap.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtDonGiaNhap.Name = "txtDonGiaNhap";
            this.txtDonGiaNhap.Size = new System.Drawing.Size(460, 31);
            this.txtDonGiaNhap.TabIndex = 11;
            // 
            // txtDonGiaBan
            // 
            this.txtDonGiaBan.Location = new System.Drawing.Point(363, 358);
            this.txtDonGiaBan.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtDonGiaBan.Name = "txtDonGiaBan";
            this.txtDonGiaBan.Size = new System.Drawing.Size(460, 31);
            this.txtDonGiaBan.TabIndex = 12;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(363, 411);
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(460, 35);
            this.txtGhiChu.TabIndex = 13;
            // 
            // cboLoaiThoiTrang
            // 
            this.cboLoaiThoiTrang.FormattingEnabled = true;
            this.cboLoaiThoiTrang.Location = new System.Drawing.Point(363, 478);
            this.cboLoaiThoiTrang.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cboLoaiThoiTrang.Name = "cboLoaiThoiTrang";
            this.cboLoaiThoiTrang.Size = new System.Drawing.Size(460, 33);
            this.cboLoaiThoiTrang.TabIndex = 14;
            // 
            // cboChatLieu
            // 
            this.cboChatLieu.FormattingEnabled = true;
            this.cboChatLieu.Location = new System.Drawing.Point(363, 556);
            this.cboChatLieu.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cboChatLieu.Name = "cboChatLieu";
            this.cboChatLieu.Size = new System.Drawing.Size(460, 33);
            this.cboChatLieu.TabIndex = 15;
            // 
            // cboKichCo
            // 
            this.cboKichCo.FormattingEnabled = true;
            this.cboKichCo.Location = new System.Drawing.Point(363, 619);
            this.cboKichCo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cboKichCo.Name = "cboKichCo";
            this.cboKichCo.Size = new System.Drawing.Size(460, 33);
            this.cboKichCo.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(136, 482);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(199, 25);
            this.label8.TabIndex = 17;
            this.label8.Text = "Chọn loại thời trang";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(181, 561);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(150, 25);
            this.label9.TabIndex = 18;
            this.label9.Text = "Chọn chất liệu";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(213, 625);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 25);
            this.label10.TabIndex = 19;
            this.label10.Text = "Chọn size";
            // 
            // picAnh
            // 
            this.picAnh.Location = new System.Drawing.Point(5, 31);
            this.picAnh.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.picAnh.Name = "picAnh";
            this.picAnh.Size = new System.Drawing.Size(1091, 632);
            this.picAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAnh.TabIndex = 20;
            this.picAnh.TabStop = false;
            // 
            // dgvQuanAo
            // 
            this.dgvQuanAo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuanAo.Location = new System.Drawing.Point(24, 710);
            this.dgvQuanAo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dgvQuanAo.Name = "dgvQuanAo";
            this.dgvQuanAo.RowHeadersWidth = 62;
            this.dgvQuanAo.Size = new System.Drawing.Size(2472, 439);
            this.dgvQuanAo.TabIndex = 21;
            // 
            // btnAnh
            // 
            this.btnAnh.Location = new System.Drawing.Point(1064, 6);
            this.btnAnh.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnAnh.Name = "btnAnh";
            this.btnAnh.Size = new System.Drawing.Size(179, 56);
            this.btnAnh.TabIndex = 22;
            this.btnAnh.Text = "Chọn ảnh";
            this.btnAnh.UseVisualStyleBackColor = true;
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.Location = new System.Drawing.Point(1064, 85);
            this.btnThemMoi.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(179, 60);
            this.btnThemMoi.TabIndex = 23;
            this.btnThemMoi.Text = "Thêm mới";
            this.btnThemMoi.UseVisualStyleBackColor = true;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(1064, 172);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(179, 60);
            this.btnLuu.TabIndex = 24;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(1064, 258);
            this.btnSua.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(179, 60);
            this.btnSua.TabIndex = 25;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(1064, 346);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(179, 60);
            this.btnXoa.TabIndex = 26;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnBoQua
            // 
            this.btnBoQua.Location = new System.Drawing.Point(1064, 431);
            this.btnBoQua.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(179, 60);
            this.btnBoQua.TabIndex = 27;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(1064, 521);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(179, 60);
            this.btnExcel.TabIndex = 28;
            this.btnExcel.Text = "Xuất ra Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(1064, 610);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(179, 54);
            this.btnThoat.TabIndex = 29;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnThoat);
            this.groupBox1.Controls.Add(this.btnExcel);
            this.groupBox1.Controls.Add(this.btnBoQua);
            this.groupBox1.Controls.Add(this.btnXoa);
            this.groupBox1.Controls.Add(this.btnSua);
            this.groupBox1.Controls.Add(this.btnLuu);
            this.groupBox1.Controls.Add(this.btnThemMoi);
            this.groupBox1.Controls.Add(this.btnAnh);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cboKichCo);
            this.groupBox1.Controls.Add(this.cboChatLieu);
            this.groupBox1.Controls.Add(this.cboLoaiThoiTrang);
            this.groupBox1.Controls.Add(this.txtGhiChu);
            this.groupBox1.Controls.Add(this.txtDonGiaBan);
            this.groupBox1.Controls.Add(this.txtDonGiaNhap);
            this.groupBox1.Controls.Add(this.txtSoLuong);
            this.groupBox1.Controls.Add(this.txtMauSac);
            this.groupBox1.Controls.Add(this.txtTenQuanAo);
            this.groupBox1.Controls.Add(this.txtMaQuanAo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(92, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(1292, 692);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picAnh);
            this.groupBox2.Location = new System.Drawing.Point(1400, 18);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Size = new System.Drawing.Size(1112, 692);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Xem trước";
            // 
            // frmQuanAo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2520, 1171);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvQuanAo);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "frmQuanAo";
            this.Text = "frmQuanAo";
            this.Load += new System.EventHandler(this.frmQuanAo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuanAo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMaQuanAo;
        private System.Windows.Forms.TextBox txtTenQuanAo;
        private System.Windows.Forms.TextBox txtMauSac;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TextBox txtDonGiaNhap;
        private System.Windows.Forms.TextBox txtDonGiaBan;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.ComboBox cboLoaiThoiTrang;
        private System.Windows.Forms.ComboBox cboChatLieu;
        private System.Windows.Forms.ComboBox cboKichCo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox picAnh;
        private System.Windows.Forms.DataGridView dgvQuanAo;
        private System.Windows.Forms.Button btnAnh;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}