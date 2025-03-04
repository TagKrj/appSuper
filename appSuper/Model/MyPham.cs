using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appSuper.Model
{
    public class MyPham
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
