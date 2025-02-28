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
            var DienTu = new DienTu
            {
                maSP = txtMaSPDienTu.Text,
                tenSP = txtTenSPDienTu.Text,
                nhaCungCap = cboNhaCungCapDienTu.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongDienTu.Text),
                giaNhap = decimal.Parse(txtGiaNhapDienTu.Text),
                giaBan = decimal.Parse(txtGiaBanDienTu.Text),
            };
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
    }
}
