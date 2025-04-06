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
    public partial class meVaBeUC : UserControl
    {
        public meVaBeUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapMeVaBe.Items.Clear();
            LoadingCboNCC();
        }

        private void LoadingData()
        {
            dgvMeVaBe.Rows.Clear();
            List<MeVaBe> meVaBes = MeVaBeController.getAllMeVaBes();
            foreach (MeVaBe meVaBe in meVaBes)
            {
                dgvMeVaBe.Rows.Add(meVaBe.maSP, meVaBe.tenSP, meVaBe.nhaCungCap, meVaBe.soLuong, meVaBe.giaNhap,
                    meVaBe.giaBan);

            }
        }

        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapMeVaBe.Items.Add(nhaCungCap.maNhaCC);
            }
        }

        private void ClearTxt()
        {
            txtMaSPMeVaBe.Text = "";
            txtTenSPMeVaBe.Text = "";
            cboNhaCungCapMeVaBe.SelectedIndex = -1;
            txtSoLuongMeVaBe.Text = "";
            txtGiaNhapMeVaBe.Text = "";
            txtGiaBanMeVaBe.Text = "";
        }

        private void btnThemMeVaBe_Click(object sender, EventArgs e)
        {

            var MeVaBeController = new MeVaBeController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPMeVaBe.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongMeVaBe.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapMeVaBe.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanMeVaBe.Text))
            {
                return;
            }
            var meVaBe = new MeVaBe
            {
                maSP = txtMaSPMeVaBe.Text,
                tenSP = txtTenSPMeVaBe.Text,
                nhaCungCap = cboNhaCungCapMeVaBe.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongMeVaBe.Text),
                giaNhap = decimal.Parse(txtGiaNhapMeVaBe.Text),
                giaBan = decimal.Parse(txtGiaBanMeVaBe.Text)
            };
            MeVaBeController.AddMeVaBes(meVaBe);
            LoadingData();
            ClearTxt();
        }

        private void btnSuaMeVaBe_Click(object sender, EventArgs e)
        {

            var MeVaBeController = new MeVaBeController();
            var checkController = new CheckController();
            if (!checkController.CheckMaNotNull(txtMaSPMeVaBe.Text))
            {
                return; // Nếu không hợp lệ, dừng xử lý
            }
            if (!int.TryParse(txtSoLuongMeVaBe.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (chỉ được nhập số)!");
                return;
            }
            if (!checkController.CheckGia(txtGiaNhapMeVaBe.Text))
            {
                return;
            }
            if (!checkController.CheckGia(txtGiaBanMeVaBe.Text))
            {
                return;
            }
            var meVaBe = new MeVaBe
            {
                maSP = txtMaSPMeVaBe.Text,
                tenSP = txtTenSPMeVaBe.Text,
                nhaCungCap = cboNhaCungCapMeVaBe.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongMeVaBe.Text),
                giaNhap = decimal.Parse(txtGiaNhapMeVaBe.Text),
                giaBan = decimal.Parse(txtGiaBanMeVaBe.Text)
            };
            if (MeVaBeController.CheckMa(meVaBe.maSP))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!");
                return;
            }

            MeVaBeController.UpdateMeVaBes(meVaBe);
            LoadingData();
            ClearTxt();
        }

        private void btnXoaMeVaBe_Click(object sender, EventArgs e)
        {
            var meVaBe = new MeVaBe
            {
                maSP = txtMaSPMeVaBe.Text,
            };
            MeVaBeController.DeleteMeVaBes(meVaBe);
            LoadingData();
            ClearTxt();
        }

        private void txtSearchMeVaBe_TextChanged(object sender, EventArgs e)
        {
            dgvMeVaBe.Rows.Clear();
            var text = txtSearchMeVaBe.Text;
            List<MeVaBe> meVaBes = MeVaBeController.SearchMeVaBes(text);
            foreach (MeVaBe meVaBe in meVaBes)
            {
                dgvMeVaBe.Rows.Add(meVaBe.maSP, meVaBe.tenSP, meVaBe.nhaCungCap, meVaBe.soLuong, meVaBe.giaNhap, meVaBe.giaBan);
            }
        }

        private void dgvMeVaBe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMeVaBe.Rows[e.RowIndex];
                txtMaSPMeVaBe.Text = row.Cells[0].Value.ToString();
                txtTenSPMeVaBe.Text = row.Cells[1].Value.ToString();
                cboNhaCungCapMeVaBe.SelectedItem = row.Cells[2].Value.ToString();
                txtSoLuongMeVaBe.Text = row.Cells[3].Value.ToString();
                txtGiaNhapMeVaBe.Text = row.Cells[4].Value.ToString();
                txtGiaBanMeVaBe.Text = row.Cells[5].Value.ToString();
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
                    MeVaBeController.ThemmoiMevaBe(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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





        private void btnNhapExcelMevaBe_Click(object sender, EventArgs e)
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
            //txtUploadMevaBe.Text = opened.FileName;
            filename = opened.FileName;
            ReadExcel();
        }
        LoadingData();

    }

        private void btnXuatExcelMevaBe_Click(object sender, EventArgs e)
        {

            var exporter = new MeVaBeController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvMeVaBe);
        }
    }
    
}
