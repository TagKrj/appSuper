using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using appSuper.Controller;
using appSuper.Model;
using xls = Microsoft.Office.Interop.Excel;

namespace appSuper
{
    public partial class nhanVienUC : UserControl
    {
        public nhanVienUC()
        {
            InitializeComponent();
            LoadingData();
        }
        public void LoadingData()
        {
            dgvNhanVien.Rows.Clear();
            List<NhanVien> nhanViens = NhanVienController.getAllNhanViens();
            foreach (NhanVien nhanVien in nhanViens)
            {
                dgvNhanVien.Rows.Add(nhanVien.maNV, nhanVien.tenNV, nhanVien.soDT, nhanVien.diaChi, nhanVien.email,nhanVien.namSinh);
            }
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            var nhanViens = new NhanVien
            {
                maNV = txtMaNV.Text,
                tenNV = txtTenNV.Text,
                namSinh = txtNamSinh.Value,
                soDT = txtSoDT.Text,
                email = txtEmail.Text,
                diaChi = txtDiaChi.Text
            };
            NhanVienController.AddNhanViens(nhanViens);
            LoadingData();
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            var nhanViens = new NhanVien
            {
                maNV = txtMaNV.Text,
                tenNV = txtTenNV.Text,
                namSinh = txtNamSinh.Value,
                soDT = txtSoDT.Text,
                email = txtEmail.Text,
                diaChi = txtDiaChi.Text
            };
            NhanVienController.UpdateNhanViens(nhanViens);
            LoadingData();
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            var nhanViens = new NhanVien
            {
                maNV = txtMaNV.Text,
            };
            NhanVienController.DeleteNhanViens(nhanViens);
            LoadingData();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells[0].Value?.ToString();
                txtTenNV.Text = row.Cells[1].Value?.ToString();
                txtSoDT.Text = row.Cells[2].Value?.ToString();
                txtDiaChi.Text = row.Cells[3].Value?.ToString();
                txtEmail.Text = row.Cells[4].Value?.ToString();
                txtNamSinh.Value = DateTime.Parse(row.Cells[5].Value?.ToString());
           
               
             
            }
        }

        private void txtSearcbNV_TextChanged(object sender, EventArgs e)
        {
            dgvNhanVien.Rows.Clear();
            var text = txtSearcbNV.Text;
            List<NhanVien> nhanViens = NhanVienController.SearchNhanViens(text);
            foreach (NhanVien nhanVien in nhanViens)
            {
                dgvNhanVien.Rows.Add(nhanVien.maNV, nhanVien.tenNV, nhanVien.namSinh, nhanVien.soDT, nhanVien.email, nhanVien.diaChi);
            }
        }
        string filename;

        private void ReadExcel()
        {
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Chưa chọn file Excel!");
                return;
            }

            // Tạo đối tượng Excel
            xls.Application excelApp = new xls.Application();
            xls.Workbook workbook = null;
            xls.Worksheet worksheet = null;

            try
            {
                workbook = excelApp.Workbooks.Open(filename);
                worksheet = workbook.Sheets[1]; // Lấy sheet đầu tiên

                int i = 2; // Bắt đầu từ hàng thứ 2 (bỏ qua tiêu đề)
                while (worksheet.Cells[i, 2]?.Value != null) // Kiểm tra ô tại hàng i, cột 2
                {
                    // Đọc dữ liệu từ các cột trong Excel
                    string maNV = worksheet.Cells[i, 2]?.Text.Trim();       // Cột 2: maNV
                    string tenNV = worksheet.Cells[i, 3]?.Text.Trim();      // Cột 3: tenNV
                    string soDT = worksheet.Cells[i, 4]?.Text.Trim();       // Cột 4: soDT
                    string diaChi = worksheet.Cells[i, 5]?.Text.Trim();     // Cột 5: diaChi
                    string email = worksheet.Cells[i, 6]?.Text.Trim();      // Cột 6: email
                    string namSinh = worksheet.Cells[i, 7]?.Text.Trim();    // Cột 7: namSinh

                    // Kiểm tra và xử lý Năm Sinh
                    DateTime namSinhDate;
                    if (!DateTime.TryParse(namSinh, out namSinhDate))
                    {
                        MessageBox.Show($"Dữ liệu không hợp lệ ở cột 'Năm Sinh', dòng {i}: {namSinh}. Yêu cầu là ngày tháng.");
                        return;
                    }

                    // Thêm vào database
                    NhanVienController.ThemmoiNhanVien(maNV, tenNV, soDT, diaChi, email, namSinhDate);
                    i++;
                }

                MessageBox.Show("Nhập dữ liệu từ Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc Excel: {ex.Message}");
            }
            finally
            {
                workbook?.Close(false); // Đóng workbook
                excelApp.Quit(); // Đóng ứng dụng Excel

                // Giải phóng tài nguyên
                if (worksheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                if (workbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                if (excelApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }

        private void btnNhapExcelNhanVien_Click(object sender, EventArgs e)
        {

            OpenFileDialog opened = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx",
                FilterIndex = 1,
                RestoreDirectory = true,
                Multiselect = false
            };

            if (opened.ShowDialog() == DialogResult.OK)
            {
                //txtUploadNhanVien.Text = opened.FileName;
                filename = opened.FileName;
                ReadExcel();
            }
            LoadingData();

        

        }

        private void btnXuatExcelNhanVien_Click(object sender, EventArgs e)
        {


            var exporter = new NhanVienController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvNhanVien);
        }
    }
}
