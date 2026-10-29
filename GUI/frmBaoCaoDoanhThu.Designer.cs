namespace QuanLyBanQuanAo.GUI
{
    partial class frmBaoCaoDoanhThu
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
            this.gbDieuKien = new System.Windows.Forms.GroupBox();
            this.cboKieu = new System.Windows.Forms.ComboBox();
            this.dtpDen = new System.Windows.Forms.DateTimePicker();
            this.dtpTu = new System.Windows.Forms.DateTimePicker();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnXem = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.dgvBaoCao = new System.Windows.Forms.DataGridView();
            this.txtSoHD = new System.Windows.Forms.TextBox();
            this.txtDoanhThu = new System.Windows.Forms.TextBox();
            this.txtLoiNhuan = new System.Windows.Forms.TextBox();
            this.gbDieuKien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Arial", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTieuDe.Location = new System.Drawing.Point(50, 40);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(429, 44);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "BÁO CÁO DOANH THU";
            // 
            // gbDieuKien
            // 
            this.gbDieuKien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDieuKien.Controls.Add(this.cboKieu);
            this.gbDieuKien.Controls.Add(this.dtpDen);
            this.gbDieuKien.Controls.Add(this.dtpTu);
            this.gbDieuKien.Controls.Add(this.btnExcel);
            this.gbDieuKien.Controls.Add(this.btnXem);
            this.gbDieuKien.Controls.Add(this.label7);
            this.gbDieuKien.Controls.Add(this.label6);
            this.gbDieuKien.Controls.Add(this.label5);
            this.gbDieuKien.Location = new System.Drawing.Point(35, 116);
            this.gbDieuKien.Name = "gbDieuKien";
            this.gbDieuKien.Size = new System.Drawing.Size(1419, 103);
            this.gbDieuKien.TabIndex = 1;
            this.gbDieuKien.TabStop = false;
            this.gbDieuKien.Text = "groupBox1";
            // 
            // cboKieu
            // 
            this.cboKieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKieu.FormattingEnabled = true;
            this.cboKieu.Location = new System.Drawing.Point(830, 43);
            this.cboKieu.Name = "cboKieu";
            this.cboKieu.Size = new System.Drawing.Size(181, 33);
            this.cboKieu.TabIndex = 3;
            // 
            // dtpDen
            // 
            this.dtpDen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDen.Location = new System.Drawing.Point(454, 41);
            this.dtpDen.Name = "dtpDen";
            this.dtpDen.Size = new System.Drawing.Size(200, 31);
            this.dtpDen.TabIndex = 2;
            // 
            // dtpTu
            // 
            this.dtpTu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTu.Location = new System.Drawing.Point(114, 41);
            this.dtpTu.Name = "dtpTu";
            this.dtpTu.Size = new System.Drawing.Size(200, 31);
            this.dtpTu.TabIndex = 2;
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(1238, 43);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(148, 41);
            this.btnExcel.TabIndex = 1;
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(1049, 41);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(173, 41);
            this.btnXem.TabIndex = 1;
            this.btnXem.Text = "Xem báo cáo";
            this.btnXem.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(709, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "Thống kê";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(344, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "Đến ngày";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "Từ ngày";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(53, 273);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(122, 25);
            this.label.TabIndex = 0;
            this.label.Text = "Số hóa đơn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(393, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Doanh thu";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(781, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Lợi nhuận gộp";
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(1273, 265);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(123, 46);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            // 
            // dgvBaoCao
            // 
            this.dgvBaoCao.AllowUserToAddRows = false;
            this.dgvBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBaoCao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBaoCao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoCao.Location = new System.Drawing.Point(88, 384);
            this.dgvBaoCao.Name = "dgvBaoCao";
            this.dgvBaoCao.ReadOnly = true;
            this.dgvBaoCao.RowHeadersVisible = false;
            this.dgvBaoCao.RowHeadersWidth = 82;
            this.dgvBaoCao.RowTemplate.Height = 33;
            this.dgvBaoCao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBaoCao.Size = new System.Drawing.Size(1280, 349);
            this.dgvBaoCao.TabIndex = 2;
            // 
            // txtSoHD
            // 
            this.txtSoHD.CausesValidation = false;
            this.txtSoHD.Location = new System.Drawing.Point(181, 270);
            this.txtSoHD.Name = "txtSoHD";
            this.txtSoHD.ReadOnly = true;
            this.txtSoHD.Size = new System.Drawing.Size(179, 31);
            this.txtSoHD.TabIndex = 3;
            // 
            // txtDoanhThu
            // 
            this.txtDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoanhThu.Location = new System.Drawing.Point(510, 267);
            this.txtDoanhThu.Name = "txtDoanhThu";
            this.txtDoanhThu.ReadOnly = true;
            this.txtDoanhThu.Size = new System.Drawing.Size(179, 31);
            this.txtDoanhThu.TabIndex = 3;
            // 
            // txtLoiNhuan
            // 
            this.txtLoiNhuan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoiNhuan.Location = new System.Drawing.Point(936, 270);
            this.txtLoiNhuan.Name = "txtLoiNhuan";
            this.txtLoiNhuan.ReadOnly = true;
            this.txtLoiNhuan.Size = new System.Drawing.Size(179, 31);
            this.txtLoiNhuan.TabIndex = 3;
            // 
            // frmBaoCaoDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1495, 783);
            this.Controls.Add(this.txtLoiNhuan);
            this.Controls.Add(this.txtDoanhThu);
            this.Controls.Add(this.txtSoHD);
            this.Controls.Add(this.dgvBaoCao);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.gbDieuKien);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "frmBaoCaoDoanhThu";
            this.Text = "Báo cáo doanh thu";
            this.Load += new System.EventHandler(this.frmBaoCaoDoanhThu_Load);
            this.gbDieuKien.ResumeLayout(false);
            this.gbDieuKien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox gbDieuKien;
        private System.Windows.Forms.DateTimePicker dtpDen;
        private System.Windows.Forms.DateTimePicker dtpTu;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboKieu;
        private System.Windows.Forms.DataGridView dgvBaoCao;
        private System.Windows.Forms.TextBox txtSoHD;
        private System.Windows.Forms.TextBox txtDoanhThu;
        private System.Windows.Forms.TextBox txtLoiNhuan;
        private System.Windows.Forms.Button btnDong;
    }
}