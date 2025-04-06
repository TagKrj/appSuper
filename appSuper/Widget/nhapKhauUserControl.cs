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
    public partial class nhapKhauUC : UserControl
    {
        public nhapKhauUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapNhapKhau.Items.Clear();
            LoadingCboNCC();
        }
        private void LoadingData()
        {
            dgvNhapKhau.Rows.Clear();
            List<NhapKhau> nhapKhaus = NhapKhauController.getAllNhapKhaus();
            foreach (NhapKhau nhapKhau in nhapKhaus)
            {
                dgvNhapKhau.Rows.Add(nhapKhau.maSP, nhapKhau.tenSP, nhapKhau.nhaCungCap, nhapKhau.soLuong, nhapKhau.giaNhap,
                    nhapKhau.giaBan);

            }
        }

        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapNhapKhau.Items.Add(nhaCungCap.maNhaCC);
            }
        }

        private void ClearTxt()
        {
            txtMaSPNhapKhau.Text = "";
            txtTenSPNhapKhau.Text = "";
            cboNhaCungCapNhapKhau.SelectedIndex = -1;
            txtSoLuongNhapKhau.Text = "";
            txtGiaNhapNhapKhau.Text = "";
            txtGiaBanNhapKhau.Text = "";
        }

        private void btnThemNhapKhau_Click(object sender, EventArgs e)
        {
            var nhapKhau = new NhapKhau
            {
                maSP = txtMaSPNhapKhau.Text,
                tenSP = txtTenSPNhapKhau.Text,
                nhaCungCap = cboNhaCungCapNhapKhau.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongNhapKhau.Text),
                giaNhap = decimal.Parse(txtGiaNhapNhapKhau.Text),
                giaBan = decimal.Parse(txtGiaBanNhapKhau.Text)
            };
            NhapKhauController.AddNhapKhaus(nhapKhau);
            LoadingData();
            ClearTxt();
        }

        private void btnSuaNhapKhau_Click(object sender, EventArgs e)
        {
            var nhapKhau = new NhapKhau
            {
                maSP = txtMaSPNhapKhau.Text,
                tenSP = txtTenSPNhapKhau.Text,
                nhaCungCap = cboNhaCungCapNhapKhau.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongNhapKhau.Text),
                giaNhap = decimal.Parse(txtGiaNhapNhapKhau.Text),
                giaBan = decimal.Parse(txtGiaBanNhapKhau.Text)
            };
            NhapKhauController.UpdateNhapKhaus(nhapKhau);
            LoadingData();
            ClearTxt();
        }

        private void btnXoaNhapKhau_Click(object sender, EventArgs e)
        {
            var nhapKhau = new NhapKhau
            {
                maSP = txtMaSPNhapKhau.Text,
            };
            NhapKhauController.DeleteNhapKhaus(nhapKhau);
            LoadingData();
            ClearTxt();
        }

        private void txtSearchNhapKhau_TextChanged(object sender, EventArgs e)
        {
            dgvNhapKhau.Rows.Clear();
            var text = txtSearchNhapKhau.Text;
            List<NhapKhau> nhapKhaus = NhapKhauController.SearchNhapKhaus(text);
            foreach (NhapKhau nhapKhau in nhapKhaus)
            {
                dgvNhapKhau.Rows.Add(nhapKhau.maSP, nhapKhau.tenSP, nhapKhau.nhaCungCap, nhapKhau.soLuong, nhapKhau.giaNhap, nhapKhau.giaBan);
            }
        }

        private void dgvNhapKhau_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhapKhau.Rows[e.RowIndex];
                txtMaSPNhapKhau.Text = row.Cells[0].Value.ToString();
                txtTenSPNhapKhau.Text = row.Cells[1].Value.ToString();
                cboNhaCungCapNhapKhau.SelectedItem = row.Cells[2].Value.ToString();
                txtSoLuongNhapKhau.Text = row.Cells[3].Value.ToString();
                txtGiaNhapNhapKhau.Text = row.Cells[4].Value.ToString();
                txtGiaBanNhapKhau.Text = row.Cells[5].Value.ToString();
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
                    NhapKhauController.ThemmoiNhapKhau(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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






        private void btnNhapExcelNhapKhau_Click(object sender, EventArgs e)
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
            //txtUploadNhapKhau.Text = opened.FileName;
            filename = opened.FileName;
            ReadExcel();
        }
        LoadingData();

    }

        private void btnXuatExcelNhapKhau_Click(object sender, EventArgs e)
        {


            var exporter = new NhapKhauController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvNhapKhau);
        }
    }
    
}
