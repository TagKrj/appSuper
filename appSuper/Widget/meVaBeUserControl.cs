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
            var meVaBe = new MeVaBe
            {
                maSP = txtMaSPMeVaBe.Text,
                tenSP = txtTenSPMeVaBe.Text,
                nhaCungCap = cboNhaCungCapMeVaBe.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongMeVaBe.Text),
                giaNhap = decimal.Parse(txtGiaNhapMeVaBe.Text),
                giaBan = decimal.Parse(txtGiaBanMeVaBe.Text)
            };
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
    }
}
