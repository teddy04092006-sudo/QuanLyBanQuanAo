using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QuanLyBanQuanAo.GUI
{
    /// <summary>
    /// Bảng màu + hàm tô màu dùng chung cho mọi form.
    /// Cách dùng: gọi Theme.Apply(this) ở cuối constructor của form.
    /// Control được thêm vào SAU đó (ví dụ thẻ sản phẩm) cũng tự được tô màu.
    /// </summary>
    internal static class Theme
    {
        // ---------------------------------------------------------------- bảng màu
        public static readonly Color Primary     = Color.FromArgb(0, 121, 140);     // xanh ngọc
        public static readonly Color PrimaryDark = Color.FromArgb(0, 77, 92);
        public static readonly Color PrimaryLight= Color.FromArgb(224, 244, 247);
        public static readonly Color Accent      = Color.FromArgb(255, 107, 107);   // san hô
        public static readonly Color Gold        = Color.FromArgb(255, 193, 7);
        public static readonly Color FormBack    = Color.FromArgb(238, 247, 249);
        public static readonly Color RowAlt      = Color.FromArgb(229, 245, 248);
        public static readonly Color RowSelect   = Color.FromArgb(255, 224, 130);
        public static readonly Color FocusBack   = Color.FromArgb(255, 250, 205);

        public static readonly Color Green  = Color.FromArgb(39, 174, 96);
        public static readonly Color Blue   = Color.FromArgb(41, 128, 185);
        public static readonly Color Orange = Color.FromArgb(243, 156, 18);
        public static readonly Color Red    = Color.FromArgb(231, 76, 60);
        public static readonly Color Purple = Color.FromArgb(142, 68, 173);
        public static readonly Color Gray   = Color.FromArgb(127, 140, 141);

        // ---------------------------------------------------------------- tô màu đệ quy
        public static void Apply(Control root)
        {
            if (root == null) return;
            Form f = root as Form;
            if (f != null && !f.IsMdiContainer) f.BackColor = FormBack;

            Style(root);
            foreach (Control c in root.Controls) Apply(c);

            // control thêm sau (tạo bằng code) cũng được tô
            root.ControlAdded -= OnControlAdded;
            root.ControlAdded += OnControlAdded;
        }

        private static void OnControlAdded(object sender, ControlEventArgs e)
        {
            Apply(e.Control);
        }

        private static void Style(Control c)
        {
            Button b = c as Button;
            if (b != null) { StyleButton(b); return; }

            DataGridView g = c as DataGridView;
            if (g != null) { StyleGrid(g); return; }

            GroupBox gb = c as GroupBox;
            if (gb != null) { StyleGroup(gb); return; }

            TextBox t = c as TextBox;
            if (t != null) { StyleTextBox(t); return; }

            Label l = c as Label;
            if (l != null) { StyleLabel(l); return; }

            TabControl tc = c as TabControl;
            if (tc != null) { tc.Font = new Font(tc.Font, FontStyle.Bold); return; }
        }

        // ---------------------------------------------------------------- nút bấm
        /// <summary>Chọn màu theo chức năng của nút (dựa vào chữ hoặc tên).</summary>
        public static Color MauNut(string text, string name)
        {
            string s = ((text ?? "") + " " + (name ?? "")).ToLowerInvariant();
            if (s.Contains("xóa") || s.Contains("xoa") || s.Contains("hủy") || s.Contains("huy")) return Red;
            if (s.Contains("sửa") || s.Contains("sua")) return Orange;
            if (s.Contains("excel") || s.Contains("in hóa") || s.Contains("btnin")) return Purple;
            if (s.Contains("lưu") || s.Contains("luu") || s.Contains("xác nhận") || s.Contains("xacnhan") || s.Contains("đặt hàng") || s.Contains("dathang")) return Blue;
            if (s.Contains("khách") || s.Contains("khach")) return Accent;
            if (s.Contains("thêm") || s.Contains("them") || s.Contains("đăng nhập") || s.Contains("dangnhap")) return Green;
            if (s.Contains("giỏ") || s.Contains("gio") || s.Contains("quầy") || s.Contains("banquay")) return Accent;
            if (s.Contains("đóng") || s.Contains("dong") || s.Contains("thoát") || s.Contains("thoat") || s.Contains("bỏ qua") || s.Contains("boqua") || s.Contains("làm mới") || s.Contains("lammoi")) return Gray;
            return Primary;
        }

        public static Color SangHon(Color c, int d)
        {
            return Color.FromArgb(Math.Min(255, c.R + d), Math.Min(255, c.G + d), Math.Min(255, c.B + d));
        }

        public static Color ToiHon(Color c, int d)
        {
            return Color.FromArgb(Math.Max(0, c.R - d), Math.Max(0, c.G - d), Math.Max(0, c.B - d));
        }

        public static void StyleButton(Button b)
        {
            Color baseColor = MauNut(b.Text, b.Name);
            b.UseVisualStyleBackColor = false;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = SangHon(baseColor, 35);
            b.FlatAppearance.MouseDownBackColor = ToiHon(baseColor, 35);
            b.ForeColor = Color.White;
            b.Cursor = Cursors.Hand;
            b.Font = new Font(b.Font, FontStyle.Bold);

            Color mau = baseColor;
            b.BackColor = b.Enabled ? mau : Color.FromArgb(189, 195, 199);
            b.EnabledChanged += delegate
            {
                b.BackColor = b.Enabled ? mau : Color.FromArgb(189, 195, 199);
            };
        }

        // ---------------------------------------------------------------- bảng dữ liệu
        public static void StyleGrid(DataGridView g)
        {
            g.EnableHeadersVisualStyles = false;
            g.BorderStyle = BorderStyle.None;
            g.BackgroundColor = Color.White;
            g.GridColor = Color.FromArgb(205, 230, 235);
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            g.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Primary;
            g.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font = new Font(g.Font, FontStyle.Bold);
            g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            g.DefaultCellStyle.SelectionBackColor = RowSelect;
            g.DefaultCellStyle.SelectionForeColor = Color.Black;
            g.AlternatingRowsDefaultCellStyle.BackColor = RowAlt;
            g.RowTemplate.Height = 26;
        }

        // ---------------------------------------------------------------- khung nhóm
        private static void StyleGroup(GroupBox gb)
        {
            gb.ForeColor = PrimaryDark;
            // ô nhập bên trong vẫn chữ đen cho dễ đọc (ForeColor của GroupBox là thuộc tính được thừa kế)
            foreach (Control c in gb.Controls)
            {
                if (c is TextBox || c is ComboBox || c is NumericUpDown || c is DateTimePicker)
                    c.ForeColor = Color.Black;
            }
            gb.ControlAdded -= GroupControlAdded;
            gb.ControlAdded += GroupControlAdded;
        }

        private static void GroupControlAdded(object sender, ControlEventArgs e)
        {
            Control c = e.Control;
            if (c is TextBox || c is ComboBox || c is NumericUpDown || c is DateTimePicker)
                c.ForeColor = Color.Black;
        }

        // ---------------------------------------------------------------- ô nhập: nền vàng nhạt khi đang gõ
        private static void StyleTextBox(TextBox t)
        {
            Color goc = t.BackColor;
            t.Enter += delegate { if (!t.ReadOnly && t.Enabled) { goc = t.BackColor; t.BackColor = FocusBack; } };
            t.Leave += delegate { if (!t.ReadOnly) t.BackColor = goc; };
        }

        // ---------------------------------------------------------------- nhãn tiêu đề
        private static void StyleLabel(Label l)
        {
            bool tieuDe = l.Font.Bold && l.Font.Size >= 13f && l.ForeColor.ToArgb() == Color.Firebrick.ToArgb();
            bool chuTrang = l.ForeColor.ToArgb() == Color.White.ToArgb();
            if (tieuDe || (l.Name == "lblTieuDe" && !chuTrang)) l.ForeColor = PrimaryDark;
        }

        // ---------------------------------------------------------------- menu màu
        public static void StyleMenu(MenuStrip m)
        {
            m.Renderer = new MenuRenderer();
            m.BackColor = PrimaryDark;
            m.ForeColor = Color.White;
            m.Font = new Font(m.Font, FontStyle.Bold);
        }

        /// <summary>Vẽ nền gradient cho một vùng.</summary>
        public static void VeGradient(Graphics g, Rectangle r, Color c1, Color c2, float goc)
        {
            if (r.Width <= 0 || r.Height <= 0) return;
            using (LinearGradientBrush br = new LinearGradientBrush(r, c1, c2, goc))
                g.FillRectangle(br, r);
        }

        private class MenuColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected { get { return Primary; } }
            public override Color MenuItemSelectedGradientBegin { get { return Primary; } }
            public override Color MenuItemSelectedGradientEnd { get { return Primary; } }
            public override Color MenuItemPressedGradientBegin { get { return Accent; } }
            public override Color MenuItemPressedGradientEnd { get { return Accent; } }
            public override Color MenuItemBorder { get { return Gold; } }
            public override Color ToolStripDropDownBackground { get { return Color.White; } }
            public override Color ImageMarginGradientBegin { get { return PrimaryLight; } }
            public override Color ImageMarginGradientMiddle { get { return PrimaryLight; } }
            public override Color ImageMarginGradientEnd { get { return PrimaryLight; } }
            public override Color MenuBorder { get { return PrimaryDark; } }
        }

        private class MenuRenderer : ToolStripProfessionalRenderer
        {
            public MenuRenderer() : base(new MenuColors()) { }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                // mục ở thanh menu ngang: chữ trắng; mục trong danh sách thả xuống: chữ xanh đậm
                e.TextColor = e.Item.Owner is MenuStrip ? Color.White : (e.Item.Selected ? Color.White : PrimaryDark);
                base.OnRenderItemText(e);
            }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                if (e.ToolStrip is MenuStrip)
                    VeGradient(e.Graphics, new Rectangle(Point.Empty, e.ToolStrip.Size), PrimaryDark, Primary, 0f);
                else
                    base.OnRenderToolStripBackground(e);
            }
        }
    }
}
