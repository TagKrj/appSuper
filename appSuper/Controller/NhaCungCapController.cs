using appSuper.Model;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsMVC.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace appSuper.Controller
{
    class NhaCungCapController
    {
        public static List<NhaCungCap> getAllNhaCungCaps()
        {
            List<NhaCungCap> NhaCungCaps = new List<NhaCungCap>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM NhaCungCap";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NhaCungCaps.Add(new NhaCungCap
                        {
                            maNhaCC = reader["maNhaCC"].ToString(),
                            tenNhaCC = reader["tenNhaCC"].ToString(),
                            diaChi = reader["diaChi"].ToString(),
                        });
                    }
                }
            }

            return NhaCungCaps;
        }

        public static void AddNhaCungCaps(NhaCungCap nhaCungCap)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO NhaCungCap(maNhaCC, tenNhaCC, diaChi) " +
                               "VALUES (@maNhaCC, @tenNhaCC, @diaChi)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNhaCC", nhaCungCap.maNhaCC);
                    cmd.Parameters.AddWithValue("@tenNhaCC", nhaCungCap.tenNhaCC);
                    cmd.Parameters.AddWithValue("@diaChi", nhaCungCap.diaChi);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateNhaCungCaps(NhaCungCap nhaCungCap)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE NhaCungCap SET tenNhaCC = @tenNhaCC, diaChi = @diaChi Where maNhaCC = @maNhaCC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNhaCC", nhaCungCap.maNhaCC);
                    cmd.Parameters.AddWithValue("@tenNhaCC", nhaCungCap.tenNhaCC);
                    cmd.Parameters.AddWithValue("@diaChi", nhaCungCap.diaChi);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteNhaCungCaps(NhaCungCap nhaCungCap)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM NhaCungCap WHERE maNhaCC = @maNhaCC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNhaCC", nhaCungCap.maNhaCC);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<NhaCungCap> SearchNhaCungCaps(string search)
        {
            List<NhaCungCap> NhaCungCaps = new List<NhaCungCap>();
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM NhaCungCap WHERE maNhaCC LIKE @search OR tenNhaCC LIKE @search OR diaChi LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhaCungCaps.Add(new NhaCungCap
                            {
                                maNhaCC = reader["maNhaCC"].ToString(),
                                tenNhaCC = reader["tenNhaCC"].ToString(),
                                diaChi = reader["diaChi"].ToString(),
                            });
                        }
                    }
                }
            }
            return NhaCungCaps;
        }
        public static void ThemmoiNhaCungCap(string maNhaCC, string tenNhaCC, string diaChi)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string sql = @"INSERT INTO NhaCungCap (maNhaCC, tenNhaCC, diaChi, createdAt, updatedAt) 
                       VALUES (@maNhaCC, @tenNhaCC, @diaChi, GETDATE(), GETDATE())";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Gắn tham số với giá trị từ phương thức
                    cmd.Parameters.AddWithValue("@maNhaCC", maNhaCC);
                    cmd.Parameters.AddWithValue("@tenNhaCC", tenNhaCC);
                    cmd.Parameters.AddWithValue("@diaChi", diaChi);

                    try
                    {
                        cmd.ExecuteNonQuery(); // Thực thi lệnh SQL
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi thêm nhà cung cấp: {ex.Message}");
                    }
                }
            }
        }
        public class ExcelExporter
        {
            public void ExportDataGridViewToExcel(DataGridView dgv)
            {
                if (dgv.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application excelApp = null;

                try
                {
                    // Khởi tạo ứng dụng Excel
                    excelApp = new Microsoft.Office.Interop.Excel.Application
                    {
                        Visible = true,
                        DisplayAlerts = false
                    };

                    Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                    Worksheet worksheet = (Worksheet)workbook.Sheets[1];
                    worksheet.Name = "NhaCungCap Data";

                    // Tiêu đề cột trong bảng SQL NhaCungCap
                    string[] columnHeaders = { "STT", "Mã Nhà Cung Cấp", "Tên Nhà Cung Cấp", "Địa Chỉ", "Ngày Tạo", "Ngày Cập Nhật" };

                    // Thêm tiêu đề vào Excel
                    for (int col = 0; col < columnHeaders.Length; col++)
                    {
                        worksheet.Cells[1, col + 1] = columnHeaders[col];
                        Range headerCell = worksheet.Cells[1, col + 1];
                        headerCell.Font.Bold = true;
                        headerCell.Interior.ColorIndex = 15; // Màu nền
                        headerCell.Borders.LineStyle = XlLineStyle.xlContinuous;
                        headerCell.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    }

                    // Thêm dữ liệu từ DataGridView vào Excel
                    for (int row = 0; row < dgv.Rows.Count; row++)
                    {
                        // Cột STT (Số Thứ Tự)
                        worksheet.Cells[row + 2, 1].Value2 = row + 1;

                        // Các cột dữ liệu từ DataGridView
                        for (int col = 0; col < dgv.Columns.Count; col++)
                        {
                            var cellValue = dgv.Rows[row].Cells[col].Value;
                            worksheet.Cells[row + 2, col + 2].Value2 = cellValue == null ? "" : cellValue.ToString();
                        }
                    }

                    // Căn chỉnh cột
                    worksheet.Columns.AutoFit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (excelApp != null) Marshal.ReleaseComObject(excelApp);
                }
            }
        }

    }
}
