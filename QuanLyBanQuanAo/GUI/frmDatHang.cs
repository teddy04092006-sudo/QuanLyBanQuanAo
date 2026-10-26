using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QuanLyBanQuanAo.GUI
{
    public partial class frmDatHang : Form
    {
        DataProcesser dtBase = new DataProcesser();
        Function func = new Function();
        DataTable dtGioHang = GioHang.Bang;   // giỏ hàng dùng chung với form xem sản phẩm (frmCuaHang)

        public frmDatHang()
        {
            InitializeComponent();
        }
        private void frmDatHang_Load(object sender, EventArgs e)
        {
            // Load danh mục loại thời trang để người mua lọc sản phẩm
            func.FillCombo("SELECT MaLoai, TenLoai FROM tblLoaiThoiTrang", cboLocLoaiThoiTrang, "MaLoai", "TenLoai");
            LoadSanPham("");

            // Khởi tạo bảng Giỏ hàng trên bộ nhớ
            if (dtGioHang.Columns.Count == 0)
            {
                dtGioHang.Columns.Add("MaQuanAo", typeof(string));
                dtGioHang.Columns.Add("TenQuanAo", typeof(string));
                dtGioHang.Columns.Add("KichCo", typeof(string));
                dtGioHang.Columns.Add("MauSac", typeof(string));
                dtGioHang.Columns.Add("SoLuong", typeof(int));
                dtGioHang.Columns.Add("DonGia", typeof(double));
                dtGioHang.Columns.Add("ThanhTien", typeof(double));
            }
            dgvGioHang.DataSource = dtGioHang;
            CapNhatTongTien();                                  // giỏ có thể đã có hàng từ form xem sản phẩm
            this.Activated += delegate { CapNhatTongTien(); };  // quay lại form này thì cập nhật lại tổng tiền
        }
        private void LoadSanPham(string maLoai)
        {
            string sql = @"SELECT qa.MaQuanAo, qa.TenQuanAo, l.TenLoai, cl.TenChatLieu, 
                                  qa.KichCo, qa.MauSac, qa.DonGiaBan, qa.SoLuong, qa.Anh, qa.GhiChu
                           FROM tblQuanAo qa
                           INNER JOIN tblLoaiThoiTrang l ON qa.MaLoai = l.MaLoai
                           INNER JOIN tblChatLieu cl ON qa.MaChatLieu = cl.MaChatLieu
                           WHERE qa.SoLuong > 0";
            if (!string.IsNullOrEmpty(maLoai))
            {
                sql += " AND qa.MaLoai = N'" + maLoai + "'";
            }
            dgvSanPham.DataSource = dtBase.DocBang(sql);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLocLoaiThoiTrang.SelectedValue != null && cboLocLoaiThoiTrang.SelectedIndex != -1)
            {
                // Tránh lỗi khi ComboBox đang nạp DataRowView lúc khởi tạo
                if (cboLocLoaiThoiTrang.SelectedValue is string)
                {
                    LoadSanPham(cboLocLoaiThoiTrang.SelectedValue.ToString());
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dgvGioHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvSanPham.CurrentRow == null) return;

            string tenQA = dgvSanPham.CurrentRow.Cells["TenQuanAo"].Value.ToString();
            string loai = dgvSanPham.CurrentRow.Cells["TenLoai"].Value.ToString();
            string chatLieu = dgvSanPham.CurrentRow.Cells["TenChatLieu"].Value.ToString();
            string size = dgvSanPham.CurrentRow.Cells["KichCo"].Value.ToString();
            string mau = dgvSanPham.CurrentRow.Cells["MauSac"].Value.ToString();
            double giaBan = Convert.ToDouble(dgvSanPham.CurrentRow.Cells["DonGiaBan"].Value);
            string fileAnh = dgvSanPham.CurrentRow.Cells["Anh"].Value.ToString();

            lblThongTinSP.Text = string.Format("Sản phẩm: {0}\nLoại: {1} | Chất liệu: {2}\nKích cỡ: {3} | Màu: {4}\nĐơn giá: {5:N0} VNĐ",
                tenQA, loai, chatLieu, size, mau, giaBan);

            string duongDanAnh = Application.StartupPath + "\\Images\\QuanAo\\" + fileAnh;
            if (!string.IsNullOrEmpty(fileAnh) && File.Exists(duongDanAnh))
            {
                picXemTruoc.Image = Image.FromFile(duongDanAnh);
            }
            else
            {
                picXemTruoc.Image = null;
            }
        }
        private void txtDienThoai_Leave(object sender, EventArgs e)
        {
            string sdt = txtDienThoai.Text.Trim();
            if (sdt == "") return;

            DataTable dtKhach = dtBase.DocBang("SELECT TenKhach, DiaChi FROM tblKhach WHERE DienThoai = N'" + sdt + "'");
            if (dtKhach.Rows.Count > 0)
            {
                txtHoTenKhach.Text = dtKhach.Rows[0]["TenKhach"].ToString();
                txtDiaChi.Text = dtKhach.Rows[0]["DiaChi"].ToString();
            }
        }
        private void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm quần áo trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maQA = dgvSanPham.CurrentRow.Cells["MaQuanAo"].Value.ToString();
            string tenQA = dgvSanPham.CurrentRow.Cells["TenQuanAo"].Value.ToString();
            string size = dgvSanPham.CurrentRow.Cells["KichCo"].Value.ToString();
            string mau = dgvSanPham.CurrentRow.Cells["MauSac"].Value.ToString();
            double donGia = Convert.ToDouble(dgvSanPham.CurrentRow.Cells["DonGiaBan"].Value);
            int tonKho = Convert.ToInt32(dgvSanPham.CurrentRow.Cells["SoLuong"].Value);
            int soLuongMua = (int)nudSoLuongMua.Value;

            if (soLuongMua <= 0 || soLuongMua > tonKho)
            {
                MessageBox.Show("Số lượng đặt mua vượt quá tồn kho (" + tonKho + ")!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataRow r in dtGioHang.Rows)
            {
                if (r["MaQuanAo"].ToString() == maQA)
                {
                    r["SoLuong"] = Convert.ToInt32(r["SoLuong"]) + soLuongMua;
                    r["ThanhTien"] = Convert.ToInt32(r["SoLuong"]) * donGia;
                    CapNhatTongTien();
                    return;
                }
            }

            DataRow newRow = dtGioHang.NewRow();
            newRow["MaQuanAo"] = maQA;
            newRow["TenQuanAo"] = tenQA;
            newRow["KichCo"] = size;
            newRow["MauSac"] = mau;
            newRow["SoLuong"] = soLuongMua;
            newRow["DonGia"] = donGia;
            newRow["ThanhTien"] = soLuongMua * donGia;
            dtGioHang.Rows.Add(newRow);

            CapNhatTongTien();
        }
        private void CapNhatTongTien()
        {
            double tong = 0;
            foreach (DataRow r in dtGioHang.Rows) tong += Convert.ToDouble(r["ThanhTien"]);
            txtTongTien.Text = tong.ToString("N0");
        }
        private void btnXacNhanDatHang_Click(object sender, EventArgs e)
        {
            if (txtDienThoai.Text.Trim() == "" || txtHoTenKhach.Text.Trim() == "" || txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Số điện thoại, Họ tên và Địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng của bạn đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHD = func.CreateKey("DH");
            double tongTien = 0;
            foreach (DataRow r in dtGioHang.Rows) tongTien += Convert.ToDouble(r["ThanhTien"]);

            // ----- Hình thức thanh toán: chuyển khoản thì hiện QR + bắt buộc tải ảnh chuyển khoản
            string hinhThuc = rdoChuyenKhoan.Checked ? "Chuyển khoản" : "Tiền mặt";
            string anhGoc = null;
            if (rdoChuyenKhoan.Checked)
            {
                using (frmThanhToanChuyenKhoan f = new frmThanhToanChuyenKhoan(maHD, tongTien))
                {
                    if (f.ShowDialog(this) != DialogResult.OK) return;   // khách hủy -> chưa ghi gì vào CSDL
                    anhGoc = f.DuongDanAnh;
                }
            }

            string sdt = txtDienThoai.Text.Trim();
            string maKhach = func.GetFieldValues("SELECT MaKhach FROM tblKhach WHERE DienThoai = N'" + sdt + "'");
            if (string.IsNullOrEmpty(maKhach))
            {
                maKhach = "KH_" + sdt;
                string sqlInsertKhach = string.Format("INSERT INTO tblKhach VALUES(N'{0}', N'{1}', N'{2}', N'{3}')",
                    maKhach, txtHoTenKhach.Text.Trim(), txtDiaChi.Text.Trim(), sdt);
                dtBase.CapNhatDuLieu(sqlInsertKhach);
            }
            else
            {
                string sqlUpdateKhach = string.Format("UPDATE tblKhach SET TenKhach = N'{0}', DiaChi = N'{1}' WHERE MaKhach = N'{2}'",
                    txtHoTenKhach.Text.Trim(), txtDiaChi.Text.Trim(), maKhach);
                dtBase.CapNhatDuLieu(sqlUpdateKhach);
            }

            // ----- Lưu ảnh chuyển khoản vào thư mục Images\ChuyenKhoan, đặt tên theo mã đơn
            string tenAnh = null, fileDich = null;
            if (anhGoc != null)
            {
                try
                {
                    string thuMuc = Path.Combine(Application.StartupPath, "Images", "ChuyenKhoan");
                    Directory.CreateDirectory(thuMuc);
                    tenAnh = maHD + Path.GetExtension(anhGoc).ToLower();
                    fileDich = Path.Combine(thuMuc, tenAnh);
                    File.Copy(anhGoc, fileDich, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không lưu được ảnh chuyển khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            bool okHD = dtBase.CapNhatDuLieu(
                "INSERT INTO tblHDBan(MaHDBan, MaNhanVien, NgayBan, MaKhach, TongTien, TrangThai, HinhThucThanhToan, AnhChuyenKhoan) " +
                "VALUES(@ma, NULL, GETDATE(), @kh, @tt, N'Chờ xác nhận', @ht, @anh)",
                DataProcesser.P("@ma", maHD), DataProcesser.P("@kh", maKhach), DataProcesser.P("@tt", tongTien),
                DataProcesser.P("@ht", hinhThuc), DataProcesser.P("@anh", tenAnh));
            if (!okHD)
            {
                // tạo đơn thất bại thì xóa ảnh vừa copy để không bị rác
                try { if (fileDich != null && File.Exists(fileDich)) File.Delete(fileDich); } catch { }
                return;
            }

            foreach (DataRow r in dtGioHang.Rows)
            {
                string maQA = r["MaQuanAo"].ToString();
                int sl = Convert.ToInt32(r["SoLuong"]);
                double dg = Convert.ToDouble(r["DonGia"]);
                double tt = Convert.ToDouble(r["ThanhTien"]);

                string sqlCT = string.Format("INSERT INTO tblChiTietHDBan VALUES(N'{0}', N'{1}', {2}, {3}, 0, {4})",
                    maHD, maQA, sl, dg, tt);
                dtBase.CapNhatDuLieu(sqlCT);

                string sqlTruKho = string.Format("UPDATE tblQuanAo SET SoLuong = SoLuong - {0} WHERE MaQuanAo = N'{1}'", sl, maQA);
                dtBase.CapNhatDuLieu(sqlTruKho);
            }

            string thongBao = "Đặt hàng thành công! Mã đơn hàng: " + maHD;
            if (hinhThuc == "Chuyển khoản")
                thongBao += "\nCửa hàng sẽ xác nhận đơn sau khi kiểm tra khoản chuyển khoản của bạn.";
            else
                thongBao += "\nBạn sẽ thanh toán bằng tiền mặt khi nhận hàng.";
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dtGioHang.Clear();
            GioHang.PhienBanKho++;   // báo form xem sản phẩm nạp lại tồn kho
            txtTongTien.Text = "0";
            rdoTienMat.Checked = true;
            picXemTruoc.Image = null;
            lblThongTinSP.Text = "Chọn sản phẩm để xem trước";
            LoadSanPham("");
        }
        private void txtDienThoai_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
