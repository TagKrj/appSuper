using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using appSuper.Model;
using Microsoft.Office.Interop.Excel;
using WinFormsMVC.Model;

namespace appSuper.Controller
{
    internal class ThuCungController
    {
        public static List<ThuCung> getAllThuCungs()
        {
            List<ThuCung> ThuCungs = new List<ThuCung>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM ThuCung";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThuCungs.Add(new ThuCung
                        {
                            maSP = reader["maSP"].ToString(),
                            tenSP = reader["tenSP"].ToString(),
                            nhaCungCap = reader["nhaCungCap"].ToString(),
                            soLuong = (int)reader["soLuong"],
                            giaNhap = (decimal)reader["giaNhap"],
                            giaBan = (decimal)reader["giaBan"],
                        });
                    }
                }
            }

            return ThuCungs;
        }

        public static void AddThuCungs(ThuCung ThuCung)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO ThuCung(maSP, tenSP, nhaCungCap, soLuong, giaNhap, giaBan) " +
                               "VALUES(@maSP, @tenSP, @nhaCungCap, @soLuong, @giaNhap, @giaBan)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", ThuCung.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", ThuCung.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", ThuCung.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", ThuCung.soLuong);
                    cmd.Parameters.AddWithValue("@giaNhap", ThuCung.giaNhap);
                    cmd.Parameters.AddWithValue("@giaBan", ThuCung.giaBan);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateThuCungs(ThuCung ThuCung)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE ThuCung SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaNhap = @giaNhap, giaBan = @giaBan WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", ThuCung.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", ThuCung.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", ThuCung.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", ThuCung.soLuong);
                    cmd.Parameters.AddWithValue("@giaNhap", ThuCung.giaNhap);
                    cmd.Parameters.AddWithValue("@giaBan", ThuCung.giaBan);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteThuCungs(ThuCung ThuCung)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM ThuCung WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", ThuCung.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<ThuCung> SearchThuCungs(string searchValue)
        {
            List<ThuCung> ThuCungs = new List<ThuCung>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM ThuCung WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThuCungs.Add(new ThuCung
                            {
                                maSP = reader["maSP"].ToString(),
                                tenSP = reader["tenSP"].ToString(),
                                nhaCungCap = reader["nhaCungCap"].ToString(),
                                soLuong = (int)reader["soLuong"],
                                giaNhap = (decimal)reader["giaNhap"],
                                giaBan = (decimal)reader["giaBan"],
                            });
                        }
                    }
                }
            }
            return ThuCungs;
        }
        public static void ThemmoiThuCung(string maSP, string tenSP, string nhaCungCap, int soLuong, decimal giaNhap, decimal giaBan)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string sql = "INSERT INTO ThuCung (maSP, tenSP, nhaCungCap, soLuong, giaNhap, giaBan) VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaNhap, @giaBan)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", maSP);
                    cmd.Parameters.AddWithValue("@tenSP", tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", soLuong);
                    cmd.Parameters.AddWithValue("@giaNhap", giaNhap);
                    cmd.Parameters.AddWithValue("@giaBan", giaBan);

                    try
                    {

                        cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message);
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
                    excelApp = new Microsoft.Office.Interop.Excel.Application
                    {
                        Visible = true,
                        DisplayAlerts = false
                    };

                    Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                    Worksheet worksheet = (Worksheet)workbook.Sheets[1];
                    worksheet.Name = "DataGridView Data";

                    worksheet.Cells[1, 1] = "STT";
                    Range headerCell = worksheet.Cells[1, 1];
                    headerCell.Font.Bold = true;
                    headerCell.Interior.ColorIndex = 15; // Màu nền
                    headerCell.Borders.LineStyle = XlLineStyle.xlContinuous;
                    headerCell.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    // Tiêu đề cột từ DataGridView
                    for (int col = 0; col < dgv.Columns.Count; col++)
                    {
                        worksheet.Cells[1, col + 2] = dgv.Columns[col].HeaderText; // Dịch sang cột tiếp theo
                        Range colHeaderCell = worksheet.Cells[1, col + 2];
                        colHeaderCell.Font.Bold = true;
                        colHeaderCell.Interior.ColorIndex = 15;
                        colHeaderCell.Borders.LineStyle = XlLineStyle.xlContinuous;
                        colHeaderCell.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    }

                    // Thêm dữ liệu cho cột "STT"
                    for (int row = 0; row < dgv.Rows.Count; row++)
                    {
                        worksheet.Cells[row + 2, 1].Value2 = row + 1; // STT là số thứ tự, bắt đầu từ 1

                        // Dữ liệu từ DataGridView (bắt đầu từ cột thứ 2)
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
