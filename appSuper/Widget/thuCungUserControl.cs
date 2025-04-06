using System;
using System.Collections.Generic;
using System.Windows.Forms;
using appSuper.Controller;
using appSuper.Model;
using xls = Microsoft.Office.Interop.Excel;

namespace appSuper
{
    public partial class thuCungUC : UserControl
    {
        public thuCungUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapThuCung.Items.Clear();
            LoadingCboNhaCC();
        }

        private void LoadingData()
        {
            dgvThuCung.Rows.Clear();
            List<ThuCung> ThuCungs = ThuCungController.getAllThuCungs();
            foreach (ThuCung ThuCung in ThuCungs)
            {
                dgvThuCung.Rows.Add(ThuCung.maSP, ThuCung.tenSP, ThuCung.nhaCungCap, ThuCung.soLuong, ThuCung.giaNhap, ThuCung.giaBan);
            }
        }

        private void LoadingCboNhaCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCung in nhaCungCaps)
            {
                cboNhaCungCapThuCung.Items.Add(nhaCung.maNhaCC);
            }
        }
        private void btnThemThuCung_Click(object sender, EventArgs e)
        {
            var ThuCung = new ThuCung
            {
                maSP = txtMaSPThuCung.Text,
                tenSP = txtTenSPThuCung.Text,
                nhaCungCap = cboNhaCungCapThuCung.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThuCung.Text),
                giaNhap = decimal.Parse(txtGiaNhapThuCung.Text),
                giaBan = decimal.Parse(txtGiaBanThuCung.Text)
            };
            ThuCungController.AddThuCungs(ThuCung);
            LoadingData();
        }
        private void btnSuaThuCung_Click(object sender, EventArgs e)
        {
            var ThuCung = new ThuCung
            {
                maSP = txtMaSPThuCung.Text,
                tenSP = txtTenSPThuCung.Text,
                nhaCungCap = cboNhaCungCapThuCung.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThuCung.Text),
                giaNhap = decimal.Parse(txtGiaNhapThuCung.Text),
                giaBan = decimal.Parse(txtGiaBanThuCung.Text)
            };
            ThuCungController.UpdateThuCungs(ThuCung);
            LoadingData();
        }

        private void btnXoaThuCung_Click(object sender, EventArgs e)
        {
            var ThuCung = new ThuCung
            {
                maSP = txtMaSPThuCung.Text,
            };
            ThuCungController.DeleteThuCungs(ThuCung);
            LoadingData();
        }

        private void txtSearchThuCung_TextChanged(object sender, EventArgs e)
        {
            dgvThuCung.Rows.Clear();
            var Text = txtSearchThuCung.Text;
            List<ThuCung> ThuCungs = ThuCungController.SearchThuCungs(Text);
            foreach (ThuCung ThuCung in ThuCungs)
            {
                dgvThuCung.Rows.Add(ThuCung.maSP, ThuCung.tenSP, ThuCung.nhaCungCap, ThuCung.soLuong, ThuCung.giaNhap, ThuCung.giaBan);
            }
        }

        private void dgvThuCung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThuCung.Rows[e.RowIndex];
                txtMaSPThuCung.Text = row.Cells[0].Value.ToString();
                txtTenSPThuCung.Text = row.Cells[1].Value.ToString();
                cboNhaCungCapThuCung.SelectedItem = row.Cells[2].Value.ToString();
                txtSoLuongThuCung.Text = row.Cells[3].Value.ToString();
                txtGiaNhapThuCung.Text = row.Cells[4].Value.ToString();
                txtGiaBanThuCung.Text = row.Cells[5].Value.ToString();
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
                    ThuCungController.ThemmoiThuCung(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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






        private void btnNhapExcelThuCung_Click(object sender, EventArgs e)
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
            //txtUploadThuCung.Text = opened.FileName;
            filename = opened.FileName;
            ReadExcel();
        }
        LoadingData();

    
}

        private void btnXuatExcelThuCung_Click(object sender, EventArgs e)
        {


            var exporter = new ThuCungController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvThuCung);
        }
    }
}
