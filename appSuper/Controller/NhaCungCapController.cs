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
    }
}
