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
        public Thuoc() { }
        public Thuoc(String maSP, String tenSP, String nhaCungCap, int soLuong, decimal giaNhap, decimal giaBan)
        {
            this.maSP = maSP;
            this.tenSP = tenSP;
            this.nhaCungCap = nhaCungCap;
            this.soLuong = soLuong;
            this.giaNhap = giaNhap;
            this.giaBan = giaBan;
        }
    }
}