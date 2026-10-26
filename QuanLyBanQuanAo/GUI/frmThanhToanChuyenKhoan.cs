using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QRCoder;

namespace QuanLyBanQuanAo.GUI
{
    /// <summary>
    /// Hộp thoại thanh toán chuyển khoản: hiện mã QR (VietQR) đã điền sẵn số tiền + nội dung,
    /// khách chuyển xong thì tải ảnh chụp màn hình chuyển khoản lên. Trả về DialogResult.OK
    /// và DuongDanAnh (đường dẫn file ảnh gốc) khi khách đã chọn ảnh hợp lệ.
    /// </summary>
    public class frmThanhToanChuyenKhoan : Form
    {
        private const long GioiHanAnh = 10L * 1024 * 1024;   // tối đa 10 MB

        private readonly double soTien;
        private readonly string noiDung;

        private PictureBox picQR, picAnh;
        private Label lblQR, lblTenAnh;
        private Button btnTaiLaiQR;

        /// <summary>Đường dẫn file ảnh chuyển khoản khách đã chọn.</summary>
        public string DuongDanAnh { get; private set; }

        public frmThanhToanChuyenKhoan(string maDon, double soTien)
        {
            this.soTien = soTien;
            // Nội dung CK: bỏ dấu gạch dưới vì nhiều ngân hàng không nhận ký tự đặc biệt
            this.noiDung = "Thanh toan " + maDon.Replace("_", "");

            this.Text = "Thanh toán chuyển khoản";
            this.ClientSize = new Size(480, 705);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            BuildUI();
            this.Shown += delegate { TaiQR(); };
        }

        private void BuildUI()
        {
            Label tieuDe = new Label();
            tieuDe.Text = "THANH TOÁN CHUYỂN KHOẢN";
            tieuDe.Font = new Font("Arial", 13, FontStyle.Bold);
            tieuDe.ForeColor = Color.Firebrick;
            tieuDe.TextAlign = ContentAlignment.MiddleCenter;
            tieuDe.Left = 10; tieuDe.Top = 8; tieuDe.Width = 460; tieuDe.Height = 28;
            Controls.Add(tieuDe);

            picQR = new PictureBox();
            picQR.Left = 110; picQR.Top = 42; picQR.Width = 260; picQR.Height = 260;
            picQR.BorderStyle = BorderStyle.FixedSingle;
            picQR.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(picQR);

            lblQR = new Label();
            lblQR.TextAlign = ContentAlignment.MiddleCenter;
            lblQR.Left = 10; lblQR.Top = 306; lblQR.Width = 460; lblQR.Height = 20;
            Controls.Add(lblQR);

            btnTaiLaiQR = UI.Btn(this, "Tải lại QR", 190, 326, 100, delegate { TaiQR(); });
            btnTaiLaiQR.Visible = false;

            int y = 360;
            UI.Lbl(this, "Ngân hàng", 20, y, 105);        UI.Txt(this, 130, y, 330, true).Text = Function.TenNganHang;
            y += 28;
            UI.Lbl(this, "Số tài khoản", 20, y, 105);     UI.Txt(this, 130, y, 330, true).Text = Function.SoTaiKhoan;
            y += 28;
            UI.Lbl(this, "Chủ tài khoản", 20, y, 105);    UI.Txt(this, 130, y, 330, true).Text = Function.TenChuTaiKhoan;
            y += 28;
            UI.Lbl(this, "Số tiền (VNĐ)", 20, y, 105);    UI.Txt(this, 130, y, 330, true).Text = soTien.ToString("N0");
            y += 28;
            UI.Lbl(this, "Nội dung CK", 20, y, 105);      UI.Txt(this, 130, y, 330, true).Text = noiDung;

            Label goiY = new Label();
            goiY.Text = "Sau khi chuyển khoản, hãy chụp màn hình giao dịch và tải ảnh lên bên dưới (bắt buộc):";
            goiY.ForeColor = Color.DarkBlue;
            goiY.Left = 20; goiY.Top = 508; goiY.Width = 440; goiY.Height = 36;
            Controls.Add(goiY);

            UI.Btn(this, "Chọn ảnh chuyển khoản...", 20, 548, 190, delegate { ChonAnh(); });

            lblTenAnh = new Label();
            lblTenAnh.Text = "(chưa chọn ảnh)";
            lblTenAnh.AutoEllipsis = true;
            lblTenAnh.TextAlign = ContentAlignment.MiddleLeft;
            lblTenAnh.Left = 220; lblTenAnh.Top = 548; lblTenAnh.Width = 240; lblTenAnh.Height = 30;
            Controls.Add(lblTenAnh);

            picAnh = new PictureBox();
            picAnh.Left = 20; picAnh.Top = 586; picAnh.Width = 160; picAnh.Height = 66;
            picAnh.BorderStyle = BorderStyle.FixedSingle;
            picAnh.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(picAnh);

            UI.Btn(this, "Xác nhận đã chuyển", 20, 662, 200, delegate { XacNhan(); });
            Button btnHuy = UI.Btn(this, "Hủy", 380, 662, 80, null);
            btnHuy.DialogResult = DialogResult.Cancel;
            this.CancelButton = btnHuy;
        }

        // ---------------------------------------------------------------- QR
        private void TaiQR()
        {
            lblQR.ForeColor = Color.Black;
            btnTaiLaiQR.Visible = false;
            try
            {
                string chuoi = Function.TaoChuoiVietQR(soTien, noiDung);
                QRCodeGenerator gen = new QRCodeGenerator();
                QRCodeData data = gen.CreateQrCode(chuoi, QRCodeGenerator.ECCLevel.M);
                QRCode qr = new QRCode(data);
                picQR.Image = qr.GetGraphic(10);
                lblQR.Text = "Quét mã bằng app ngân hàng - số tiền và nội dung đã điền sẵn";
            }
            catch (Exception ex)
            {
                picQR.Image = null;
                lblQR.ForeColor = Color.Red;
                lblQR.Text = "Không tạo được QR: " + ex.Message;
                btnTaiLaiQR.Visible = true;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmThanhToanChuyenKhoan
            // 
            this.ClientSize = new System.Drawing.Size(322, 256);
            this.Name = "frmThanhToanChuyenKhoan";
            this.Load += new System.EventHandler(this.frmThanhToanChuyenKhoan_Load);
            this.ResumeLayout(false);

        }

        private void frmThanhToanChuyenKhoan_Load(object sender, EventArgs e)
        {

        }

        // ---------------------------------------------------------------- ảnh chuyển khoản
        private void ChonAnh()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh chuyển khoản";
                ofd.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    if (new FileInfo(ofd.FileName).Length > GioiHanAnh)
                    {
                        MessageBox.Show("Ảnh quá lớn (tối đa 10 MB). Vui lòng chọn ảnh khác.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // đọc vào bộ nhớ để không khóa file gốc
                    byte[] bytes = File.ReadAllBytes(ofd.FileName);
                    Image img = Image.FromStream(new MemoryStream(bytes));   // ném lỗi nếu không phải ảnh hợp lệ
                    picAnh.Image = img;
                    DuongDanAnh = ofd.FileName;
                    lblTenAnh.Text = Path.GetFileName(ofd.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("File được chọn không phải ảnh hợp lệ.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void XacNhan()
        {
            if (string.IsNullOrEmpty(DuongDanAnh))
            {
                MessageBox.Show("Vui lòng tải lên ảnh chuyển khoản trước khi xác nhận!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
