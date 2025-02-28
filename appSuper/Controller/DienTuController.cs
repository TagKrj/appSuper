using appSuper.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMVC.Model;

namespace appSuper.Controller
{
    class DienTuController
    {
        public static List<DienTu> getAllDienTus()
        {
            List<DienTu> DienTus = new List<DienTu>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM DienTu";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DienTus.Add(new DienTu
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

            return DienTus;
        }

        public static void AddDienTus(DienTu DienTu)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO DienTu(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", DienTu.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", DienTu.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", DienTu.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", DienTu.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", DienTu.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", DienTu.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateDienTus(DienTu DienTu)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE DienTu SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", DienTu.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", DienTu.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", DienTu.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", DienTu.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", DienTu.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", DienTu.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteDienTus(DienTu DienTu)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM DienTu WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", DienTu.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<DienTu> SearchDienTus(string searchValue)
        {
            List<DienTu> DienTus = new List<DienTu>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM DienTu WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DienTus.Add(new DienTu
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

            return DienTus;
        }
    }
}
