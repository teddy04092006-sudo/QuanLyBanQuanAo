namespace QuanLyBanQuanAo.GUI
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanAo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoaiThoiTrang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChatLieu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLapHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongKe = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoanhThu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDanhMuc,
            this.mnuHoaDon,
            this.mnuThongKe,
            this.mnuThoat});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(962, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuDanhMuc
            // 
            this.mnuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuanAo,
            this.mnuLoaiThoiTrang,
            this.mnuChatLieu,
            this.mnuKhachHang,
            this.mnuNhanVien});
            this.mnuDanhMuc.Name = "mnuDanhMuc";
            this.mnuDanhMuc.Size = new System.Drawing.Size(74, 22);
            this.mnuDanhMuc.Text = "Danh mục";
            // 
            // mnuQuanAo
            // 
            this.mnuQuanAo.Name = "mnuQuanAo";
            this.mnuQuanAo.Size = new System.Drawing.Size(151, 22);
            this.mnuQuanAo.Text = "Quần áo";
            this.mnuQuanAo.Click += new System.EventHandler(this.quầnÁoToolStripMenuItem_Click);
            // 
            // mnuLoaiThoiTrang
            // 
            this.mnuLoaiThoiTrang.Name = "mnuLoaiThoiTrang";
            this.mnuLoaiThoiTrang.Size = new System.Drawing.Size(151, 22);
            this.mnuLoaiThoiTrang.Text = "Loại thời trang";
            this.mnuLoaiThoiTrang.Click += new System.EventHandler(this.mnuLoaiThoiTrang_Click);
            // 
            // mnuChatLieu
            // 
            this.mnuChatLieu.Name = "mnuChatLieu";
            this.mnuChatLieu.Size = new System.Drawing.Size(151, 22);
            this.mnuChatLieu.Text = "Chất liệu";
            this.mnuChatLieu.Click += new System.EventHandler(this.mnuChatLieu_Click);
            // 
            // mnuKhachHang
            // 
            this.mnuKhachHang.Name = "mnuKhachHang";
            this.mnuKhachHang.Size = new System.Drawing.Size(151, 22);
            this.mnuKhachHang.Text = "Khách hàng";
            this.mnuKhachHang.Click += new System.EventHandler(this.mnuKhachHang_Click);
            // 
            // mnuNhanVien
            // 
            this.mnuNhanVien.Name = "mnuNhanVien";
            this.mnuNhanVien.Size = new System.Drawing.Size(151, 22);
            this.mnuNhanVien.Text = "Nhân viên";
            this.mnuNhanVien.Click += new System.EventHandler(this.mnuNhanVien_Click);
            // 
            // mnuHoaDon
            // 
            this.mnuHoaDon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLapHoaDon,
            this.mnuDatHang});
            this.mnuHoaDon.Name = "mnuHoaDon";
            this.mnuHoaDon.Size = new System.Drawing.Size(65, 22);
            this.mnuHoaDon.Text = "Hóa đơn";
            // 
            // mnuLapHoaDon
            // 
            this.mnuLapHoaDon.Name = "mnuLapHoaDon";
            this.mnuLapHoaDon.Size = new System.Drawing.Size(219, 22);
            this.mnuLapHoaDon.Text = "Quản lý đơn hàng & hóa đơn";
            this.mnuLapHoaDon.Click += new System.EventHandler(this.mnuLapHoaDon_Click);
            // 
            // mnuDatHang
            // 
            this.mnuDatHang.Name = "mnuDatHang";
            this.mnuDatHang.Size = new System.Drawing.Size(219, 22);
            this.mnuDatHang.Text = "Đặt mua quần áo";
            this.mnuDatHang.Click += new System.EventHandler(this.mnuDatHang_Click);
            // 
            // mnuThongKe
            // 
            this.mnuThongKe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDoanhThu});
            this.mnuThongKe.Name = "mnuThongKe";
            this.mnuThongKe.Size = new System.Drawing.Size(68, 22);
            this.mnuThongKe.Text = "Thống kê";
            // 
            // mnuDoanhThu
            // 
            this.mnuDoanhThu.Name = "mnuDoanhThu";
            this.mnuDoanhThu.Size = new System.Drawing.Size(174, 22);
            this.mnuDoanhThu.Text = "Báo cáo doanh thu";
            this.mnuDoanhThu.Click += new System.EventHandler(this.mnuDoanhThu_Click);
            // 
            // mnuThoat
            // 
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Size = new System.Drawing.Size(49, 22);
            this.mnuThoat.Text = "Thoát";
            this.mnuThoat.Click += new System.EventHandler(this.thoátToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 547);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuThongKe;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanAo;
        private System.Windows.Forms.ToolStripMenuItem mnuNhanVien;
        private System.Windows.Forms.ToolStripMenuItem mnuLapHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuDatHang;
        private System.Windows.Forms.ToolStripMenuItem mnuDoanhThu;
        private System.Windows.Forms.ToolStripMenuItem mnuLoaiThoiTrang;
        private System.Windows.Forms.ToolStripMenuItem mnuChatLieu;
        private System.Windows.Forms.ToolStripMenuItem mnuKhachHang;
    }
}