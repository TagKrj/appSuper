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
    public partial class giaoDucUC : UserControl
    {
        public giaoDucUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapGiaoDuc.Items.Clear();
            LoadingCboNCC();
        }

        private void LoadingData()
        {
            dgvGiaoDuc.Rows.Clear();
            List<GiaoDuc> giaoDucs = GiaoDucController.getAllGiaoDucs();
            foreach (GiaoDuc giaoDuc in giaoDucs)
            {
                dgvGiaoDuc.Rows.Add(giaoDuc.maSP, giaoDuc.tenSP, giaoDuc.nhaCungCap, giaoDuc.soLuong, giaoDuc.giaNhap,
                    giaoDuc.giaBan);

            }
        }

        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapGiaoDuc.Items.Add(nhaCungCap.maNhaCC);
            }
        }

        private void ClearTxt()
        {
            txtMaSPGiaoDuc.Text = "";
            txtTenSPGiaoDuc.Text = "";
            cboNhaCungCapGiaoDuc.SelectedIndex = -1;
            txtSoLuongGiaoDuc.Text = "";
            txtGiaNhapGiaoDuc.Text = "";
            txtGiaBanGiaoDuc.Text = "";
        }

        private void btnThemGiaoDuc_Click(object sender, EventArgs e)
        {

            var GiaoDucController = new GiaoDucController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPGiaoDuc.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongGiaoDuc.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapGiaoDuc.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanGiaoDuc.Text))
            {
                return;
            }
            var giaoDuc = new GiaoDuc
            {
                maSP = txtMaSPGiaoDuc.Text,
                tenSP = txtTenSPGiaoDuc.Text,
                nhaCungCap = cboNhaCungCapGiaoDuc.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongGiaoDuc.Text),
                giaNhap = decimal.Parse(txtGiaNhapGiaoDuc.Text),
                giaBan = decimal.Parse(txtGiaBanGiaoDuc.Text)
            };
            GiaoDucController.AddGiaoDucs(giaoDuc);
            LoadingData();
            ClearTxt();
        }

        private void btnSuaGiaoDuc_Click(object sender, EventArgs e)
        {
            var GiaoDucController = new GiaoDucController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPGiaoDuc.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongGiaoDuc.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapGiaoDuc.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanGiaoDuc.Text))
            {
                return;
            }
            var giaoDuc = new GiaoDuc
            {
                maSP = txtMaSPGiaoDuc.Text,
                tenSP = txtTenSPGiaoDuc.Text,
                nhaCungCap = cboNhaCungCapGiaoDuc.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongGiaoDuc.Text),
                giaNhap = decimal.Parse(txtGiaNhapGiaoDuc.Text),
                giaBan = decimal.Parse(txtGiaBanGiaoDuc.Text)
            };
            if (GiaoDucController.CheckMa(giaoDuc.maSP))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!");
                return;
            }

            GiaoDucController.UpdateGiaoDucs(giaoDuc);
            LoadingData();
            ClearTxt();
        }

        private void btnXoaGiaoDuc_Click(object sender, EventArgs e)
        {
            var giaoDuc = new GiaoDuc
            {
                maSP = txtMaSPGiaoDuc.Text,
            };
            GiaoDucController.DeleteGiaoDucs(giaoDuc);
            LoadingData();
            ClearTxt();
        }

        private void txtSearchGiaoDuc_TextChanged(object sender, EventArgs e)
        {
            dgvGiaoDuc.Rows.Clear();
            var text = txtSearchGiaoDuc.Text;
            List<GiaoDuc> giaoDucs = GiaoDucController.SearchGiaoDucs(text);
            foreach (GiaoDuc giaoDuc in giaoDucs)
            {
                dgvGiaoDuc.Rows.Add(giaoDuc.maSP, giaoDuc.tenSP, giaoDuc.nhaCungCap, giaoDuc.soLuong, giaoDuc.giaNhap, giaoDuc.giaBan);
            }
        }

        private void dgvGiaoDuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvGiaoDuc.Rows[e.RowIndex];
                txtMaSPGiaoDuc.Text = row.Cells[0].Value.ToString();
                txtTenSPGiaoDuc.Text = row.Cells[1].Value.ToString();
                cboNhaCungCapGiaoDuc.SelectedItem = row.Cells[2].Value.ToString();
                txtSoLuongGiaoDuc.Text = row.Cells[3].Value.ToString();
                txtGiaNhapGiaoDuc.Text = row.Cells[4].Value.ToString();
                txtGiaBanGiaoDuc.Text = row.Cells[5].Value.ToString();
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
                    GiaoDucController.ThemmoiGiaoDuc(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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






        private void btnNhapExcelGiaoDuc_Click(object sender, EventArgs e)
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
            //txtUploadGiaoDuc.Text = opened.FileName;
            filename = opened.FileName;
            ReadExcel();
        }
        LoadingData();

    }

        private void btnXuatExcelGiaoDuc_Click(object sender, EventArgs e)
        {


            var exporter = new GiaoDucController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvGiaoDuc);
        }
    }
    
}
