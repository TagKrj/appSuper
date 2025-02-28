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
    class GiaDungController
    {
        public static List<GiaDung> getAllGiaDungs()
        {
            List<GiaDung> GiaDungs = new List<GiaDung>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM GiaDung";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GiaDungs.Add(new GiaDung
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

            return GiaDungs;
        }

        public static void AddGiaDungs(GiaDung GiaDung)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO GiaDung(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", GiaDung.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", GiaDung.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", GiaDung.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", GiaDung.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", GiaDung.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", GiaDung.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateGiaDungs(GiaDung GiaDung)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE GiaDung SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", GiaDung.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", GiaDung.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", GiaDung.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", GiaDung.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", GiaDung.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", GiaDung.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteGiaDungs(GiaDung GiaDung)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM GiaDung WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", GiaDung.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<GiaDung> SearchGiaDungs(string searchValue)
        {
            List<GiaDung> GiaDungs = new List<GiaDung>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM GiaDung WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GiaDungs.Add(new GiaDung
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

            return GiaDungs;
        }
    }
}
