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
    public partial class thoiTrangUC : UserControl
    {
        public thoiTrangUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapThoiTrang.Items.Clear();
            LoadingCboNCC();
        }
        private void LoadingData()
        {
            dgvThoiTrang.Rows.Clear();
            List<ThoiTrang> thoiTrangs = ThoiTrangController.getAllThoiTrangs();
            foreach (ThoiTrang thoitrang in thoiTrangs)
            {
                dgvThoiTrang.Rows.Add(thoitrang.maSP, thoitrang.tenSP, thoitrang.nhaCungCap, thoitrang.soLuong, thoitrang.giaNhap, thoitrang.giaBan);

            }
        }

        private void btnThemThoiTrang_Click(object sender, EventArgs e)
        {
            var thoiTrang = new ThoiTrang
            {
                maSP = txtMaSPThoiTrang.Text,
                tenSP = txtTenSPThoiTrang.Text,
                nhaCungCap = cboNhaCungCapThoiTrang.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThoiTrang.Text),
                giaNhap = decimal.Parse(txtGiaNhapThoiTrang.Text),
                giaBan = decimal.Parse(txtGiaBanThoiTrang.Text)
            };
            ThoiTrangController.AddThoiTrangs(thoiTrang);
            LoadingData();
        }

        private void btnSuaThoiTrang_Click(object sender, EventArgs e)
        {
            var thoiTrang = new ThoiTrang
            {
                maSP = txtMaSPThoiTrang.Text,
                tenSP = txtTenSPThoiTrang.Text,
                nhaCungCap = cboNhaCungCapThoiTrang.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThoiTrang.Text),
                giaNhap = decimal.Parse(txtGiaNhapThoiTrang.Text),
                giaBan = decimal.Parse(txtGiaBanThoiTrang.Text)
            };
            ThoiTrangController.UpdateThoiTrangs(thoiTrang);
            LoadingData();
        }

        private void btnXoaThoiTrang_Click(object sender, EventArgs e)
        {
            var thoiTrang = new ThoiTrang
            {
                maSP = txtMaSPThoiTrang.Text,
            };
            ThoiTrangController.DeleteThoiTrangs(thoiTrang);
            LoadingData();
        }

        private void txtSearchThoiTrang_TextChanged(object sender, EventArgs e)
        {
            dgvThoiTrang.Rows.Clear();
            var text = txtSearchThoiTrang.Text;
            List<ThoiTrang> thoiTrangs = ThoiTrangController.SearchThoiTrangs(text);
            foreach (ThoiTrang thoitrang in thoiTrangs)
            {
                dgvThoiTrang.Rows.Add(thoitrang.maSP, thoitrang.tenSP, thoitrang.nhaCungCap, thoitrang.soLuong, thoitrang.giaNhap, thoitrang.giaBan);
            }
        }

        private void dgvThoiTrang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThoiTrang.Rows[e.RowIndex];
                txtMaSPThoiTrang.Text = row.Cells[0].Value.ToString();
                txtTenSPThoiTrang.Text = row.Cells[1].Value.ToString();
                cboNhaCungCapThoiTrang.SelectedItem = row.Cells[2].Value.ToString();
                txtSoLuongThoiTrang.Text = row.Cells[3].Value.ToString();
                txtGiaNhapThoiTrang.Text = row.Cells[4].Value.ToString();
                txtGiaBanThoiTrang.Text = row.Cells[5].Value.ToString();
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapThoiTrang.Items.Add(nhaCungCap.maNhaCC);
            }
        }
    }
}
