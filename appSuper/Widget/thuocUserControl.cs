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

namespace appSuper
{
    public partial class thuocUC : UserControl
    {
        public thuocUC()
        {
            InitializeComponent();
            cboNCCThuoc.Items.Clear();
            LoadingData();
            LoadingCboNCC();

        }

        private void LoadingData()
        {
            dgvThuoc.Rows.Clear();
            List<Thuoc> thuoc = ThuocController.getAllThuocs();
            foreach (Thuoc t in thuoc)
            {
                dgvThuoc.Rows.Add(t.maSP, t.tenSP, t.nhaCungCap, t.soLuong, t.giaNhap, t.giaBan);

            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNCCThuoc.Items.Add(nhaCungCap.maNhaCC);
            }
        }

        private void btnThemThuoc_Click(object sender, EventArgs e)
        {
            var thuoc = new Thuoc
            {
                maSP = txtMaSPThuoc.Text,
                tenSP = txtTenSPThuoc.Text,
                nhaCungCap = cboNCCThuoc.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThuoc.Text),
                giaNhap = decimal.Parse(txtGiaNhapThuoc.Text),
                giaBan = decimal.Parse(txtGiaBanThuoc.Text)
            };
            ThuocController.AddThuocs(thuoc);
            LoadingData();
        }

        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThuoc.Rows[e.RowIndex];
                txtMaSPThuoc.Text = row.Cells[0].Value?.ToString();
                txtTenSPThuoc.Text = row.Cells[1].Value?.ToString();
                cboNCCThuoc.SelectedItem = row.Cells[2].Value?.ToString();
                txtSoLuongThuoc.Text = row.Cells[3].Value?.ToString();
                txtGiaNhapThuoc.Text = row.Cells[4].Value?.ToString();
                txtGiaBanThuoc.Text = row.Cells[5].Value?.ToString();
            }
        }

        private void btnSuaThuoc_Click(object sender, EventArgs e)
        {
            var thuoc = new Thuoc
            {
                maSP = txtMaSPThuoc.Text,
                tenSP = txtTenSPThuoc.Text,
                nhaCungCap = cboNCCThuoc.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThuoc.Text),
                giaNhap = decimal.Parse(txtGiaNhapThuoc.Text),
                giaBan = decimal.Parse(txtGiaBanThuoc.Text)
            };
            ThuocController.UpdateThuocs(thuoc);
            LoadingData();
        }

        private void btnXoaThuoc_Click(object sender, EventArgs e)
        {
            var thuoc = new Thuoc
            {
                maSP = txtMaSPThuoc.Text,
                tenSP = txtTenSPThuoc.Text,
                nhaCungCap = cboNCCThuoc.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThuoc.Text),
                giaNhap = decimal.Parse(txtGiaNhapThuoc.Text),
                giaBan = decimal.Parse(txtGiaBanThuoc.Text)
            };
            ThuocController.DeleteThuocs(thuoc);
            LoadingData();
        }

        private void txtSearchThuoc_TextChanged(object sender, EventArgs e)
        {
            dgvThuoc.Rows.Clear();
            var searchThuoc = txtSearchThuoc.Text;
            List<Thuoc> thuoc = ThuocController.SearchThuocsWithMaSP(searchThuoc);
            foreach (Thuoc t in thuoc)
            {
                dgvThuoc.Rows.Add(t.maSP, t.tenSP, t.nhaCungCap, t.soLuong, t.giaNhap, t.giaBan);

            }
        }
    }
}
