using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyBanQuanAo.DAL
{
    /// <summary>Đại diện 1 giao dịch (transaction) đang mở. Dùng trong DataProcesser.ThucThiGiaoDich.</summary>
    public class GiaoDich
    {
        internal SqlConnection Conn;
        internal SqlTransaction Tran;

        /// <summary>Chạy INSERT/UPDATE/DELETE, trả về số dòng bị ảnh hưởng.</summary>
        public int Chay(string sql, params SqlParameter[] ps)
        {
            using (SqlCommand cmd = new SqlCommand(sql, Conn, Tran))
            {
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Lấy 1 giá trị đơn (null nếu không có dòng).</summary>
        public object LayGiaTri(string sql, params SqlParameter[] ps)
        {
            using (SqlCommand cmd = new SqlCommand(sql, Conn, Tran))
            {
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                object o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? null : o;
            }
        }
    }

    public class DataProcesser
    {
        private static string LayChuoiKetNoi()
        {
            ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["QuanLyBanQuanAo"];
            if (cs != null && !string.IsNullOrEmpty(cs.ConnectionString)) return cs.ConnectionString;
            return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuanLyBanQuanAo;Integrated Security=True";
        }

        private readonly string strConnect = LayChuoiKetNoi();

        /// <summary>Tạo tham số SQL (tự đổi null thành DBNull).</summary>
        public static SqlParameter P(string ten, object giaTri)
        {
            return new SqlParameter(ten, giaTri ?? DBNull.Value);
        }

        private static string ThongBaoLoi(SqlException ex)
        {
            if (ex.Number == 547) return "Dữ liệu đang được dùng ở bảng khác (hoặc giá trị không hợp lệ) nên không thể thực hiện thao tác này.";
            if (ex.Number == 2627 || ex.Number == 2601) return "Mã này đã tồn tại, vui lòng nhập mã khác.";
            return ex.Message;
        }

        /// <summary>SELECT trả về DataTable. Dùng tham số để tránh SQL Injection.</summary>
        public DataTable DocBang(string sql, params SqlParameter[] ps)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    if (ps != null && ps.Length > 0) da.SelectCommand.Parameters.AddRange(ps);
                    da.Fill(dt);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ThongBaoLoi(ex), "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>INSERT/UPDATE/DELETE. Trả về true nếu thành công.</summary>
        public bool CapNhatDuLieu(string sql, params SqlParameter[] ps)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ThongBaoLoi(ex), "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Chạy nhiều lệnh trong 1 giao dịch: lỗi ở bất kỳ lệnh nào (hoặc ném Exception) thì Rollback tất cả.
        /// </summary>
        public bool ThucThiGiaoDich(Action<GiaoDich> work, out string loi)
        {
            loi = "";
            using (SqlConnection conn = new SqlConnection(strConnect))
            {
                try { conn.Open(); }
                catch (Exception ex) { loi = "Không kết nối được CSDL: " + ex.Message; return false; }

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        work(new GiaoDich { Conn = conn, Tran = tran });
                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        try { tran.Rollback(); } catch { }
                        loi = (ex is SqlException) ? ThongBaoLoi((SqlException)ex) : ex.Message;
                        return false;
                    }
                }
            }
        }
    }
}
