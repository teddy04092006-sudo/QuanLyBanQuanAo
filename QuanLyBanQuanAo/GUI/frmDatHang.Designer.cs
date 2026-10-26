namespace QuanLyBanQuanAo.GUI
{
    partial class frmDatHang
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
            this.cboLocLoaiThoiTrang = new System.Windows.Forms.ComboBox();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.picXemTruoc = new System.Windows.Forms.PictureBox();
            this.nudSoLuongMua = new System.Windows.Forms.NumericUpDown();
            this.btnThemVaoGio = new System.Windows.Forms.Button();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHoTenKhach = new System.Windows.Forms.TextBox();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnXacNhanDatHang = new System.Windows.Forms.Button();
            this.lblThongTinSP = new System.Windows.Forms.Label();
            this.grpThanhToan = new System.Windows.Forms.GroupBox();
            this.rdoTienMat = new System.Windows.Forms.RadioButton();
            this.rdoChuyenKhoan = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picXemTruoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongMua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.grpThanhToan.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboLocLoaiThoiTrang
            // 
            this.cboLocLoaiThoiTrang.FormattingEnabled = true;
            this.cboLocLoaiThoiTrang.Location = new System.Drawing.Point(150, 43);
            this.cboLocLoaiThoiTrang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboLocLoaiThoiTrang.Name = "cboLocLoaiThoiTrang";
            this.cboLocLoaiThoiTrang.Size = new System.Drawing.Size(206, 28);
            this.cboLocLoaiThoiTrang.TabIndex = 0;
            this.cboLocLoaiThoiTrang.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dgvSanPham
            // 
            this.dgvSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSanPham.Location = new System.Drawing.Point(833, 473);
            this.dgvSanPham.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.RowHeadersWidth = 62;
            this.dgvSanPham.Size = new System.Drawing.Size(754, 399);
            this.dgvSanPham.TabIndex = 1;
            this.dgvSanPham.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSanPham_CellContentClick);
            // 
            // picXemTruoc
            // 
            this.picXemTruoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picXemTruoc.Location = new System.Drawing.Point(27, 337);
            this.picXemTruoc.Name = "picXemTruoc";
            this.picXemTruoc.Size = new System.Drawing.Size(754, 481);
            this.picXemTruoc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picXemTruoc.TabIndex = 2;
            this.picXemTruoc.TabStop = false;
            // 
            // nudSoLuongMua
            // 
            this.nudSoLuongMua.Location = new System.Drawing.Point(150, 113);
            this.nudSoLuongMua.Name = "nudSoLuongMua";
            this.nudSoLuongMua.Size = new System.Drawing.Size(51, 26);
            this.nudSoLuongMua.TabIndex = 3;
            // 
            // btnThemVaoGio
            // 
            this.btnThemVaoGio.Location = new System.Drawing.Point(123, 273);
            this.btnThemVaoGio.Name = "btnThemVaoGio";
            this.btnThemVaoGio.Size = new System.Drawing.Size(147, 49);
            this.btnThemVaoGio.TabIndex = 4;
            this.btnThemVaoGio.Text = "Thêm vào giỏ";
            this.btnThemVaoGio.UseVisualStyleBackColor = true;
            this.btnThemVaoGio.Click += new System.EventHandler(this.btnThemVaoGio_Click);
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioHang.Location = new System.Drawing.Point(833, 34);
            this.dgvGioHang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.RowHeadersWidth = 62;
            this.dgvGioHang.Size = new System.Drawing.Size(754, 388);
            this.dgvGioHang.TabIndex = 5;
            this.dgvGioHang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGioHang_CellContentClick);
            // 
            // txtTongTien
            // 
            this.txtTongTien.Location = new System.Drawing.Point(150, 184);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(206, 26);
            this.txtTongTien.TabIndex = 6;
            this.txtTongTien.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Lọc thời trang";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Số lượng mua";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tổng tiền";
            // 
            // txtHoTenKhach
            // 
            this.txtHoTenKhach.Location = new System.Drawing.Point(596, 45);
            this.txtHoTenKhach.Name = "txtHoTenKhach";
            this.txtHoTenKhach.Size = new System.Drawing.Size(215, 26);
            this.txtHoTenKhach.TabIndex = 10;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(596, 181);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(215, 26);
            this.txtDiaChi.TabIndex = 12;
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Location = new System.Drawing.Point(596, 113);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(215, 26);
            this.txtDienThoai.TabIndex = 13;
            this.txtDienThoai.TextChanged += new System.EventHandler(this.txtDienThoai_TextChanged);
            this.txtDienThoai.Leave += new System.EventHandler(this.txtDienThoai_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(456, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Họ tên khách";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(456, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Số điện thoại";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(456, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "Địa chỉ";
            // 
            // btnXacNhanDatHang
            // 
            this.btnXacNhanDatHang.Location = new System.Drawing.Point(546, 273);
            this.btnXacNhanDatHang.Name = "btnXacNhanDatHang";
            this.btnXacNhanDatHang.Size = new System.Drawing.Size(147, 49);
            this.btnXacNhanDatHang.TabIndex = 17;
            this.btnXacNhanDatHang.Text = "Đặt hàng";
            this.btnXacNhanDatHang.UseVisualStyleBackColor = true;
            this.btnXacNhanDatHang.Click += new System.EventHandler(this.btnXacNhanDatHang_Click);
            // 
            // lblThongTinSP
            // 
            this.lblThongTinSP.AutoSize = true;
            this.lblThongTinSP.Location = new System.Drawing.Point(216, 852);
            this.lblThongTinSP.Name = "lblThongTinSP";
            this.lblThongTinSP.Size = new System.Drawing.Size(367, 20);
            this.lblThongTinSP.TabIndex = 18;
            this.lblThongTinSP.Text = "Chọn sản phẩm để xem trước hình ảnh và thông số";
            // 
            // grpThanhToan
            // 
            this.grpThanhToan.Controls.Add(this.rdoChuyenKhoan);
            this.grpThanhToan.Controls.Add(this.rdoTienMat);
            this.grpThanhToan.Location = new System.Drawing.Point(456, 212);
            this.grpThanhToan.Name = "grpThanhToan";
            this.grpThanhToan.Size = new System.Drawing.Size(355, 56);
            this.grpThanhToan.TabIndex = 19;
            this.grpThanhToan.TabStop = false;
            this.grpThanhToan.Text = "Hình thức thanh toán";
            // 
            // rdoTienMat
            // 
            this.rdoTienMat.AutoSize = true;
            this.rdoTienMat.Checked = true;
            this.rdoTienMat.Location = new System.Drawing.Point(15, 24);
            this.rdoTienMat.Name = "rdoTienMat";
            this.rdoTienMat.Size = new System.Drawing.Size(96, 24);
            this.rdoTienMat.TabIndex = 0;
            this.rdoTienMat.TabStop = true;
            this.rdoTienMat.Text = "Tiền mặt";
            this.rdoTienMat.UseVisualStyleBackColor = true;
            // 
            // rdoChuyenKhoan
            // 
            this.rdoChuyenKhoan.AutoSize = true;
            this.rdoChuyenKhoan.Location = new System.Drawing.Point(160, 24);
            this.rdoChuyenKhoan.Name = "rdoChuyenKhoan";
            this.rdoChuyenKhoan.Size = new System.Drawing.Size(180, 24);
            this.rdoChuyenKhoan.TabIndex = 1;
            this.rdoChuyenKhoan.Text = "Chuyển khoản (QR)";
            this.rdoChuyenKhoan.UseVisualStyleBackColor = true;
            // 
            // frmDatHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1912, 978);
            this.Controls.Add(this.grpThanhToan);
            this.Controls.Add(this.lblThongTinSP);
            this.Controls.Add(this.btnXacNhanDatHang);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDienThoai);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.txtHoTenKhach);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.dgvGioHang);
            this.Controls.Add(this.btnThemVaoGio);
            this.Controls.Add(this.nudSoLuongMua);
            this.Controls.Add(this.picXemTruoc);
            this.Controls.Add(this.dgvSanPham);
            this.Controls.Add(this.cboLocLoaiThoiTrang);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmDatHang";
            this.Text = "frmDatHang";
            this.Load += new System.EventHandler(this.frmDatHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picXemTruoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongMua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.grpThanhToan.ResumeLayout(false);
            this.grpThanhToan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboLocLoaiThoiTrang;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.PictureBox picXemTruoc;
        private System.Windows.Forms.NumericUpDown nudSoLuongMua;
        private System.Windows.Forms.Button btnThemVaoGio;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHoTenKhach;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnXacNhanDatHang;
        private System.Windows.Forms.Label lblThongTinSP;
        private System.Windows.Forms.GroupBox grpThanhToan;
        private System.Windows.Forms.RadioButton rdoTienMat;
        private System.Windows.Forms.RadioButton rdoChuyenKhoan;
    }
}