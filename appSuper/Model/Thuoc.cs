using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMVC.Model;

namespace appSuper.Model
{
    class Thuoc
    {
        public int Id { get; set; }
        public String maSP { get; set; }
        public String tenSP { get; set; }
        public String nhaCungCap { get; set; }
        public int soLuong { get; set; }
        public decimal giaNhap { get; set; }
        public decimal giaBan { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}