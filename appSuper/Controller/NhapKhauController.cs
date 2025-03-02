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
    class NhapKhauController
    {
        public static List<NhapKhau> getAllNhapKhaus()
        {
            List<NhapKhau> NhapKhaus = new List<NhapKhau>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM NhapKhau";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NhapKhaus.Add(new NhapKhau
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

            return NhapKhaus;
        }

        public static void AddNhapKhaus(NhapKhau NhapKhau)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO NhapKhau(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", NhapKhau.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", NhapKhau.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", NhapKhau.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", NhapKhau.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", NhapKhau.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", NhapKhau.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateNhapKhaus(NhapKhau NhapKhau)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE NhapKhau SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", NhapKhau.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", NhapKhau.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", NhapKhau.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", NhapKhau.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", NhapKhau.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", NhapKhau.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteNhapKhaus(NhapKhau NhapKhau)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM NhapKhau WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", NhapKhau.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<NhapKhau> SearchNhapKhaus(string searchValue)
        {
            List<NhapKhau> NhapKhaus = new List<NhapKhau>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM NhapKhau WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhapKhaus.Add(new NhapKhau
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

            return NhapKhaus;
        }
    }
}
