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
    class GiaoDucController
    {
        public static List<GiaoDuc> getAllGiaoDucs()
        {
            List<GiaoDuc> GiaoDucs = new List<GiaoDuc>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM GiaoDuc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GiaoDucs.Add(new GiaoDuc
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

            return GiaoDucs;
        }

        public static void AddGiaoDucs(GiaoDuc GiaoDuc)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO GiaoDuc(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", GiaoDuc.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", GiaoDuc.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", GiaoDuc.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", GiaoDuc.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", GiaoDuc.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", GiaoDuc.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateGiaoDucs(GiaoDuc GiaoDuc)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE GiaoDuc SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", GiaoDuc.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", GiaoDuc.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", GiaoDuc.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", GiaoDuc.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", GiaoDuc.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", GiaoDuc.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteGiaoDucs(GiaoDuc GiaoDuc)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM GiaoDuc WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", GiaoDuc.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<GiaoDuc> SearchGiaoDucs(string searchValue)
        {
            List<GiaoDuc> GiaoDucs = new List<GiaoDuc>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM GiaoDuc WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GiaoDucs.Add(new GiaoDuc
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

            return GiaoDucs;
        }
    }
}
