using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using appSuper.Model;
using WinFormsMVC.Model;

namespace appSuper.Controller
{
    public class ThucPhamController
    {
        public static List<ThucPham> getAllThucPhams()
        {
            List<ThucPham> ThucPhams = new List<ThucPham>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM ThucPham";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThucPhams.Add(new ThucPham
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

            return ThucPhams;
        }

        public static void AddThucPhams(ThucPham ThucPham)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO ThucPham(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", ThucPham.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", ThucPham.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", ThucPham.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", ThucPham.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", ThucPham.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", ThucPham.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateThucPhams(ThucPham ThucPham)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE ThucPham SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", ThucPham.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", ThucPham.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", ThucPham.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", ThucPham.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", ThucPham.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", ThucPham.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteThucPhams(ThucPham ThucPham)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM ThucPham WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", ThucPham.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<ThucPham> SearchThucPhams(string searchValue)
        {
            List<ThucPham> ThucPhams = new List<ThucPham>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM ThucPham WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThucPhams.Add(new ThucPham
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

            return ThucPhams;
        }
    }
}
