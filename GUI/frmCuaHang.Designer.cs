namespace QuanLyBanQuanAo.GUI
{
    partial class frmCuaHang
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
            this.flow = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCT = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlGio = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.lblTim = new System.Windows.Forms.Label();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.lblSapXep = new System.Windows.Forms.Label();
            this.cboSapXep = new System.Windows.Forms.ComboBox();
            this.lblDem = new System.Windows.Forms.Label();
            this.btnGio = new System.Windows.Forms.Button();
            this.picCT = new System.Windows.Forms.PictureBox();
            this.lblTenCT = new System.Windows.Forms.Label();
            this.lblGiaCT = new System.Windows.Forms.Label();
            this.lblThongSoCT = new System.Windows.Forms.Label();
            this.lblSL = new System.Windows.Forms.Label();
            this.nudSL = new System.Windows.Forms.NumericUpDown();
            this.btnThem = new System.Windows.Forms.Button();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.pnlCT.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlGio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSL)).BeginInit();
            this.SuspendLayout();
            // 
            // flow
            // 
            this.flow.AutoScroll = true;
            this.flow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flow.Location = new System.Drawing.Point(0, 80);
            this.flow.Name = "flow";
            this.flow.Padding = new System.Windows.Forms.Padding(8);
            this.flow.Size = new System.Drawing.Size(830, 640);
            this.flow.TabIndex = 0;
            // 
            // pnlCT
            // 
            this.pnlCT.Controls.Add(this.picCT);
            this.pnlCT.Controls.Add(this.lblTenCT);
            this.pnlCT.Controls.Add(this.lblGiaCT);
            this.pnlCT.Controls.Add(this.lblThongSoCT);
            this.pnlCT.Controls.Add(this.lblSL);
            this.pnlCT.Controls.Add(this.nudSL);
            this.pnlCT.Controls.Add(this.btnThem);
            this.pnlCT.Controls.Add(this.lblThongBao);
            this.pnlCT.AutoScroll = true;
            this.pnlCT.BackColor = System.Drawing.Color.White;
            this.pnlCT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCT.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCT.Location = new System.Drawing.Point(830, 80);
            this.pnlCT.Name = "pnlCT";
            this.pnlCT.Size = new System.Drawing.Size(370, 640);
            this.pnlCT.TabIndex = 1;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Controls.Add(this.lblLoai);
            this.pnlTop.Controls.Add(this.cboLoai);
            this.pnlTop.Controls.Add(this.lblTim);
            this.pnlTop.Controls.Add(this.txtTim);
            this.pnlTop.Controls.Add(this.btnTim);
            this.pnlTop.Controls.Add(this.lblSapXep);
            this.pnlTop.Controls.Add(this.cboSapXep);
            this.pnlTop.Controls.Add(this.lblDem);
            this.pnlTop.Controls.Add(this.pnlGio);
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(246)))), ((int)(((byte)(240)))));
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 80);
            this.pnlTop.TabIndex = 2;
            // 
            // pnlGio
            // 
            this.pnlGio.Controls.Add(this.btnGio);
            this.pnlGio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(246)))), ((int)(((byte)(240)))));
            this.pnlGio.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlGio.Location = new System.Drawing.Point(940, 0);
            this.pnlGio.Name = "pnlGio";
            this.pnlGio.Size = new System.Drawing.Size(260, 80);
            this.pnlGio.TabIndex = 9;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = false;
            this.lblTieuDe.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTieuDe.Location = new System.Drawing.Point(12, 6);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(600, 28);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "CỬA HÀNG THỜI TRANG - XEM SẢN PHẨM";
            this.lblTieuDe.UseMnemonic = false;
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Location = new System.Drawing.Point(12, 44);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(35, 20);
            this.lblLoai.TabIndex = 1;
            this.lblLoai.Text = "Loại";
            // 
            // cboLoai
            // 
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Location = new System.Drawing.Point(50, 40);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(170, 21);
            this.cboLoai.TabIndex = 2;
            // 
            // lblTim
            // 
            this.lblTim.AutoSize = true;
            this.lblTim.Location = new System.Drawing.Point(235, 44);
            this.lblTim.Name = "lblTim";
            this.lblTim.Size = new System.Drawing.Size(55, 20);
            this.lblTim.TabIndex = 3;
            this.lblTim.Text = "Tìm tên";
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(295, 40);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(180, 20);
            this.txtTim.TabIndex = 4;
            this.txtTim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTim_KeyDown);
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(480, 38);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(55, 30);
            this.btnTim.TabIndex = 5;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // lblSapXep
            // 
            this.lblSapXep.AutoSize = true;
            this.lblSapXep.Location = new System.Drawing.Point(550, 44);
            this.lblSapXep.Name = "lblSapXep";
            this.lblSapXep.Size = new System.Drawing.Size(55, 20);
            this.lblSapXep.TabIndex = 6;
            this.lblSapXep.Text = "Sắp xếp";
            // 
            // cboSapXep
            // 
            this.cboSapXep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSapXep.FormattingEnabled = true;
            this.cboSapXep.Items.AddRange(new object[] {
            "Mặc định",
            "Giá tăng dần",
            "Giá giảm dần",
            "Tên A → Z"});
            this.cboSapXep.Location = new System.Drawing.Point(610, 40);
            this.cboSapXep.Name = "cboSapXep";
            this.cboSapXep.Size = new System.Drawing.Size(160, 21);
            this.cboSapXep.TabIndex = 7;
            // 
            // lblDem
            // 
            this.lblDem.AutoSize = false;
            this.lblDem.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblDem.Location = new System.Drawing.Point(785, 44);
            this.lblDem.Name = "lblDem";
            this.lblDem.Size = new System.Drawing.Size(150, 20);
            this.lblDem.TabIndex = 8;
            // 
            // btnGio
            // 
            this.btnGio.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGio.Location = new System.Drawing.Point(15, 34);
            this.btnGio.Name = "btnGio";
            this.btnGio.Size = new System.Drawing.Size(235, 36);
            this.btnGio.TabIndex = 0;
            this.btnGio.Text = "Giỏ hàng (0) - Đặt hàng";
            this.btnGio.UseVisualStyleBackColor = true;
            this.btnGio.Click += new System.EventHandler(this.btnGio_Click);
            // 
            // picCT
            // 
            this.picCT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.picCT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCT.Location = new System.Drawing.Point(20, 15);
            this.picCT.Name = "picCT";
            this.picCT.Size = new System.Drawing.Size(320, 320);
            this.picCT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCT.TabIndex = 0;
            this.picCT.TabStop = false;
            // 
            // lblTenCT
            // 
            this.lblTenCT.AutoSize = false;
            this.lblTenCT.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenCT.Location = new System.Drawing.Point(20, 345);
            this.lblTenCT.Name = "lblTenCT";
            this.lblTenCT.Size = new System.Drawing.Size(320, 48);
            this.lblTenCT.TabIndex = 1;
            this.lblTenCT.UseMnemonic = false;
            // 
            // lblGiaCT
            // 
            this.lblGiaCT.AutoSize = false;
            this.lblGiaCT.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaCT.ForeColor = System.Drawing.Color.Firebrick;
            this.lblGiaCT.Location = new System.Drawing.Point(20, 396);
            this.lblGiaCT.Name = "lblGiaCT";
            this.lblGiaCT.Size = new System.Drawing.Size(320, 30);
            this.lblGiaCT.TabIndex = 2;
            // 
            // lblThongSoCT
            // 
            this.lblThongSoCT.AutoSize = false;
            this.lblThongSoCT.Location = new System.Drawing.Point(20, 432);
            this.lblThongSoCT.Name = "lblThongSoCT";
            this.lblThongSoCT.Size = new System.Drawing.Size(320, 120);
            this.lblThongSoCT.TabIndex = 3;
            this.lblThongSoCT.UseMnemonic = false;
            // 
            // lblSL
            // 
            this.lblSL.AutoSize = true;
            this.lblSL.Location = new System.Drawing.Point(20, 564);
            this.lblSL.Name = "lblSL";
            this.lblSL.Size = new System.Drawing.Size(70, 20);
            this.lblSL.TabIndex = 4;
            this.lblSL.Text = "Số lượng";
            // 
            // nudSL
            // 
            this.nudSL.Location = new System.Drawing.Point(100, 560);
            this.nudSL.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSL.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSL.Name = "nudSL";
            this.nudSL.Size = new System.Drawing.Size(70, 20);
            this.nudSL.TabIndex = 5;
            this.nudSL.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(20, 600);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(320, 40);
            this.btnThem.TabIndex = 6;
            this.btnThem.Text = "Thêm vào giỏ hàng";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // lblThongBao
            // 
            this.lblThongBao.AutoSize = false;
            this.lblThongBao.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblThongBao.Location = new System.Drawing.Point(20, 648);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(320, 40);
            this.lblThongBao.TabIndex = 7;
            this.lblThongBao.UseMnemonic = false;
            // 
            // frmCuaHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.flow);
            this.Controls.Add(this.pnlCT);
            this.Controls.Add(this.pnlTop);
            this.Name = "frmCuaHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem sản phẩm";
            this.Activated += new System.EventHandler(this.frmCuaHang_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCuaHang_FormClosed);
            this.Load += new System.EventHandler(this.frmCuaHang_Load);
            this.pnlCT.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlGio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flow;
        private System.Windows.Forms.Panel pnlCT;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlGio;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Label lblSapXep;
        private System.Windows.Forms.ComboBox cboSapXep;
        private System.Windows.Forms.Label lblDem;
        private System.Windows.Forms.Button btnGio;
        private System.Windows.Forms.PictureBox picCT;
        private System.Windows.Forms.Label lblTenCT;
        private System.Windows.Forms.Label lblGiaCT;
        private System.Windows.Forms.Label lblThongSoCT;
        private System.Windows.Forms.Label lblSL;
        private System.Windows.Forms.NumericUpDown nudSL;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblThongBao;
    }
}