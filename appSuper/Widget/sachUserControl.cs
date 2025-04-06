using System;
using System.Collections.Generic;
using System.Windows.Forms;
using appSuper.Controller;
using appSuper.Model;
using xls = Microsoft.Office.Interop.Excel;

namespace appSuper
{
    public partial class sachUC : UserControl
    {
        public sachUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapSach.Items.Clear();
            LoadingCboNCC();
        }

        private void LoadingData()
        {
            dgvSach.Rows.Clear();
            List<Sach> Saches = SachController.getAllSaches();
            foreach (Sach Sach in Saches)
            {
                dgvSach.Rows.Add(Sach.maSP, Sach.tenSP, Sach.nhaCungCap, Sach.soLuong, Sach.giaNhap, Sach.giaBan);
            }
        }

        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapSach.Items.Add(nhaCungCap.maNhaCC);
            }
        }

        private void btnThemSach_Click(object sender, EventArgs e)
        {

            var SachController = new SachController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPSach.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongSach.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapSach.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanSach.Text))
            {
                return;
            }
            var Sach = new Sach
            {
                maSP = txtMaSPSach.Text,
                tenSP = txtTenSPSach.Text,
                nhaCungCap = cboNhaCungCapSach.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongSach.Text),
                giaNhap = decimal.Parse(txtGiaNhapSach.Text),
                giaBan = decimal.Parse(txtGiaBanSach.Text)
            };
            SachController.AddSaches(Sach);
            LoadingData();
        }

        private void btnSuaSach_Click(object sender, EventArgs e)
        {

            var SachController = new SachController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPSach.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongSach.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapSach.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanSach.Text))
            {
                return;
            }
            var Sach = new Sach
            {
                maSP = txtMaSPSach.Text,
                tenSP = txtTenSPSach.Text,
                nhaCungCap = cboNhaCungCapSach.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongSach.Text),
                giaNhap = decimal.Parse(txtGiaNhapSach.Text),
                giaBan = decimal.Parse(txtGiaBanSach.Text)
            };
            if (SachController.CheckMa(Sach.maSP))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!");
                return;
            }

            SachController.UpdateSaches(Sach);
            LoadingData();
        }

        private void btnXoaSach_Click(object sender, EventArgs e)
        {
            var Sach = new Sach
            {
                maSP = txtMaSPSach.Text,
            };
            SachController.DeleteSaches(Sach);
            LoadingData();
        }

        private void txtSearchSach_TextChanged(object sender, EventArgs e)
        {
            dgvSach.Rows.Clear();
            var text = txtSearchSach.Text;
            List<Sach> Saches = SachController.SearchSaches(text);
            foreach (Sach Sach in Saches)
            {
                dgvSach.Rows.Add(Sach.maSP, Sach.tenSP, Sach.nhaCungCap, Sach.soLuong, Sach.giaNhap, Sach.giaBan);
            }
        }

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSach.Rows[e.RowIndex];
                txtMaSPSach.Text = row.Cells[0].Value.ToString();
                txtTenSPSach.Text = row.Cells[1].Value.ToString();
                cboNhaCungCapSach.SelectedItem = row.Cells[2].Value.ToString();
                txtSoLuongSach.Text = row.Cells[3].Value.ToString();
                txtGiaNhapSach.Text = row.Cells[4].Value.ToString();
                txtGiaBanSach.Text = row.Cells[5].Value.ToString();
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
                    SachController.ThemmoiSach(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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





        private void btnNhapExcelSach_Click(object sender, EventArgs e)
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
            //txtUploadSach.Text = opened.FileName;
            filename = opened.FileName;
            ReadExcel();
        }
        LoadingData();

    
}

        private void btnXuatExcelSach_Click(object sender, EventArgs e)
        {


            var exporter = new SachController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvSach);
        }
    }
}

