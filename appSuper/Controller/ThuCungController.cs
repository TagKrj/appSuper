using System.Collections.Generic;
using System.Data.SqlClient;
using appSuper.Model;
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
    }
}
