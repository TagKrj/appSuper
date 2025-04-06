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
    public partial class giaDungUC : UserControl
    {
        public giaDungUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapGiaDung.Items.Clear();
            LoadingCboNCC();
        }

        public void LoadingData()
        {
            List<GiaDung> GiaDungs = GiaDungController.getAllGiaDungs();
            dgvGiaDung.Rows.Clear();
            foreach (var giaDung in GiaDungs)
            {
                dgvGiaDung.Rows.Add(giaDung.maSP, giaDung.tenSP, giaDung.nhaCungCap, giaDung.soLuong, giaDung.giaNhap, giaDung.giaBan);
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapGiaDung.Items.Add(nhaCungCap.maNhaCC);
            }
        }

        private void btnThemGiaDung_Click(object sender, EventArgs e)
        {
            var GiaDungController = new GiaDungController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPGiaDung.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongGiaDung.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapGiaDung.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanGiaDung.Text))
            {
                return;
            }
            var GiaDung = new GiaDung
            {
                maSP = txtMaSPGiaDung.Text,
                tenSP = txtTenSPGiaDung.Text,
                nhaCungCap = cboNhaCungCapGiaDung.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongGiaDung.Text),
                giaNhap = decimal.Parse(txtGiaNhapGiaDung.Text),
                giaBan = decimal.Parse(txtGiaBanGiaDung.Text),
            };
            GiaDungController.AddGiaDungs(GiaDung);
            LoadingData();
        }

        private void btnSuaGiaDung_Click(object sender, EventArgs e)
        {
            var GiaDungController = new GiaDungController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPGiaDung.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongGiaDung.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapGiaDung.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanGiaDung.Text))
            {
                return;
            }
            var GiaDung = new GiaDung
            {
                maSP = txtMaSPGiaDung.Text,
                tenSP = txtTenSPGiaDung.Text,
                nhaCungCap = cboNhaCungCapGiaDung.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongGiaDung.Text),
                giaNhap = decimal.Parse(txtGiaNhapGiaDung.Text),
                giaBan = decimal.Parse(txtGiaBanGiaDung.Text),
            };
            if (GiaDungController.CheckMa(GiaDung.maSP))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!");
                return;
            }

            GiaDungController.UpdateGiaDungs(GiaDung);
            LoadingData();
        }

        private void btnXoaGiaDung_Click(object sender, EventArgs e)
        {
            var GiaDung = new GiaDung
            {
                maSP = txtMaSPGiaDung.Text
            };
            GiaDungController.DeleteGiaDungs(GiaDung);
            LoadingData();
        }

        private void txtSearchGiaDung_TextChanged(object sender, EventArgs e)
        {
            var text = txtSearchGiaDung.Text;
            List<GiaDung> GiaDungs = GiaDungController.SearchGiaDungs(text);
            dgvGiaDung.Rows.Clear();
            foreach (var giaDung in GiaDungs)
            {
                dgvGiaDung.Rows.Add(giaDung.maSP, giaDung.tenSP, giaDung.nhaCungCap, giaDung.soLuong, giaDung.giaNhap, giaDung.giaBan);
            }
        }

        private void dgvGiaDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvGiaDung.Rows[e.RowIndex];
                txtMaSPGiaDung.Text = row.Cells[0].Value?.ToString();
                txtTenSPGiaDung.Text = row.Cells[1].Value?.ToString();
                cboNhaCungCapGiaDung.SelectedItem = row.Cells[2].Value?.ToString();
                txtSoLuongGiaDung.Text = row.Cells[3].Value?.ToString();
                txtGiaNhapGiaDung.Text = row.Cells[4].Value?.ToString();
                txtGiaBanGiaDung.Text = row.Cells[5].Value?.ToString();
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
                    GiaDungController.ThemmoiGiaDung(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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




        private void btnNhapExcelGiaDung_Click(object sender, EventArgs e)
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
            //txtUploadGiaDung.Text = opened.FileName;
            filename = opened.FileName;
            ReadExcel();
        }
        LoadingData();

    }

        private void btnXuatExcelGiaDung_Click(object sender, EventArgs e)
        {
            var exporter = new GiaDungController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvGiaDung);
        }
    }
    
}
