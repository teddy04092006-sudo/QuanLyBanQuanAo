namespace QuanLyBanQuanAo.GUI
{
    partial class frmQuanLyDonHang
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblCho = new System.Windows.Forms.Label();
            this.gbLoc = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTT = new System.Windows.Forms.ComboBox();
            this.dtpTu = new System.Windows.Forms.DateTimePicker();
            this.dtpDen = new System.Windows.Forms.DateTimePicker();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.dgvDon = new System.Windows.Forms.DataGridView();
            this.lblInfo = new System.Windows.Forms.Label();
            this.dgvCT = new System.Windows.Forms.DataGridView();
            this.lblTong = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnBanQuay = new System.Windows.Forms.Button();
            this.btnXemCK = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.gbLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCT)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(34, 31);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(70, 25);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "label1";
            // 
            // lblCho
            // 
            this.lblCho.AutoSize = true;
            this.lblCho.Location = new System.Drawing.Point(745, 31);
            this.lblCho.Name = "lblCho";
            this.lblCho.Size = new System.Drawing.Size(70, 25);
            this.lblCho.TabIndex = 0;
            this.lblCho.Text = "label1";
            // 
            // gbLoc
            // 
            this.gbLoc.Controls.Add(this.btnLoc);
            this.gbLoc.Controls.Add(this.txtTim);
            this.gbLoc.Controls.Add(this.dtpDen);
            this.gbLoc.Controls.Add(this.dtpTu);
            this.gbLoc.Controls.Add(this.cboTT);
            this.gbLoc.Controls.Add(this.label6);
            this.gbLoc.Controls.Add(this.label);
            this.gbLoc.Controls.Add(this.label4);
            this.gbLoc.Controls.Add(this.label3);
            this.gbLoc.Location = new System.Drawing.Point(25, 71);
            this.gbLoc.Name = "gbLoc";
            this.gbLoc.Size = new System.Drawing.Size(1301, 89);
            this.gbLoc.TabIndex = 1;
            this.gbLoc.TabStop = false;
            this.gbLoc.Text = "Lọc đơn hàng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Trạng thái";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Từ ngày";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(555, 36);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(51, 25);
            this.label.TabIndex = 0;
            this.label.Text = "Đến";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(796, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "Tìm (mã/tên/SĐT)";
            // 
            // cboTT
            // 
            this.cboTT.FormattingEnabled = true;
            this.cboTT.Location = new System.Drawing.Point(124, 36);
            this.cboTT.Name = "cboTT";
            this.cboTT.Size = new System.Drawing.Size(133, 33);
            this.cboTT.TabIndex = 1;
            // 
            // dtpTu
            // 
            this.dtpTu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTu.Location = new System.Drawing.Point(371, 34);
            this.dtpTu.Name = "dtpTu";
            this.dtpTu.Size = new System.Drawing.Size(178, 31);
            this.dtpTu.TabIndex = 2;
            // 
            // dtpDen
            // 
            this.dtpDen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDen.Location = new System.Drawing.Point(612, 34);
            this.dtpDen.Name = "dtpDen";
            this.dtpDen.Size = new System.Drawing.Size(178, 31);
            this.dtpDen.TabIndex = 2;
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(977, 36);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(179, 31);
            this.txtTim.TabIndex = 3;
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(1177, 29);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(118, 45);
            this.btnLoc.TabIndex = 4;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = true;
            // 
            // dgvDon
            // 
            this.dgvDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDon.Location = new System.Drawing.Point(39, 183);
            this.dgvDon.Name = "dgvDon";
            this.dgvDon.RowHeadersWidth = 82;
            this.dgvDon.RowTemplate.Height = 33;
            this.dgvDon.Size = new System.Drawing.Size(1287, 175);
            this.dgvDon.TabIndex = 2;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(34, 381);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(70, 25);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "label1";
            // 
            // dgvCT
            // 
            this.dgvCT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCT.Location = new System.Drawing.Point(39, 431);
            this.dgvCT.Name = "dgvCT";
            this.dgvCT.RowHeadersWidth = 82;
            this.dgvCT.RowTemplate.Height = 33;
            this.dgvCT.Size = new System.Drawing.Size(1287, 191);
            this.dgvCT.TabIndex = 2;
            // 
            // lblTong
            // 
            this.lblTong.AutoSize = true;
            this.lblTong.Location = new System.Drawing.Point(34, 648);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(70, 25);
            this.lblTong.TabIndex = 0;
            this.lblTong.Text = "label1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 701);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(160, 25);
            this.label9.TabIndex = 0;
            this.label9.Text = "Nhân viên xử lý";
            // 
            // cboNV
            // 
            this.cboNV.FormattingEnabled = true;
            this.cboNV.Location = new System.Drawing.Point(200, 703);
            this.cboNV.Name = "cboNV";
            this.cboNV.Size = new System.Drawing.Size(172, 33);
            this.cboNV.TabIndex = 1;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Location = new System.Drawing.Point(413, 701);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(161, 45);
            this.btnXacNhan.TabIndex = 4;
            this.btnXacNhan.Text = "Xác nhận đơn";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(580, 701);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(118, 45);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "Hủy đơn";
            this.btnHuy.UseVisualStyleBackColor = true;
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(704, 701);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(187, 45);
            this.btnIn.TabIndex = 4;
            this.btnIn.Text = "In hóa đơn Excel";
            this.btnIn.UseVisualStyleBackColor = true;
            // 
            // btnBanQuay
            // 
            this.btnBanQuay.Location = new System.Drawing.Point(897, 701);
            this.btnBanQuay.Name = "btnBanQuay";
            this.btnBanQuay.Size = new System.Drawing.Size(284, 45);
            this.btnBanQuay.TabIndex = 4;
            this.btnBanQuay.Text = "Bán tại quầy (lập HĐ mới)";
            this.btnBanQuay.UseVisualStyleBackColor = true;
            // 
            // btnXemCK
            // 
            this.btnXemCK.Location = new System.Drawing.Point(1150, 638);
            this.btnXemCK.Name = "btnXemCK";
            this.btnXemCK.Size = new System.Drawing.Size(176, 45);
            this.btnXemCK.TabIndex = 4;
            this.btnXemCK.Text = "Xem ảnh CK";
            this.btnXemCK.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(1209, 696);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(118, 45);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            // 
            // frmQuanLyDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 770);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnXemCK);
            this.Controls.Add(this.btnBanQuay);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.dgvCT);
            this.Controls.Add(this.dgvDon);
            this.Controls.Add(this.gbLoc);
            this.Controls.Add(this.lblCho);
            this.Controls.Add(this.cboNV);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblTong);
            this.Controls.Add(this.lblInfo);
            this.Name = "frmQuanLyDonHang";
            this.Text = "frmQuanLyDonHang";
            this.Load += new System.EventHandler(this.frmQuanLyDonHang_Load);
            this.gbLoc.ResumeLayout(false);
            this.gbLoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblCho;
        private System.Windows.Forms.GroupBox gbLoc;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.DateTimePicker dtpDen;
        private System.Windows.Forms.DateTimePicker dtpTu;
        private System.Windows.Forms.ComboBox cboTT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvDon;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView dgvCT;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboNV;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnBanQuay;
        private System.Windows.Forms.Button btnXemCK;
        private System.Windows.Forms.Button btnDong;
    }
}