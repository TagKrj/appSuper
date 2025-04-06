using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using appSuper.Model;
using Microsoft.Office.Interop.Excel;
using WinFormsMVC.Model;

namespace appSuper.Controller
{
    class NhanVienController
    {
        public static List<NhanVien> getAllNhanViens()
        {
            List<NhanVien> NhanViens = new List<NhanVien>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM NhanVien";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NhanViens.Add(new NhanVien
                        {
                            maNV = reader["maNV"].ToString(),
                            tenNV = reader["tenNV"].ToString(),
                            soDT = reader["soDT"].ToString(),
                            diaChi = reader["diaChi"].ToString(),
                            email = reader["email"].ToString(),
                            namSinh = (DateTime)reader["namSinh"],
                        });
                    }
                }
            }

            return NhanViens;
        }

        public static void AddNhanViens(NhanVien nhanVien)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO NhanVien(maNV, tenNV, namSinh, diaChi, soDT, email) " +
                             "VALUES (@maNV, @tenNV, @namSinh, @diaChi, @soDT, @email)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNV", nhanVien.maNV);
                    cmd.Parameters.AddWithValue("@tenNV", nhanVien.tenNV);
                    cmd.Parameters.AddWithValue("@namSinh", nhanVien.namSinh);
                    cmd.Parameters.AddWithValue("@soDT", nhanVien.soDT);
                    cmd.Parameters.AddWithValue("@email", nhanVien.email);
                    cmd.Parameters.AddWithValue("@diaChi", nhanVien.diaChi);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateNhanViens(NhanVien nhanVien)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE NhanVien SET tenNV = @tenNV, " +
                             "namSinh = @namSinh, diaChi = @diaChi, soDT = @soDT, email = @email " +
                             "WHERE maNV = @maNV";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNV", nhanVien.maNV);
                    cmd.Parameters.AddWithValue("@tenNV", nhanVien.tenNV);
                    cmd.Parameters.AddWithValue("@namSinh", nhanVien.namSinh);
                    cmd.Parameters.AddWithValue("@soDT", nhanVien.soDT);
                    cmd.Parameters.AddWithValue("@email", nhanVien.email);
                    cmd.Parameters.AddWithValue("@diaChi", nhanVien.diaChi);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteNhanViens(NhanVien nhanVien)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM NhanVien WHERE maNV = @maNV";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNV", nhanVien.maNV);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<NhanVien> SearchNhanViens(string searchValue)
        {
            List<NhanVien> NhanViens = new List<NhanVien>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM NhanVien WHERE maNV LIKE @search OR tenNV LIKE @search OR diaChi LIKE @search OR soDT LIKE @search OR email LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhanViens.Add(new NhanVien
                            {
                                maNV = reader["maNV"].ToString(),
                                tenNV = reader["tenNV"].ToString(),
                                soDT = reader["soDT"].ToString(),
                                diaChi = reader["diaChi"].ToString(),
                                email = reader["email"].ToString(),
                                namSinh = (DateTime)reader["namSinh"],
                            });
                        }
                    }
                }
            }
            return NhanViens;
        }
        public static void ThemmoiNhanVien(string maNV, string tenNV, string soDT, string diaChi, string email, DateTime namSinh)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string sql = @"INSERT INTO NhanVien 
                       (maNV, tenNV, soDT, diaChi, email, namSinh, createdAt, updatedAt) 
                       VALUES 
                       (@maNV, @tenNV, @soDT, @diaChi, @email, @namSinh, GETDATE(), GETDATE())";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Gắn tham số từ phương thức vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@maNV", maNV);
                    cmd.Parameters.AddWithValue("@tenNV", tenNV);
                    cmd.Parameters.AddWithValue("@soDT", soDT);
                    cmd.Parameters.AddWithValue("@diaChi", diaChi);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@namSinh", namSinh);

                    try
                    {
                      
                        cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}");
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
                    worksheet.Name = "NhanVien Data";

                    // Tiêu đề cột
                    string[] columnHeaders = { "STT", "Mã Nhân Viên", "Tên Nhân Viên", "Số Điện Thoại", "Địa Chỉ", "Email", "Năm Sinh", "Ngày Tạo", "Ngày Cập Nhật" };

                    // Định dạng tiêu đề
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
                        // Cột STT
                        worksheet.Cells[row + 2, 1].Value2 = row + 1;

                        for (int col = 0; col < dgv.Columns.Count; col++)
                        {
                            var cellValue = dgv.Rows[row].Cells[col].Value;

                            // Xử lý số điện thoại: Thêm dấu `'` trước số để giữ nguyên số 0
                            if (dgv.Columns[col].HeaderText == "Số Điện Thoại")
                            {
                                string phoneValue = cellValue?.ToString() ?? "";
                                if (!string.IsNullOrEmpty(phoneValue) && !phoneValue.StartsWith("0"))
                                {
                                    phoneValue = "'0" + phoneValue;
                                }
                                worksheet.Cells[row + 2, col + 2].NumberFormat = "@"; // Định dạng cột dưới dạng text
                                worksheet.Cells[row + 2, col + 2].Value2 = "'0" + phoneValue;
                            }
                            // Xử lý ngày tháng: Định dạng dd/MM/yyyy
                            else if (dgv.Columns[col].HeaderText == "Năm Sinh" || dgv.Columns[col].HeaderText == "Ngày Tạo" || dgv.Columns[col].HeaderText == "Ngày Cập Nhật")
                            {
                                DateTime dateValue;
                                if (DateTime.TryParse(cellValue?.ToString(), out dateValue))
                                {
                                    worksheet.Cells[row + 2, col + 2].NumberFormat = "dd/MM/yyyy";
                                    worksheet.Cells[row + 2, col + 2].Value2 = dateValue.ToString("dd/MM/yyyy");
                                }
                                else
                                {
                                    worksheet.Cells[row + 2, col + 2].Value2 = "Không hợp lệ";
                                }
                            }
                            // Các cột khác
                            else
                            {
                                worksheet.Cells[row + 2, col + 2].Value2 = cellValue?.ToString() ?? "";
                            }
                        }
                    }

                    // Tự động căn chỉnh cột
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
