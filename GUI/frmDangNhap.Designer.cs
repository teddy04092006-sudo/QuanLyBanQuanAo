namespace QuanLyBanQuanAo.GUI
{
    partial class frmDangNhap
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
            this.pnlTrai = new System.Windows.Forms.Panel();
            this.lblSlogan = new System.Windows.Forms.Label();
            this.lblQuanAo = new System.Windows.Forms.Label();
            this.lblShop = new System.Windows.Forms.Label();
            this.pbAo = new System.Windows.Forms.PictureBox();
            this.lblChao = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.lblKe = new System.Windows.Forms.Label();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnKhachMuaHang = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslSanSang = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslPhu = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlTrai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAo)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTrai (nền gradient vẽ trong pnlTrai_Paint)
            //
            this.pnlTrai.BackColor = System.Drawing.Color.FromArgb(0, 109, 119);
            this.pnlTrai.Controls.Add(this.lblSlogan);
            this.pnlTrai.Controls.Add(this.lblQuanAo);
            this.pnlTrai.Controls.Add(this.lblShop);
            this.pnlTrai.Controls.Add(this.pbAo);
            this.pnlTrai.Location = new System.Drawing.Point(0, 0);
            this.pnlTrai.Name = "pnlTrai";
            this.pnlTrai.Size = new System.Drawing.Size(240, 358);
            this.pnlTrai.TabIndex = 0;
            this.pnlTrai.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTrai_Paint);
            //
            // lblSlogan
            //
            this.lblSlogan.BackColor = System.Drawing.Color.Transparent;
            this.lblSlogan.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Italic);
            this.lblSlogan.ForeColor = System.Drawing.Color.FromArgb(255, 235, 150);
            this.lblSlogan.Location = new System.Drawing.Point(0, 270);
            this.lblSlogan.Name = "lblSlogan";
            this.lblSlogan.Size = new System.Drawing.Size(240, 48);
            this.lblSlogan.TabIndex = 3;
            this.lblSlogan.Text = "Quản lý\r\nbán hàng";
            this.lblSlogan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblQuanAo
            //
            this.lblQuanAo.BackColor = System.Drawing.Color.Transparent;
            this.lblQuanAo.Font = new System.Drawing.Font("Times New Roman", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblQuanAo.ForeColor = System.Drawing.Color.FromArgb(255, 222, 110);
            this.lblQuanAo.Location = new System.Drawing.Point(0, 190);
            this.lblQuanAo.Name = "lblQuanAo";
            this.lblQuanAo.Size = new System.Drawing.Size(240, 32);
            this.lblQuanAo.TabIndex = 2;
            this.lblQuanAo.Text = "Quần Áo";
            this.lblQuanAo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblShop
            //
            this.lblShop.BackColor = System.Drawing.Color.Transparent;
            this.lblShop.Font = new System.Drawing.Font("Times New Roman", 26F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblShop.ForeColor = System.Drawing.Color.White;
            this.lblShop.Location = new System.Drawing.Point(0, 140);
            this.lblShop.Name = "lblShop";
            this.lblShop.Size = new System.Drawing.Size(240, 50);
            this.lblShop.TabIndex = 1;
            this.lblShop.Text = "Shop";
            this.lblShop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pbAo
            //
            this.pbAo.BackColor = System.Drawing.Color.Transparent;
            this.pbAo.Location = new System.Drawing.Point(80, 50);
            this.pbAo.Name = "pbAo";
            this.pbAo.Size = new System.Drawing.Size(80, 70);
            this.pbAo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbAo.TabIndex = 0;
            this.pbAo.TabStop = false;
            //
            // lblChao
            //
            this.lblChao.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblChao.ForeColor = System.Drawing.Color.FromArgb(0, 80, 88);
            this.lblChao.Location = new System.Drawing.Point(268, 28);
            this.lblChao.Name = "lblChao";
            this.lblChao.Size = new System.Drawing.Size(344, 52);
            this.lblChao.TabIndex = 1;
            this.lblChao.Text = "Chào mừng bạn đến với cửa hàng!\r\nVui lòng nhập tên đăng nhập và mật khẩu để tiếp tục.";
            //
            // label1
            //
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(0, 109, 119);
            this.label1.Location = new System.Drawing.Point(268, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên đăng nhập:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            //
            // txtTenDangNhap
            //
            this.txtTenDangNhap.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtTenDangNhap.Location = new System.Drawing.Point(268, 120);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(344, 27);
            this.txtTenDangNhap.TabIndex = 0;
            this.txtTenDangNhap.TextChanged += new System.EventHandler(this.txtTenDangNhap_TextChanged);
            //
            // label2
            //
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(0, 109, 119);
            this.label2.Location = new System.Drawing.Point(268, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(344, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mật khẩu:";
            //
            // txtMatKhau
            //
            this.txtMatKhau.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtMatKhau.Location = new System.Drawing.Point(268, 184);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(344, 27);
            this.txtMatKhau.TabIndex = 1;
            this.txtMatKhau.TextChanged += new System.EventHandler(this.txtMatKhau_TextChanged);
            //
            // lblKe
            //
            this.lblKe.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.lblKe.Location = new System.Drawing.Point(268, 232);
            this.lblKe.Name = "lblKe";
            this.lblKe.Size = new System.Drawing.Size(344, 3);
            this.lblKe.TabIndex = 6;
            //
            // btnDangNhap
            //
            this.btnDangNhap.Location = new System.Drawing.Point(268, 248);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(168, 40);
            this.btnDangNhap.TabIndex = 2;
            this.btnDangNhap.Text = "Đăng Nhập";
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            //
            // btnThoat
            //
            this.btnThoat.Location = new System.Drawing.Point(444, 248);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(168, 40);
            this.btnThoat.TabIndex = 3;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            //
            // btnKhachMuaHang
            //
            this.btnKhachMuaHang.Location = new System.Drawing.Point(268, 298);
            this.btnKhachMuaHang.Name = "btnKhachMuaHang";
            this.btnKhachMuaHang.Size = new System.Drawing.Size(344, 40);
            this.btnKhachMuaHang.TabIndex = 4;
            this.btnKhachMuaHang.Text = "Đăng Nhập Khách";
            this.btnKhachMuaHang.UseVisualStyleBackColor = true;
            this.btnKhachMuaHang.Click += new System.EventHandler(this.btnKhachMuaHang_Click);
            //
            // statusStrip1
            //
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslSanSang,
            this.tsslPhu});
            this.statusStrip1.Location = new System.Drawing.Point(0, 358);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(640, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 7;
            //
            // tsslSanSang
            //
            this.tsslSanSang.Name = "tsslSanSang";
            this.tsslSanSang.Size = new System.Drawing.Size(60, 17);
            this.tsslSanSang.Text = "Sẵn sàng";
            //
            // tsslPhu
            //
            this.tsslPhu.Name = "tsslPhu";
            this.tsslPhu.Size = new System.Drawing.Size(0, 17);
            //
            // frmDangNhap
            //
            this.AcceptButton = this.btnDangNhap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(640, 380);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnKhachMuaHang);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.lblKe);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTenDangNhap);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblChao);
            this.Controls.Add(this.pnlTrai);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập hệ thống - Shop Quần Áo";
            this.Load += new System.EventHandler(this.frmDangNhap_Load);
            this.pnlTrai.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAo)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTrai;
        private System.Windows.Forms.Label lblSlogan;
        private System.Windows.Forms.Label lblQuanAo;
        private System.Windows.Forms.Label lblShop;
        private System.Windows.Forms.PictureBox pbAo;
        private System.Windows.Forms.Label lblChao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Label lblKe;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnKhachMuaHang;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslSanSang;
        private System.Windows.Forms.ToolStripStatusLabel tsslPhu;

        // Nền gradient cho panel trái (xanh ngọc -> tím)
        private void pnlTrai_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (this.pnlTrai.Width <= 0 || this.pnlTrai.Height <= 0) return;
            using (System.Drawing.Drawing2D.LinearGradientBrush br = new System.Drawing.Drawing2D.LinearGradientBrush(
                this.pnlTrai.ClientRectangle,
                System.Drawing.Color.FromArgb(0, 121, 140),
                System.Drawing.Color.FromArgb(106, 76, 147),
                90f))
            {
                e.Graphics.FillRectangle(br, this.pnlTrai.ClientRectangle);
            }
        }

        // ---- Vẽ icon áo pixel bằng code (không cần file ảnh) - nằm ngoài InitializeComponent ----
        private static readonly string[] MauAo =
        {
            "...KKKK..KKKK...",
            "..KBBBBKKBBBBK..",
            ".KBBBBBBBBBBBBK.",
            "KBHBBBBBBBBBBBSK",
            "KBHBBBBBBBBBBBSK",
            "KKKKBBBBBBBBKKKK",
            "...KBHBBBBBBSK..",
            "...KBHBBBBBBSK..",
            "...KBHBBBBBBSK..",
            "...KBBBBBBBBSK..",
            "...KBBBBBBBBSK..",
            "...KBBBBBBBBSK..",
            "...KBBBBBBBBSK..",
            "...KKKKKKKKKK..."
        };

        /// <summary>Tạo ảnh áo pixel. canvas = 0: đúng kích thước áo; canvas &gt; 0: ảnh vuông canvas x canvas, áo nằm giữa.</summary>
        private static System.Drawing.Bitmap TaoAnhAo(int heSo, int canvas)
        {
            int w = canvas > 0 ? canvas : 16 * heSo;
            int h = canvas > 0 ? canvas : MauAo.Length * heSo;
            int ox = (w - 16 * heSo) / 2;
            int oy = (h - MauAo.Length * heSo) / 2;
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(w, h);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.Transparent);
                for (int y = 0; y < MauAo.Length; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        System.Drawing.Color c;
                        switch (MauAo[y][x])
                        {
                            case 'K': c = System.Drawing.Color.FromArgb(0, 48, 54); break;
                            case 'B': c = System.Drawing.Color.FromArgb(224, 242, 241); break;
                            case 'H': c = System.Drawing.Color.White; break;
                            case 'S': c = System.Drawing.Color.FromArgb(150, 200, 200); break;
                            default: continue;
                        }
                        using (System.Drawing.SolidBrush br = new System.Drawing.SolidBrush(c))
                            g.FillRectangle(br, ox + x * heSo, oy + y * heSo, heSo, heSo);
                    }
                }
            }
            return bmp;
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            this.pbAo.Image = TaoAnhAo(5, 0);
            using (System.Drawing.Bitmap nho = TaoAnhAo(2, 32))
            {
                this.Icon = System.Drawing.Icon.FromHandle(nho.GetHicon());
            }
        }
    }
}