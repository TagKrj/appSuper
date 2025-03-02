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
            var giaoDuc = new GiaoDuc
            {
                maSP = txtMaSPGiaoDuc.Text,
                tenSP = txtTenSPGiaoDuc.Text,
                nhaCungCap = cboNhaCungCapGiaoDuc.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongGiaoDuc.Text),
                giaNhap = decimal.Parse(txtGiaNhapGiaoDuc.Text),
                giaBan = decimal.Parse(txtGiaBanGiaoDuc.Text)
            };
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
    }
}
