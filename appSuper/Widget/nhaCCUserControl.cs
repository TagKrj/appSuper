using appSuper.Controller;
using appSuper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xls = Microsoft.Office.Interop.Excel;

namespace appSuper
{
    public partial class nhaCCUC : UserControl
    {
        public nhaCCUC()
        {
            InitializeComponent();
            LoadingData();
        }
        private void LoadingData()
        {
            dgvNhaCC.Rows.Clear();
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (NhaCungCap nhaCungCap in nhaCungCaps)
            {
                dgvNhaCC.Rows.Add(nhaCungCap.maNhaCC, nhaCungCap.tenNhaCC,nhaCungCap.diaChi);

            }
        }

        private void btnThemNhaCC_Click(object sender, EventArgs e)
        {

            var NhaCungCapController = new NhaCungCapController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaNhaCC.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
         
            var nhaCungCap = new NhaCungCap
            {
                maNhaCC = txtMaNhaCC.Text,
                tenNhaCC = txtTenNhaCC.Text,
                diaChi = txtDiaChiNhaCC.Text
            };
            if (NhaCungCapController.CheckMa(nhaCungCap.maNhaCC))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!");
                return;
            }

            NhaCungCapController.AddNhaCungCaps(nhaCungCap);
            LoadingData();
        }

        private void btnSuaNhaCC_Click(object sender, EventArgs e)
        {
            var NhaCungCapController = new NhaCungCapController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaNhaCC.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
          
            var nhaCungCap = new NhaCungCap
            {
                maNhaCC = txtMaNhaCC.Text,
                tenNhaCC = txtTenNhaCC.Text,
                diaChi = txtDiaChiNhaCC.Text
            };
            NhaCungCapController.UpdateNhaCungCaps(nhaCungCap);
            LoadingData();
        }

        private void dgvNhaCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhaCC.Rows[e.RowIndex];
                txtMaNhaCC.Text = row.Cells[0].Value?.ToString();
                txtTenNhaCC.Text = row.Cells[1].Value?.ToString();
                txtDiaChiNhaCC.Text = row.Cells[2].Value?.ToString();
            }
        }

        private void btnXoaNhaCC_Click(object sender, EventArgs e)
        {
            var nhaCungCap = new NhaCungCap
            {
                maNhaCC = txtMaNhaCC.Text
            };
            NhaCungCapController.DeleteNhaCungCaps(nhaCungCap);
            LoadingData();
        }

        private void txtSearchNhaCC_TextChanged(object sender, EventArgs e)
        {
            dgvNhaCC.Rows.Clear();
            var searchText = txtSearchNhaCC.Text;
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.SearchNhaCungCaps(searchText);
            foreach (NhaCungCap nhaCungCap in nhaCungCaps)
            {
                dgvNhaCC.Rows.Add(nhaCungCap.maNhaCC, nhaCungCap.tenNhaCC, nhaCungCap.diaChi);

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
                    string maNhaCC = worksheet.Cells[i, 2]?.Text.Trim();        // Cột 2: maNhaCC
                    string tenNhaCC = worksheet.Cells[i, 3]?.Text.Trim();       // Cột 3: tenNhaCC
                    string diaChi = worksheet.Cells[i, 4]?.Text.Trim();         // Cột 4: diaChi

                    // Kiểm tra dữ liệu
                    if (string.IsNullOrEmpty(maNhaCC) || string.IsNullOrEmpty(tenNhaCC) || string.IsNullOrEmpty(diaChi))
                    {
                        MessageBox.Show($"Dữ liệu không hợp lệ tại dòng {i}. Các cột maNhaCC, tenNhaCC, và diaChi không được để trống.");
                        return;
                    }

                    // Gửi dữ liệu đến Controller để thêm vào cơ sở dữ liệu
                    NhaCungCapController.ThemmoiNhaCungCap(maNhaCC, tenNhaCC, diaChi);
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
                excelApp.Quit();        // Đóng ứng dụng Excel

                // Giải phóng tài nguyên
                if (worksheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                if (workbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                if (excelApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }

        private void btnNhapExcelNhaCC_Click(object sender, EventArgs e)
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
                //txtUploadNhaCungCap.Text = opened.FileName;
                filename = opened.FileName;
                ReadExcel();
            }
            LoadingData();

        }

        private void btnXuatNhaCC_Click(object sender, EventArgs e)
        {
            var exporter = new NhaCungCapController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvNhaCC);
        }
    }
}
