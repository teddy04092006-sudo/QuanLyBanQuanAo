using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanQuanAo.BLL;
using QuanLyBanQuanAo.DAL;

namespace QuanLyBanQuanAo.GUI
{
    /// <summary>Mô tả 1 cột của bảng danh mục. Kind: "text" | "phone" | "date" | "combo".</summary>
    public class ColSpec
    {
        public string Col, Header, Kind;
        public bool Required;
        public string[] Items;

        public ColSpec(string col, string header, string kind, bool required, string[] items)
        {
            Col = col; Header = header; Kind = kind; Required = required; Items = items;
        }
        public ColSpec(string col, string header) : this(col, header, "text", true, null) { }
    }

    /// <summary>
    /// Form cập nhật danh mục dùng chung: Thêm / Sửa / Xóa / Lưu / Bỏ qua / Tìm kiếm / Xuất Excel.
    /// Cột đầu tiên luôn là khóa chính (kiểu chuỗi).
    /// </summary>
    public class frmDanhMuc : Form
    {
        private readonly DataProcesser db = new DataProcesser();
        private readonly Function func = new Function();
        private readonly string title, table;
        private readonly ColSpec[] cols;
        private readonly bool choPhepSua;

        private Control[] inputs;
        private DataGridView dgv;
        private TextBox txtTim;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnBo;
        private string mode = "";      // "" | "them" | "sua"
        private string maChon = "";

        public frmDanhMuc(string title, string table, ColSpec[] cols, bool choPhepSua)
        {
            this.title = title; this.table = table; this.cols = cols; this.choPhepSua = choPhepSua;
            this.Name = "frmDM_" + table;
            this.Text = title;
            this.ClientSize = new Size(960, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            BuildUI();
            Theme.Apply(this);
            this.Load += delegate { LoadGrid(""); SetMode(""); };
        }

        // ---------------------------------------------------------------- giao diện
        private void BuildUI()
        {
            Label lbl = new Label();
            lbl.Text = title.ToUpper();
            lbl.Font = new Font("Arial", 14, FontStyle.Bold);
            lbl.ForeColor = Color.Firebrick;
            lbl.Left = 10; lbl.Top = 8; lbl.Width = 900; lbl.Height = 28;
            Controls.Add(lbl);

            int rows = (cols.Length + 1) / 2;
            GroupBox gb = new GroupBox();
            gb.Text = "Thông tin";
            gb.Left = 10; gb.Top = 42; gb.Width = 930; gb.Height = 30 + rows * 34;
            gb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(gb);

            inputs = new Control[cols.Length];
            for (int i = 0; i < cols.Length; i++)
            {
                int x = 15 + (i % 2) * 465, y = 22 + (i / 2) * 34;
                UI.Lbl(gb, cols[i].Header + (cols[i].Required ? " *" : ""), x, y, 105);
                inputs[i] = TaoO(cols[i], gb, x + 110, y);
            }

            int yb = gb.Bottom + 8;
            btnThem = UI.Btn(this, "Thêm mới", 10, yb, 95, delegate { ThemMoi(); });
            btnSua = UI.Btn(this, "Sửa", 110, yb, 80, delegate { Sua(); });
            btnXoa = UI.Btn(this, "Xóa", 195, yb, 80, delegate { Xoa(); });
            btnLuu = UI.Btn(this, "Lưu", 280, yb, 80, delegate { Luu(); });
            btnBo = UI.Btn(this, "Bỏ qua", 365, yb, 80, delegate { SetMode(""); XoaTrang(); });
            UI.Btn(this, "Xuất Excel", 450, yb, 100, delegate { XuatExcel(); });
            UI.Btn(this, "Đóng", 555, yb, 80, delegate { Close(); });

            int y2 = yb + 40;
            UI.Lbl(this, "Tìm kiếm:", 10, y2, 65);
            txtTim = UI.Txt(this, 80, y2, 260, false);
            txtTim.KeyDown += delegate (object s, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) LoadGrid(txtTim.Text.Trim()); };
            UI.Btn(this, "Tìm", 350, y2 - 3, 70, delegate { LoadGrid(txtTim.Text.Trim()); });
            UI.Btn(this, "Hiện tất cả", 425, y2 - 3, 100, delegate { txtTim.Text = ""; LoadGrid(""); });

            int top = y2 + 36;
            dgv = UI.Grid(this, 10, top, 930, ClientSize.Height - top - 10);
            dgv.CellClick += delegate (object s, DataGridViewCellEventArgs e) { ChonDong(e.RowIndex); };
        }

        private Control TaoO(ColSpec c, Control parent, int x, int y)
        {
            Control ctl;
            if (c.Kind == "date")
            {
                DateTimePicker d = new DateTimePicker();
                d.Format = DateTimePickerFormat.Short; d.Width = 150;
                d.MaxDate = DateTime.Today;
                ctl = d;
            }
            else if (c.Kind == "combo")
            {
                ComboBox cb = new ComboBox();
                cb.DropDownStyle = ComboBoxStyle.DropDownList; cb.Width = 150;
                if (c.Items != null) cb.Items.AddRange(c.Items);
                ctl = cb;
            }
            else
            {
                TextBox t = new TextBox();
                t.Width = 300;
                ctl = t;
            }
            ctl.Left = x; ctl.Top = y;
            parent.Controls.Add(ctl);
            return ctl;
        }

        // ---------------------------------------------------------------- dữ liệu
        private void LoadGrid(string tuKhoa)
        {
            string sql = "SELECT " + string.Join(", ", cols.Select(c => c.Col)) + " FROM " + table;
            List<SqlParameter> ps = new List<SqlParameter>();
            if (tuKhoa != "")
            {
                sql += " WHERE " + string.Join(" OR ", cols.Select(c => "CAST(" + c.Col + " AS NVARCHAR(200)) LIKE @k"));
                ps.Add(DataProcesser.P("@k", "%" + tuKhoa + "%"));
            }
            sql += " ORDER BY " + cols[0].Col;
            dgv.DataSource = db.DocBang(sql, ps.ToArray());
            UI.Headers(dgv, cols.Select(c => c.Header).ToArray());
            for (int i = 0; i < cols.Length && i < dgv.Columns.Count; i++)
                if (cols[i].Kind == "date") dgv.Columns[i].DefaultCellStyle.Format = "dd/MM/yyyy";
            maChon = "";
        }

        private object LayGiaTri(int i)
        {
            Control c = inputs[i];
            if (c is DateTimePicker) return ((DateTimePicker)c).Value.Date;
            string s = c.Text.Trim();
            return s == "" ? null : s;
        }

        private void DatGiaTri(int i, object v)
        {
            Control c = inputs[i];
            string s = (v == null || v == DBNull.Value) ? "" : v.ToString();
            if (c is DateTimePicker)
            {
                DateTime d;
                DateTimePicker dp = (DateTimePicker)c;
                if (v is DateTime) d = (DateTime)v;
                else if (!DateTime.TryParse(s, out d)) d = DateTime.Today;
                if (d > dp.MaxDate) d = dp.MaxDate;
                if (d < dp.MinDate) d = dp.MinDate;
                dp.Value = d;
            }
            else if (c is ComboBox)
            {
                ComboBox cb = (ComboBox)c;
                cb.SelectedIndex = cb.Items.IndexOf(s);
            }
            else c.Text = s;
        }

        private void XoaTrang()
        {
            for (int i = 0; i < cols.Length; i++) DatGiaTri(i, null);
            maChon = "";
        }

        private void ChonDong(int row)
        {
            if (row < 0 || mode != "") return;
            for (int i = 0; i < cols.Length; i++)
                DatGiaTri(i, dgv.Rows[row].Cells[i].Value);
            maChon = dgv.Rows[row].Cells[0].Value.ToString();
            SetMode("");
        }

        private void SetMode(string m)
        {
            mode = m;
            bool dangSua = m != "";
            for (int i = 0; i < inputs.Length; i++)
                inputs[i].Enabled = dangSua && !(i == 0 && m == "sua");   // không cho sửa khóa chính
            bool coChon = maChon != "";
            btnThem.Enabled = !dangSua && choPhepSua;
            btnSua.Enabled = !dangSua && choPhepSua && coChon;
            btnXoa.Enabled = !dangSua && choPhepSua && coChon;
            btnLuu.Enabled = dangSua;
            btnBo.Enabled = dangSua;
        }

        // ---------------------------------------------------------------- thao tác
        private void ThemMoi()
        {
            XoaTrang();
            SetMode("them");
            inputs[0].Focus();
        }

        private void Sua()
        {
            if (maChon == "") { MessageBox.Show("Bạn chưa chọn dòng nào để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            SetMode("sua");
            if (inputs.Length > 1) inputs[1].Focus();
        }

        private bool KiemTra()
        {
            for (int i = 0; i < cols.Length; i++)
            {
                object v = LayGiaTri(i);
                if (cols[i].Required && (v == null || (v is string && ((string)v) == "")))
                {
                    MessageBox.Show("Bạn phải nhập " + cols[i].Header + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    inputs[i].Focus();
                    return false;
                }
                if (cols[i].Kind == "phone" && v != null && !func.KiemTraSoDienThoai((string)v))
                {
                    MessageBox.Show("Số điện thoại phải gồm 10 chữ số và bắt đầu bằng số 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    inputs[i].Focus();
                    return false;
                }
            }
            return true;
        }

        private void Luu()
        {
            if (!KiemTra()) return;
            List<SqlParameter> ps = new List<SqlParameter>();
            for (int i = 0; i < cols.Length; i++) ps.Add(DataProcesser.P("@p" + i, LayGiaTri(i)));

            bool ok;
            if (mode == "them")
            {
                DataTable trung = db.DocBang("SELECT 1 FROM " + table + " WHERE " + cols[0].Col + " = @k",
                    DataProcesser.P("@k", LayGiaTri(0)));
                if (trung.Rows.Count > 0)
                {
                    MessageBox.Show("Mã này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    inputs[0].Focus();
                    return;
                }
                string tenCot = string.Join(", ", cols.Select(c => c.Col));
                string thamSo = string.Join(", ", Enumerable.Range(0, cols.Length).Select(i => "@p" + i));
                ok = db.CapNhatDuLieu("INSERT INTO " + table + " (" + tenCot + ") VALUES (" + thamSo + ")", ps.ToArray());
            }
            else
            {
                string gan = string.Join(", ", Enumerable.Range(1, cols.Length - 1).Select(i => cols[i].Col + " = @p" + i));
                ok = db.CapNhatDuLieu("UPDATE " + table + " SET " + gan + " WHERE " + cols[0].Col + " = @p0", ps.ToArray());
            }

            if (ok)
            {
                MessageBox.Show(mode == "them" ? "Thêm mới thành công!" : "Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XoaTrang();
                SetMode("");
                LoadGrid(txtTim.Text.Trim());
            }
        }

        private void Xoa()
        {
            if (maChon == "") return;
            if (MessageBox.Show("Bạn có chắc muốn xóa \"" + maChon + "\" không?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (db.CapNhatDuLieu("DELETE FROM " + table + " WHERE " + cols[0].Col + " = @k", DataProcesser.P("@k", maChon)))
            {
                XoaTrang();
                SetMode("");
                LoadGrid(txtTim.Text.Trim());
            }
        }

        private void XuatExcel()
        {
            ExcelHelper.XuatBang((DataTable)dgv.DataSource, "DANH SÁCH " + title.ToUpper(),
                cols.Select(c => c.Header).ToArray(), table.Replace("tbl", ""));
        }
    }
}
