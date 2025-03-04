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
    }
    }

