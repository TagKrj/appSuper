using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using appSuper.Model;
using appSuper.Controller;
using Microsoft.VisualBasic;
namespace appSuper.View
{
    public partial class xuatHangForm: Form
    {
        private List<DienTu> danhSachDienTu = new List<DienTu>();
        private List<XuatHangItem> danhSachXuatHang = new List<XuatHangItem>();

        public xuatHangForm()
        {
            InitializeComponent();
        }

        // Phương thức nhận dữ liệu sản phẩm điện tử
        public void NhanSanPhamDienTu(List<DienTu> sanPhamDienTu)
        {
            danhSachDienTu = sanPhamDienTu;
            HienThiSanPhamTrongKho();
        }

        // Hiển thị sản phẩm trong kho vào DataGridView bên trái
        private void HienThiSanPhamTrongKho()
        {
            dgvThuoc.Rows.Clear();
            foreach (var item in danhSachDienTu)
            {
                dgvThuoc.Rows.Add(item.tenSP, item.soLuong);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Thêm sản phẩm vào danh sách xuất hàng (bên phải)
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            if (dgvThuoc.SelectedRows.Count == 0 && dgvThuoc.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int row = dgvThuoc.CurrentRow.Index;
            string tenSP = dgvThuoc.Rows[row].Cells[0].Value.ToString();
            int soLuongTrongKho = Convert.ToInt32(dgvThuoc.Rows[row].Cells[1].Value);

            // Tạo form nhập số lượng xuất thay vì dùng InputBox
            Form formNhapSoLuong = new Form();
            formNhapSoLuong.Text = "Nhập số lượng";
            formNhapSoLuong.FormBorderStyle = FormBorderStyle.FixedDialog;
            formNhapSoLuong.StartPosition = FormStartPosition.CenterParent;
            formNhapSoLuong.MinimizeBox = false;
            formNhapSoLuong.MaximizeBox = false;
            formNhapSoLuong.Size = new Size(300, 150);

            Label lblThongBao = new Label();
            lblThongBao.Text = "Nhập số lượng sản phẩm cần xuất:";
            lblThongBao.AutoSize = true;
            lblThongBao.Location = new Point(10, 20);
            
            NumericUpDown numSoLuong = new NumericUpDown();
            numSoLuong.Minimum = 1;
            numSoLuong.Maximum = soLuongTrongKho;
            numSoLuong.Value = 1;
            numSoLuong.Location = new Point(10, 50);
            numSoLuong.Width = 100;
            
            Button btnOK = new Button();
            btnOK.Text = "OK";
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(120, 80);
            
            Button btnCancel = new Button();
            btnCancel.Text = "Hủy";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(200, 80);
            
            formNhapSoLuong.Controls.Add(lblThongBao);
            formNhapSoLuong.Controls.Add(numSoLuong);
            formNhapSoLuong.Controls.Add(btnOK);
            formNhapSoLuong.Controls.Add(btnCancel);
            formNhapSoLuong.AcceptButton = btnOK;
            formNhapSoLuong.CancelButton = btnCancel;
            
            // Hiển thị form và xử lý kết quả
            if (formNhapSoLuong.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            
            int soLuongXuat = (int)numSoLuong.Value;

            if (soLuongXuat <= 0)
            {
                MessageBox.Show("Số lượng xuất phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (soLuongXuat > soLuongTrongKho)
            {
                MessageBox.Show("Số lượng xuất không được lớn hơn số lượng trong kho!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lấy thông tin sản phẩm từ danh sách điện tử
            var sanPham = danhSachDienTu.FirstOrDefault(x => x.tenSP == tenSP);
            if (sanPham != null)
            {
                // Kiểm tra xem đã có sản phẩm này trong danh sách xuất hàng chưa
                var existingItem = danhSachXuatHang.FirstOrDefault(x => x.TenSP == tenSP);
                if (existingItem != null)
                {
                    // Đã có sản phẩm này, cộng dồn số lượng
                    existingItem.SoLuongXuat += soLuongXuat;
                }
                else
                {
                    // Chưa có, thêm mới vào danh sách xuất hàng
                    danhSachXuatHang.Add(new XuatHangItem
                    {
                        MaSP = sanPham.maSP,
                        TenSP = sanPham.tenSP,
                        SoLuongXuat = soLuongXuat,
                        GiaBan = sanPham.giaBan,
                        LoaiSanPham = "DienTu"
                    });
                }

                // Cập nhật danh sách xuất hàng bên phải
                CapNhatDataGridViewXuatHang();
            }
        }

        // Xóa sản phẩm khỏi danh sách xuất hàng
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0 && guna2DataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int row = guna2DataGridView1.CurrentRow.Index;
            string tenSP = guna2DataGridView1.Rows[row].Cells[0].Value.ToString();

            // Xóa khỏi danh sách xuất hàng
            var itemToRemove = danhSachXuatHang.FirstOrDefault(x => x.TenSP == tenSP);
            if (itemToRemove != null)
            {
                danhSachXuatHang.Remove(itemToRemove);
                // Cập nhật lại danh sách xuất hàng
                CapNhatDataGridViewXuatHang();
            }
        }

        // Cập nhật DataGridView bên phải hiển thị danh sách xuất hàng
        private void CapNhatDataGridViewXuatHang()
        {
            guna2DataGridView1.Rows.Clear();
            foreach (var item in danhSachXuatHang)
            {
                guna2DataGridView1.Rows.Add(item.TenSP, item.SoLuongXuat);
            }
        }

        // Xử lý khi nhấn nút xuất hàng
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (danhSachXuatHang.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Giảm số lượng trong database
            foreach (var item in danhSachXuatHang)
            {
                if (item.LoaiSanPham == "DienTu")
                {
                    DienTuController.GiamSoLuong(item.MaSP, item.SoLuongXuat);
                }
            }

            // Hiển thị hóa đơn
            billForm bill = new billForm(danhSachXuatHang);
            bill.ShowDialog();

            // Đóng form hiện tại
            this.Close();
        }

        // Tìm kiếm sản phẩm
        private void txtSearchThuoc_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchThuoc.Text.ToLower();
            
            if (string.IsNullOrWhiteSpace(searchText))
            {
                HienThiSanPhamTrongKho();
                return;
            }
            
            dgvThuoc.Rows.Clear();
            foreach (var item in danhSachDienTu)
            {
                if (item.tenSP.ToLower().Contains(searchText) || 
                    item.maSP.ToLower().Contains(searchText))
                {
                    dgvThuoc.Rows.Add(item.tenSP, item.soLuong);
                }
            }
        }
    }

    // Lớp đại diện cho một mục xuất hàng
    public class XuatHangItem
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuongXuat { get; set; }
        public decimal GiaBan { get; set; }
        public string LoaiSanPham { get; set; } // Loại sản phẩm: DienTu, GiaDung, ...
        
        public decimal ThanhTien
        {
            get { return SoLuongXuat * GiaBan; }
        }
    }
}
