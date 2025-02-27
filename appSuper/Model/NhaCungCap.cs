using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appSuper.Model
{
    class NhaCungCap
    {
        public NhaCungCap() { }

        public int Id { get; set; }
        public String maNhaCC { get; set; }
        public String tenNhaCC { get; set; }
        public String diaChi { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
