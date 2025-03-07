using System;
using System.Collections.Generic;
using System.Windows.Forms;
using appSuper.Controller;
using appSuper.Model;


namespace appSuper
{
    public partial class thuCungUC : UserControl
    {
        public thuCungUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapThuCung.Items.Clear();
            LoadingCboNhaCC();
        }

        private void LoadingData()
        {
            dgvThuCung.Rows.Clear();
            List<ThuCung> ThuCungs = ThuCungController.getAllThuCungs();
            foreach (ThuCung ThuCung in ThuCungs)
            {
                dgvThuCung.Rows.Add(ThuCung.maSP, ThuCung.tenSP, ThuCung.nhaCungCap, ThuCung.soLuong, ThuCung.giaNhap, ThuCung.giaBan);
            }
        }

        private void LoadingCboNhaCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCung in nhaCungCaps)
            {
                cboNhaCungCapThuCung.Items.Add(nhaCung.maNhaCC);
            }
        }
        private void btnThemThuCung_Click(object sender, EventArgs e)
        {
            var ThuCung = new ThuCung
            {
                maSP = txtMaSPThuCung.Text,
                tenSP = txtTenSPThuCung.Text,
                nhaCungCap = cboNhaCungCapThuCung.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThuCung.Text),
                giaNhap = decimal.Parse(txtGiaNhapThuCung.Text),
                giaBan = decimal.Parse(txtGiaBanThuCung.Text)
            };
            ThuCungController.AddThuCungs(ThuCung);
            LoadingData();
        }
        private void btnSuaThuCung_Click(object sender, EventArgs e)
        {
            var ThuCung = new ThuCung
            {
                maSP = txtMaSPThuCung.Text,
                tenSP = txtTenSPThuCung.Text,
                nhaCungCap = cboNhaCungCapThuCung.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThuCung.Text),
                giaNhap = decimal.Parse(txtGiaNhapThuCung.Text),
                giaBan = decimal.Parse(txtGiaBanThuCung.Text)
            };
            ThuCungController.UpdateThuCungs(ThuCung);
            LoadingData();
        }

        private void btnXoaThuCung_Click(object sender, EventArgs e)
        {
            var ThuCung = new ThuCung
            {
                maSP = txtMaSPThuCung.Text,
            };
            ThuCungController.DeleteThuCungs(ThuCung);
            LoadingData();
        }

        private void txtSearchThuCung_TextChanged(object sender, EventArgs e)
        {
            dgvThuCung.Rows.Clear();
            var Text = txtSearchThuCung.Text;
            List<ThuCung> ThuCungs = ThuCungController.SearchThuCungs(Text);
            foreach (ThuCung ThuCung in ThuCungs)
            {
                dgvThuCung.Rows.Add(ThuCung.maSP, ThuCung.tenSP, ThuCung.nhaCungCap, ThuCung.soLuong, ThuCung.giaNhap, ThuCung.giaBan);
            }
        }

        private void dgvThuCung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThuCung.Rows[e.RowIndex];
                txtMaSPThuCung.Text = row.Cells[0].Value.ToString();
                txtTenSPThuCung.Text = row.Cells[1].Value.ToString();
                cboNhaCungCapThuCung.SelectedItem = row.Cells[2].Value.ToString();
                txtSoLuongThuCung.Text = row.Cells[3].Value.ToString();
                txtGiaNhapThuCung.Text = row.Cells[4].Value.ToString();
                txtGiaBanThuCung.Text = row.Cells[5].Value.ToString();
            }
        }
    }
}
