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
    internal class MyPhamController
    {
        public static List<MyPham> getAllMyPhams()
        {
            List<MyPham> MyPhams = new List<MyPham>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM MyPham";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MyPhams.Add(new MyPham
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

            return MyPhams;
        }

        public static void AddMyPhams(MyPham MyPham)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO MyPham(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", MyPham.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", MyPham.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", MyPham.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", MyPham.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", MyPham.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", MyPham.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateMyPhams(MyPham MyPham)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE MyPham SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", MyPham.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", MyPham.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", MyPham.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", MyPham.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", MyPham.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", MyPham.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteMyPhams(MyPham MyPham)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM MyPham WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", MyPham.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<MyPham> SearchMyPhams(string searchValue)
        {
            List<MyPham> MyPhams = new List<MyPham>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM MyPham WHERE maSP LIKE @search OR tenSP LIKE @search OR nhaCungCap LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MyPhams.Add(new MyPham
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

            return MyPhams;
        }
    }
}
