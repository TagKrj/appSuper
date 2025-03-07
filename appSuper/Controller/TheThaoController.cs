using System.Collections.Generic;
using System.Data.SqlClient;
using appSuper.Model;
using WinFormsMVC.Model;

namespace appSuper.Controller
{
    internal class TheThaoController
    {
        public static List<TheThao> getAllTheThaos()
        {
            List<TheThao> TheThaos = new List<TheThao>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM TheThao";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TheThaos.Add(new TheThao
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

            return TheThaos;
        }

        public static void AddTheThaos(TheThao TheThao)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO TheThao(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", TheThao.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", TheThao.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", TheThao.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", TheThao.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", TheThao.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", TheThao.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateTheThaos(TheThao TheThao)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE TheThao SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", TheThao.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", TheThao.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", TheThao.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", TheThao.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", TheThao.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", TheThao.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteTheThaos(TheThao TheThao)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM TheThao WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", TheThao.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<TheThao> SearchTheThaos(string searchValue)
        {
            List<TheThao> TheThaos = new List<TheThao>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM TheThao WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TheThaos.Add(new TheThao
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

            return TheThaos;
        }
    }
}

