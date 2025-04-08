using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using appSuper.View;
using appSuper.Model;

namespace appSuper.View
{
    public partial class billForm: Form
    {
        private List<XuatHangItem> danhSachXuatHang;
        private decimal tongTien = 0;
        private int tongSoLuong = 0;
        
        public billForm()
        {
            InitializeComponent();
        }

        // Constructor mới nhận danh sách xuất hàng
        public billForm(List<XuatHangItem> xuatHangItems)
        {
            InitializeComponent();
            this.danhSachXuatHang = xuatHangItems;
        }

        private void billForm_Load(object sender, EventArgs e)
        {
            // Cập nhật ngày và giờ hiện tại
            label6.Text = DateTime.Now.ToString("dd/MM/yyyy");
            label12.Text = DateTime.Now.ToString("HH:mm");
            
            // Tạo số hóa đơn ngẫu nhiên
            Random random = new Random();
            label8.Text = random.Next(100000, 999999).ToString();

            // Nếu có danh sách xuất hàng từ constructor
            if (danhSachXuatHang != null && danhSachXuatHang.Count > 0)
            {
                // Xóa dữ liệu mẫu
                dataGridView1.Rows.Clear();
                tongTien = 0;
                tongSoLuong = 0;

                // Thêm các mục xuất hàng vào DataGridView
                foreach (var item in danhSachXuatHang)
                {
                    decimal thanhTien = item.SoLuongXuat * item.GiaBan;
                    dataGridView1.Rows.Add(item.TenSP, item.SoLuongXuat, item.GiaBan.ToString("#,##0"), thanhTien.ToString("#,##0"));
                    
                    tongTien += thanhTien;
                    tongSoLuong += item.SoLuongXuat;
                }

                // Cập nhật tổng số lượng và tổng tiền
                label15.Text = tongSoLuong.ToString();
                label16.Text = tongTien.ToString("#,##0");
            }
            else
            {
                // Trường hợp demo (không có dữ liệu thực)
                dataGridView1.Rows.Add(2);
                dataGridView1.Rows[0].Cells[0].Value = "Bùi Hiền Trang";
                dataGridView1.Rows[0].Cells[1].Value = "01"; 
                dataGridView1.Rows[0].Cells[2].Value = "234.000";
                dataGridView1.Rows[0].Cells[3].Value = "457.777";

                dataGridView1.Rows.Add(2);
                dataGridView1.Rows[1].Cells[0].Value = "Bùi Hiền Trang";
                dataGridView1.Rows[1].Cells[1].Value = "01";
                dataGridView1.Rows[1].Cells[2].Value = "234.000";
                dataGridView1.Rows[1].Cells[3].Value = "457.777";

                dataGridView1.Rows.Add(2);
                dataGridView1.Rows[2].Cells[0].Value = "Bùi Hiền Trang";
                dataGridView1.Rows[2].Cells[1].Value = "01";
                dataGridView1.Rows[2].Cells[2].Value = "234.000";
                dataGridView1.Rows[2].Cells[3].Value = "457.777";

                dataGridView1.Rows.Add(2);
                dataGridView1.Rows[3].Cells[0].Value = "Bùi Hiền Trang";
                dataGridView1.Rows[3].Cells[1].Value = "01";
                dataGridView1.Rows[3].Cells[2].Value = "234.000";
                dataGridView1.Rows[3].Cells[3].Value = "457.777";
            }
        }
    }
}
