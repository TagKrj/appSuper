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
    public class DoChoiController
    {
        public static List<DoChoi> getAllDoChois()
        {
            List<DoChoi> DoChois = new List<DoChoi>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM DoChoi";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DoChois.Add(new DoChoi
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

            return DoChois;
        }

        public static void AddDoChois(DoChoi DoChoi)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO DoChoi(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", DoChoi.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", DoChoi.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", DoChoi.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", DoChoi.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", DoChoi.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", DoChoi.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateDoChois(DoChoi DoChoi)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE DoChoi SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", DoChoi.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", DoChoi.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", DoChoi.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", DoChoi.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", DoChoi.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", DoChoi.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteDoChois(DoChoi DoChoi)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM DoChoi WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", DoChoi.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<DoChoi> SearchDoChois(string searchValue)
        {
            List<DoChoi> DoChois = new List<DoChoi>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM DoChoi WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DoChois.Add(new DoChoi
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

            return DoChois;
        }
    }
}
