namespace QuanLyBanQuanAo.GUI
{
    partial class frmThanhToanChuyenKhoan
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
            this.pnlTieuDe = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.gbQR = new System.Windows.Forms.GroupBox();
            this.picQR = new System.Windows.Forms.PictureBox();
            this.lblQR = new System.Windows.Forms.Label();
            this.btnTaiLaiQR = new System.Windows.Forms.Button();
            this.gbThongTin = new System.Windows.Forms.GroupBox();
            this.lblNganHang = new System.Windows.Forms.Label();
            this.txtNganHang = new System.Windows.Forms.TextBox();
            this.lblSTK = new System.Windows.Forms.Label();
            this.txtSTK = new System.Windows.Forms.TextBox();
            this.lblChuTK = new System.Windows.Forms.Label();
            this.txtChuTK = new System.Windows.Forms.TextBox();
            this.lblSoTien = new System.Windows.Forms.Label();
            this.txtSoTien = new System.Windows.Forms.TextBox();
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.gbAnh = new System.Windows.Forms.GroupBox();
            this.picAnh = new System.Windows.Forms.PictureBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.lblTenAnh = new System.Windows.Forms.Label();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.pnlTieuDe.SuspendLayout();
            this.gbQR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.gbThongTin.SuspendLayout();
            this.gbAnh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            this.SuspendLayout();
            //
            // pnlTieuDe
            //
            this.pnlTieuDe.BackColor = System.Drawing.Color.FromArgb(0, 77, 92);
            this.pnlTieuDe.Controls.Add(this.lblTieuDe);
            this.pnlTieuDe.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTieuDe.Location = new System.Drawing.Point(0, 0);
            this.pnlTieuDe.Name = "pnlTieuDe";
            this.pnlTieuDe.Size = new System.Drawing.Size(780, 48);
            this.pnlTieuDe.TabIndex = 0;
            //
            // lblTieuDe
            //
            this.lblTieuDe.BackColor = System.Drawing.Color.Transparent;
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.lblTieuDe.Text = "THANH TOÁN CHUYỂN KHOẢN";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // gbQR
            //
            this.gbQR.Controls.Add(this.picQR);
            this.gbQR.Controls.Add(this.lblQR);
            this.gbQR.Controls.Add(this.btnTaiLaiQR);
            this.gbQR.Location = new System.Drawing.Point(14, 58);
            this.gbQR.Name = "gbQR";
            this.gbQR.Size = new System.Drawing.Size(300, 400);
            this.gbQR.TabIndex = 1;
            this.gbQR.TabStop = false;
            this.gbQR.Text = "Mã QR VietQR";
            //
            // picQR
            //
            this.picQR.BackColor = System.Drawing.Color.White;
            this.picQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQR.Location = new System.Drawing.Point(20, 26);
            this.picQR.Name = "picQR";
            this.picQR.Size = new System.Drawing.Size(260, 260);
            this.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQR.TabIndex = 0;
            this.picQR.TabStop = false;
            //
            // lblQR
            //
            this.lblQR.Location = new System.Drawing.Point(16, 294);
            this.lblQR.Name = "lblQR";
            this.lblQR.Size = new System.Drawing.Size(268, 60);
            this.lblQR.TabIndex = 1;
            this.lblQR.Text = "Đang tạo mã QR...";
            this.lblQR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnTaiLaiQR
            //
            this.btnTaiLaiQR.Location = new System.Drawing.Point(80, 360);
            this.btnTaiLaiQR.Name = "btnTaiLaiQR";
            this.btnTaiLaiQR.Size = new System.Drawing.Size(140, 30);
            this.btnTaiLaiQR.TabIndex = 2;
            this.btnTaiLaiQR.Text = "Tải lại QR";
            this.btnTaiLaiQR.UseVisualStyleBackColor = true;
            this.btnTaiLaiQR.Click += new System.EventHandler(this.btnTaiLaiQR_Click);
            //
            // gbThongTin
            //
            this.gbThongTin.Controls.Add(this.lblNganHang);
            this.gbThongTin.Controls.Add(this.txtNganHang);
            this.gbThongTin.Controls.Add(this.lblSTK);
            this.gbThongTin.Controls.Add(this.txtSTK);
            this.gbThongTin.Controls.Add(this.lblChuTK);
            this.gbThongTin.Controls.Add(this.txtChuTK);
            this.gbThongTin.Controls.Add(this.lblSoTien);
            this.gbThongTin.Controls.Add(this.txtSoTien);
            this.gbThongTin.Controls.Add(this.lblNoiDung);
            this.gbThongTin.Controls.Add(this.txtNoiDung);
            this.gbThongTin.Location = new System.Drawing.Point(326, 58);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(440, 200);
            this.gbThongTin.TabIndex = 2;
            this.gbThongTin.TabStop = false;
            this.gbThongTin.Text = "Thông tin chuyển khoản";
            //
            // lblNganHang
            //
            this.lblNganHang.Location = new System.Drawing.Point(14, 30);
            this.lblNganHang.Name = "lblNganHang";
            this.lblNganHang.Size = new System.Drawing.Size(90, 20);
            this.lblNganHang.Text = "Ngân hàng:";
            //
            // txtNganHang
            //
            this.txtNganHang.Location = new System.Drawing.Point(110, 26);
            this.txtNganHang.Name = "txtNganHang";
            this.txtNganHang.ReadOnly = true;
            this.txtNganHang.Size = new System.Drawing.Size(316, 20);
            this.txtNganHang.TabIndex = 0;
            //
            // lblSTK
            //
            this.lblSTK.Location = new System.Drawing.Point(14, 62);
            this.lblSTK.Name = "lblSTK";
            this.lblSTK.Size = new System.Drawing.Size(90, 20);
            this.lblSTK.Text = "Số tài khoản:";
            //
            // txtSTK
            //
            this.txtSTK.Location = new System.Drawing.Point(110, 58);
            this.txtSTK.Name = "txtSTK";
            this.txtSTK.ReadOnly = true;
            this.txtSTK.Size = new System.Drawing.Size(316, 20);
            this.txtSTK.TabIndex = 1;
            //
            // lblChuTK
            //
            this.lblChuTK.Location = new System.Drawing.Point(14, 94);
            this.lblChuTK.Name = "lblChuTK";
            this.lblChuTK.Size = new System.Drawing.Size(90, 20);
            this.lblChuTK.Text = "Chủ tài khoản:";
            //
            // txtChuTK
            //
            this.txtChuTK.Location = new System.Drawing.Point(110, 90);
            this.txtChuTK.Name = "txtChuTK";
            this.txtChuTK.ReadOnly = true;
            this.txtChuTK.Size = new System.Drawing.Size(316, 20);
            this.txtChuTK.TabIndex = 2;
            //
            // lblSoTien
            //
            this.lblSoTien.Location = new System.Drawing.Point(14, 126);
            this.lblSoTien.Name = "lblSoTien";
            this.lblSoTien.Size = new System.Drawing.Size(90, 20);
            this.lblSoTien.Text = "Số tiền (VNĐ):";
            //
            // txtSoTien
            //
            this.txtSoTien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtSoTien.Location = new System.Drawing.Point(110, 122);
            this.txtSoTien.Name = "txtSoTien";
            this.txtSoTien.ReadOnly = true;
            this.txtSoTien.Size = new System.Drawing.Size(316, 22);
            this.txtSoTien.TabIndex = 3;
            //
            // lblNoiDung
            //
            this.lblNoiDung.Location = new System.Drawing.Point(14, 158);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(90, 20);
            this.lblNoiDung.Text = "Nội dung CK:";
            //
            // txtNoiDung
            //
            this.txtNoiDung.Location = new System.Drawing.Point(110, 154);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.ReadOnly = true;
            this.txtNoiDung.Size = new System.Drawing.Size(316, 20);
            this.txtNoiDung.TabIndex = 4;
            //
            // gbAnh
            //
            this.gbAnh.Controls.Add(this.picAnh);
            this.gbAnh.Controls.Add(this.btnChonAnh);
            this.gbAnh.Controls.Add(this.lblTenAnh);
            this.gbAnh.Location = new System.Drawing.Point(326, 266);
            this.gbAnh.Name = "gbAnh";
            this.gbAnh.Size = new System.Drawing.Size(440, 192);
            this.gbAnh.TabIndex = 3;
            this.gbAnh.TabStop = false;
            this.gbAnh.Text = "Ảnh chụp màn hình chuyển khoản";
            //
            // picAnh
            //
            this.picAnh.BackColor = System.Drawing.Color.White;
            this.picAnh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAnh.Location = new System.Drawing.Point(14, 24);
            this.picAnh.Name = "picAnh";
            this.picAnh.Size = new System.Drawing.Size(150, 150);
            this.picAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnh.TabIndex = 0;
            this.picAnh.TabStop = false;
            //
            // btnChonAnh
            //
            this.btnChonAnh.Location = new System.Drawing.Point(180, 24);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(180, 32);
            this.btnChonAnh.TabIndex = 1;
            this.btnChonAnh.Text = "Tải ảnh lên...";
            this.btnChonAnh.UseVisualStyleBackColor = true;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            //
            // lblTenAnh
            //
            this.lblTenAnh.Location = new System.Drawing.Point(180, 66);
            this.lblTenAnh.Name = "lblTenAnh";
            this.lblTenAnh.Size = new System.Drawing.Size(250, 100);
            this.lblTenAnh.Text = "Chưa chọn ảnh";
            //
            // btnXacNhan
            //
            this.btnXacNhan.Location = new System.Drawing.Point(326, 468);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(440, 38);
            this.btnXacNhan.TabIndex = 4;
            this.btnXacNhan.Text = "Xác nhận đã chuyển khoản";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            //
            // frmThanhToanChuyenKhoan
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 520);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.gbAnh);
            this.Controls.Add(this.gbThongTin);
            this.Controls.Add(this.gbQR);
            this.Controls.Add(this.pnlTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThanhToanChuyenKhoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thanh toán chuyển khoản";
            this.Shown += new System.EventHandler(this.frmThanhToanChuyenKhoan_Shown);
            this.pnlTieuDe.ResumeLayout(false);
            this.gbQR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.gbAnh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox gbQR;
        private System.Windows.Forms.PictureBox picQR;
        private System.Windows.Forms.Label lblQR;
        private System.Windows.Forms.Button btnTaiLaiQR;
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.Label lblNganHang;
        private System.Windows.Forms.TextBox txtNganHang;
        private System.Windows.Forms.Label lblSTK;
        private System.Windows.Forms.TextBox txtSTK;
        private System.Windows.Forms.Label lblChuTK;
        private System.Windows.Forms.TextBox txtChuTK;
        private System.Windows.Forms.Label lblSoTien;
        private System.Windows.Forms.TextBox txtSoTien;
        private System.Windows.Forms.Label lblNoiDung;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.GroupBox gbAnh;
        private System.Windows.Forms.PictureBox picAnh;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.Label lblTenAnh;
        private System.Windows.Forms.Button btnXacNhan;
    }
}
