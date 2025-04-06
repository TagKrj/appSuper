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
    public partial class thuocUC : UserControl
    {
        public thuocUC()
        {
            InitializeComponent();
            cboNhaCungCapThuoc.Items.Clear();
            LoadingData();
            LoadingCboNCC();

        }

        private void LoadingData()
        {
            dgvThuoc.Rows.Clear();
            List<Thuoc> thuoc = ThuocController.getAllThuocs();
            foreach (Thuoc t in thuoc)
            {
                dgvThuoc.Rows.Add(t.maSP, t.tenSP, t.nhaCungCap, t.soLuong, t.giaNhap, t.giaBan);

            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapThuoc.Items.Add(nhaCungCap.maNhaCC);
            }
        }

        private void btnThemThuoc_Click(object sender, EventArgs e)
        {

            var ThuocController = new ThuocController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPThuoc.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongThuoc.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapThuoc.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanThuoc.Text))
            {
                return;
            }
            var thuoc = new Thuoc
            {
                maSP = txtMaSPThuoc.Text,
                tenSP = txtTenSPThuoc.Text,
                nhaCungCap = cboNhaCungCapThuoc.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThuoc.Text),
                giaNhap = decimal.Parse(txtGiaNhapThuoc.Text),
                giaBan = decimal.Parse(txtGiaBanThuoc.Text)
            };
            ThuocController.AddThuocs(thuoc);
            LoadingData();
        }

        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThuoc.Rows[e.RowIndex];
                txtMaSPThuoc.Text = row.Cells[0].Value?.ToString();
                txtTenSPThuoc.Text = row.Cells[1].Value?.ToString();
                cboNhaCungCapThuoc.SelectedItem = row.Cells[2].Value?.ToString();
                txtSoLuongThuoc.Text = row.Cells[3].Value?.ToString();
                txtGiaNhapThuoc.Text = row.Cells[4].Value?.ToString();
                txtGiaBanThuoc.Text = row.Cells[5].Value?.ToString();
            }
        }

        private void btnSuaThuoc_Click(object sender, EventArgs e)
        {
            var ThuocController = new ThuocController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPThuoc.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongThuoc.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapThuoc.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanThuoc.Text))
            {
                return;
            }
            var thuoc = new Thuoc
            {
                maSP = txtMaSPThuoc.Text,
                tenSP = txtTenSPThuoc.Text,
                nhaCungCap = cboNhaCungCapThuoc.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThuoc.Text),
                giaNhap = decimal.Parse(txtGiaNhapThuoc.Text),
                giaBan = decimal.Parse(txtGiaBanThuoc.Text)
            };
            if (ThuocController.CheckMa(thuoc.maSP))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!");
                return;
            }

            ThuocController.UpdateThuocs(thuoc);
            LoadingData();
        }

        private void btnXoaThuoc_Click(object sender, EventArgs e)
        {
            var thuoc = new Thuoc
            {
                maSP = txtMaSPThuoc.Text,
                tenSP = txtTenSPThuoc.Text,
                nhaCungCap = cboNhaCungCapThuoc.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThuoc.Text),
                giaNhap = decimal.Parse(txtGiaNhapThuoc.Text),
                giaBan = decimal.Parse(txtGiaBanThuoc.Text)
            };
            ThuocController.DeleteThuocs(thuoc);
            LoadingData();
        }

        private void txtSearchThuoc_TextChanged(object sender, EventArgs e)
        {
            dgvThuoc.Rows.Clear();
            var searchThuoc = txtSearchThuoc.Text;
            List<Thuoc> thuoc = ThuocController.SearchThuocs(searchThuoc);
            foreach (Thuoc t in thuoc)
            {
                dgvThuoc.Rows.Add(t.maSP, t.tenSP, t.nhaCungCap, t.soLuong, t.giaNhap, t.giaBan);

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
                    ThuocController.ThemmoiThuoc(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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




       

        private void btnNhapExcelThuoc_Click(object sender, EventArgs e)
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
            //txtUploadThuoc.Text = opened.FileName;
            filename = opened.FileName;
            ReadExcel();
        }
        LoadingData();

    
}

        private void btnXuatExcelThuoc_Click(object sender, EventArgs e)
        {


            var exporter = new ThuocController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvThuoc);
        }
    }
}
