using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyBanQuanAo.BLL
{
    public static class ExcelHelper
    {
        private const string BoLocFile = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";

        // Chuỗi toàn chữ số (vd số điện thoại 0912...) phải ghi dạng text, nếu không Excel làm mất số 0 đầu
        private static object ChuanHoa(object v)
        {
            if (v == null || v == DBNull.Value) return "";
            string s = v as string;
            if (s != null && Regex.IsMatch(s, @"^\d+$")) return "'" + s;
            if (v is DateTime) return ((DateTime)v).ToString("dd/MM/yyyy");
            return v;
        }

        /// <summary>Xuất 1 DataTable ra Excel (tiêu đề + bảng có viền).</summary>
        public static void XuatBang(DataTable dt, string tieuDe, string[] headers, string tenSheet)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = BoLocFile;
                dlg.FileName = tenSheet + ".xlsx";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                Excel.Application app = null;
                try
                {
                    app = new Excel.Application();
                    app.DisplayAlerts = false;
                    Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
                    Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];
                    ws.Name = tenSheet;

                    int nc = dt.Columns.Count, nr = dt.Rows.Count;

                    Excel.Range title = ws.Range[ws.Cells[1, 1], ws.Cells[1, nc]];
                    title.Merge();
                    title.Value2 = tieuDe;
                    title.Font.Size = 15;
                    title.Font.Bold = true;
                    title.Font.Color = ColorTranslator.ToOle(Color.Red);
                    title.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    for (int c = 0; c < nc; c++)
                        ws.Cells[3, c + 1] = (headers != null && c < headers.Length) ? headers[c] : dt.Columns[c].ColumnName;
                    Excel.Range head = ws.Range[ws.Cells[3, 1], ws.Cells[3, nc]];
                    head.Font.Bold = true;
                    head.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    head.Interior.Color = ColorTranslator.ToOle(Color.LightGray);

                    object[,] arr = new object[nr, nc];
                    for (int r = 0; r < nr; r++)
                        for (int c = 0; c < nc; c++)
                            arr[r, c] = ChuanHoa(dt.Rows[r][c]);
                    Excel.Range body = ws.Range[ws.Cells[4, 1], ws.Cells[3 + nr, nc]];
                    body.Value2 = arr;

                    Excel.Range all = ws.Range[ws.Cells[3, 1], ws.Cells[3 + nr, nc]];
                    all.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    ws.Columns.AutoFit();

                    wb.SaveAs(dlg.FileName);
                    wb.Close();
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không xuất được Excel (máy cần cài Microsoft Excel).\n" + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (app != null) { app.Quit(); Marshal.ReleaseComObject(app); }
                }
            }
        }

        /// <summary>
        /// In hóa đơn bán hàng ra Excel. ct có các cột theo thứ tự:
        /// MaQuanAo, TenQuanAo, KichCo, MauSac, SoLuong, DonGia, GiamGia, ThanhTien
        /// </summary>
        public static void InHoaDon(string maHD, DateTime ngay, string tenNV, string tenKhach, string sdt,
            string diaChi, string hinhThuc, DataTable ct, double tong, string bangChu)
        {
            if (ct == null || ct.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có mặt hàng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = BoLocFile;
                dlg.FileName = "HoaDon_" + maHD + ".xlsx";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                Excel.Application app = null;
                try
                {
                    app = new Excel.Application();
                    app.DisplayAlerts = false;
                    Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
                    Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];
                    ws.Name = "HoaDon";

                    // --- Đầu trang: thông tin cửa hàng ---
                    ws.Cells[1, 1] = Function.TenShop;
                    ws.Cells[2, 1] = Function.DiaChiShop;
                    ws.Cells[3, 1] = Function.DienThoaiShop;
                    Excel.Range shop = ws.Range["A1:I3"];
                    shop.Font.Bold = true;
                    shop.Font.Color = ColorTranslator.ToOle(Color.Blue);

                    Excel.Range tieuDe = ws.Range["A5:I5"];
                    tieuDe.Merge();
                    tieuDe.Value2 = "HÓA ĐƠN BÁN HÀNG";
                    tieuDe.Font.Size = 16;
                    tieuDe.Font.Bold = true;
                    tieuDe.Font.Color = ColorTranslator.ToOle(Color.Red);
                    tieuDe.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    // --- Thông tin hóa đơn ---
                    ws.Cells[7, 1] = "Mã hóa đơn: " + maHD;
                    ws.Cells[7, 6] = "Ngày bán: " + ngay.ToString("dd/MM/yyyy HH:mm");
                    ws.Cells[8, 1] = "Khách hàng: " + tenKhach + "   -   SĐT: " + sdt;
                    ws.Cells[9, 1] = "Địa chỉ: " + diaChi;
                    ws.Cells[10, 1] = "Nhân viên bán hàng: " + tenNV;
                    ws.Cells[10, 6] = "Thanh toán: " + hinhThuc;

                    // --- Bảng hàng ---
                    string[] hd = { "STT", "Mã hàng", "Tên hàng", "Size", "Màu", "SL", "Đơn giá", "Giảm giá %", "Thành tiền" };
                    for (int c = 0; c < hd.Length; c++) ws.Cells[12, c + 1] = hd[c];
                    Excel.Range head = ws.Range["A12:I12"];
                    head.Font.Bold = true;
                    head.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    head.Interior.Color = ColorTranslator.ToOle(Color.LightGray);

                    int n = ct.Rows.Count;
                    object[,] arr = new object[n, 9];
                    for (int i = 0; i < n; i++)
                    {
                        DataRow r = ct.Rows[i];
                        arr[i, 0] = i + 1;
                        arr[i, 1] = r["MaQuanAo"].ToString();
                        arr[i, 2] = r["TenQuanAo"].ToString();
                        arr[i, 3] = r["KichCo"].ToString();
                        arr[i, 4] = r["MauSac"].ToString();
                        arr[i, 5] = Convert.ToInt32(r["SoLuong"]);
                        arr[i, 6] = Convert.ToDouble(r["DonGia"]);
                        arr[i, 7] = Convert.ToDouble(r["GiamGia"]);
                        arr[i, 8] = Convert.ToDouble(r["ThanhTien"]);
                    }
                    int cuoi = 12 + n;
                    Excel.Range body = ws.Range[ws.Cells[13, 1], ws.Cells[cuoi, 9]];
                    body.Value2 = arr;
                    ws.Range[ws.Cells[13, 7], ws.Cells[cuoi + 1, 7]].NumberFormat = "#,##0";
                    ws.Range[ws.Cells[13, 9], ws.Cells[cuoi + 1, 9]].NumberFormat = "#,##0";

                    // --- Tổng tiền ---
                    int rt = cuoi + 1;
                    Excel.Range gop = ws.Range[ws.Cells[rt, 1], ws.Cells[rt, 8]];
                    gop.Merge();
                    gop.Value2 = "TỔNG TIỀN";
                    gop.Font.Bold = true;
                    gop.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ws.Cells[rt, 9] = tong;
                    ((Excel.Range)ws.Cells[rt, 9]).Font.Bold = true;
                    ws.Range[ws.Cells[12, 1], ws.Cells[rt, 9]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    ws.Cells[rt + 2, 1] = "Bằng chữ: " + bangChu;
                    ((Excel.Range)ws.Cells[rt + 2, 1]).Font.Italic = true;
                    if (hinhThuc == "Chuyển khoản")
                        ws.Cells[rt + 3, 1] = Function.ThongTinChuyenKhoan;

                    ws.Cells[rt + 5, 2] = "Người mua hàng";
                    ws.Cells[rt + 5, 7] = "Người bán hàng";
                    ws.Cells[rt + 6, 2] = "(Ký, ghi rõ họ tên)";
                    ws.Cells[rt + 6, 7] = "(Ký, ghi rõ họ tên)";
                    ws.Range[ws.Cells[rt + 5, 1], ws.Cells[rt + 5, 9]].Font.Bold = true;

                    ws.Columns.AutoFit();
                    ((Excel.Range)ws.Columns[1]).ColumnWidth = 6;

                    wb.SaveAs(dlg.FileName);
                    wb.Close();
                    MessageBox.Show("In hóa đơn ra Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không xuất được Excel (máy cần cài Microsoft Excel).\n" + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (app != null) { app.Quit(); Marshal.ReleaseComObject(app); }
                }
            }
        }
    }
}
