using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appSuper.Model
{
    class NhanVien
    {
        public int Id { get; set; }
        public String maNV { get; set; }
        public String tenNV { get; set; }
        public String soDT { get; set; }
        public String diaChi { get; set; }
        public String email { get; set; }
        public DateTime namSinh { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
