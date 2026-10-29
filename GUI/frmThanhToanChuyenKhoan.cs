using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QRCoder;

namespace QuanLyBanQuanAo.GUI
{
    /// <summary>
    /// Hộp thoại thanh toán chuyển khoản: hiện mã QR (VietQR) đã điền sẵn số tiền + nội dung,
    /// khách chuyển xong thì tải ảnh chụp màn hình chuyển khoản lên. Trả về DialogResult.OK
    /// và DuongDanAnh (đường dẫn file ảnh gốc) khi khách đã chọn ảnh hợp lệ.
    /// Giao diện nằm trong frmThanhToanChuyenKhoan.Designer.cs (kéo thả được).
    /// </summary>
    public partial class frmThanhToanChuyenKhoan : Form
    {
        private const long GioiHanAnh = 10L * 1024 * 1024;   // tối đa 10 MB

        private readonly double soTien;
        private readonly string noiDung;

        /// <summary>Đường dẫn file ảnh chuyển khoản khách đã chọn.</summary>
        public string DuongDanAnh { get; private set; }

        // Constructor không tham số chỉ để Designer mở được form
        public frmThanhToanChuyenKhoan() : this("", 0) { }

        public frmThanhToanChuyenKhoan(string maDon, double soTien)
        {
            InitializeComponent();
            Theme.Apply(this);

            this.soTien = soTien;
            // Nội dung CK: bỏ dấu gạch dưới vì nhiều ngân hàng không nhận ký tự đặc biệt
            this.noiDung = "Thanh toan " + (maDon ?? "").Replace("_", "");

            txtNganHang.Text = Function.TenNganHang;
            txtSTK.Text = Function.SoTaiKhoan;
            txtChuTK.Text = Function.TenChuTaiKhoan;
            txtSoTien.Text = soTien.ToString("N0");
            txtNoiDung.Text = noiDung;
            btnTaiLaiQR.Visible = false;
        }

        private void frmThanhToanChuyenKhoan_Shown(object sender, EventArgs e)
        {
            if (DesignMode) return;
            TaiQR();
        }

        private void btnTaiLaiQR_Click(object sender, EventArgs e)
        {
            TaiQR();
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

        // ---------------------------------------------------------------- ảnh chuyển khoản
        private void btnChonAnh_Click(object sender, EventArgs e)
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

        private void btnXacNhan_Click(object sender, EventArgs e)
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