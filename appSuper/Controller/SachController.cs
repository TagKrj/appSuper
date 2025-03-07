using System.Collections.Generic;
using System.Data.SqlClient;
using appSuper.Model;
using WinFormsMVC.Model;

namespace appSuper.Controller
{
    internal class SachController
    {
        public static List<Sach> getAllSaches()
        {
            List<Sach> Saches = new List<Sach>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM Sach";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Saches.Add(new Sach
                        {
                            maSP = reader["maSP"].ToString(),
                            tenSP = reader["tenSP"].ToString(),
                            nhaCungCap = reader["nhaCungCap"].ToString(),
                            soLuong = (int)reader["soLuong"],
                            giaNhap = (decimal)reader["giaNhap"],
                            giaBan = (decimal)reader["giaBan"]
                        });
                    }
                }
            }
            return Saches;
        }
        public static void AddSaches(Sach Sach)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO Sach(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", Sach.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", Sach.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", Sach.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", Sach.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", Sach.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", Sach.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateSaches(Sach Sach)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE Sach SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", Sach.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", Sach.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", Sach.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", Sach.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", Sach.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", Sach.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteSaches(Sach Sach)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM Sach WHERE maSP = @maSP";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", Sach.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Sach> SearchSaches(string searchValue)
        {
            List<Sach> Saches = new List<Sach>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM Sach WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Saches.Add(new Sach
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

            return Saches;
        }
    }
}
