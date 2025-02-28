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
    public partial class giaDungUC : UserControl
    {
        public giaDungUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapGiaDung.Items.Clear();
            LoadingCboNCC();
        }

        public void LoadingData()
        {
            List<GiaDung> GiaDungs = GiaDungController.getAllGiaDungs();
            dgvGiaDung.Rows.Clear();
            foreach (var giaDung in GiaDungs)
            {
                dgvGiaDung.Rows.Add(giaDung.maSP, giaDung.tenSP, giaDung.nhaCungCap, giaDung.soLuong, giaDung.giaNhap, giaDung.giaBan);
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapGiaDung.Items.Add(nhaCungCap.maNhaCC);
            }
        }

        private void btnThemGiaDung_Click(object sender, EventArgs e)
        {
            var GiaDung = new GiaDung
            {
                maSP = txtMaSPGiaDung.Text,
                tenSP = txtTenSPGiaDung.Text,
                nhaCungCap = cboNhaCungCapGiaDung.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongGiaDung.Text),
                giaNhap = decimal.Parse(txtGiaNhapGiaDung.Text),
                giaBan = decimal.Parse(txtGiaBanGiaDung.Text),
            };
            GiaDungController.AddGiaDungs(GiaDung);
            LoadingData();
        }

        private void btnSuaGiaDung_Click(object sender, EventArgs e)
        {
            var GiaDung = new GiaDung
            {
                maSP = txtMaSPGiaDung.Text,
                tenSP = txtTenSPGiaDung.Text,
                nhaCungCap = cboNhaCungCapGiaDung.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongGiaDung.Text),
                giaNhap = decimal.Parse(txtGiaNhapGiaDung.Text),
                giaBan = decimal.Parse(txtGiaBanGiaDung.Text),
            };
            GiaDungController.UpdateGiaDungs(GiaDung);
            LoadingData();
        }

        private void btnXoaGiaDung_Click(object sender, EventArgs e)
        {
            var GiaDung = new GiaDung
            {
                maSP = txtMaSPGiaDung.Text
            };
            GiaDungController.DeleteGiaDungs(GiaDung);
            LoadingData();
        }

        private void txtSearchGiaDung_TextChanged(object sender, EventArgs e)
        {
            var text = txtSearchGiaDung.Text;
            List<GiaDung> GiaDungs = GiaDungController.SearchGiaDungs(text);
            dgvGiaDung.Rows.Clear();
            foreach (var giaDung in GiaDungs)
            {
                dgvGiaDung.Rows.Add(giaDung.maSP, giaDung.tenSP, giaDung.nhaCungCap, giaDung.soLuong, giaDung.giaNhap, giaDung.giaBan);
            }
        }

        private void dgvGiaDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvGiaDung.Rows[e.RowIndex];
                txtMaSPGiaDung.Text = row.Cells[0].Value?.ToString();
                txtTenSPGiaDung.Text = row.Cells[1].Value?.ToString();
                cboNhaCungCapGiaDung.SelectedItem = row.Cells[2].Value?.ToString();
                txtSoLuongGiaDung.Text = row.Cells[3].Value?.ToString();
                txtGiaNhapGiaDung.Text = row.Cells[4].Value?.ToString();
                txtGiaBanGiaDung.Text = row.Cells[5].Value?.ToString();
            }
        }
    }
}
