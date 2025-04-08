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
using Guna.UI2.AnimatorNS;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using xls = Microsoft.Office.Interop.Excel;

namespace appSuper
{
    public partial class dienTuUC : UserControl
    {
        public dienTuUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapDienTu.Items.Clear();
            LoadingCboNCC();
        }

        public void LoadingData()
        {
            List<DienTu> DienTus = DienTuController.getAllDienTus();
            dgvDienTu.Rows.Clear();
            foreach (DienTu DienTu in DienTus)
            {
                dgvDienTu.Rows.Add(DienTu.maSP, DienTu.tenSP, DienTu.nhaCungCap, DienTu.soLuong, DienTu.giaNhap, DienTu.giaBan);
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapDienTu.Items.Add(nhaCungCap.maNhaCC);
            }
        }
        private void btnThemDienTu_Click(object sender, EventArgs e)

        {
            var dienTuController = new DienTuController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPDienTu.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongDienTu.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapDienTu.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanDienTu.Text))
            {
                return;
            }

            var DienTu = new DienTu
            {
                maSP = txtMaSPDienTu.Text,
                
                tenSP = txtTenSPDienTu.Text, 
                nhaCungCap = cboNhaCungCapDienTu.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongDienTu.Text),
                giaNhap = decimal.Parse(txtGiaNhapDienTu.Text),
                giaBan = decimal.Parse(txtGiaBanDienTu.Text),
            };
            if (dienTuController.CheckMa(DienTu.maSP))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!");
                return;
            }


            DienTuController.AddDienTus(DienTu);
            LoadingData();
        }

        private void btnSuaDienTu_Click(object sender, EventArgs e)
        {
            var DienTu = new DienTu
            {
                maSP = txtMaSPDienTu.Text,
                tenSP = txtTenSPDienTu.Text,
                nhaCungCap = cboNhaCungCapDienTu.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongDienTu.Text),
                giaNhap = decimal.Parse(txtGiaNhapDienTu.Text),
                giaBan = decimal.Parse(txtGiaBanDienTu.Text),
            };
            DienTuController.UpdateDienTus(DienTu);
            LoadingData();
        }

        private void btnXoaDienTu_Click(object sender, EventArgs e)
        {
            var DienTu = new DienTu
            {
                maSP = txtMaSPDienTu.Text,
            };
            DienTuController.DeleteDienTus(DienTu);
            LoadingData();
        }

        private void txtSearchDienTu_TextChanged(object sender, EventArgs e)
        {
            var text = txtSearchDienTu.Text;
            List<DienTu> DienTus = DienTuController.SearchDienTus(text);
            dgvDienTu.Rows.Clear();
            foreach (DienTu DienTu in DienTus)
            {
                dgvDienTu.Rows.Add(DienTu.maSP, DienTu.tenSP, DienTu.nhaCungCap, DienTu.soLuong, DienTu.giaNhap, DienTu.giaBan);
            }
        }

        private void dgvDienTu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDienTu.Rows[e.RowIndex];
                txtMaSPDienTu.Text = row.Cells[0].Value?.ToString();
                txtTenSPDienTu.Text = row.Cells[1].Value?.ToString();
                cboNhaCungCapDienTu.SelectedItem = row.Cells[2].Value?.ToString();
                txtSoLuongDienTu.Text = row.Cells[3].Value?.ToString();
                txtGiaNhapDienTu.Text = row.Cells[4].Value?.ToString();
                txtGiaBanDienTu.Text = row.Cells[5].Value?.ToString();
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

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
                    DienTuController.ThemmoiDienTu(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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

        private void btnNhapExcelDienTu_Click(object sender, EventArgs e)
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
                //txtUploadDienTu.Text = opened.FileName;
                filename = opened.FileName;
                ReadExcel();
            }
            LoadingData();

        }

        private void btnXuatExcelDienTu_Click(object sender, EventArgs e)
        {

            var exporter = new DienTuController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvDienTu);
        }

        private void btnXuatHang_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dgvDienTu.SelectedRows.Count == 0 && dgvDienTu.SelectedCells.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xuất kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin sản phẩm từ hàng được chọn
            var selectedRow = dgvDienTu.CurrentRow;
            if (selectedRow != null)
            {
                // Tạo danh sách sản phẩm cần xuất
                List<DienTu> sanPhamCanXuat = new List<DienTu>();
                
                DienTu dienTu = new DienTu
                {
                    maSP = selectedRow.Cells[0].Value?.ToString(),
                    tenSP = selectedRow.Cells[1].Value?.ToString(),
                    nhaCungCap = selectedRow.Cells[2].Value?.ToString(),
                    soLuong = Convert.ToInt32(selectedRow.Cells[3].Value),
                    giaNhap = Convert.ToDecimal(selectedRow.Cells[4].Value),
                    giaBan = Convert.ToDecimal(selectedRow.Cells[5].Value)
                };

                sanPhamCanXuat.Add(dienTu);

                // Mở form xuất hàng và truyền dữ liệu
                View.xuatHangForm xuatHang = new View.xuatHangForm();
                xuatHang.NhanSanPhamDienTu(sanPhamCanXuat);
                xuatHang.ShowDialog();
            }
        }
    }
    
}
