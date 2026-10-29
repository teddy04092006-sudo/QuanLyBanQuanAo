using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyBanQuanAo.GUI
{
    /// <summary>Hàm tiện ích dựng nhanh control bằng code (các form mới không dùng Designer).</summary>
    internal static class UI
    {
        public static Label Lbl(Control p, string text, int x, int y, int w)
        {
            Label l = new Label();
            l.Text = text; l.Left = x; l.Top = y + 4; l.Width = w; l.Height = 20;
            p.Controls.Add(l);
            return l;
        }

        public static TextBox Txt(Control p, int x, int y, int w, bool readOnly)
        {
            TextBox t = new TextBox();
            t.Left = x; t.Top = y; t.Width = w; t.ReadOnly = readOnly;
            if (readOnly) t.BackColor = Color.FromArgb(245, 245, 215);
            p.Controls.Add(t);
            return t;
        }

        public static ComboBox Cbo(Control p, int x, int y, int w)
        {
            ComboBox c = new ComboBox();
            c.Left = x; c.Top = y; c.Width = w;
            c.DropDownStyle = ComboBoxStyle.DropDownList;
            p.Controls.Add(c);
            return c;
        }

        public static Button Btn(Control p, string text, int x, int y, int w, EventHandler click)
        {
            Button b = new Button();
            b.Text = text; b.Left = x; b.Top = y; b.Width = w; b.Height = 30;
            if (click != null) b.Click += click;
            p.Controls.Add(b);
            return b;
        }

        public static NumericUpDown Nud(Control p, int x, int y, int w, int min, int max, int val)
        {
            NumericUpDown n = new NumericUpDown();
            n.Left = x; n.Top = y; n.Width = w;
            n.Minimum = min; n.Maximum = max; n.Value = val;
            p.Controls.Add(n);
            return n;
        }

        public static DataGridView Grid(Control p, int x, int y, int w, int h)
        {
            DataGridView g = new DataGridView();
            g.Left = x; g.Top = y; g.Width = w; g.Height = h;
            g.ReadOnly = true;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.MultiSelect = false;
            g.RowHeadersVisible = false;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.BackgroundColor = Color.White;
            g.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            p.Controls.Add(g);
            return g;
        }

        public static void Headers(DataGridView g, params string[] h)
        {
            for (int i = 0; i < h.Length && i < g.Columns.Count; i++)
                g.Columns[i].HeaderText = h[i];
        }

        public static void Format(DataGridView g, string fmt, params int[] cols)
        {
            foreach (int c in cols)
                if (c < g.Columns.Count) g.Columns[c].DefaultCellStyle.Format = fmt;
        }
    }
}
