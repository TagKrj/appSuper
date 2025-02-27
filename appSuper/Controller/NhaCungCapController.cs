using appSuper.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMVC.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace appSuper.Controller
{
    class NhaCungCapController
    {
        public static List<NhaCungCap> getAllNhaCungCaps()
        {
            List<NhaCungCap> NhaCungCaps = new List<NhaCungCap>();

            using (SqlConnection conn = Database.GetConnection()) // Sử dụng DatabaseHelper
            {
                string query = "SELECT * FROM NhaCungCap";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NhaCungCaps.Add(new NhaCungCap
                        {
                            maNhaCC = reader["maNhaCC"].ToString(),
                            tenNhaCC = reader["tenNhaCC"].ToString(),
                            diaChi = reader["diaChi"].ToString(),
                        });
                    }
                }
            }

            return NhaCungCaps;
        }

        public static void AddNhaCungCaps(NhaCungCap nhaCungCap)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO NhaCungCap(maNhaCC, tenNhaCC, diaChi) " +
                               "VALUES (@maNhaCC, @tenNhaCC, @diaChi)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNhaCC", nhaCungCap.maNhaCC);
                    cmd.Parameters.AddWithValue("@tenNhaCC", nhaCungCap.tenNhaCC);
                    cmd.Parameters.AddWithValue("@diaChi", nhaCungCap.diaChi);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateNhaCungCaps(NhaCungCap nhaCungCap)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE NhaCungCap SET tenNhaCC = @tenNhaCC, diaChi = @diaChi Where maNhaCC = @maNhaCC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNhaCC", nhaCungCap.maNhaCC);
                    cmd.Parameters.AddWithValue("@tenNhaCC", nhaCungCap.tenNhaCC);
                    cmd.Parameters.AddWithValue("@diaChi", nhaCungCap.diaChi);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteNhaCungCaps(NhaCungCap nhaCungCap)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM NhaCungCap WHERE maNhaCC = @maNhaCC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNhaCC", nhaCungCap.maNhaCC);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<NhaCungCap> SearchNhaCungCaps(string search)
        {
            List<NhaCungCap> NhaCungCaps = new List<NhaCungCap>();
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM NhaCungCap WHERE maNhaCC LIKE @search OR tenNhaCC LIKE @search OR diaChi LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhaCungCaps.Add(new NhaCungCap
                            {
                                maNhaCC = reader["maNhaCC"].ToString(),
                                tenNhaCC = reader["tenNhaCC"].ToString(),
                                diaChi = reader["diaChi"].ToString(),
                            });
                        }
                    }
                }
            }
            return NhaCungCaps;
        }
    }
}
