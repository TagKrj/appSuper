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
    class ThoiTrangController
    {
        public static List<ThoiTrang> getAllThoiTrangs()
        {
            List<ThoiTrang> ThoiTrangs = new List<ThoiTrang>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM ThoiTrang";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThoiTrangs.Add(new ThoiTrang
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

            return ThoiTrangs;
        }

        public static void AddThoiTrangs(ThoiTrang ThoiTrang)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO ThoiTrang(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", ThoiTrang.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", ThoiTrang.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", ThoiTrang.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", ThoiTrang.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", ThoiTrang.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", ThoiTrang.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateThoiTrangs(ThoiTrang ThoiTrang)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE ThoiTrang SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", ThoiTrang.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", ThoiTrang.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", ThoiTrang.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", ThoiTrang.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", ThoiTrang.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", ThoiTrang.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteThoiTrangs(ThoiTrang ThoiTrang)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM ThoiTrang WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", ThoiTrang.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<ThoiTrang> SearchThoiTrangs(string searchValue)
        {
            List<ThoiTrang> ThoiTrangs = new List<ThoiTrang>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM ThoiTrang WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThoiTrangs.Add(new ThoiTrang
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

            return ThoiTrangs;
        }
    }
}
