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
    public partial class thucPhamUC : UserControl
    {
        public thucPhamUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapThucPham.Items.Clear();
            LoadingCboNCC();


        }
        public void LoadingData()
        {
            List<ThucPham> ThucPhams = ThucPhamController.getAllThucPhams();
            dgvThucPham.Rows.Clear();
            foreach (ThucPham ThucPham in ThucPhams)
            {
                dgvThucPham.Rows.Add(ThucPham.maSP, ThucPham.tenSP, ThucPham.nhaCungCap, ThucPham.soLuong, ThucPham.giaNhap, ThucPham.giaBan);
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapThucPham.Items.Add(nhaCungCap.maNhaCC);
            }
        }
        private void ClearTxt()
        {
            txtMaSPThucPham.Text = "";
            txtTenSPThucPham.Text = "";
            cboNhaCungCapThucPham.SelectedIndex = -1;
            txtSoLuongThucPham.Text = "";
            txtGiaNhapThucPham.Text = "";
            txtGiaBanThucPham.Text = "";
        }

        private void btnThemThucPham_Click(object sender, EventArgs e)
        {
            var ThucPham = new ThucPham
            {
                maSP = txtMaSPThucPham.Text,
                tenSP = txtTenSPThucPham.Text,
                nhaCungCap = cboNhaCungCapThucPham.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThucPham.Text),
                giaNhap = decimal.Parse(txtGiaNhapThucPham.Text),
                giaBan = decimal.Parse(txtGiaBanThucPham.Text),
            };
            ThucPhamController.AddThucPhams(ThucPham);
            LoadingData();
            ClearTxt();
        }

        private void btnSuaThucPham_Click(object sender, EventArgs e)
        {
            var ThucPham = new ThucPham
            {
                maSP = txtMaSPThucPham.Text,
                tenSP = txtTenSPThucPham.Text,
                nhaCungCap = cboNhaCungCapThucPham.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThucPham.Text),
                giaNhap = decimal.Parse(txtGiaNhapThucPham.Text),
                giaBan = decimal.Parse(txtGiaBanThucPham.Text),
            };
            ThucPhamController.UpdateThucPhams(ThucPham);
            LoadingData();
            ClearTxt();
        }

        private void btnXoaThucPham_Click(object sender, EventArgs e)
        {
            var ThucPham = new ThucPham
            {
                maSP = txtMaSPThucPham.Text,
            };
            ThucPhamController.DeleteThucPhams(ThucPham);
            LoadingData();
            ClearTxt();
        }

        private void txtSearchThucPham_TextChanged(object sender, EventArgs e)
        {
            var text = txtSearchThucPham.Text;
            List<ThucPham> ThucPhams = ThucPhamController.SearchThucPhams(text);
            dgvThucPham.Rows.Clear();
            foreach (ThucPham ThucPham in ThucPhams)
            {
                dgvThucPham.Rows.Add(ThucPham.maSP, ThucPham.tenSP, ThucPham.nhaCungCap, ThucPham.soLuong, ThucPham.giaNhap, ThucPham.giaBan);
            }
        }

        private void dgvThucPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThucPham.Rows[e.RowIndex];
                txtMaSPThucPham.Text = row.Cells[0].Value?.ToString();
                txtTenSPThucPham.Text = row.Cells[1].Value?.ToString();
                cboNhaCungCapThucPham.SelectedItem = row.Cells[2].Value?.ToString();
                txtSoLuongThucPham.Text = row.Cells[3].Value?.ToString();
                txtGiaNhapThucPham.Text = row.Cells[4].Value?.ToString();
                txtGiaBanThucPham.Text = row.Cells[5].Value?.ToString();
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
                worksheet = workbook.Sheets[1];

                int i = 2;
                while (worksheet.Cells[i, 2]?.Value != null)
                {
                    string maSP = worksheet.Cells[i, 2]?.Text.Trim();
                    string tenSP = worksheet.Cells[i, 3]?.Text.Trim();
                    string nhaCungCap = worksheet.Cells[i, 4]?.Text.Trim();
                    string soLuong = worksheet.Cells[i, 5]?.Text.Trim();
                    string giaNhap = worksheet.Cells[i, 6]?.Text.Trim();
                    string giaBan = worksheet.Cells[i, 7]?.Text.Trim();
                    int soLuongInt;
                    decimal giaNhapDecimal, giaBanDecimal;
                    if (!int.TryParse(soLuong, out soLuongInt))
                    {
                        MessageBox.Show($"Dữ liệu không hợp lệ ở cột 'Số Lượng', dòng {i}: {soLuong}. Yêu cầu là số nguyên.");
                        return;
                    }

                    // Giá nhập phải là kiểu số thực
                    if (!decimal.TryParse(giaNhap, out giaNhapDecimal))
                    {
                        MessageBox.Show($"Dữ liệu không hợp lệ ở cột 'Giá Nhập', dòng {i}: {giaNhap}. Yêu cầu là số thực.");
                        return;
                    }

                    // Giá bán phải là kiểu số thực
                    if (!decimal.TryParse(giaBan, out giaBanDecimal))
                    {
                        MessageBox.Show($"Dữ liệu không hợp lệ ở cột 'Giá Bán', dòng {i}: {giaBan}. Yêu cầu là số thực.");
                        return;
                    }

                    // Thêm vào database
                    ThucPhamController.ThemmoiThucPham(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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






        private void btnNhapExcelNhapThucPham_Click(object sender, EventArgs e)
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
            //txtUploadThucPham.Text = opened.FileName;
            filename = opened.FileName;
            ReadExcel();
        }
        LoadingData();

    
}

        private void btnNhapExcelXuatThucPham_Click(object sender, EventArgs e)
        {
            var exporter = new ThucPhamController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvThucPham);
        }
    }
}
