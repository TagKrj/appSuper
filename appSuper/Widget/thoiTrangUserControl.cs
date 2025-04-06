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
    public partial class thoiTrangUC : UserControl
    {
        public thoiTrangUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapThoiTrang.Items.Clear();
            LoadingCboNCC();
        }
        private void LoadingData()
        {
            dgvThoiTrang.Rows.Clear();
            List<ThoiTrang> thoiTrangs = ThoiTrangController.getAllThoiTrangs();
            foreach (ThoiTrang thoitrang in thoiTrangs)
            {
                dgvThoiTrang.Rows.Add(thoitrang.maSP, thoitrang.tenSP, thoitrang.nhaCungCap, thoitrang.soLuong, thoitrang.giaNhap, thoitrang.giaBan);

            }
        }

        private void btnThemThoiTrang_Click(object sender, EventArgs e)
        {

            var ThoiTrangController = new ThoiTrangController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPThoiTrang.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongThoiTrang.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapThoiTrang.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanThoiTrang.Text))
            {
                return;
            }
            var thoiTrang = new ThoiTrang
            {
                maSP = txtMaSPThoiTrang.Text,
                tenSP = txtTenSPThoiTrang.Text,
                nhaCungCap = cboNhaCungCapThoiTrang.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThoiTrang.Text),
                giaNhap = decimal.Parse(txtGiaNhapThoiTrang.Text),
                giaBan = decimal.Parse(txtGiaBanThoiTrang.Text)
            };
            ThoiTrangController.AddThoiTrangs(thoiTrang);
            LoadingData();
        }

        private void btnSuaThoiTrang_Click(object sender, EventArgs e)
        {

            var ThoiTrangController = new ThoiTrangController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPThoiTrang.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongThoiTrang.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapThoiTrang.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanThoiTrang.Text))
            {
                return;
            }
            var thoiTrang = new ThoiTrang
            {
                maSP = txtMaSPThoiTrang.Text,
                tenSP = txtTenSPThoiTrang.Text,
                nhaCungCap = cboNhaCungCapThoiTrang.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThoiTrang.Text),
                giaNhap = decimal.Parse(txtGiaNhapThoiTrang.Text),
                giaBan = decimal.Parse(txtGiaBanThoiTrang.Text)
            };
            if (ThoiTrangController.CheckMa(thoiTrang.maSP))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!");
                return;
            }

            ThoiTrangController.UpdateThoiTrangs(thoiTrang);
            LoadingData();
        }

        private void btnXoaThoiTrang_Click(object sender, EventArgs e)
        {
            var thoiTrang = new ThoiTrang
            {
                maSP = txtMaSPThoiTrang.Text,
            };
            ThoiTrangController.DeleteThoiTrangs(thoiTrang);
            LoadingData();
        }

        private void txtSearchThoiTrang_TextChanged(object sender, EventArgs e)
        {
            dgvThoiTrang.Rows.Clear();
            var text = txtSearchThoiTrang.Text;
            List<ThoiTrang> thoiTrangs = ThoiTrangController.SearchThoiTrangs(text);
            foreach (ThoiTrang thoitrang in thoiTrangs)
            {
                dgvThoiTrang.Rows.Add(thoitrang.maSP, thoitrang.tenSP, thoitrang.nhaCungCap, thoitrang.soLuong, thoitrang.giaNhap, thoitrang.giaBan);
            }
        }

        private void dgvThoiTrang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThoiTrang.Rows[e.RowIndex];
                txtMaSPThoiTrang.Text = row.Cells[0].Value.ToString();
                txtTenSPThoiTrang.Text = row.Cells[1].Value.ToString();
                cboNhaCungCapThoiTrang.SelectedItem = row.Cells[2].Value.ToString();
                txtSoLuongThoiTrang.Text = row.Cells[3].Value.ToString();
                txtGiaNhapThoiTrang.Text = row.Cells[4].Value.ToString();
                txtGiaBanThoiTrang.Text = row.Cells[5].Value.ToString();
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapThoiTrang.Items.Add(nhaCungCap.maNhaCC);
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
                    ThoiTrangController.ThemmoiThoiTrang(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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






        private void btnNhapExcelThoiTrang_Click(object sender, EventArgs e)
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
            //txtUploadThoiTrang.Text = opened.FileName;
            filename = opened.FileName;
            ReadExcel();
        }
        LoadingData();

    }

        private void btnXuatExcelThoiTrang_Click(object sender, EventArgs e)
        {


            var exporter = new ThoiTrangController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvThoiTrang);
        }
    }
    
}
