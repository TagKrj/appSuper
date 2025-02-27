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
    class ThuocController
    {
        public static List<Thuoc> getAllThuocs()
        {
            List<Thuoc> Thuocs = new List<Thuoc>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM Thuoc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Thuocs.Add(new Thuoc
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

            return Thuocs;
        }

        public static void AddThuocs(Thuoc thuoc)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO Thuoc(maSP, tenSP, nhaCungCap, soLuong, giaBan, giaNhap) " +
                               "VALUES (@maSP, @tenSP, @nhaCungCap, @soLuong, @giaBan, @giaNhap)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", thuoc.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", thuoc.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", thuoc.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", thuoc.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", thuoc.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", thuoc.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateThuocs(Thuoc thuoc)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE Thuoc SET tenSP = @tenSP, nhaCungCap = @nhaCungCap, soLuong = @soLuong, giaBan = @giaBan, giaNhap = @giaNhap WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", thuoc.maSP);
                    cmd.Parameters.AddWithValue("@tenSP", thuoc.tenSP);
                    cmd.Parameters.AddWithValue("@nhaCungCap", thuoc.nhaCungCap);
                    cmd.Parameters.AddWithValue("@soLuong", thuoc.soLuong);
                    cmd.Parameters.AddWithValue("@giaBan", thuoc.giaBan);
                    cmd.Parameters.AddWithValue("@giaNhap", thuoc.giaNhap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteThuocs(Thuoc thuoc)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM Thuoc WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", thuoc.maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<Thuoc> SearchThuocsWithMaSP(string searchValue)
        {
            List<Thuoc> Thuocs = new List<Thuoc>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM Thuoc WHERE maSP LIKE '%' + @searchValue + '%'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Thuocs.Add(new Thuoc
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

            return Thuocs;
        }

    }
}
