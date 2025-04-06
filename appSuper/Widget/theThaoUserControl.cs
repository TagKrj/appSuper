using System;
using System.Collections.Generic;
using System.Windows.Forms;
using appSuper.Controller;
using appSuper.Model;
using xls = Microsoft.Office.Interop.Excel;
namespace appSuper
{
    public partial class theThaoUC : UserControl
    {
        public theThaoUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapTheThao.Items.Clear();
            LoadingCboNCC();
        }
        private void LoadingData()
        {
            dgvTheThao.Rows.Clear();
            List<TheThao> TheThaos = TheThaoController.getAllTheThaos();
            foreach (TheThao TheThao in TheThaos)
            {
                dgvTheThao.Rows.Add(TheThao.maSP, TheThao.tenSP, TheThao.nhaCungCap, TheThao.soLuong, TheThao.giaNhap, TheThao.giaBan);

            }
        }

        private void btnThemTheThao_Click(object sender, System.EventArgs e)
        {
            var TheThao = new TheThao
            {
                maSP = txtMaSPTheThao.Text,
                tenSP = txtTenSPTheThao.Text,
                nhaCungCap = cboNhaCungCapTheThao.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongTheThao.Text),
                giaNhap = decimal.Parse(txtGiaNhapTheThao.Text),
                giaBan = decimal.Parse(txtGiaBanTheThao.Text)
            };
            TheThaoController.AddTheThaos(TheThao);
            LoadingData();
        }

        private void btnSuaTheThao_Click(object sender, System.EventArgs e)
        {
            var TheThao = new TheThao
            {
                maSP = txtMaSPTheThao.Text,
                tenSP = txtTenSPTheThao.Text,
                nhaCungCap = cboNhaCungCapTheThao.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongTheThao.Text),
                giaNhap = decimal.Parse(txtGiaNhapTheThao.Text),
                giaBan = decimal.Parse(txtGiaBanTheThao.Text)
            };
            TheThaoController.UpdateTheThaos(TheThao);
            LoadingData();
        }

        private void btnXoaTheThao_Click(object sender, System.EventArgs e)
        {
            var TheThao = new TheThao
            {
                maSP = txtMaSPTheThao.Text,
            };
            TheThaoController.DeleteTheThaos(TheThao);
            LoadingData();
        }

        private void txtSearchTheThao_TextChanged(object sender, System.EventArgs e)
        {
            dgvTheThao.Rows.Clear();
            var text = txtSearchTheThao.Text;
            List<TheThao> TheThaos = TheThaoController.SearchTheThaos(text);
            foreach (TheThao TheThao in TheThaos)
            {
                dgvTheThao.Rows.Add(TheThao.maSP, TheThao.tenSP, TheThao.nhaCungCap, TheThao.soLuong, TheThao.giaNhap, TheThao.giaBan);
            }
        }

        private void dgvTheThao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTheThao.Rows[e.RowIndex];
                txtMaSPTheThao.Text = row.Cells[0].Value.ToString();
                txtTenSPTheThao.Text = row.Cells[1].Value.ToString();
                cboNhaCungCapTheThao.SelectedItem = row.Cells[2].Value.ToString();
                txtSoLuongTheThao.Text = row.Cells[3].Value.ToString();
                txtGiaNhapTheThao.Text = row.Cells[4].Value.ToString();
                txtGiaBanTheThao.Text = row.Cells[5].Value.ToString();
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapTheThao.Items.Add(nhaCungCap.maNhaCC);
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
                    TheThaoController.ThemmoiTheThao(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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






        private void btnNhapExcelTheThao_Click(object sender, System.EventArgs e)
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
            //txtUploadTheThao.Text = opened.FileName;
            filename = opened.FileName;
            ReadExcel();
        }
        LoadingData();

    }

        private void btnXuatExelTheThao_Click(object sender, EventArgs e)
        {


            var exporter = new TheThaoController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvTheThao);
        }
    }
}
