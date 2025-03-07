using System;
using System.Collections.Generic;
using System.Windows.Forms;
using appSuper.Controller;
using appSuper.Model;

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
            var Sach = new Sach
            {
                maSP = txtMaSPSach.Text,
                tenSP = txtTenSPSach.Text,
                nhaCungCap = cboNhaCungCapSach.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongSach.Text),
                giaNhap = decimal.Parse(txtGiaNhapSach.Text),
                giaBan = decimal.Parse(txtGiaBanSach.Text)
            };
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
    }
}

