﻿using System;
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
    public partial class doChoiUC : UserControl
    {
        public doChoiUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapDoChoi.Items.Clear();
            LoadingCboNCC();


        }
        public void LoadingData()
        {
            List<DoChoi> DoChois = DoChoiController.getAllDoChois();
            dgvDoChoi.Rows.Clear();
            foreach (DoChoi DoChoi in DoChois)
            {
                dgvDoChoi.Rows.Add(DoChoi.maSP, DoChoi.tenSP, DoChoi.nhaCungCap, DoChoi.soLuong, DoChoi.giaNhap, DoChoi.giaBan);
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapDoChoi.Items.Add(nhaCungCap.maNhaCC);
            }
        }
        private void ClearTxt()
        {
            txtMaSPDoChoi.Text = "";
            txtTenSPDoChoi.Text = "";
            cboNhaCungCapDoChoi.SelectedIndex = -1;
            txtSoLuongDoChoi.Text = "";
            txtGiaNhapDoChoi.Text = "";
            txtGiaBanDoChoi.Text = "";
        }

        private void txtThemDoChoi_Click(object sender, EventArgs e)
        {
            var DoChoi = new DoChoi
            {
                maSP = txtMaSPDoChoi.Text,
                tenSP = txtTenSPDoChoi.Text,
                nhaCungCap = cboNhaCungCapDoChoi.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongDoChoi.Text),
                giaNhap = decimal.Parse(txtGiaNhapDoChoi.Text),
                giaBan = decimal.Parse(txtGiaBanDoChoi.Text),
            };
            DoChoiController.AddDoChois(DoChoi);
            LoadingData();
            ClearTxt();
        }

        private void txtSuaDoChoi_Click(object sender, EventArgs e)
        {
            var DoChoi = new DoChoi
            {
                maSP = txtMaSPDoChoi.Text,
                tenSP = txtTenSPDoChoi.Text,
                nhaCungCap = cboNhaCungCapDoChoi.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongDoChoi.Text),
                giaNhap = decimal.Parse(txtGiaNhapDoChoi.Text),
                giaBan = decimal.Parse(txtGiaBanDoChoi.Text),
            };
            DoChoiController.UpdateDoChois(DoChoi);
            LoadingData();
            ClearTxt();
        }

        private void txtXoaDoChoi_Click(object sender, EventArgs e)
        {
            var DoChoi = new DoChoi
            {
                maSP = txtMaSPDoChoi.Text,
            };
            DoChoiController.DeleteDoChois(DoChoi);
            LoadingData();
            ClearTxt();
        }

        private void txtSearchDoChoi_TextChanged(object sender, EventArgs e)
        {
            var text = txtSearchDoChoi.Text;
            List<DoChoi> DoChois = DoChoiController.SearchDoChois(text);
            dgvDoChoi.Rows.Clear();
            foreach (DoChoi DoChoi in DoChois)
            {
                dgvDoChoi.Rows.Add(DoChoi.maSP, DoChoi.tenSP, DoChoi.nhaCungCap, DoChoi.soLuong, DoChoi.giaNhap, DoChoi.giaBan);
            }
        }

        private void dgvDoChoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDoChoi.Rows[e.RowIndex];
                txtMaSPDoChoi.Text = row.Cells[0].Value?.ToString();
                txtTenSPDoChoi.Text = row.Cells[1].Value?.ToString();
                cboNhaCungCapDoChoi.SelectedItem = row.Cells[2].Value?.ToString();
                txtSoLuongDoChoi.Text = row.Cells[3].Value?.ToString();
                txtGiaNhapDoChoi.Text = row.Cells[4].Value?.ToString();
                txtGiaBanDoChoi.Text = row.Cells[5].Value?.ToString();
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
                    DoChoiController.ThemmoiDoChoi(maSP, tenSP, nhaCungCap, soLuongInt, giaNhapDecimal, giaBanDecimal);
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

        private void btnNhapExcelDoChoi_Click(object sender, EventArgs e)
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
                //txtUploadDoChoi.Text = opened.FileName;
                filename = opened.FileName;
                ReadExcel();
            }
            LoadingData();

        }

        private void btnXuatExcelDoChoi_Click(object sender, EventArgs e)
        {

            var exporter = new DoChoiController.ExcelExporter();
            exporter.ExportDataGridViewToExcel(dgvDoChoi);
        }
    }
}
    

