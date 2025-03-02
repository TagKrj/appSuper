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

namespace appSuper
{
    public partial class nhapKhauUC : UserControl
    {
        public nhapKhauUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapNhapKhau.Items.Clear();
            LoadingCboNCC();
        }
        private void LoadingData()
        {
            dgvNhapKhau.Rows.Clear();
            List<NhapKhau> nhapKhaus = NhapKhauController.getAllNhapKhaus();
            foreach (NhapKhau nhapKhau in nhapKhaus)
            {
                dgvNhapKhau.Rows.Add(nhapKhau.maSP, nhapKhau.tenSP, nhapKhau.nhaCungCap, nhapKhau.soLuong, nhapKhau.giaNhap,
                    nhapKhau.giaBan);

            }
        }

        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapNhapKhau.Items.Add(nhaCungCap.maNhaCC);
            }
        }

        private void ClearTxt()
        {
            txtMaSPNhapKhau.Text = "";
            txtTenSPNhapKhau.Text = "";
            cboNhaCungCapNhapKhau.SelectedIndex = -1;
            txtSoLuongNhapKhau.Text = "";
            txtGiaNhapNhapKhau.Text = "";
            txtGiaBanNhapKhau.Text = "";
        }

        private void btnThemNhapKhau_Click(object sender, EventArgs e)
        {
            var nhapKhau = new NhapKhau
            {
                maSP = txtMaSPNhapKhau.Text,
                tenSP = txtTenSPNhapKhau.Text,
                nhaCungCap = cboNhaCungCapNhapKhau.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongNhapKhau.Text),
                giaNhap = decimal.Parse(txtGiaNhapNhapKhau.Text),
                giaBan = decimal.Parse(txtGiaBanNhapKhau.Text)
            };
            NhapKhauController.AddNhapKhaus(nhapKhau);
            LoadingData();
            ClearTxt();
        }

        private void btnSuaNhapKhau_Click(object sender, EventArgs e)
        {
            var nhapKhau = new NhapKhau
            {
                maSP = txtMaSPNhapKhau.Text,
                tenSP = txtTenSPNhapKhau.Text,
                nhaCungCap = cboNhaCungCapNhapKhau.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongNhapKhau.Text),
                giaNhap = decimal.Parse(txtGiaNhapNhapKhau.Text),
                giaBan = decimal.Parse(txtGiaBanNhapKhau.Text)
            };
            NhapKhauController.UpdateNhapKhaus(nhapKhau);
            LoadingData();
            ClearTxt();
        }

        private void btnXoaNhapKhau_Click(object sender, EventArgs e)
        {
            var nhapKhau = new NhapKhau
            {
                maSP = txtMaSPNhapKhau.Text,
            };
            NhapKhauController.DeleteNhapKhaus(nhapKhau);
            LoadingData();
            ClearTxt();
        }

        private void txtSearchNhapKhau_TextChanged(object sender, EventArgs e)
        {
            dgvNhapKhau.Rows.Clear();
            var text = txtSearchNhapKhau.Text;
            List<NhapKhau> nhapKhaus = NhapKhauController.SearchNhapKhaus(text);
            foreach (NhapKhau nhapKhau in nhapKhaus)
            {
                dgvNhapKhau.Rows.Add(nhapKhau.maSP, nhapKhau.tenSP, nhapKhau.nhaCungCap, nhapKhau.soLuong, nhapKhau.giaNhap, nhapKhau.giaBan);
            }
        }

        private void dgvNhapKhau_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhapKhau.Rows[e.RowIndex];
                txtMaSPNhapKhau.Text = row.Cells[0].Value.ToString();
                txtTenSPNhapKhau.Text = row.Cells[1].Value.ToString();
                cboNhaCungCapNhapKhau.SelectedItem = row.Cells[2].Value.ToString();
                txtSoLuongNhapKhau.Text = row.Cells[3].Value.ToString();
                txtGiaNhapNhapKhau.Text = row.Cells[4].Value.ToString();
                txtGiaBanNhapKhau.Text = row.Cells[5].Value.ToString();
            }
        }
    }
}
