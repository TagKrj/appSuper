using appSuper.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using WinFormsMVC.Model;

namespace appSuper.Controller
{
    public class MeVaBeController
    {
        public static List<MeVaBe> getAllMeVaBes()
        {
            List<MeVaBe> MeVaBes = new List<MeVaBe>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM MeVaBe";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MeVaBes.Add(new MeVaBe
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

            return MeVaBes;
        }

        public static void AddMeVaBes(MeVaBe MeVaBe)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO MeVaBe(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", MeVaBe.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", MeVaBe.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", MeVaBe.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", MeVaBe.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", MeVaBe.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", MeVaBe.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateMeVaBes(MeVaBe MeVaBe)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE MeVaBe SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", MeVaBe.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", MeVaBe.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", MeVaBe.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", MeVaBe.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", MeVaBe.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", MeVaBe.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteMeVaBes(MeVaBe MeVaBe)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM MeVaBe WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", MeVaBe.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<MeVaBe> SearchMeVaBes(string searchValue)
        {
            List<MeVaBe> MeVaBes = new List<MeVaBe>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM MeVaBe WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MeVaBes.Add(new MeVaBe
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

            return MeVaBes;
        }
    }
}