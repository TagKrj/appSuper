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
    class NhanVienController
    {
        public static List<NhanVien> getAllNhanViens()
        {
            List<NhanVien> NhanViens = new List<NhanVien>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM NhanVien";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NhanViens.Add(new NhanVien
                        {
                            maNV = reader["maNV"].ToString(),
                            tenNV = reader["tenNV"].ToString(),
                            soDT = reader["soDT"].ToString(),
                            diaChi = reader["diaChi"].ToString(),
                            email = reader["email"].ToString(),
                            namSinh = (DateTime)reader["namSinh"],
                        });
                    }
                }
            }

            return NhanViens;
        }

        public static void AddNhanViens(NhanVien nhanVien)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO NhanVien(maNV, tenNV, namSinh, diaChi, soDT, email) " +
                             "VALUES (@maNV, @tenNV, @namSinh, @diaChi, @soDT, @email)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNV", nhanVien.maNV);
                    cmd.Parameters.AddWithValue("@tenNV", nhanVien.tenNV);
                    cmd.Parameters.AddWithValue("@namSinh", nhanVien.namSinh);
                    cmd.Parameters.AddWithValue("@soDT", nhanVien.soDT);
                    cmd.Parameters.AddWithValue("@email", nhanVien.email);
                    cmd.Parameters.AddWithValue("@diaChi", nhanVien.diaChi);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateNhanViens(NhanVien nhanVien)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE NhanVien SET tenNV = @tenNV, " +
                             "namSinh = @namSinh, diaChi = @diaChi, soDT = @soDT, email = @email " +
                             "WHERE maNV = @maNV";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNV", nhanVien.maNV);
                    cmd.Parameters.AddWithValue("@tenNV", nhanVien.tenNV);
                    cmd.Parameters.AddWithValue("@namSinh", nhanVien.namSinh);
                    cmd.Parameters.AddWithValue("@soDT", nhanVien.soDT);
                    cmd.Parameters.AddWithValue("@email", nhanVien.email);
                    cmd.Parameters.AddWithValue("@diaChi", nhanVien.diaChi);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteNhanViens(NhanVien nhanVien)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM NhanVien WHERE maNV = @maNV";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNV", nhanVien.maNV);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<NhanVien> SearchNhanViens(string searchValue)
        {
            List<NhanVien> NhanViens = new List<NhanVien>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM NhanVien WHERE maNV LIKE @search OR tenNV LIKE @search OR diaChi LIKE @search OR soDT LIKE @search OR email LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhanViens.Add(new NhanVien
                            {
                                maNV = reader["maNV"].ToString(),
                                tenNV = reader["tenNV"].ToString(),
                                soDT = reader["soDT"].ToString(),
                                diaChi = reader["diaChi"].ToString(),
                                email = reader["email"].ToString(),
                                namSinh = (DateTime)reader["namSinh"],
                            });
                        }
                    }
                }
            }
            return NhanViens;
        }
    }
}
