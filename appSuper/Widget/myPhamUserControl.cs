using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using appSuper.Controller;
using appSuper.Model;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using xls= Microsoft.Office.Interop.Excel;

namespace appSuper
{
    public partial class myPhamUC : UserControl
    {
        public myPhamUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapMyPham.Items.Clear();
            LoadingCboNCC();


        }
        public void LoadingData()
        {
            List<MyPham> MyPhams = MyPhamController.getAllMyPhams();
            dgvMyPham.Rows.Clear();
            foreach (MyPham MyPham in MyPhams)
            {
                dgvMyPham.Rows.Add(MyPham.maSP, MyPham.tenSP, MyPham.nhaCungCap, MyPham.soLuong, MyPham.giaNhap, MyPham.giaBan);
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapMyPham.Items.Add(nhaCungCap.maNhaCC);
            }
        }
        private void ClearTxt()
        {
            txtMaSPMyPham.Text = "";
            txtTenSPMyPham.Text = "";
            cboNhaCungCapMyPham.SelectedIndex = -1;
            txtSoLuongMyPham.Text = "";
            txtGiaNhapMyPham.Text = "";
            txtGiaBanMyPham.Text = "";
        }

        private void btnThemMyPham_Click(object sender, EventArgs e)
        {
            var MyPham = new MyPham
            {
                maSP = txtMaSPMyPham.Text,
                tenSP = txtTenSPMyPham.Text,
                nhaCungCap = cboNhaCungCapMyPham.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongMyPham.Text),
                giaNhap = decimal.Parse(txtGiaNhapMyPham.Text),
                giaBan = decimal.Parse(txtGiaBanMyPham.Text),
            };
            MyPhamController.AddMyPhams(MyPham);
            LoadingData();
            ClearTxt();
        }

        private void btnSuaMyPham_Click(object sender, EventArgs e)
        {
            var MyPham = new MyPham
            {
                maSP = txtMaSPMyPham.Text,
                tenSP = txtTenSPMyPham.Text,
                nhaCungCap = cboNhaCungCapMyPham.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongMyPham.Text),
                giaNhap = decimal.Parse(txtGiaNhapMyPham.Text),
                giaBan = decimal.Parse(txtGiaBanMyPham.Text),
            };
            MyPhamController.UpdateMyPhams(MyPham);
            LoadingData();
            ClearTxt();
        }

        private void btnXoaMyPham_Click(object sender, EventArgs e)
        {
            var MyPham = new MyPham
            {
                maSP = txtMaSPMyPham.Text,
            };
            MyPhamController.DeleteMyPhams(MyPham);
            LoadingData();
            ClearTxt();
        }

        private void txtSearchMyPham_TextChanged(object sender, EventArgs e)
        {
            var text = txtSearchMyPham.Text;
            List<MyPham> MyPhams = MyPhamController.SearchMyPhams(text);
            dgvMyPham.Rows.Clear();
            foreach (MyPham MyPham in MyPhams)
            {
                dgvMyPham.Rows.Add(MyPham.maSP, MyPham.tenSP, MyPham.nhaCungCap, MyPham.soLuong, MyPham.giaNhap, MyPham.giaBan);
            }
        }

        private void dgvMyPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMyPham.Rows[e.RowIndex];
                txtMaSPMyPham.Text = row.Cells[0].Value?.ToString();
                txtTenSPMyPham.Text = row.Cells[1].Value?.ToString();
                cboNhaCungCapMyPham.SelectedItem = row.Cells[2].Value?.ToString();
                txtSoLuongMyPham.Text = row.Cells[3].Value?.ToString();
                txtGiaNhapMyPham.Text = row.Cells[4].Value?.ToString();
                txtGiaBanMyPham.Text = row.Cells[5].Value?.ToString();
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
                    MyPhamController.ThemmoiMyPham(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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

        private void btnNhapExcelMyPham_Click(object sender, EventArgs e)
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
                //txtUploadMyPham.Text = opened.FileName;
                filename = opened.FileName;
                ReadExcel();
            }
            LoadingData();

        }

        private void btnXuatExcelMyPham_Click(object sender, EventArgs e)
        {

      
            var exporter = new MyPhamController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvMyPham); 
        
    }
}

}

